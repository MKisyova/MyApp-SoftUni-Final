namespace BookstoreApp.Web.ViewModels.Books
{
    using System.Collections.Generic;

    public class AllBooksListViewModel : PagingViewModel
    {
        public IEnumerable<SmallBookViewModel> Books { get; set; }
    }
}
