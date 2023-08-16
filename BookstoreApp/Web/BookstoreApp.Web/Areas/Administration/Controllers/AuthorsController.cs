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
            model.GenresItems = this.genresService.GetAllGenresAsKeyValuePair();

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAuthorInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.GenresItems = this.genresService.GetAllGenresAsKeyValuePair();
                return this.View(input);
            }

            await this.authorsService.CreateAsync(input);
            this.TempData["Message"] = "Author created successfully.";

            return this.RedirectToAction(nameof(this.All));
        }

        public IActionResult Edit(int id)
        {
            var input = this.authorsService.GetById<EditAuthorInputModel>(id);
            input.GenresItems = this.genresService.GetAllGenresAsKeyValuePair();

            return this.View(input);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditAuthorInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.GenresItems = this.genresService.GetAllGenresAsKeyValuePair();
                return this.View(input);
            }

            await this.authorsService.UpdateAsync(id, input);
            this.TempData["Message"] = "Author updated successfully.";

            return this.RedirectToAction(nameof(this.All));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await this.authorsService.DeleteAsync(id);
            this.TempData["Message"] = "Author deleted successfully.";

            return this.RedirectToAction(nameof(this.All));
        }
    }
}
