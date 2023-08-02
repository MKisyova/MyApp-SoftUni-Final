namespace BookstoreApp.Web.ViewModels.Books
{
    using AutoMapper;
    using BookstoreApp.Data.Models;
    using BookstoreApp.Services.Mapping;

    public class SmallBookViewModel : IMapFrom<Book>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string ImageId { get; set; }

        public string Title { get; set; }

        public decimal Price { get; set; }

        public string AuthorName { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Book, SmallBookViewModel>()
                .ForMember(x => x.ImageId, opt =>
                    opt.MapFrom(x =>
                        "/images/books/" + x.ImageId + "." + x.Image.Extension));
        }
    }
}
