namespace BookstoreApp.Data.Models
{
    using BookstoreApp.Data.Common.Models;

    public class Bestseller : BaseDeletableModel<int>
    {
        public virtual Book Book { get; set; }

        public uint SalesCount { get; set; }
    }
}
