namespace BookstoreApp.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class BooksController : BaseController
    {
        public IActionResult Bestsellers()
        {
            return this.View();
        }

        public IActionResult NewBooks()
        {
            return this.View();
        }
    }
}
