namespace BookstoreApp.Data.Models
{
    using BookstoreApp.Data.Common.Models;

    public class Vote : BaseModel<int>
    {
        public int BookId { get; set; }

        public virtual Book Book { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public byte Value { get; set; }
    }
}
