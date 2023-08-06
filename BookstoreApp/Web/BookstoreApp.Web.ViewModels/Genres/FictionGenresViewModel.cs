namespace BookstoreApp.Web.ViewModels.Genres
{
    using System.Collections.Generic;

    using BookstoreApp.Web.ViewModels.Books;

    public class FictionGenresViewModel
    {
        public IEnumerable<SingleGenreViewModel> Genres { get; set; }

        public IEnumerable<SmallBookViewModel> Books { get; set; }
    }
}
