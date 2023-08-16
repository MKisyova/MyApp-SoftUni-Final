namespace BookstoreApp.Web.Areas.Administration.Controllers
{
    using System;
    using System.Threading.Tasks;

    using BookstoreApp.Common;
    using BookstoreApp.Services.Data;
    using BookstoreApp.Web.ViewModels.Administration.Books;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;

    public class BooksController : AdministrationController
    {
        private readonly IBooksService booksService;
        private readonly IAuthorsService authorsService;
        private readonly IGenresService genresService;
        private readonly IWebHostEnvironment environment;

        public BooksController(
            IBooksService booksService,
            IAuthorsService authorsService,
            IGenresService genresService,
            IWebHostEnvironment environment)
        {
            this.booksService = booksService;
            this.authorsService = authorsService;
            this.genresService = genresService;
            this.environment = environment;
        }

        public IActionResult All(int pageNumber = 1)
        {
            var viewModel = new AllBooksInTableViewModel
            {
                ActionName = nameof(this.All),
                ItemsPerPage = GlobalConstants.ItemsPerPage,
                PageNumber = pageNumber,
                TotalItemsCount = this.booksService.GetCount(),
                Books = this.booksService
                .GetAll<SingleBookInTableViewModel>(pageNumber, GlobalConstants.ItemsPerPage),
            };

            return this.View(viewModel);
        }

        public IActionResult Create()
        {
            var model = new CreateBookInputModel();
            model.Authors = this.authorsService.GetAllAuthorsAsKeyValuePair();
            model.Genres = this.genresService.GetAllGenresAsKeyValuePair();

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBookInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.Authors = this.authorsService.GetAllAuthorsAsKeyValuePair();
                input.Genres = this.genresService.GetAllGenresAsKeyValuePair();
                return this.View(input);
            }

            try
            {
                await this.booksService.CreateAsync(input, $"{this.environment.WebRootPath}/images");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                input.Authors = this.authorsService.GetAllAuthorsAsKeyValuePair();
                input.Genres = this.genresService.GetAllGenresAsKeyValuePair();
                return this.View(input);
            }

            this.TempData["Message"] = "Book created successfully.";
            return this.RedirectToAction(nameof(this.All));
        }

        public IActionResult Edit(int id)
        {
            var input = this.booksService.GetById<EditBookInputModel>(id);
            input.Genres = this.genresService.GetAllGenresAsKeyValuePair();
            input.Authors = this.authorsService.GetAllAuthorsAsKeyValuePair();

            return this.View(input);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditBookInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.Genres = this.genresService.GetAllGenresAsKeyValuePair();
                input.Authors = this.authorsService.GetAllAuthorsAsKeyValuePair();
                return this.View(input);
            }

            await this.booksService.UpdateAsync(id, input, $"{this.environment.WebRootPath}/images");
            this.TempData["Message"] = "Book updated successfully.";

            return this.RedirectToAction(nameof(this.All));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await this.booksService.DeleteAsync(id);
            this.TempData["Message"] = "Book deleted successfully.";

            return this.RedirectToAction(nameof(this.All));
        }
    }
}
