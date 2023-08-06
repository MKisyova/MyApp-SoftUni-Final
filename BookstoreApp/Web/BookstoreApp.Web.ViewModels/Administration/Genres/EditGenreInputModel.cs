namespace BookstoreApp.Web.ViewModels.Administration.Genres
{
    using BookstoreApp.Data.Models;
    using BookstoreApp.Services.Mapping;

    public class EditGenreInputModel : BaseGenreInputModel, IMapFrom<Genre>
    {
        public int Id { get; set; }
    }
}
