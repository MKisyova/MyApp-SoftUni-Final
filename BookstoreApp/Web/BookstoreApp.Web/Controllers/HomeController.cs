namespace BookstoreApp.Web.Controllers
{
    using System.Diagnostics;

    using BookstoreApp.Common;
    using BookstoreApp.Services.Data;
    using BookstoreApp.Web.ViewModels;
    using BookstoreApp.Web.ViewModels.Books;
    using BookstoreApp.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IBooksService booksService;
        private readonly IAuthorsService authorsService;
        private readonly IGenresService genresService;

        public HomeController(
            IBooksService booksService,
            IAuthorsService authorsService,
            IGenresService genresService)
        {
            this.booksService = booksService;
            this.authorsService = authorsService;
            this.genresService = genresService;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel
            {
                BooksCount = this.booksService.GetCount(),
                AuthorsCount = this.authorsService.GetCount(),
                GenresCount = this.genresService.GetCount(),
                Books = this.booksService.GetBookForHomePage<SmallBookViewModel>(GlobalConstants.BooksForHomePageCount),
            };

            return this.View(viewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
