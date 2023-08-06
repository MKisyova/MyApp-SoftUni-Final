namespace BookstoreApp.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BookstoreApp.Web.ViewModels.Administration.Books;

    public interface IBooksService
    {
        T GetById<T>(int id);

        IEnumerable<T> GetAll<T>(int page, int itemsPerPage);

        IEnumerable<T> GetAllRandom<T>(int page, int itemsPerPage);

        IEnumerable<T> GetByGenreId<T>(int genreId);

        IEnumerable<T> GetByGenresFiction<T>();

        IEnumerable<T> GetByGenresNonfiction<T>();

        IEnumerable<T> GetAllNewBooks<T>(int page, int itemsPerPage);

        IEnumerable<T> GetBookForHomePage<T>(int booksCount);

        IEnumerable<T> GetByKeyword<T>(string keyword);

        int GetCountByGenreId(int genreId);

        int GetCount();

        Task CreateAsync(CreateBookInputModel input, string imagePath);

        IEnumerable<KeyValuePair<string, string>> GetAllBooksAsKeyValuePair();
    }
}
