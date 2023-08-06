namespace BookstoreApp.Web.ViewModels.Administration.Genres
{
    using System.Collections.Generic;

    using BookstoreApp.Web.ViewModels.Genres;

    public class AllGenresInTableViewModel : PagingViewModel
    {
        public IEnumerable<SingleGenreInTableViewModel> Genres { get; set; }
    }
}
