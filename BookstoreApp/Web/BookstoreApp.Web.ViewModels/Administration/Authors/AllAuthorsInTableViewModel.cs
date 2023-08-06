namespace BookstoreApp.Web.ViewModels.Administration.Authors
{
    using System.Collections.Generic;

    public class AllAuthorsInTableViewModel : PagingViewModel
    {
        public IEnumerable<SingleAuthorInTableViewModel> Authors { get; set; }
    }
}
