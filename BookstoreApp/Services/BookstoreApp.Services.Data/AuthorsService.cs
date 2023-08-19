namespace BookstoreApp.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BookstoreApp.Data.Common.Repositories;
    using BookstoreApp.Data.Models;
    using BookstoreApp.Services.Mapping;
    using BookstoreApp.Web.ViewModels.Administration.Authors;

    public class AuthorsService : IAuthorsService
    {
        private readonly IDeletableEntityRepository<Author> authorsRepository;
        private readonly IDeletableEntityRepository<Genre> genresRepository;
        private readonly IRepository<AuthorGenre> authorGenresRepository;

        public AuthorsService(
            IDeletableEntityRepository<Author> authorsRepository,
            IDeletableEntityRepository<Genre> genresRepository,
            IRepository<AuthorGenre> authorGenresRepository)
        {
            this.authorsRepository = authorsRepository;
            this.genresRepository = genresRepository;
            this.authorGenresRepository = authorGenresRepository;
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

        public async Task UpdateAsync(int id, EditAuthorInputModel input)
        {
            var author = this.authorsRepository.All().FirstOrDefault(x => x.Id == id);
            author.Name = input.Name;
            author.ShortBiography = input.ShortBiography;

            var currentGenres = this.authorGenresRepository.All()
                .Where(x => x.AuthorId == id);
            foreach (var genre in currentGenres)
            {
                this.authorGenresRepository.Delete(genre);
            }

            if (input.GenreIds != null)
            {
                foreach (var inputGenreId in input.GenreIds)
                {
                    var genre = this.genresRepository.All().FirstOrDefault(x => x.Id == inputGenreId);
                    author.Genres.Add(new AuthorGenre { Genre = genre, });
                }
            }

            await this.authorsRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var author = this.authorsRepository.All().FirstOrDefault(x => x.Id == id);
            this.authorsRepository.Delete(author);
            await this.authorsRepository.SaveChangesAsync();
        }

        public int GetCount()
        {
            return this.authorsRepository.AllAsNoTracking().Count();
        }

        public IEnumerable<T> GetAll<T>(int page, int itemsPerPage)
        {
            var authors = this.authorsRepository.AllAsNoTracking()
                .OrderBy(x => x.Name)
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                .To<T>()
                .ToList();

            return authors;
        }

        public T GetById<T>(int id)
        {
            var author = this.authorsRepository.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return author;
        }
    }
}
