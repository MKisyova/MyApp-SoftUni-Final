namespace BookstoreApp.Web.ViewModels.Administration.Authors
{
    using BookstoreApp.Data.Models;
    using BookstoreApp.Services.Mapping;

    public class SingleAuthorInTableViewModel : IMapFrom<Author>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
