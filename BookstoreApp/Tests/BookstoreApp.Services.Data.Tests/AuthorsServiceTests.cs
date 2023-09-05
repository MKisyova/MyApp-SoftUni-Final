namespace BookstoreApp.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using BookstoreApp.Data.Common.Repositories;
    using BookstoreApp.Data.Models;
    using BookstoreApp.Services.Mapping;
    using BookstoreApp.Web.ViewModels;
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
            var mockAuthorsRepoAuthors = new Mock<IDeletableEntityRepository<Author>>();
            mockAuthorsRepoAuthors.Setup(x => x.AllAsNoTracking()).Returns(authors.AsQueryable);
            var mockAuthorsRepoGenres = new Mock<IDeletableEntityRepository<Genre>>();
            var mockAuthorsRepoAuthorsGenres = new Mock<IRepository<AuthorGenre>>();

            // mockVotesRepo.Setup(x => x.AddAsync(It.IsAny<Genre>()))
            //    .Callback((Genre genre) => genres.Add(genre));
            var service = new AuthorsService(mockAuthorsRepoAuthors.Object, mockAuthorsRepoGenres.Object, mockAuthorsRepoAuthorsGenres.Object);
            return service;
        }
    }
}
