namespace BookstoreApp.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BookstoreApp.Data.Common.Repositories;
    using BookstoreApp.Data.Models;
    using BookstoreApp.Services.Mapping;
    using BookstoreApp.Web.ViewModels.Genres;

    public class GenresService : IGenresService
    {
        private readonly IDeletableEntityRepository<Genre> genresRepository;

        public GenresService(IDeletableEntityRepository<Genre> genresRepository)
        {
            this.genresRepository = genresRepository;
        }

        public IEnumerable<T> GetAll<T>()
        {
            var genres = this.genresRepository.AllAsNoTracking()
                .OrderBy(x => x.Name)
                .To<T>()
                .ToList();

            return genres;
        }

        public T GetById<T>(int id)
        {
            var genre = this.genresRepository.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return genre;
        }

        public async Task CreateAsync(CreateGenreInputModel input)
        {
            var genre = new Genre
            {
                Name = input.Name,
            };

            await this.genresRepository.AddAsync(genre);
            await this.genresRepository.SaveChangesAsync();
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllGenresAsKeyValuePair()
        {
            return this.genresRepository.AllAsNoTracking().Select(x => new
            {
                x.Id,
                x.Name,
            }).OrderBy(x => x.Name)
              .ToList().Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name));
        }
    }
}
