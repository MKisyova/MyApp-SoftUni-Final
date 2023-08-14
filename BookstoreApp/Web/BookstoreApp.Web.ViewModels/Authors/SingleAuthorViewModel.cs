namespace BookstoreApp.Web.ViewModels.Authors
{
    using BookstoreApp.Data.Models;
    using BookstoreApp.Services.Mapping;

    public class SingleAuthorViewModel : IMapFrom<Author>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ShortBiography { get; set; }
    }
}
