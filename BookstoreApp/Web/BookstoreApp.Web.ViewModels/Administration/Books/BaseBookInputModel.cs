namespace BookstoreApp.Web.ViewModels.Administration.Books
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public abstract class BaseBookInputModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        [MinLength(20)]
        public string Description { get; set; }

        [Range(1, 10000)]
        public int Pages { get; set; }

        [Range(0, 5000)]
        public decimal Price { get; set; }

        [Range(1, 2023)]
        [Display(Name = "Year published")]
        public short YearPublished { get; set; }

        [Display(Name = "Author")]
        public int AuthorId { get; set; }

        public IEnumerable<KeyValuePair<string, string>> Authors { get; set; }

        [Display(Name = "Genres")]
        public IEnumerable<int> GenreIds { get; set; }

        public IEnumerable<KeyValuePair<string, string>> Genres { get; set; }

        [Display(Name = "Upload cover image")]
        public IFormFile Image { get; set; }
    }
}
