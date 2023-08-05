namespace BookstoreApp.Data.Models
{
    using BookstoreApp.Data.Common.Models;

    public class BestsellingBook : BaseDeletableModel<int>
    {
        public uint SalesCount { get; set; }

        public int BookId { get; set; }

        public virtual Book Book { get; set; }
    }
}
