namespace BookstoreApp.Web.Controllers
{
    using System.Linq;

    using BookstoreApp.Common;
    using BookstoreApp.Data.Models;
    using BookstoreApp.Services.Data;
    using BookstoreApp.Web.ViewModels.Authors;
    using BookstoreApp.Web.ViewModels.Books;
    using Microsoft.AspNetCore.Mvc;

    public class AuthorsController : BaseController
    {
        private readonly IAuthorsService authorsService;
        private readonly IBooksService booksService;

        public AuthorsController(
            IAuthorsService authorsService,
            IBooksService booksService)
        {
            this.authorsService = authorsService;
            this.booksService = booksService;
        }

        public IActionResult All(int pageNumber = 1)
        {
            var viewModel = new AllAuthorsListViewModel
            {
                ActionName = nameof(this.All),
                ItemsPerPage = GlobalConstants.ItemsPerPage,
                PageNumber = pageNumber,
                TotalItemsCount = this.authorsService.GetCount(),
                Authors = this.authorsService.GetAll<SingleAuthorViewModel>(pageNumber, GlobalConstants.ItemsPerPage),
            };

            foreach (var author in viewModel.Authors)
            {
                author.ShortBiography = $"{string.Join(" ", author.ShortBiography.Split(' ').Take(GlobalConstants.WordsFromShortBiography))} ...";
            }

            return this.View(viewModel);
        }

        public IActionResult BooksByAuthor(int id, int pageNumber = 1)
        {
            var viewModel = new BooksByAuthorViewModel
            {
                ActionName = nameof(this.BooksByAuthor),
                ItemsPerPage = GlobalConstants.ItemsPerPage,
                PageNumber = pageNumber,
                TotalItemsCount = this.booksService.GetCountByAuthorId(id),
                Author = this.authorsService.GetById<SingleAuthorViewModel>(id),
                Books = this.booksService.GetByAuthorId<SmallBookViewModel>(id),
            };

            return this.View(viewModel);
        }
    }
}
