namespace BookstoreApp.Web.ViewModels
{
    using System;

    public class PagingViewModel
    {
        public int PageNumber { get; set; }

        public bool HasPreviousPage => this.PageNumber > 1;

        public bool HasNextPage => this.PageNumber < this.PagesCount;

        public int PreviousPageNumber => this.PageNumber - 1;

        public int NextPageNumber => this.PageNumber + 1;

        public int PagesCount => (int)Math.Ceiling((double)this.TotalItemsCount / this.ItemsPerPage);

        public int TotalItemsCount { get; set; }

        public int ItemsPerPage { get; set; }

        public string ActionName { get; set; }
    }
}
