namespace BookstoreApp.Web.ViewModels.Books
{
    using System.Collections.Generic;

    using AutoMapper;
    using BookstoreApp.Data.Models;
    using BookstoreApp.Services.Mapping;

    public class SingleBookViewModel : IMapFrom<Book>, IHaveCustomMappings
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public int Pages { get; set; }

        public decimal Price { get; set; }

        public short YearPublished { get; set; }

        public string AuthorName { get; set; }

        public string AuthorShortBiography { get; set; }

        public IEnumerable<BookGenresViewModel> Genres { get; set; }

        public string ImageId { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Book, SingleBookViewModel>()
                .ForMember(x => x.ImageId, opt =>
                    opt.MapFrom(x =>
                        "/images/books/" + x.ImageId + "." + x.Image.Extension));
        }
    }
}
