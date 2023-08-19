namespace BookstoreApp.Web.ViewModels.Administration.Books
{
    using System.Collections.Generic;

    using AutoMapper;
    using BookstoreApp.Data.Models;
    using BookstoreApp.Services.Mapping;

    public class EditBookInputModel : BaseBookInputModel, IMapFrom<Book>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public IEnumerable<BookGenresViewModel> Genres { get; set; }

        public string ImageId { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Book, EditBookInputModel>()
                .ForMember(x => x.ImageId, opt =>
                    opt.MapFrom(x =>
                        "/images/books/" + x.ImageId + "." + x.Image.Extension));
        }
    }
}
