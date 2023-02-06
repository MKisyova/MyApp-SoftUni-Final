namespace BookstoreApp.Services.Data
{
    using System.Collections.Generic;

    using BookstoreApp.Web.ViewModels.Books;

    public interface IBooksService
    {
        IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 6);

        int GetCount();
    }
}
