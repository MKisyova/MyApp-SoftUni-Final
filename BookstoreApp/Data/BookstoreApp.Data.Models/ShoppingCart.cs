namespace BookstoreApp.Data.Models
{
    using System.Collections.Generic;

    using BookstoreApp.Data.Common.Models;

    public class ShoppingCart : BaseModel<int>
    {
        public ShoppingCart()
        {
            this.Books = new List<ShoppingCartBook>();
        }

        public virtual ICollection<ShoppingCartBook> Books { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public string AddressForDelivery { get; set; }
    }
}
