namespace BookstoreApp.Web.ViewModels.ShoppingCart
{
    using BookstoreApp.Data.Models;
    using BookstoreApp.Services.Mapping;

    public class ShoppingCartIdViewModel : IMapFrom<ShoppingCart>
    {
        public string UserId { get; set; }
    }
}
