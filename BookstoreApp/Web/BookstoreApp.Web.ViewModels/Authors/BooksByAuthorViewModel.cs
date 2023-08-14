namespace BookstoreApp.Web.ViewModels.Authors
{
    using System.Collections.Generic;

    using BookstoreApp.Web.ViewModels.Books;

    public class BooksByAuthorViewModel : PagingViewModel
    {
        public SingleAuthorViewModel Author { get; set; }

        public IEnumerable<SmallBookViewModel> Books { get; set; }
    }
}
