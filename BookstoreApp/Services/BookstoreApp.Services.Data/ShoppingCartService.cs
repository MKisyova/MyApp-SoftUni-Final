namespace BookstoreApp.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using BookstoreApp.Data.Common.Repositories;
    using BookstoreApp.Data.Models;
    using BookstoreApp.Services.Mapping;
    using BookstoreApp.Web.ViewModels.ShoppingCart;

    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IRepository<ShoppingCart> shoppingCartRepository;
        private readonly IRepository<ShoppingCartBook> bookShoppingCartRepository;
        private readonly IDeletableEntityRepository<Book> booksRepository;

        public ShoppingCartService(
            IRepository<ShoppingCart> shoppingCartRepository,
            IRepository<ShoppingCartBook> bookShoppingCartRepository,
            IDeletableEntityRepository<Book> booksRepository)
        {
            this.shoppingCartRepository = shoppingCartRepository;
            this.bookShoppingCartRepository = bookShoppingCartRepository;
            this.booksRepository = booksRepository;
        }

        public async Task CreateAsync(string userId)
        {
            var shoppingCart = this.shoppingCartRepository.All()
            .FirstOrDefault(x => x.UserId == userId);

            if (shoppingCart == null)
            {
                shoppingCart = new ShoppingCart
                {
                    UserId = userId,
                };

                await this.shoppingCartRepository.AddAsync(shoppingCart);
                await this.shoppingCartRepository.SaveChangesAsync();
            }
        }

        public T GetCartByUserId<T>(string userId)
        {
            var cart = this.shoppingCartRepository.AllAsNoTracking()
                .Where(x => x.UserId == userId)
                .To<T>().FirstOrDefault();

            return cart;
        }

        public async Task GetCart(ShoppingCartInputModel input, string userId)
        {
            var shoppingCart = this.shoppingCartRepository.All()
            .FirstOrDefault(x => x.UserId == userId);

            // if (shoppingCart == null)
            // {
            //    shoppingCart = new ShoppingCart
            //    {
            //        UserId = userId,
            //    };
            //    await this.shoppingCartRepository.AddAsync(shoppingCart);
            // }
            shoppingCart.AddressForDelivery = input.AddressForDelivery;
            foreach (var bookId in input.BookIds)
            {
                var book = this.booksRepository.AllAsNoTracking()
                .Where(x => x.Id == bookId)
                .FirstOrDefault();

                shoppingCart.Books.Add(new ShoppingCartBook { Book = book });
            }

            await this.shoppingCartRepository.SaveChangesAsync();
        }

        public async Task AddToCartAsync(string userId, BookIdViewModel model)
        {
            var shoppingCart = this.shoppingCartRepository.All()
                .FirstOrDefault(x => x.UserId == userId);

            var book = this.booksRepository.AllAsNoTracking()
                .Where(x => x.Id == model.Id)
                .FirstOrDefault();

            shoppingCart.Books.Add(new ShoppingCartBook { Book = book });

            await this.shoppingCartRepository.SaveChangesAsync();
        }

        public async Task RemoveFromCartAsync(string userId, BookIdViewModel book)
        {
            var cart = this.shoppingCartRepository.All()
                .FirstOrDefault(x => x.UserId == userId);

            var books = this.bookShoppingCartRepository.All()
                .Where(x => x.ShoppingCartId == cart.Id);

            var bookToRemove = books.FirstOrDefault(x => x.BookId == book.Id);

            if (bookToRemove != null)
            {
                this.bookShoppingCartRepository.Delete(bookToRemove);
            }

            await this.shoppingCartRepository.SaveChangesAsync();
        }
    }
}
