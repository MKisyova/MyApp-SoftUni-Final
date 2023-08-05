namespace BookstoreApp.Data.Models
{
    using System.Collections.Generic;

    using BookstoreApp.Data.Common.Models;

    public class Genre : BaseDeletableModel<int>
    {
        public Genre()
        {
            this.Authors = new HashSet<AuthorGenre>();
            this.Books = new List<BookGenre>();
        }

        public string Name { get; set; }

        public bool IsFiction { get; set; }

        public virtual ICollection<AuthorGenre> Authors { get; set; }

        public virtual ICollection<BookGenre> Books { get; set; }
    }
}
