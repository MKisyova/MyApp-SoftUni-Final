namespace BookstoreApp.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    using BookstoreApp.Data.Common.Models;
    using Microsoft.EntityFrameworkCore.Metadata.Internal;

    public class Book : BaseDeletableModel<int>
    {
        public Book()
        {
            this.Genres = new HashSet<BookGenre>();
            this.ShoppingCarts = new List<ShoppingCartBook>();
        }

        public string Title { get; set; }

        public string Description { get; set; }

        public int Pages { get; set; }

        [Column(TypeName = "decimal(6, 2)")]
        public decimal Price { get; set; }

        public short YearPublished { get; set; }

        public int AuthorId { get; set; }

        public virtual Author Author { get; set; }

        public virtual ICollection<BookGenre> Genres { get; set; }

        public string ImageId { get; set; }

        public virtual Image Image { get; set; }

        public virtual ICollection<Vote> Votes { get; set; }

        public virtual BestsellingBook BestsellingBook { get; set; }

        public virtual ICollection<ShoppingCartBook> ShoppingCarts { get; set; }
    }
}
