namespace BookstoreApp.Web.ViewModels.ShoppingCart
{
    using AutoMapper;
    using BookstoreApp.Data.Models;
    using BookstoreApp.Services.Mapping;

    public class BookViewModel : IMapFrom<ShoppingCartBook>, IHaveCustomMappings
    {
        public int BookId { get; set; }

        public string BookImageId { get; set; }

        public string BookTitle { get; set; }

        public decimal BookPrice { get; set; }

        public string BookAuthorName { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ShoppingCartBook, BookViewModel>()
                .ForMember(x => x.BookImageId, opt =>
                    opt.MapFrom(x =>
                        "/images/books/" + x.Book.ImageId + "." + x.Book.Image.Extension));
        }
    }
}
