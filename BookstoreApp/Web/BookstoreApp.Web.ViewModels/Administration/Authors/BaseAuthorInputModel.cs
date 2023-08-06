namespace BookstoreApp.Web.ViewModels.Administration.Authors
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public abstract class BaseAuthorInputModel
    {
        [Required]
        [MinLength(2)]
        public string Name { get; set; }

        [Display(Name = "Short biography")]
        public string ShortBiography { get; set; }

        [Display(Name = "Genres")]
        public IEnumerable<int> GenreIds { get; set; }

        public IEnumerable<KeyValuePair<string, string>> Genres { get; set; }
    }
}
