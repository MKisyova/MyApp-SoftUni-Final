namespace BookstoreApp.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class CategoriesController : BaseController
    {
        public IActionResult All()
        {
            return this.View();
        }

        public IActionResult PopularCategories()
        {
            return this.View();
        }
    }
}
