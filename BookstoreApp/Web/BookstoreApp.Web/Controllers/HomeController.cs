namespace BookstoreApp.Web.Controllers
{
    using System.Diagnostics;

    using BookstoreApp.Services.Data;
    using BookstoreApp.Web.ViewModels;
    using BookstoreApp.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IGetCountsService countsService;

        public HomeController(IGetCountsService countsService)
        {
            this.countsService = countsService;
        }

        public IActionResult Index()
        {
            var counts = this.countsService.GetCounts();
            var viewModel = new IndexViewModel
            {
                BooksCount = counts.BooksCount,
                AuthorsCount = counts.AuthorsCount,
                GenresCount = counts.GenresCount,
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
