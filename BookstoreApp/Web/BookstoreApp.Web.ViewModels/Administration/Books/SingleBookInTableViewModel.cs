namespace BookstoreApp.Web.ViewModels.Administration.Books
{
    using System.Collections.Generic;

    using BookstoreApp.Data.Models;
    using BookstoreApp.Services.Mapping;
    using BookstoreApp.Web.ViewModels.Books;

    public class SingleBookInTableViewModel : IMapFrom<Book>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string AuthorName { get; set; }

        public decimal Price { get; set; }

        public int Pages { get; set; }

        public short YearPublished { get; set; }
    }
}
