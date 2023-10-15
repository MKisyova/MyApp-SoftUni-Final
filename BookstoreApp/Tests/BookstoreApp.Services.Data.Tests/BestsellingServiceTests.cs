namespace BookstoreApp.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using BookstoreApp.Data.Common.Repositories;
    using BookstoreApp.Data.Models;
    using BookstoreApp.Services.Mapping;
    using BookstoreApp.Web.ViewModels;
    using Moq;
    using Xunit;

    public class BestsellingServiceTests
    {
        [Theory]
        [InlineData(new int[] { 1 }, new int[] { 2 })]
        [InlineData(new int[] { 1, 2 }, new int[] { 2, 3 })]
        [InlineData(new int[] { 1, 2, 3, 4 }, new int[] { 2, 3, 4, 5 })]
        public async Task IncreaseBestsellingBooksValueShouldIncreaseTheSellingValueToTheBoughtBooks(int[] bookIds, int[] expectedResult)
        {
            var mockRepoBestsellingBooks = new Mock<IDeletableEntityRepository<BestsellingBook>>();
            var bestsellingBooks = this.TestData();

            mockRepoBestsellingBooks.Setup(x => x.All())
           .Returns(bestsellingBooks
           .Where(x => bookIds.Contains(x.BookId)).AsQueryable);

            var service = this.MockService(mockRepoBestsellingBooks);
            await service.IncreaseBestsellingBooksValue(bookIds);

            var boughtBooks = bestsellingBooks.Where(x => bookIds.Contains(x.BookId)).OrderBy(x => x.BookId).Select(x => x.SalesCount).ToArray();

            for (int i = 0; i < expectedResult.Count(); i++)
            {
                Assert.Equal(expectedResult[i], (int)boughtBooks[i]);
            }
        }

        [Theory]
        [InlineData(new int[] { 1 }, new int[] { 1 })]
        [InlineData(new int[] { 1, 2 }, new int[] { 1, 2 })]
        [InlineData(new int[] { 1, 2, 3, 4 }, new int[] { 1, 2, 3, 4 })]
        public async Task IncreaseBestsellingBooksValueShouldGetTheBoughtBooksCorrectly(int[] bookIds, int[] expectedResult)
        {
           // var mockRepoBestsellingBooks = new Mock<IDeletableEntityRepository<BestsellingBook>>();
           // var bestsellingBooks = this.TestData();

           // mockRepoBestsellingBooks.Setup(x => x.All())
           //.Returns(bestsellingBooks
           //.Where(x => bookIds.Contains(x.BookId)).AsQueryable);

           // var service = this.MockService(mockRepoBestsellingBooks);
           // await service.IncreaseBestsellingBooksValue(bookIds);

           // var boughtBooks = bestsellingBooks.Where(x => bookIds.Contains(x.BookId)).OrderBy(x => x.BookId).Select(x => x.SalesCount).ToArray();

           // for (int i = 0; i < expectedResult.Count(); i++)
           // {
           //     Assert.Equal(expectedResult[i], (int)boughtBooks[i]);
           // }
        }

        private List<BestsellingBook> TestData()
        {
            return new List<BestsellingBook>
            {
                new BestsellingBook
                {
                    Id = 1,
                    BookId = 1,
                    SalesCount = 1,
                },
                new BestsellingBook
                {
                    Id = 2,
                    BookId = 2,
                    SalesCount = 2,
                },
                new BestsellingBook
                {
                    Id = 3,
                    BookId = 3,
                    SalesCount = 3,
                },
                new BestsellingBook
                {
                    Id = 4,
                    BookId = 4,
                    SalesCount = 4,
                },
            };
        }

        private List<Book> TestDataBooks()
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
                },
            };

            return books;
        }

        private BestsellingService MockService(Mock<IDeletableEntityRepository<BestsellingBook>> mockRepoBestsellingBooks)
        {
            var mockRepoBooks = new Mock<IDeletableEntityRepository<Book>>();
            var service = new BestsellingService(
                mockRepoBooks.Object,
                mockRepoBestsellingBooks.Object);
            return service;
        }
    }
}
