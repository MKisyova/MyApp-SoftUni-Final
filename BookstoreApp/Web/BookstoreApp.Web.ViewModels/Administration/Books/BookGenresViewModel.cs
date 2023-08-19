namespace BookstoreApp.Web.ViewModels.Administration.Books
{
    using BookstoreApp.Data.Models;
    using BookstoreApp.Services.Mapping;

    public class BookGenresViewModel : IMapFrom<BookGenre>
    {
        public int GenreId { get; set; }

        public string GenreName { get; set; }
    }
}
