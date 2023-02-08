namespace BookstoreApp.Web.ViewModels.Books
{
    using System.Collections.Generic;

    public class AllBooksListViewModel
    {
        public IEnumerable<AllBooksViewModel> Books { get; set; }

        public int PageNumber { get; set; }

        public int BooksCount { get; set; }

        public int ItemsPerPage { get; set; }
    }
}
