namespace BookstoreApp.Web.Controllers
{
    using BookstoreApp.Common;
    using BookstoreApp.Services.Data;
    using BookstoreApp.Web.ViewModels.Books;
    using Microsoft.AspNetCore.Mvc;

    public class BooksController : BaseController
    {
        private readonly IBooksService booksService;
        private readonly IAuthorsService authorsService;
        private readonly IGenresService genresService;

        public BooksController(
            IBooksService booksService,
            IAuthorsService authorsService,
            IGenresService genresService)
        {
            this.booksService = booksService;
            this.authorsService = authorsService;
            this.genresService = genresService;
        }

        public IActionResult All(int pageNumber = 1)
        {
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

        public IActionResult NewBooks(int pageNumber = 1)
        {
            var viewModel = new AllBooksListViewModel
            {
                ActionName = nameof(this.NewBooks),
                ItemsPerPage = GlobalConstants.ItemsPerPage,
                PageNumber = pageNumber,
                TotalItemsCount = this.booksService.GetCount(),
                Books = this.booksService
                .GetAllNewBooks<SmallBookViewModel>(pageNumber, GlobalConstants.ItemsPerPage),
            };

            return this.View(viewModel);
        }

        public IActionResult Bestsellers(int pageNumber = 1)
        {
            var viewModel = new AllBooksListViewModel
            {
                ActionName = nameof(this.NewBooks),
                ItemsPerPage = GlobalConstants.ItemsPerPage,
                PageNumber = pageNumber,
                TotalItemsCount = this.booksService.GetCount(),
                Books = this.booksService
                .GetBySalesCount<SmallBookViewModel>(pageNumber, GlobalConstants.ItemsPerPage, GlobalConstants.BestsellingBooksCount),
            };

            return this.View(viewModel);
        }

        public IActionResult TopRated(int pageNumber = 1)
        {
            var viewModel = new AllBooksListViewModel
            {
                ActionName = nameof(this.NewBooks),
                ItemsPerPage = GlobalConstants.ItemsPerPage,
                PageNumber = pageNumber,
                TotalItemsCount = this.booksService.GetCount(),
                Books = this.booksService
                .GetTopRated<SmallBookViewModel>(pageNumber, GlobalConstants.ItemsPerPage),
            };

            return this.View(viewModel);
        }

        public IActionResult ById(int id)
        {
            var book = this.booksService.GetById<SingleBookViewModel>(id);
            return this.View(book);
        }
    }
}
