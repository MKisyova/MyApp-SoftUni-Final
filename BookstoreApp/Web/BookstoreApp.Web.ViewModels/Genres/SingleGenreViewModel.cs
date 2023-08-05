namespace BookstoreApp.Web.ViewModels.Genres
{
    using BookstoreApp.Data.Models;
    using BookstoreApp.Services.Mapping;

    public class SingleGenreViewModel : IMapFrom<Genre>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsFiction { get; set; }
    }
}
