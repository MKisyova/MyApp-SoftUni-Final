namespace BookstoreApp.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class EBooksController : BaseController
    {
        public IActionResult All()
        {
            return this.View();
        }
    }
}
