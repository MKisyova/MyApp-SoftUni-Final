namespace BookstoreApp.Web.ViewModels.Administration.Books
{
    using BookstoreApp.Data.Models;
    using BookstoreApp.Services.Mapping;

    public class EditBookInputModel : BaseBookInputModel, IMapFrom<Book>
    {
        public int Id { get; set; }
    }
}
