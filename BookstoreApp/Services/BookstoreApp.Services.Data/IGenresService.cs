namespace BookstoreApp.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BookstoreApp.Web.ViewModels.Genres;

    public interface IGenresService
    {
        Task CreateAsync(CreateGenreInputModel input);

        IEnumerable<KeyValuePair<string, string>> GetAllGenresAsKeyValuePair();
    }
}
