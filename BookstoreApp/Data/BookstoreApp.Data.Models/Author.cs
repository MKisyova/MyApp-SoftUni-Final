namespace BookstoreApp.Data.Models
{
    using System.Collections.Generic;

    using BookstoreApp.Data.Common.Models;

    public class Author : BaseDeletableModel<int>
    {
        public Author()
        {
            this.Books = new HashSet<Book>();
            this.Genres = new HashSet<AuthorGenre>();
        }

        public string Name { get; set; }

        public string ShortBiography { get; set; }

        public virtual ICollection<Book> Books { get; set; }

        public virtual ICollection<AuthorGenre> Genres { get; set; }
    }
}
