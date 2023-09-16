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
        public void GetByIdShouldReturnTheCorrectAuthorById(int id, int expectedResult)
        {
            var mockRepoAuthors = new Mock<IDeletableEntityRepository<Author>>();
            mockRepoAuthors.Setup(x => x.AllAsNoTracking())
                .Returns(this.TestData()
                .Where(a => a.Id == id).AsQueryable);
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
                .OrderBy(a => a.Name)
                .Select(a => new KeyValuePair<string, string>(a.Id.ToString(), a.Name));

            Assert.Equal(expectedResult, service.GetAllAuthorsAsKeyValuePair());
        }

        [Fact]
        public async Task DeleteShouldDeleteAuthorSuccessfully()
        {
            var authorId = 1;
            var authors = new List<Author>()
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
            };

            var mockRepoAuthors = new Mock<IDeletableEntityRepository<Author>>();
            mockRepoAuthors.Setup(x => x.All())
                .Returns(authors.AsQueryable);
            mockRepoAuthors.Setup(x => x.Delete(It.IsAny<Author>()))
                .Callback((Author author) => authors.Remove(author));
            var mockRepoGenres = new Mock<IDeletableEntityRepository<Genre>>();
            var mockRepoAuthorsGenres = new Mock<IRepository<AuthorGenre>>();

            var service = new AuthorsService(
               mockRepoAuthors.Object,
               mockRepoGenres.Object,
               mockRepoAuthorsGenres.Object);

            await service.DeleteAsync(authorId);

            Assert.Single(authors);
            Assert.DoesNotContain(authors, author => author.Id == authorId);
            mockRepoAuthors.Verify(x => x.SaveChangesAsync(), Times.Once);
            mockRepoAuthors.Verify(x => x.Delete(It.IsAny<Author>()), Times.Once);
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
            var service = new AuthorsService(
                mockRepoAuthors.Object,
                mockRepoGenres.Object,
                mockRepoAuthorsGenres.Object);
            return service;
        }
    }
}
