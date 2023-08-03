namespace BookstoreApp.Web.ViewModels.Genres
{
    using System.Collections.Generic;

    using BookstoreApp.Web.ViewModels.Books;

    public class BooksByGenreViewModel : PagingViewModel
    {
        public SingleGenreViewModel Genre { get; set; }

        public IEnumerable<SmallBookViewModel> Books { get; set; }
    }
}
