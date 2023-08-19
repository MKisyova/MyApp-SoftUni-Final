namespace BookstoreApp.Web.ViewModels.ShoppingCart
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using BookstoreApp.Data.Models;
    using BookstoreApp.Services.Mapping;

    public class ShoppingCartInputModel : IMapFrom<ShoppingCart>
    {
        public ShoppingCartInputModel()
        {
            this.BookIds = new List<int>();
        }

        public int Id { get; set; }

        [Display(Name = "User email address")]
        public string UserEmail { get; set; }

        public ICollection<BookViewModel> Books { get; set; }

        public ICollection<int> BookIds { get; set; }

        [Required]
        [Display(Name = "Address for delivery")]
        public string AddressForDelivery { get; set; }
    }
}
