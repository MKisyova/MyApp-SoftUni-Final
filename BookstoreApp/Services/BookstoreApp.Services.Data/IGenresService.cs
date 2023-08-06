namespace BookstoreApp.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BookstoreApp.Web.ViewModels.Administration.Genres;

    public interface IGenresService
    {
        IEnumerable<T> GetAll<T>();

        IEnumerable<T> GetAllWithPaging<T>(int page, int itemsPerPage);

        IEnumerable<T> GetAllFiction<T>();

        IEnumerable<T> GetAllNonfiction<T>();

        int GetCount();

        T GetById<T>(int id);

        Task CreateAsync(CreateGenreInputModel input);

        Task UpdateAsync(int id, EditGenreInputModel input);

        IEnumerable<KeyValuePair<string, string>> GetAllGenresAsKeyValuePair();
    }
}
