namespace BookstoreApp.Web.Controllers
{
    using BookstoreApp.Services.Data;
    using BookstoreApp.Web.ViewModels.Books;
    using Microsoft.AspNetCore.Mvc;

    public class BooksController : BaseController
    {
        private readonly IBooksService booksService;

        public BooksController(IBooksService booksService)
        {
            this.booksService = booksService;
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
            const int ItemsPerPage = 6;

            var viewModel = new AllBooksListViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                BooksCount = this.booksService.GetCount(),
                Books = this.booksService.GetAll<AllBooksViewModel>(id, ItemsPerPage),
            };

            return this.View(viewModel);
        }
    }
}
