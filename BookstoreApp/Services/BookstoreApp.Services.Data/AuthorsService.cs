namespace BookstoreApp.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BookstoreApp.Data.Common.Repositories;
    using BookstoreApp.Data.Models;

    using BookstoreApp.Web.ViewModels.Authors;

    public class AuthorsService : IAuthorsService
    {
        private readonly IDeletableEntityRepository<Author> authorsRepository;
        private readonly IDeletableEntityRepository<Genre> genresRepository;
        private readonly IDeletableEntityRepository<Book> booksRepository;

        public AuthorsService(
            IDeletableEntityRepository<Author> authorsRepository,
            IDeletableEntityRepository<Genre> genresRepository,
            IDeletableEntityRepository<Book> booksRepository)
        {
            this.authorsRepository = authorsRepository;
            this.genresRepository = genresRepository;
            this.booksRepository = booksRepository;
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllAuthorsAsKeyValuePair()
        {
            return this.authorsRepository.AllAsNoTracking().Select(x => new
            {
                x.Id,
                x.Name,
            }).OrderBy(x => x.Name)
              .ToList().Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name));
        }

        public async Task CreateAsync(CreateAuthorInputModel input)
        {
            var author = new Author
            {
                Name = input.Name,
                ShortBiography = input.ShortBiography,
            };

            if (input.GenreIds != null)
            {
                foreach (var inputGenreId in input.GenreIds)
                {
                    var genre = this.genresRepository.All().FirstOrDefault(x => x.Id == inputGenreId);
                    author.Genres.Add(new AuthorGenre { Genre = genre, });
                }
            }

            await this.authorsRepository.AddAsync(author);
            await this.authorsRepository.SaveChangesAsync();
        }
    }
}
