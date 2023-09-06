namespace BookstoreApp.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using BookstoreApp.Data.Common.Repositories;
    using BookstoreApp.Data.Models;
    using BookstoreApp.Services.Mapping;
    using BookstoreApp.Web.ViewModels;
    using BookstoreApp.Web.ViewModels.Authors;
    using Moq;
    using Xunit;

    public class AuthorsServiceTests
    {
        public AuthorsServiceTests()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
        }

        [Fact]
        public void GetCountShouldReturnTheTotalAuthorsCount()
        {
            AuthorsService service = this.MockService(this.TestData());

            Assert.Equal(4, service.GetCount());
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [InlineData(3, 3)]
        public void GetByIdShouldReturnTheCorrectId(int id, int expectedResult)
        {
            var mockRepoAuthors = new Mock<IDeletableEntityRepository<Author>>();
            mockRepoAuthors.Setup(x => x.AllAsNoTracking())
                .Returns(this.TestData()
                .Where(g => g.Id == id).AsQueryable);
            var mockRepoGenres = new Mock<IDeletableEntityRepository<Genre>>();
            var mockRepoAuthorsGenres = new Mock<IRepository<AuthorGenre>>();

            var service = new AuthorsService(
               mockRepoAuthors.Object,
               mockRepoGenres.Object,
               mockRepoAuthorsGenres.Object);

            Assert.Equal(expectedResult, service.GetById<SingleAuthorViewModel>(id).Id);
        }

        [Theory]
        [InlineData(1, 2, 2)]
        [InlineData(1, 3, 3)]
        [InlineData(1, 4, 4)]
        public void GetAllShouldReturnAllAuthors(int page, int itemsPerPage, int expectedResult)
        {
            var mockRepoAuthors = new Mock<IDeletableEntityRepository<Author>>();
            mockRepoAuthors.Setup(x => x.AllAsNoTracking())
                .Returns(this.TestData()
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage).AsQueryable);
            var mockRepoGenres = new Mock<IDeletableEntityRepository<Genre>>();
            var mockRepoAuthorsGenres = new Mock<IRepository<AuthorGenre>>();

            var service = new AuthorsService(
               mockRepoAuthors.Object,
               mockRepoGenres.Object,
               mockRepoAuthorsGenres.Object);

            Assert.Equal(expectedResult, service.GetAll<SingleAuthorViewModel>(page, itemsPerPage).Count());
        }

        [Fact]
        public void GetAllAuthorsAsKeyValuePairShouldReturnAllAsKeyValuePair()
        {
            AuthorsService service = this.MockService(this.TestData());
            var expectedResult = this.TestData()
                .OrderBy(g => g.Name)
                .Select(g => new KeyValuePair<string, string>(g.Id.ToString(), g.Name));

            Assert.Equal(expectedResult, service.GetAllAuthorsAsKeyValuePair());
        }

        private List<Author> TestData()
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

        private AuthorsService MockService(List<Author> authors)
        {
            var mockRepoAuthors = new Mock<IDeletableEntityRepository<Author>>();
            mockRepoAuthors.Setup(x => x.AllAsNoTracking()).Returns(authors.AsQueryable);
            var mockRepoGenres = new Mock<IDeletableEntityRepository<Genre>>();
            var mockRepoAuthorsGenres = new Mock<IRepository<AuthorGenre>>();

            // mockVotesRepo.Setup(x => x.AddAsync(It.IsAny<Genre>()))
            //    .Callback((Genre genre) => genres.Add(genre));
            var service = new AuthorsService(mockRepoAuthors.Object, mockRepoGenres.Object, mockRepoAuthorsGenres.Object);
            return service;
        }
    }
}
