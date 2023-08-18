namespace BookstoreApp.Data.Models
{
    public class ShoppingCartBook
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public virtual Book Book { get; set; }

        public int ShoppingCartId { get; set; }

        public virtual ShoppingCart ShoppingCart { get; set; }
    }
}
