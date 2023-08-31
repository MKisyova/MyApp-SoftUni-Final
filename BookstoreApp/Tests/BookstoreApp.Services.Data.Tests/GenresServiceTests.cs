namespace BookstoreApp.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using BookstoreApp.Data.Common.Repositories;
    using BookstoreApp.Data.Models;
    using BookstoreApp.Services.Mapping;
    using BookstoreApp.Web.ViewModels;
    using BookstoreApp.Web.ViewModels.Genres;
    using Moq;
    using Xunit;

    public class GenresServiceTests
    {
        public GenresServiceTests()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
        }

        [Fact]
        public void GetCountShouldReturnTheTotalGenresCount()
        {
            GenresService service = this.MockService(this.TestData());

            Assert.Equal(3, service.GetCount());
        }

        [Fact]
        public void GetAllShouldReturnAllGenres()
        {
            GenresService service = this.MockService(this.TestData());

            Assert.Equal(3, service.GetAll<SingleGenreViewModel>().Count());
        }

        private List<Genre> TestData()
        {
            return new List<Genre>
            {
                new Genre
                {
                    Name = "Action",
                    IsFiction = true,
                },
                new Genre
                {
                    Name = "Horror",
                    IsFiction = true,
                },
                new Genre
                {
                    Name = "Travel",
                    IsFiction = false,
                },
            };
        }

        private GenresService MockService(List<Genre> genres)
        {
            var mockVotesRepo = new Mock<IDeletableEntityRepository<Genre>>();
            mockVotesRepo.Setup(x => x.AllAsNoTracking()).Returns(genres.AsQueryable);
            mockVotesRepo.Setup(x => x.AddAsync(It.IsAny<Genre>()))
                .Callback((Genre genre) => genres.Add(genre));

            var service = new GenresService(mockVotesRepo.Object);
            return service;
        }
    }
}
