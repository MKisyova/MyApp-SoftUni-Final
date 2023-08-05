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
        private readonly IGetCountsService countsService;
        private readonly IBooksService booksService;

        public HomeController(
            IGetCountsService countsService,
            IBooksService booksService)
        {
            this.countsService = countsService;
            this.booksService = booksService;
        }

        public IActionResult Index()
        {
            var counts = this.countsService.GetCounts();
            var viewModel = new IndexViewModel
            {
                BooksCount = counts.BooksCount,
                AuthorsCount = counts.AuthorsCount,
                GenresCount = counts.GenresCount,
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
