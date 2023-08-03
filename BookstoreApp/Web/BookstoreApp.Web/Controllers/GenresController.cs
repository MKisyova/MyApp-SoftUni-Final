namespace BookstoreApp.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
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

        //public IActionResult ById(int id)
        //{
        //    var genre = this.genresService.GetById<BooksByGenreViewModel>(id);
        //    return this.View(genre);
        //}

        public IActionResult BooksByGenreId(int id, int pageNumber = 1)
        {
            var viewModel = new BooksByGenreViewModel
            {
                ItemsPerPage = GlobalConstants.ItemsPerPage,
                PageNumber = pageNumber,
                TotalItemsCount = this.booksService.GetByGenreId<SmallBookViewModel>(id).Count(),
                Genre = this.genresService.GetById<SingleGenreViewModel>(id),
                Books = this.booksService.GetByGenreId<SmallBookViewModel>(id),
            };

            return this.View(viewModel);
        }

        public IActionResult Create()
        {
            var model = new CreateGenreInputModel();

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateGenreInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.genresService.CreateAsync(input);

            // redirect to all + tempData
            return this.Redirect("/");
        }
    }
}
