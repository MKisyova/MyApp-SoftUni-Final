namespace BookstoreApp.Web.ViewModels.Genres
{
    using System.Collections.Generic;

    public class AllGenresViewModel
    {
        public IEnumerable<SingleGenreViewModel> Genres { get; set; }
    }
}
