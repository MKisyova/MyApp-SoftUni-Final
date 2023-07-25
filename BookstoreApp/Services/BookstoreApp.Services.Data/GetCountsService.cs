namespace BookstoreApp.Services.Data
{
    using System.Linq;

    using BookstoreApp.Data.Common.Repositories;
    using BookstoreApp.Data.Models;
    using BookstoreApp.Web.ViewModels.Home;

    public class GetCountsService : IGetCountsService
    {
        private readonly IDeletableEntityRepository<Book> booksRepository;
        private readonly IDeletableEntityRepository<Author> authorsRepository;
        private readonly IRepository<Genre> genresRepository;

        public GetCountsService(
            IDeletableEntityRepository<Book> booksRepository,
            IDeletableEntityRepository<Author> authorsRepository,
            IRepository<Genre> genresRepository)
        {
            this.booksRepository = booksRepository;
            this.authorsRepository = authorsRepository;
            this.genresRepository = genresRepository;
        }

        public IndexViewModel GetCounts()
        {
            var data = new IndexViewModel
            {
                BooksCount = this.booksRepository.All().Count(),
                AuthorsCount = this.authorsRepository.All().Count(),
                GenresCount = this.genresRepository.All().Count(),
            };

            return data;
        }
    }
}
