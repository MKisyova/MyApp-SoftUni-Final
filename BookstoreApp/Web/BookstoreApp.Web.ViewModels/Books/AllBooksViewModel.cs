namespace BookstoreApp.Web.ViewModels.Books
{
    public class AllBooksViewModel
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int AuthorId { get; set; }

        public string Author { get; set; }

    }
}
