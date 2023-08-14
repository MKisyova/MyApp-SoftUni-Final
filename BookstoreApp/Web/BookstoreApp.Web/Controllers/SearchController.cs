namespace BookstoreApp.Web.Controllers
{
    using BookstoreApp.Services.Data;
    using BookstoreApp.Web.ViewModels.Books;
    using BookstoreApp.Web.ViewModels.Search;
    using Microsoft.AspNetCore.Mvc;

    public class SearchController : BaseController
    {
        private readonly IBooksService booksService;

        public SearchController(IBooksService booksService)
        {
            this.booksService = booksService;
        }

        public IActionResult Index()
        {
            //var viewModel = new SearchInputModel();

            return this.View();
        }

        public IActionResult Result(SearchInputModel input)
        {
            var viewModel = new SearchResultViewModel
            {
                Books = this.booksService.GetByKeyword<SmallBookViewModel>(input.Keyword),
            };

            return this.View(viewModel);
        }
    }
}
