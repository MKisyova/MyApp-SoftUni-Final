namespace BookstoreApp.Web.ViewModels.Books
{
    using Microsoft.AspNetCore.Http;

    public class CreateBookInputModel : BaseBookInputModel
    {
        public IFormFile Image { get; set; }
    }
}
