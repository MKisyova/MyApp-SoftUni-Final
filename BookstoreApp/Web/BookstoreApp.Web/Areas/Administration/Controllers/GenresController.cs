namespace BookstoreApp.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using BookstoreApp.Common;
    using BookstoreApp.Services.Data;
    using BookstoreApp.Web.ViewModels.Administration.Genres;
    using Microsoft.AspNetCore.Mvc;

    public class GenresController : AdministrationController
    {
        private readonly IGenresService genresService;

        public GenresController(IGenresService genresService)
        {
            this.genresService = genresService;
        }

        public IActionResult All(int pageNumber = 1)
        {
            var viewModel = new AllGenresInTableViewModel
            {
                ActionName = nameof(this.All),
                ItemsPerPage = GlobalConstants.ItemsPerPage,
                PageNumber = pageNumber,
                TotalItemsCount = this.genresService.GetCount(),
                Genres = this.genresService
                .GetAllWithPaging<SingleGenreInTableViewModel>(pageNumber, GlobalConstants.ItemsPerPage),
            };

            return this.View(viewModel);
        }

        public IActionResult Create()
        {
            var model = new CreateGenreInputModel();

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateGenreInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.genresService.CreateAsync(input);

            // redirect to all + tempData
            return this.RedirectToAction(nameof(this.All));
        }

        public IActionResult Edit(int id)
        {
            var model = this.genresService.GetById<EditGenreInputModel>(id);

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditGenreInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.genresService.UpdateAsync(id, input);

            // redirect to all + tempData
            return this.RedirectToAction(nameof(this.All));
        }
    }
}
