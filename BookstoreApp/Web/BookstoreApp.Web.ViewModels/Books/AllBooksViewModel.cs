namespace BookstoreApp.Web.ViewModels.Books
{
    using BookstoreApp.Data.Models;
    using BookstoreApp.Services.Mapping;

    public class AllBooksViewModel : IMapFrom<Book>
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int YearPublished { get; set; }

        public int Pages { get; set; }

        public decimal Price { get; set; }

        public int AuthorId { get; set; }

        public string AuthorFirstName { get; set; }

        public string AuthorLastName { get; set; }
    }
}
