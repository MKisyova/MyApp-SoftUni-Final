﻿namespace BookstoreApp.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BookstoreApp.Web.ViewModels.Administration.Authors;

    public interface IAuthorsService
    {
        IEnumerable<KeyValuePair<string, string>> GetAllAuthorsAsKeyValuePair();

        Task CreateAsync(CreateAuthorInputModel input);

        Task UpdateAsync(int id, EditAuthorInputModel input);

        Task DeleteAsync(int id);

        int GetCount();

        IEnumerable<T> GetAll<T>(int page, int itemsPerPage);

        T GetById<T>(int id);
    }
}
