namespace BookstoreApp.Web.ViewModels.Authors
{
    using System.Collections.Generic;

    public class AllAuthorsListViewModel : PagingViewModel
    {
        public IEnumerable<SingleAuthorViewModel> Authors { get; set; }
    }
}
