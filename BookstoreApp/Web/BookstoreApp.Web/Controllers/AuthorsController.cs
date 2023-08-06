namespace BookstoreApp.Web.Controllers
{
    using BookstoreApp.Services.Data;

    public class AuthorsController : BaseController
    {
        private readonly IAuthorsService authorsService;
        private readonly IGenresService genresService;

        public AuthorsController(
            IAuthorsService authorsService,
            IGenresService genresService)
        {
            this.authorsService = authorsService;
            this.genresService = genresService;
        }
    }
}
