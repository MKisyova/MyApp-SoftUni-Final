namespace BookstoreApp.Web.ViewModels.Books
{
    using System;
    using System.Collections.Generic;

    public class AllBooksListViewModel : PagingViewModel
    {
        public IEnumerable<AllBooksViewModel> Books { get; set; }

    }
}
