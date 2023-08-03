namespace BookstoreApp.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BookstoreApp.Web.ViewModels.Books;

    public interface IBooksService
    {
        IEnumerable<T> GetAll<T>(int page, int itemsPerPage);

        IEnumerable<T> GetAllNewBooks<T>(int page, int itemsPerPage);

        IEnumerable<T> GetBookForHomePage<T>(int booksCount);

        int GetCount();

        Task CreateAsync(CreateBookInputModel input, string imagePath);

        IEnumerable<KeyValuePair<string, string>> GetAllBooksAsKeyValuePair();

        T GetById<T>(int id);

        IEnumerable<T> GetByGenreId<T>(int genreId);
    }
}
