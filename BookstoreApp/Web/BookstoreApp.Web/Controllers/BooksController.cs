namespace BookstoreApp.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using BookstoreApp.Services.Data;
    using BookstoreApp.Web.ViewModels.Books;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;

    public class BooksController : BaseController
    {
        private readonly IBooksService booksService;
        private readonly IAuthorsService authorsService;
        private readonly IWebHostEnvironment environment;

        public BooksController(
            IBooksService booksService,
            IAuthorsService authorsService,
            IWebHostEnvironment environment)
        {
            this.booksService = booksService;
            this.authorsService = authorsService;
            this.environment = environment;
        }

        public IActionResult Bestsellers()
        {
            return this.View();
        }

        public IActionResult NewBooks()
        {
            return this.View();
        }

        // Books/All/1,2,3 page
        public IActionResult All(int id = 1)
        {
            const int ItemsPerPage = 12;

            var viewModel = new AllBooksListViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                TotalItemsCount = this.booksService.GetCount(),
                Books = this.booksService.GetAll<AllBooksViewModel>(id, ItemsPerPage),
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

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBookInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.Authors = this.authorsService.GetAllAuthorsAsKeyValuePair();
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
                return this.View(input);
            }

            // redirect to all + tempData
            return this.RedirectToAction(nameof(this.All));
        }
    }
}
