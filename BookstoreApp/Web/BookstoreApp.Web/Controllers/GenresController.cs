namespace BookstoreApp.Web.Controllers
{
    using BookstoreApp.Common;
    using BookstoreApp.Services.Data;
    using BookstoreApp.Web.ViewModels.Books;
    using BookstoreApp.Web.ViewModels.Genres;
    using Microsoft.AspNetCore.Mvc;

    public class GenresController : BaseController
    {
        private readonly IGenresService genresService;
        private readonly IBooksService booksService;

        public GenresController(
            IGenresService genresService,
            IBooksService booksService)
        {
            this.genresService = genresService;
            this.booksService = booksService;
        }

        public IActionResult All()
        {
            var viewModel = new AllGenresViewModel
            {
                Genres = this.genresService.GetAll<SingleGenreViewModel>(),
            };

            return this.View(viewModel);
        }

        public IActionResult Fiction(int pageNumber = 1)
        {
            var viewModel = new FictionGenresViewModel
            {
                ActionName = nameof(this.Fiction),
                ItemsPerPage = GlobalConstants.ItemsPerPage,
                PageNumber = pageNumber,
                TotalItemsCount = this.booksService.GetCountByGenresFiction(),
                Genres = this.genresService.GetAllFiction<SingleGenreViewModel>(),
                Books = this.booksService.GetByGenresFiction<SmallBookViewModel>(pageNumber, GlobalConstants.ItemsPerPage),
            };

            return this.View(viewModel);
        }

        public IActionResult Nonfiction(int pageNumber = 1)
        {
            var viewModel = new NonfictionGenresViewModel
            {
                ActionName = nameof(this.Nonfiction),
                ItemsPerPage = GlobalConstants.ItemsPerPage,
                PageNumber = pageNumber,
                TotalItemsCount = this.booksService.GetCountByGenresFiction(),
                Genres = this.genresService.GetAllNonfiction<SingleGenreViewModel>(),
                Books = this.booksService.GetByGenresNonfiction<SmallBookViewModel>(pageNumber, GlobalConstants.ItemsPerPage),
            };

            return this.View(viewModel);
        }

        public IActionResult BooksByGenre(int id, int pageNumber = 1)
        {
            var viewModel = new BooksByGenreViewModel
            {
                ActionName = nameof(this.BooksByGenre),
                ItemsPerPage = GlobalConstants.ItemsPerPage,
                PageNumber = pageNumber,
                TotalItemsCount = this.booksService.GetCountByGenreId(id),
                Genre = this.genresService.GetById<SingleGenreViewModel>(id),
                Books = this.booksService.GetByGenreId<SmallBookViewModel>(id, pageNumber, GlobalConstants.ItemsPerPage),
            };

            return this.View(viewModel);
        }
    }
}
