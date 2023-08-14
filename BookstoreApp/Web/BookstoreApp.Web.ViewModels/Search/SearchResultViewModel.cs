namespace BookstoreApp.Web.ViewModels.Search
{
    using System.Collections.Generic;

    using BookstoreApp.Web.ViewModels.Books;

    public class SearchResultViewModel
    {
        public IEnumerable<SmallBookViewModel> Books { get; set; }
    }
}
