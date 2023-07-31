namespace BookstoreApp.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BookstoreApp.Web.ViewModels.Authors;

    public interface IAuthorsService
    {
        IEnumerable<KeyValuePair<string, string>> GetAllAuthorsAsKeyValuePair();

        Task CreateAsync(CreateAuthorInputModel input);
    }
}
