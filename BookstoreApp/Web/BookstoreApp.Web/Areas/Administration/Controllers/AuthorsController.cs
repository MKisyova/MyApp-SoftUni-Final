namespace BookstoreApp.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using BookstoreApp.Common;
    using BookstoreApp.Services.Data;
    using BookstoreApp.Web.ViewModels.Administration.Authors;
    using Microsoft.AspNetCore.Mvc;

    public class AuthorsController : AdministrationController
    {
        private readonly IAuthorsService authorsService;
        private readonly IGenresService genresService;

        public AuthorsController(
            IAuthorsService authorsService,
            IGenresService genresService)
        {
            this.authorsService = authorsService;
            this.genresService = genresService;
        }

        public IActionResult All(int pageNumber = 1)
        {
            var viewModel = new AllAuthorsInTableViewModel
            {
                ActionName = nameof(this.All),
                ItemsPerPage = GlobalConstants.ItemsPerPage,
                PageNumber = pageNumber,
                TotalItemsCount = this.authorsService.GetCount(),
                Authors = this.authorsService
                .GetAll<SingleAuthorInTableViewModel>(pageNumber, GlobalConstants.ItemsPerPage),
            };

            return this.View(viewModel);
        }

        public IActionResult Create()
        {
            var model = new CreateAuthorInputModel();
            model.Genres = this.genresService.GetAllGenresAsKeyValuePair();

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAuthorInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.Genres = this.genresService.GetAllGenresAsKeyValuePair();
                return this.View(input);
            }

            await this.authorsService.CreateAsync(input);

            // redirect to all + tempData
            return this.RedirectToAction(nameof(this.All));
        }
    }
}
