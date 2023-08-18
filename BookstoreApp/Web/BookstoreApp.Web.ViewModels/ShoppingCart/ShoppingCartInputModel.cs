namespace BookstoreApp.Web.ViewModels.ShoppingCart
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using BookstoreApp.Data.Models;
    using BookstoreApp.Services.Mapping;
    using BookstoreApp.Web.ViewModels.Books;

    public class ShoppingCartInputModel : IMapFrom<ShoppingCart>
    {
        public string UserId { get; set; }

        [Display(Name = "User email address")]
        public string UserEmail { get; set; }

        public ICollection<BookViewModel> Books { get; set; }

        public IEnumerable<int> BookIds { get; set; }

        [Required]
        [Display(Name = "Address for delivery")]
        public string AddressForDelivery { get; set; }
    }
}
