namespace BookstoreApp.Web.ViewModels.Genres
{
    using System.ComponentModel.DataAnnotations;

    public class BaseGenreInputModel
    {
        [Required]
        [MinLength(2)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Is genre fiction")]
        public bool IsFiction { get; set; }
    }
}
