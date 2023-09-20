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

        IEnumerable<T> GetByAuthorId<T>(int authorId, int page, int itemsPerPage);

        IEnumerable<T> GetByGenreId<T>(int genreId, int page, int itemsPerPage);

        IEnumerable<T> GetByGenresFiction<T>(int page, int itemsPerPage);

        IEnumerable<T> GetByGenresNonfiction<T>(int page, int itemsPerPage);

        IEnumerable<T> GetAllNewBooks<T>(int page, int itemsPerPage);

        IEnumerable<T> GetBookForHomePage<T>(int booksCount);

        IEnumerable<T> GetByKeyword<T>(string keyword);

        IEnumerable<T> GetBySalesCount<T>(int page, int itemsPerPage);

        IEnumerable<T> GetTopRated<T>(int page, int itemsPerPage);

        int GetCountByGenreId(int genreId);

        int GetCountByAuthorId(int authorId);

        int GetCountByGenresFiction();

        int GetCountByGenresNonFiction();

        int GetCountByTopRated();

        int GetCountBySalesCount();

        int GetCount();

        Task CreateAsync(CreateBookInputModel input, string imagePath);

        Task UpdateAsync(int id, EditBookInputModel input, string imagePath);

        Task DeleteAsync(int id);
    }
}
