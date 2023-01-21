namespace BookstoreApp.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class AudioBooksController : BaseController
    {
        public IActionResult All()
        {
            return this.View();
        }
    }
}
