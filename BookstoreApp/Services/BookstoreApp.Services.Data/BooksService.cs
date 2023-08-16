namespace BookstoreApp.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using BookstoreApp.Common;
    using BookstoreApp.Data.Common.Repositories;
    using BookstoreApp.Data.Models;
    using BookstoreApp.Services.Mapping;
    using BookstoreApp.Web.ViewModels.Administration.Books;

    public class BooksService : IBooksService
    {
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
            Stream fileStream = await this.AddImage(input, imagePath, book);

            await this.booksRepository.AddAsync(book);
            await this.booksRepository.SaveChangesAsync();
        }

        private async Task<Stream> AddImage(BaseBookInputModel input, string imagePath, Book book)
        {
            var extension = Path.GetExtension(input.Image.FileName).TrimStart('.');
            if (!GlobalConstants.AllowedExtensions.Any(x => extension.EndsWith(x)))
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
            Stream fileStream = new FileStream(physicalPath, FileMode.Create);
            await input.Image.CopyToAsync(fileStream);
            return fileStream;
        }

        public async Task UpdateAsync(int id, EditBookInputModel input, string imagePath)
        {
            var book = this.booksRepository.All().FirstOrDefault(x => x.Id == id);
            book.Title = input.Title;
            book.AuthorId = input.AuthorId;
            book.YearPublished = input.YearPublished;
            book.Pages = input.Pages;
            book.Price = input.Price;
            if (input.Image != null)
            {
                Stream fileStream = await this.AddImage(input, imagePath, book);
            }

            if (input.GenreIds != null)
            {
                book.Genres.Clear();

                foreach (var inputGenreId in input.GenreIds)
                {
                    var genre = this.genresRepository.All().FirstOrDefault(x => x.Id == inputGenreId);
                    book.Genres.Add(new BookGenre { Genre = genre, });
                }
            }

            // book.ImageId = input.Image;
            // book.Genres = input.GenreIds;
            await this.booksRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var book = this.booksRepository.All().FirstOrDefault(x => x.Id == id);
            this.booksRepository.Delete(book);
            await this.booksRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>(int page, int itemsPerPage)
        {
            var books = this.booksRepository.AllAsNoTracking()
                .OrderBy(x => x.Title)
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                .To<T>()
                .ToList();

            return books;
        }

        public IEnumerable<T> GetAllRandom<T>(int page, int itemsPerPage)
        {
            var books = this.booksRepository.AllAsNoTracking()
                .OrderBy(x => Guid.NewGuid())
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                .To<T>()
                .ToList();

            return books;
        }

        public IEnumerable<T> GetAllNewBooks<T>(int page, int itemsPerPage)
        {
            var books = this.booksRepository.AllAsNoTracking()
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                .To<T>()
                .ToList();

            return books;
        }

        public IEnumerable<T> GetBookForHomePage<T>(int booksCount)
        {
            var books = this.booksRepository.AllAsNoTracking()
                .OrderBy(x => Guid.NewGuid())
                .Take(booksCount)
                .To<T>()
                .ToList();

            return books;
        }

        public int GetCount()
        {
            return this.booksRepository.AllAsNoTracking().Count();
        }

        public int GetCountByGenreId(int genreId)
        {
            return this.booksRepository.AllAsNoTracking()
                .Where(x => x.Genres.Any(g => g.GenreId == genreId)).Count();
        }

        public int GetCountByAuthorId(int authorId)
        {
            return this.booksRepository.AllAsNoTracking()
                .Where(x => x.AuthorId == authorId).Count();
        }

        public int GetCountByGenresFiction()
        {
            return this.booksRepository.AllAsNoTracking()
                .Where(x => x.Genres.Any(g => g.Genre.IsFiction))
                .Count();
        }

        public T GetById<T>(int id)
        {
            var book = this.booksRepository.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return book;
        }

        public IEnumerable<T> GetByGenreId<T>(int genreId)
        {
            var books = this.booksRepository.AllAsNoTracking()
                .Where(x => x.Genres.Any(g => g.GenreId == genreId))
                .To<T>().ToList();

            return books;
        }

        public IEnumerable<T> GetByGenresFiction<T>()
        {
            var books = this.booksRepository.AllAsNoTracking()
                .Where(x => x.Genres.Any(g => g.Genre.IsFiction))
                .To<T>().ToList();

            return books;
        }

        public IEnumerable<T> GetByGenresNonfiction<T>()
        {
            var books = this.booksRepository.AllAsNoTracking()
                .Where(x => x.Genres.Any(g => g.Genre.IsFiction == false))
                .To<T>().ToList();

            return books;
        }

        public IEnumerable<T> GetByAuthorId<T>(int authorId)
        {
            var books = this.booksRepository.AllAsNoTracking()
                .Where(x => x.AuthorId == authorId)
                .To<T>().ToList();

            return books;
        }

        public IEnumerable<T> GetByKeyword<T>(string keyword)
        {
            var books = this.booksRepository.AllAsNoTracking()
                .Where(x => x.Title.Contains(keyword) || x.Author.Name.Contains(keyword))
                .To<T>()
                .ToList();

            return books;
        }

        public IEnumerable<T> GetBySalesCount<T>(int page, int itemsPerPage)
        {
            var books = this.booksRepository.AllAsNoTracking()
                .OrderByDescending(x => x.BestsellingBook.SalesCount)
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                .To<T>().ToList();

            return books;
        }

        public IEnumerable<T> GetTopRated<T>(int page, int itemsPerPage)
        {
            var books = this.booksRepository.AllAsNoTracking()
                .Where(x => x.Votes.Count() > 0)
                .OrderByDescending(x => x.Votes.Average(v => v.Value))
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                .To<T>().ToList();

            return books;
        }

        public int GetCountByTopRated()
        {
            return this.booksRepository.AllAsNoTracking()
                .Where(x => x.Votes.Count() > 0)
                .Count();
        }
    }
}
