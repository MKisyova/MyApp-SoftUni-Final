namespace BookstoreApp.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using BookstoreApp.Common;
    using BookstoreApp.Services.Data;
    using BookstoreApp.Web.ViewModels.Books;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;

    public class BooksController : BaseController
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

        public IActionResult Bestsellers()
        {
            return this.View();
        }

        public IActionResult NewBooks(int pageNumber = 1)
        {
            var viewModel = new AllBooksListViewModel
            {
                ActionName = nameof(this.NewBooks),
                ItemsPerPage = GlobalConstants.ItemsPerPage,
                PageNumber = pageNumber,
                TotalItemsCount = this.booksService.GetCount(),
                Books = this.booksService.GetAllNewBooks<SmallBookViewModel>(pageNumber, GlobalConstants.ItemsPerPage),
            };

            return this.View(viewModel);
        }

        // Books/All/1,2,3 page
        public IActionResult All(int pageNumber = 1)
        {
            // const int ItemsPerPage = 12;
            var viewModel = new AllBooksListViewModel
            {
                ActionName = nameof(this.All),
                ItemsPerPage = GlobalConstants.ItemsPerPage,
                PageNumber = pageNumber,
                TotalItemsCount = this.booksService.GetCount(),
                Books = this.booksService.GetAll<SmallBookViewModel>(pageNumber, GlobalConstants.ItemsPerPage),
            };

            return this.View(viewModel);
        }

        public IActionResult ById(int id)
        {
            var book = this.booksService.GetById<SingleBookViewModel>(id);
            return this.View(book);
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

            // redirect to all + tempData
            return this.RedirectToAction(nameof(this.All));
        }
    }
}
