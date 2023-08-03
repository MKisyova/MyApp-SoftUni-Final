namespace BookstoreApp.Web.ViewModels.Home
{
    using System.Collections.Generic;

    using BookstoreApp.Web.ViewModels.Books;

    public class IndexViewModel
    {
        public int BooksCount { get; set; }

        public int AuthorsCount { get; set; }

        public int GenresCount { get; set; }

        public IEnumerable<SmallBookViewModel> Books { get; set; }
    }
}
