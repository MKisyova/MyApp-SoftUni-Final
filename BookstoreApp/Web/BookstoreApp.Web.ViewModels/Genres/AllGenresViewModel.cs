namespace BookstoreApp.Web.ViewModels.Genres
{
    using System.Collections.Generic;

    public class AllGenresViewModel : PagingViewModel
    {
        public IEnumerable<SingleGenreViewModel> Genres { get; set; }
    }
}
