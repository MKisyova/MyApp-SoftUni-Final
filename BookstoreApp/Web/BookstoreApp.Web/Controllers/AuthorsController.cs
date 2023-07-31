namespace BookstoreApp.Web.Controllers
{
    using System.Threading.Tasks;

    using BookstoreApp.Services.Data;
    using BookstoreApp.Web.ViewModels.Authors;
    using Microsoft.AspNetCore.Mvc;

    public class AuthorsController : BaseController
    {
        private readonly IAuthorsService authorsService;
        private readonly IBooksService booksService;
        private readonly IGenresService genresService;

        public AuthorsController(
            IAuthorsService authorsService,
            IBooksService booksService,
            IGenresService genresService)
        {
            this.authorsService = authorsService;
            this.booksService = booksService;
            this.genresService = genresService;
        }

        // [Authorize]
        // [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            var model = new CreateAuthorInputModel();
            //model.Books = this.booksService.GetAllBooksAsKeyValuePair();
            model.Genres = this.genresService.GetAllGenresAsKeyValuePair();

            return this.View(model);
        }

        // [Authorize]
        // [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateAuthorInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                //input.Books = this.booksService.GetAllBooksAsKeyValuePair();
                input.Genres = this.genresService.GetAllGenresAsKeyValuePair();
                return this.View(input);
            }

            await this.authorsService.CreateAsync(input);

            // redirect to all + tempData
            return this.Redirect("/");
        }
    }
}
