namespace BookstoreApp.Web.ViewModels.Administration.Books
{
    using System.Collections.Generic;

    public class AllBooksInTableViewModel : PagingViewModel
    {
        public IEnumerable<SingleBookInTableViewModel> Books { get; set; }
    }
}
