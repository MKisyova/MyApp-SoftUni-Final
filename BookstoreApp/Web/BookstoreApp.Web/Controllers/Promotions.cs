namespace BookstoreApp.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class Promotions : BaseController
    {
        public IActionResult SpecialOffers()
        {
            return this.View();
        }
    }
}
