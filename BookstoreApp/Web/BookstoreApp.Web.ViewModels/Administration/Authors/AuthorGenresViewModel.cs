namespace BookstoreApp.Web.ViewModels.Administration.Authors
{
    using BookstoreApp.Data.Models;
    using BookstoreApp.Services.Mapping;

    public class AuthorGenresViewModel : IMapFrom<AuthorGenre>
    {
        public int GenreId { get; set; }

        public string GenreName { get; set; }
    }
}
