namespace BookstoreApp.Web.ViewModels.ShoppingCart
{
    using BookstoreApp.Data.Models;
    using BookstoreApp.Services.Mapping;

    public class BookIdViewModel : IMapFrom<Book>
    {
        public int Id { get; set; }
    }
}
