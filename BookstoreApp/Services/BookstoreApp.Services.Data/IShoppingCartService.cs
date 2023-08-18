namespace BookstoreApp.Services.Data
{
    using System.Threading.Tasks;

    using BookstoreApp.Web.ViewModels.ShoppingCart;

    public interface IShoppingCartService
    {
        T GetCartByUserId<T>(string userId);

        Task CreateAsync(string userId);

        Task GetCart(ShoppingCartInputModel input, string userId);

        Task AddToCartAsync(string userId, BookIdViewModel model);

        Task RemoveFromCartAsync(string userId, BookIdViewModel model);
    }
}
