namespace BookstoreApp.Web.ViewModels.Books
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using BookstoreApp.Data.Models;
    using BookstoreApp.Services.Mapping;

    public class SingleBookViewModel : IMapFrom<Book>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int Pages { get; set; }

        public decimal Price { get; set; }

        public short YearPublished { get; set; }

        public string AuthorName { get; set; }

        public string AuthorShortBiography { get; set; }

        public IEnumerable<BookGenresViewModel> Genres { get; set; }

        public string ImageId { get; set; }

        public double AverageVote { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Book, SingleBookViewModel>()
                .ForMember(x => x.AverageVote, opt =>
                    opt.MapFrom(x => x.Votes.Count() == 0 ? 0 : x.Votes.Average(v => v.Value)))
                .ForMember(x => x.ImageId, opt =>
                    opt.MapFrom(x =>
                        "/images/books/" + x.ImageId + "." + x.Image.Extension));
        }
    }
}
