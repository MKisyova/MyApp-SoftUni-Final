namespace BookstoreApp.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using BookstoreApp.Data.Common.Repositories;
    using BookstoreApp.Data.Models;
    using BookstoreApp.Services.Mapping;
    using BookstoreApp.Web.ViewModels.Books;

    public class BooksService : IBooksService
    {
        private readonly string[] allowedExtensions = new[] { "jpg", "png" };
        private readonly IDeletableEntityRepository<Book> booksRepository;
        private readonly IDeletableEntityRepository<Genre> genresRepository;

        public BooksService(
            IDeletableEntityRepository<Book> booksRepository,
            IDeletableEntityRepository<Genre> genresRepository)
        {
            this.booksRepository = booksRepository;
            this.genresRepository = genresRepository;
        }

        public async Task CreateAsync(CreateBookInputModel input, string imagePath)
        {
            var book = new Book
            {
                Title = input.Title,
                Description = input.Description,
                Pages = input.Pages,
                Price = input.Price,
                YearPublished = input.YearPublished,
                AuthorId = input.AuthorId,

                // Image
            };

            if (input.GenreIds != null)
            {
                foreach (var inputGenreId in input.GenreIds)
                {
                    var genre = this.genresRepository.All().FirstOrDefault(x => x.Id == inputGenreId);
                    book.Genres.Add(new BookGenre { Genre = genre, });
                }
            }

            Directory.CreateDirectory($"{imagePath}/books/");
            var extension = Path.GetExtension(input.Image.FileName).TrimStart('.');
            if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
            {
                throw new Exception($"Invalid image extension {extension}");
            }

            var image = new Image
            {
                Book = book,
                Extension = extension,
            };
            book.Image = image;

            var physicalPath = $"{imagePath}/books/{image.Id}.{extension}";
            using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
            await input.Image.CopyToAsync(fileStream);

            await this.booksRepository.AddAsync(book);
            await this.booksRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 6)
        {
            var books = this.booksRepository.AllAsNoTracking()
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                .To<T>()
                .ToList();

            return books;
        }

        public int GetCount()
        {
            return this.booksRepository.All().Count();
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllBooksAsKeyValuePair()
        {
            return this.booksRepository.AllAsNoTracking().Select(x => new
            {
                x.Id,
                x.Title,
            }).OrderBy(x => x.Title)
              .ToList().Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Title));
        }

        public T GetById<T>(int id)
        {
            var book = this.booksRepository.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return book;
        }
    }
}
