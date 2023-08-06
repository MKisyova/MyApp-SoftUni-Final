namespace BookstoreApp.Web.ViewModels.Administration.Genres
{
    using BookstoreApp.Data.Models;
    using BookstoreApp.Services.Mapping;

    public class SingleGenreInTableViewModel : IMapFrom<Genre>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsFiction { get; set; }
    }
}
