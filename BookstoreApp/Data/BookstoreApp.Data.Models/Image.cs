namespace BookstoreApp.Data.Models
{
    using System;

    using BookstoreApp.Data.Common.Models;

    public class Image : BaseDeletableModel<string>
    {
        public Image()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public virtual Book Book { get; set; }

        public string Extension { get; set; }
    }
}
