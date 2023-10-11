namespace BookstoreApp.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using BookstoreApp.Data.Common.Repositories;
    using BookstoreApp.Data.Models;
    using BookstoreApp.Services.Mapping;
    using BookstoreApp.Web.ViewModels;
    using BookstoreApp.Web.ViewModels.Administration.Books;
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
            var mockRepoBooks = new Mock<IDeletableEntityRepository<Book>>();
            mockRepoBooks.Setup(x => x.AllAsNoTracking()).Returns(this.TestData().AsQueryable);
            var service = this.MockService(mockRepoBooks);

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
            var service = this.MockService(mockRepoBooks);

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
            var service = this.MockService(mockRepoBooks);

            Assert.Equal(expectedResult, service.GetCountByGenreId(genreId));
        }

        [Fact]
        public void GetCountByGenreFictionShouldReturnTheBooksCountWithGenreFiction()
        {
            var mockRepoBooks = new Mock<IDeletableEntityRepository<Book>>();
            mockRepoBooks.Setup(x => x.AllAsNoTracking())
                .Returns(this.TestData()
                .Where(b => b.Genres.Any(g => g.Genre.IsFiction)).AsQueryable);
            var service = this.MockService(mockRepoBooks);

            Assert.Equal(2, service.GetCountByGenresFiction());
        }

        [Fact]
        public void GetCountByGenreNonFictionShouldReturnTheBooksCountWithGenreNonFiction()
        {
            var mockRepoBooks = new Mock<IDeletableEntityRepository<Book>>();
            mockRepoBooks.Setup(x => x.AllAsNoTracking())
                .Returns(this.TestData()
                .Where(b => b.Genres.Any(g => g.Genre.IsFiction == false)).AsQueryable);
            var service = this.MockService(mockRepoBooks);

            Assert.Equal(2, service.GetCountByGenresNonFiction());
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [InlineData(3, 3)]
        public void GetByIdShouldReturnTheCorrectBookById(int id, int expectedResult)
        {
            var mockRepoBooks = new Mock<IDeletableEntityRepository<Book>>();
            mockRepoBooks.Setup(x => x.AllAsNoTracking())
                .Returns(this.TestData()
                .Where(g => g.Id == id).AsQueryable);
            var service = this.MockService(mockRepoBooks);

            Assert.Equal(expectedResult, service.GetById<SingleBookInTableViewModel>(id).Id);
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
            var service = this.MockService(mockRepoBooks);

            Assert.Equal(expectedResult, service.GetAll<SingleBookInTableViewModel>(page, itemsPerPage).Count());
        }

        [Theory]
        [InlineData(1, 2, 2)]
        [InlineData(1, 3, 3)]
        [InlineData(1, 4, 4)]
        public void GetAllRandomShouldReturnAllBooksInRandomOrder(int page, int itemsPerPage, int expectedResult)
        {
            var mockRepoBooks = new Mock<IDeletableEntityRepository<Book>>();
            mockRepoBooks.Setup(x => x.AllAsNoTracking())
                .Returns(this.TestData()
                .OrderBy(x => Guid.NewGuid())
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage).AsQueryable);
            var service = this.MockService(mockRepoBooks);

            var getAllRandomCalledFirstTime = service.GetAllRandom<SingleBookInTableViewModel>(page, itemsPerPage).ToArray();
            var getAllRandomCalledSecondTime = service.GetAllRandom<SingleBookInTableViewModel>(page, itemsPerPage).ToArray();
            var resultFromGetAllRandomIsAlwaysDifferent = false;

            for (int i = 0; i < getAllRandomCalledFirstTime.Count(); i++)
            {
                if (getAllRandomCalledFirstTime[i].Id != getAllRandomCalledSecondTime[i].Id)
                {
                    resultFromGetAllRandomIsAlwaysDifferent = true;
                }
            }

            Assert.True(resultFromGetAllRandomIsAlwaysDifferent);
            Assert.Equal(expectedResult, service.GetAllRandom<SingleBookInTableViewModel>(page, itemsPerPage).Count());
        }

        [Theory]
        [InlineData(2, 2)]
        [InlineData(3, 3)]
        [InlineData(4, 4)]
        public void GetBookForHomePageShouldReturnTheGivenCountOfBooksInRandomOrder(int booksCount, int expectedResult)
        {
            var mockRepoBooks = new Mock<IDeletableEntityRepository<Book>>();
            mockRepoBooks.Setup(x => x.AllAsNoTracking())
                .Returns(this.TestData()
                .OrderBy(x => Guid.NewGuid())
                .Take(booksCount).AsQueryable);
            var service = this.MockService(mockRepoBooks);

            var getBookForHomePageCalledFirstTime = service.GetBookForHomePage<SingleBookInTableViewModel>(booksCount).ToArray();
            var getBookForHomePageCalledSecondTime = service.GetBookForHomePage<SingleBookInTableViewModel>(booksCount).ToArray();
            var resultFromBookForHomePageIsAlwaysDifferent = false;

            for (int i = 0; i < getBookForHomePageCalledFirstTime.Count(); i++)
            {
                if (getBookForHomePageCalledFirstTime[i].Id != getBookForHomePageCalledSecondTime[i].Id)
                {
                    resultFromBookForHomePageIsAlwaysDifferent = true;
                }
            }

            Assert.True(resultFromBookForHomePageIsAlwaysDifferent);
            Assert.Equal(expectedResult, service.GetBookForHomePage<SingleBookInTableViewModel>(booksCount).Count());
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
            var service = this.MockService(mockRepoBooks);

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
            var service = this.MockService(mockRepoBooks);

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
            var service = this.MockService(mockRepoBooks);

            Assert.Equal(expectedResult, service.GetByGenresNonfiction<SingleBookInTableViewModel>(page, itemsPerPage).Count());
        }

        [Theory]
        [InlineData(1, 1, 2, 1)]
        [InlineData(1, 1, 3, 1)]
        [InlineData(2, 1, 4, 1)]
        public void GetByGenreIdShouldReturnAllBooksWithTheGivenGenreId(int genreId, int page, int itemsPerPage, int expectedResult)
        {
            var mockRepoBooks = new Mock<IDeletableEntityRepository<Book>>();
            mockRepoBooks.Setup(x => x.AllAsNoTracking())
                .Returns(this.TestData()
                .Where(x => x.Genres.Any(g => g.GenreId == genreId))
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage).AsQueryable);
            var service = this.MockService(mockRepoBooks);

            Assert.Equal(expectedResult, service.GetByGenreId<SingleBookInTableViewModel>(genreId, page, itemsPerPage).Count());
        }

        [Theory]
        [InlineData(1, 1, 2, 2)]
        [InlineData(2, 1, 3, 1)]
        [InlineData(3, 1, 4, 1)]
        public void GetByAuthorIdShouldReturnAllBooksWithTheGivenAuthorId(int authorId, int page, int itemsPerPage, int expectedResult)
        {
            var mockRepoBooks = new Mock<IDeletableEntityRepository<Book>>();
            mockRepoBooks.Setup(x => x.AllAsNoTracking())
                .Returns(this.TestData()
                .Where(x => x.AuthorId == authorId)
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage).AsQueryable);
            var service = this.MockService(mockRepoBooks);

            Assert.Equal(expectedResult, service.GetByAuthorId<SingleBookInTableViewModel>(authorId, page, itemsPerPage).Count());
        }

        [Theory]
        [InlineData("1", new[] { "1" })]
        [InlineData("Ana", new[] { "1", "4" })]
        [InlineData("2", new[] { "2" })]
        public void GetByKeywordShouldReturnAllBooksWithTheGivenKeyword(string keyword, string[] expectedResult)
        {
            var mockRepoBooks = new Mock<IDeletableEntityRepository<Book>>();
            mockRepoBooks.Setup(x => x.AllAsNoTracking())
                .Returns(this.TestData()
                .Where(x => x.Title.Contains(keyword) || x.Author.Name.Contains(keyword)).AsQueryable);
            var service = this.MockService(mockRepoBooks);

            Assert.Equal(expectedResult, service.GetByKeyword<SingleBookInTableViewModel>(keyword).Select(x => x.Title));
        }

        [Theory]
        [InlineData(1, 2, 2)]
        [InlineData(1, 3, 3)]
        [InlineData(1, 4, 3)]
        public void GetBySalesCountShouldReturnAllBooksOrderedBySalesCount(int page, int itemsPerPage, int expectedResult)
        {
            var mockRepoBooks = new Mock<IDeletableEntityRepository<Book>>();
            mockRepoBooks.Setup(x => x.AllAsNoTracking())
                .Returns(this.TestData()
                .Where(x => x.BestsellingBook.SalesCount > 0)
                .OrderByDescending(x => x.BestsellingBook.SalesCount)
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage).AsQueryable);
            var service = this.MockService(mockRepoBooks);

            var bookWithMostSalesId = 3;
            var secondBookWithMostSalesId = 2;
            var bookWithMostSalesIdFromTheCollection = service.GetBySalesCount<SingleBookInTableViewModel>(page, itemsPerPage).ToArray()[0].Id;
            var secondBookWithMostSalesIdFromTheCollection = service.GetBySalesCount<SingleBookInTableViewModel>(page, itemsPerPage).ToArray()[1].Id;

            Assert.Equal(expectedResult, service.GetBySalesCount<SingleBookInTableViewModel>(page, itemsPerPage).Count());
            Assert.Equal(bookWithMostSalesId, bookWithMostSalesIdFromTheCollection);
            Assert.Equal(secondBookWithMostSalesId, secondBookWithMostSalesIdFromTheCollection);
        }

        [Fact]
        public void GetCountBySalesCountShouldReturnTheBooksCountOfAllTheBooksThatHaveSalesCount()
        {
            var mockRepoBooks = new Mock<IDeletableEntityRepository<Book>>();
            mockRepoBooks.Setup(x => x.AllAsNoTracking())
                .Returns(this.TestData()
                .Where(x => x.BestsellingBook.SalesCount > 0).AsQueryable);
            var service = this.MockService(mockRepoBooks);

            Assert.Equal(3, service.GetCountBySalesCount());
        }

        [Theory]
        [InlineData(1, 2, 2)]
        [InlineData(1, 3, 3)]
        [InlineData(1, 4, 4)]
        public void GetTopRatedShouldReturnAllBooksOrderedByVotes(int page, int itemsPerPage, int expectedResult)
        {
            var mockRepoBooks = new Mock<IDeletableEntityRepository<Book>>();
            mockRepoBooks.Setup(x => x.AllAsNoTracking())
                .Returns(this.TestData()
                .Where(x => x.Votes.Count() > 0)
                .OrderByDescending(x => x.Votes.Average(v => v.Value))
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage).AsQueryable);
            var service = this.MockService(mockRepoBooks);

            var topRatedBookId = 4;
            var secondtopRatedBookId = 3;
            var topRatedBookIdFromTheCollection = service.GetTopRated<SingleBookInTableViewModel>(page, itemsPerPage).ToArray()[0].Id;
            var secondTopRatedBookIdFromTheCollection = service.GetTopRated<SingleBookInTableViewModel>(page, itemsPerPage).ToArray()[1].Id;

            Assert.Equal(expectedResult, service.GetTopRated<SingleBookInTableViewModel>(page, itemsPerPage).Count());
            Assert.Equal(topRatedBookId, topRatedBookIdFromTheCollection);
            Assert.Equal(secondtopRatedBookId, secondTopRatedBookIdFromTheCollection);
        }

        [Fact]
        public void GetCountByTopRatedShouldReturnTheBooksCountOfTheBooksThatHaveVotes()
        {
            var mockRepoBooks = new Mock<IDeletableEntityRepository<Book>>();
            mockRepoBooks.Setup(x => x.AllAsNoTracking())
                .Returns(this.TestData()
                .Where(x => x.Votes.Count() > 0).AsQueryable);
            var service = this.MockService(mockRepoBooks);

            Assert.Equal(4, service.GetCountByTopRated());
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
                    BestsellingBook = this.TestDataBestsellingBooks().FirstOrDefault(x => x.BookId == 1),
                    Votes = this.TestDataVotes().Where(x => x.BookId == 1).ToList(),
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
                    BestsellingBook = this.TestDataBestsellingBooks().FirstOrDefault(x => x.BookId == 2),
                    Votes = this.TestDataVotes().Where(x => x.BookId == 2).ToList(),
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
                    BestsellingBook = this.TestDataBestsellingBooks().FirstOrDefault(x => x.BookId == 3),
                    Votes = this.TestDataVotes().Where(x => x.BookId == 3).ToList(),
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
                    BestsellingBook = this.TestDataBestsellingBooks().FirstOrDefault(x => x.BookId == 4),
                    Votes = this.TestDataVotes().Where(x => x.BookId == 4).ToList(),
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

        private List<BestsellingBook> TestDataBestsellingBooks()
        {
            return new List<BestsellingBook>
            {
                new BestsellingBook
                {
                    BookId = 1,
                    SalesCount = 1,
                },
                new BestsellingBook
                {
                    BookId = 2,
                    SalesCount = 2,
                },
                new BestsellingBook
                {
                    BookId = 3,
                    SalesCount = 3,
                },
                new BestsellingBook
                {
                    BookId = 4,
                    SalesCount = 0,
                },
            };
        }

        private List<Vote> TestDataVotes()
        {
            return new List<Vote>
            {
                new Vote
                {
                    BookId = 1,
                    Value = 1,
                },
                new Vote
                {
                    BookId = 2,
                    Value = 2,
                },
                new Vote
                {
                    BookId = 3,
                    Value = 3,
                },
                new Vote
                {
                    BookId = 4,
                    Value = 4,
                },
            };
        }

        private BooksService MockService(Mock<IDeletableEntityRepository<Book>> mockRepoBooks)
        {
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
