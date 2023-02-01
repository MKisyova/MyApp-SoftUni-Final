namespace BookstoreApp.Services.Data
{
    using System.Collections.Generic;

    using BookstoreApp.Web.ViewModels.Books;

    public interface IBooksService
    {
        IEnumerable<AllBooksListViewModel> GetAll(int page, int itemsPerPage = 12);
    }
}
