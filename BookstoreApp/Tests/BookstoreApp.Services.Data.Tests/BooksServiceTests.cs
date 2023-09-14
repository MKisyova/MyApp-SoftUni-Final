﻿namespace BookstoreApp.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using BookstoreApp.Data.Common.Repositories;
    using BookstoreApp.Data.Models;
    using BookstoreApp.Services.Mapping;
    using BookstoreApp.Web.ViewModels;
    using BookstoreApp.Web.ViewModels.Administration.Books;
    using BookstoreApp.Web.ViewModels.Authors;
    using BookstoreApp.Web.ViewModels.Books;
    using BookstoreApp.Web.ViewModels.ShoppingCart;
    using Moq;
    using Xunit;

    public class BooksServiceTests
    {
        public BooksServiceTests()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
        }

        [Fact]
        public void GetCountShouldReturnTheTotalBooksCount()
        {
            BooksService service = this.MockService(this.TestData());

            Assert.Equal(4, service.GetCount());
        }

        [Theory]
        [InlineData(1, 2)]
        [InlineData(2, 1)]
        [InlineData(3, 1)]
        public void GetCountByAuthorIdShouldReturnTheBooksCountWithTheGivenAuthorId(int authorId, int expectedResult)
        {
            var mockRepoBooks = new Mock<IDeletableEntityRepository<Book>>();
            mockRepoBooks.Setup(x => x.AllAsNoTracking())
                .Returns(this.TestData()
                .Where(b => b.AuthorId == authorId).AsQueryable);
            var mockRepoGenres = new Mock<IDeletableEntityRepository<Genre>>();
            var mockRepoBooksGenres = new Mock<IRepository<BookGenre>>();

            var service = new BooksService(
               mockRepoBooks.Object,
               mockRepoGenres.Object,
               mockRepoBooksGenres.Object);

            Assert.Equal(expectedResult, service.GetCountByAuthorId(authorId));
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 1)]
        [InlineData(3, 1)]
        public void GetCountByGenreIdShouldReturnTheBooksCountWithTheGivenGenreId(int genreId, int expectedResult)
        {
            var mockRepoBooks = new Mock<IDeletableEntityRepository<Book>>();
            mockRepoBooks.Setup(x => x.AllAsNoTracking())
                .Returns(this.TestData()
                .Where(b => b.Genres.Any(g => g.GenreId == genreId)).AsQueryable);
            var mockRepoGenres = new Mock<IDeletableEntityRepository<Genre>>();
            var mockRepoBooksGenres = new Mock<IRepository<BookGenre>>();

            var service = new BooksService(
               mockRepoBooks.Object,
               mockRepoGenres.Object,
               mockRepoBooksGenres.Object);

            Assert.Equal(expectedResult, service.GetCountByGenreId(genreId));
        }

        [Fact]
        public void GetCountByGenreFictionShouldReturnTheBooksCountWithGenreFiction()
        {
            var mockRepoBooks = new Mock<IDeletableEntityRepository<Book>>();
            mockRepoBooks.Setup(x => x.AllAsNoTracking())
                .Returns(this.TestData()
                .Where(b => b.Genres.Any(g => g.Genre.IsFiction)).AsQueryable);
            var mockRepoGenres = new Mock<IDeletableEntityRepository<Genre>>();
            var mockRepoBooksGenres = new Mock<IRepository<BookGenre>>();

            var service = new BooksService(
               mockRepoBooks.Object,
               mockRepoGenres.Object,
               mockRepoBooksGenres.Object);

            Assert.Equal(2, service.GetCountByGenresFiction());
        }

        [Fact]
        public void GetCountByGenreNonFictionShouldReturnTheBooksCountWithGenreNonFiction()
        {
            var mockRepoBooks = new Mock<IDeletableEntityRepository<Book>>();
            mockRepoBooks.Setup(x => x.AllAsNoTracking())
                .Returns(this.TestData()
                .Where(b => b.Genres.Any(g => g.Genre.IsFiction == false)).AsQueryable);
            var mockRepoGenres = new Mock<IDeletableEntityRepository<Genre>>();
            var mockRepoBooksGenres = new Mock<IRepository<BookGenre>>();

            var service = new BooksService(
               mockRepoBooks.Object,
               mockRepoGenres.Object,
               mockRepoBooksGenres.Object);

            Assert.Equal(2, service.GetCountByGenresNonFiction());
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [InlineData(3, 3)]
        public void GetByIdShouldReturnTheCorrectId(int id, int expectedResult)
        {
            var mockRepoBooks = new Mock<IDeletableEntityRepository<Book>>();
            mockRepoBooks.Setup(x => x.AllAsNoTracking())
                .Returns(this.TestData()
                .Where(g => g.Id == id).AsQueryable);
            var mockRepoGenres = new Mock<IDeletableEntityRepository<Genre>>();
            var mockRepoBooksGenres = new Mock<IRepository<BookGenre>>();

            var service = new BooksService(
               mockRepoBooks.Object,
               mockRepoGenres.Object,
               mockRepoBooksGenres.Object);

            Assert.Equal(expectedResult, service.GetById<BookIdViewModel>(id).Id);
        }

        [Theory]
        [InlineData(1, 2, 2)]
        [InlineData(1, 3, 3)]
        [InlineData(1, 4, 4)]
        public void GetAllShouldReturnAllBooks(int page, int itemsPerPage, int expectedResult)
        {
            var mockRepoBooks = new Mock<IDeletableEntityRepository<Book>>();
            mockRepoBooks.Setup(x => x.AllAsNoTracking())
                .Returns(this.TestData()
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage).AsQueryable);
            var mockRepoGenres = new Mock<IDeletableEntityRepository<Genre>>();
            var mockRepoBooksGenres = new Mock<IRepository<BookGenre>>();

            var service = new BooksService(
               mockRepoBooks.Object,
               mockRepoGenres.Object,
               mockRepoBooksGenres.Object);

            Assert.Equal(expectedResult, service.GetAll<SingleBookInTableViewModel>(page, itemsPerPage).Count());
        }

        [Theory]
        [InlineData(1, 2, 2, 4)]
        [InlineData(1, 3, 3, 4)]
        [InlineData(1, 4, 4, 4)]
        public void GetAllNewBooksShouldReturnAllBooksSortedByNewlyAdded(int page, int itemsPerPage, int expectedResultBooksPerPage, int lastAddedBookIdAsExpectedResult)
        {
            var mockRepoBooks = new Mock<IDeletableEntityRepository<Book>>();
            mockRepoBooks.Setup(x => x.AllAsNoTracking())
                .Returns(this.TestData()
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage).AsQueryable);
            var mockRepoGenres = new Mock<IDeletableEntityRepository<Genre>>();
            var mockRepoBooksGenres = new Mock<IRepository<BookGenre>>();

            var service = new BooksService(
               mockRepoBooks.Object,
               mockRepoGenres.Object,
               mockRepoBooksGenres.Object);

            Assert.Equal(lastAddedBookIdAsExpectedResult, service.GetAllNewBooks<SingleBookInTableViewModel>(page, itemsPerPage).FirstOrDefault().Id);
            Assert.Equal(expectedResultBooksPerPage, service.GetAllNewBooks<SingleBookInTableViewModel>(page, itemsPerPage).Count());
        }

        [Theory]
        [InlineData(1, 2, 2)]
        [InlineData(1, 3, 2)]
        [InlineData(1, 4, 2)]
        public void GetByGenresFictionShouldReturnAllBooksWithGenresFiction(int page, int itemsPerPage, int expectedResult)
        {
            var mockRepoBooks = new Mock<IDeletableEntityRepository<Book>>();
            mockRepoBooks.Setup(x => x.AllAsNoTracking())
                .Returns(this.TestData()
                .Where(x => x.Genres.Any(g => g.Genre.IsFiction))
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage).AsQueryable);
            var mockRepoGenres = new Mock<IDeletableEntityRepository<Genre>>();
            var mockRepoBooksGenres = new Mock<IRepository<BookGenre>>();

            var service = new BooksService(
               mockRepoBooks.Object,
               mockRepoGenres.Object,
               mockRepoBooksGenres.Object);

            Assert.Equal(expectedResult, service.GetByGenresFiction<SingleBookInTableViewModel>(page, itemsPerPage).Count());
        }

        [Theory]
        [InlineData(1, 2, 2)]
        [InlineData(1, 3, 2)]
        [InlineData(1, 4, 2)]
        public void GetByGenresNonFictionShouldReturnAllBooksWithGenresNonFiction(int page, int itemsPerPage, int expectedResult)
        {
            var mockRepoBooks = new Mock<IDeletableEntityRepository<Book>>();
            mockRepoBooks.Setup(x => x.AllAsNoTracking())
                .Returns(this.TestData()
                .Where(x => x.Genres.Any(g => g.Genre.IsFiction == false))
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage).AsQueryable);
            var mockRepoGenres = new Mock<IDeletableEntityRepository<Genre>>();
            var mockRepoBooksGenres = new Mock<IRepository<BookGenre>>();

            var service = new BooksService(
               mockRepoBooks.Object,
               mockRepoGenres.Object,
               mockRepoBooksGenres.Object);

            Assert.Equal(expectedResult, service.GetByGenresNonfiction<SingleBookInTableViewModel>(page, itemsPerPage).Count());
        }

        private List<Book> TestData()
        {
            var books = new List<Book>
            {
                new Book
                {
                    Id = 1,
                    Title = "1",
                    Description = "some description for test",
                    Pages = 100,
                    Price = 10,
                    YearPublished = 1990,
                    AuthorId = 1,
                    Author = this.TestDataAuthors().FirstOrDefault(x => x.Id == 1),
                },
                new Book
                {
                    Id = 2,
                    Title = "2",
                    Description = "some description for test",
                    Pages = 200,
                    Price = 20,
                    YearPublished = 2000,
                    AuthorId = 2,
                    Author = this.TestDataAuthors().FirstOrDefault(x => x.Id == 2),
                },
                new Book
                {
                    Id = 3,
                    Title = "3",
                    Description = "some description for test",
                    Pages = 300,
                    Price = 30,
                    YearPublished = 2010,
                    AuthorId = 3,
                    Author = this.TestDataAuthors().FirstOrDefault(x => x.Id == 3),
                },
                new Book
                {
                    Id = 4,
                    Title = "4",
                    Description = "some description for test",
                    Pages = 400,
                    Price = 40,
                    YearPublished = 1950,
                    AuthorId = 1,
                    Author = this.TestDataAuthors().FirstOrDefault(x => x.Id == 1),
                },
            };

            int genreId = 1;
            foreach (var book in books)
            {
                var genre = this.TestDataGenres().FirstOrDefault(x => x.Id == genreId);
                book.Genres.Add(new BookGenre
                {
                    Book = book,
                    BookId = book.Id,
                    Genre = genre,
                    GenreId = genre.Id,
                });
                genreId++;
            }

            return books;
        }

        private List<Author> TestDataAuthors()
        {
            return new List<Author>
            {
                new Author
                {
                    Id = 1,
                    Name = "Ana",
                },
                new Author
                {
                    Id = 2,
                    Name = "Maria",
                },
                new Author
                {
                    Id = 3,
                    Name = "Ivan",
                },
                new Author
                {
                    Id = 4,
                    Name = "Stoyan",
                },
            };
        }

        private List<Genre> TestDataGenres()
        {
            return new List<Genre>
            {
                new Genre
                {
                    Id = 1,
                    Name = "Action",
                    IsFiction = true,
                },
                new Genre
                {
                    Id = 2,
                    Name = "Horror",
                    IsFiction = true,
                },
                new Genre
                {
                    Id = 3,
                    Name = "Travel",
                    IsFiction = false,
                },
                new Genre
                {
                    Id = 4,
                    Name = "Philosophy",
                    IsFiction = false,
                },
            };
        }

        private BooksService MockService(List<Book> books)
        {
            var mockRepoBooks = new Mock<IDeletableEntityRepository<Book>>();
            mockRepoBooks.Setup(x => x.AllAsNoTracking()).Returns(books.AsQueryable);
            var mockRepoGenres = new Mock<IDeletableEntityRepository<Genre>>();
            var mockRepoBooksGenres = new Mock<IRepository<BookGenre>>();

            var service = new BooksService(
                mockRepoBooks.Object,
                mockRepoGenres.Object,
                mockRepoBooksGenres.Object);
            return service;
        }
    }
}
