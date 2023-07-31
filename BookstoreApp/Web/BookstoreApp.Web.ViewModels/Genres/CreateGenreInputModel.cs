namespace BookstoreApp.Web.ViewModels.Genres
{
    using System.ComponentModel.DataAnnotations;

    public class CreateGenreInputModel
    {
        [Required]
        [MinLength(2)]
        public string Name { get; set; }
    }
}
