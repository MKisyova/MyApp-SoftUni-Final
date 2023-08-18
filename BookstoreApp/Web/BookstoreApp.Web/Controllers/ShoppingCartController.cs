namespace BookstoreApp.Web.Controllers
{
    using System.Threading.Tasks;

    using BookstoreApp.Data.Models;
    using BookstoreApp.Services.Data;
    using BookstoreApp.Web.ViewModels.ShoppingCart;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class ShoppingCartController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IShoppingCartService shoppingCartService;
        private readonly IBooksService booksService;
        private readonly IBestsellingService bestsellingService;

        public ShoppingCartController(
            UserManager<ApplicationUser> userManager,
            IShoppingCartService shoppingCartService,
            IBooksService booksService,
            IBestsellingService bestsellingService)
        {
            this.userManager = userManager;
            this.shoppingCartService = shoppingCartService;
            this.booksService = booksService;
            this.bestsellingService = bestsellingService;
        }

        public async Task<IActionResult> Details()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var cart = this.shoppingCartService.GetCartByUserId<ShoppingCartIdViewModel>(user.Id);

            if (cart == null)
            {
                await this.shoppingCartService.CreateAsync(user.Id);
                cart = this.shoppingCartService.GetCartByUserId<ShoppingCartIdViewModel>(user.Id);
            }

            var input = this.shoppingCartService.GetCartByUserId<ShoppingCartInputModel>(cart.UserId);
            return this.View(input);
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(ShoppingCartInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);
            await this.shoppingCartService.GetCart(input, user.Id);
            await this.bestsellingService.IncreaseBestsellingBooksValue(input.BookIds);

            this.TempData["Message"] = "Order successfully placed.";

            return this.RedirectToAction("All", "Books");
        }

        public async Task<IActionResult> AddToCart(int id)
        {
            var bookId = this.booksService.GetById<BookIdViewModel>(id);

            var user = await this.userManager.GetUserAsync(this.User);
            var cart = this.shoppingCartService.GetCartByUserId<ShoppingCartIdViewModel>(user.Id);

            if (cart == null)
            {
                await this.shoppingCartService.CreateAsync(user.Id);
                cart = this.shoppingCartService.GetCartByUserId<ShoppingCartIdViewModel>(user.Id);
            }

            await this.shoppingCartService.AddToCartAsync(user.Id, bookId);

            return this.RedirectToAction(nameof(this.Details));
        }

        public async Task<IActionResult> RemoveFromCart(int id)
        {
            var bookId = this.booksService.GetById<BookIdViewModel>(id);
            var user = await this.userManager.GetUserAsync(this.User);
            await this.shoppingCartService.RemoveFromCartAsync(user.Id, bookId);

            return this.RedirectToAction(nameof(this.Details));
        }
    }
}
