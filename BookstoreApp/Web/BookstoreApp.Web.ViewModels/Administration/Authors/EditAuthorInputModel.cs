namespace BookstoreApp.Web.ViewModels.Administration.Authors
{
    using System.Collections.Generic;

    using BookstoreApp.Data.Models;
    using BookstoreApp.Services.Mapping;

    public class EditAuthorInputModel : BaseAuthorInputModel, IMapFrom<Author>
    {
        public int Id { get; set; }

        public IEnumerable<AuthorGenresViewModel> Genres { get; set; }
    }
}
