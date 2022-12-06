using Microsoft.AspNetCore.Mvc;

namespace BookstoreApp.Web.Controllers
{
    public class CategoriesController : BaseController
    {
        public IActionResult All()
        {
            return this.View();
        }
    }
}
