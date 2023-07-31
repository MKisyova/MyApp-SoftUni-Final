namespace BookstoreApp.Web.Controllers
{
    using System.Threading.Tasks;

    using BookstoreApp.Services.Data;
    using BookstoreApp.Web.ViewModels.Genres;
    using Microsoft.AspNetCore.Mvc;

    public class GenresController : BaseController
    {
        private readonly IGenresService genresService;

        public GenresController(IGenresService genresService)
        {
            this.genresService = genresService;
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
