namespace BookstoreApp.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using BookstoreApp.Data.Common.Repositories;
    using BookstoreApp.Data.Models;
    using BookstoreApp.Services.Mapping;
    using BookstoreApp.Web.ViewModels;
    using BookstoreApp.Web.ViewModels.Administration.Genres;
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

            Assert.Equal(4, service.GetCount());
        }

        [Fact]
        public void GetAllShouldReturnAllGenres()
        {
            GenresService service = this.MockService(this.TestData());

            Assert.Equal(4, service.GetAll<SingleGenreViewModel>().Count());
        }

        [Fact]
        public void GetAllFictionShouldReturnAllFictionGenres()
        {
            var mockVotesRepo = new Mock<IDeletableEntityRepository<Genre>>();
            mockVotesRepo.Setup(x => x.AllAsNoTracking())
                .Returns(this.TestData()
                .Where(g => g.IsFiction).AsQueryable);

            var service = new GenresService(mockVotesRepo.Object);

            Assert.Equal(2, service.GetAllFiction<SingleGenreViewModel>().Count());
        }

        [Fact]
        public void GetAllNonFictionShouldReturnAllNonFictionGenres()
        {
            var mockVotesRepo = new Mock<IDeletableEntityRepository<Genre>>();
            mockVotesRepo.Setup(x => x.AllAsNoTracking())
                .Returns(this.TestData()
                .Where(g => g.IsFiction == false).AsQueryable);

            var service = new GenresService(mockVotesRepo.Object);

            Assert.Equal(2, service.GetAllNonfiction<SingleGenreViewModel>().Count());
        }

        [Theory]
        [InlineData(1, 2, 2)]
        [InlineData(1, 3, 3)]
        [InlineData(1, 4, 4)]
        public void GetAllWithPagingShouldReturnCorrectGenresCountPerPage(int page, int itemsPerPage, int expectedResult)
        {
            var mockVotesRepo = new Mock<IDeletableEntityRepository<Genre>>();
            mockVotesRepo.Setup(x => x.AllAsNoTracking())
                .Returns(this.TestData()
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage).AsQueryable);

            var service = new GenresService(mockVotesRepo.Object);

            Assert.Equal(expectedResult, service.GetAllWithPaging<SingleGenreInTableViewModel>(page, itemsPerPage).Count());
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [InlineData(3, 3)]
        public void GetByIdShouldReturnTheCorrectId(int id, int expectedResult)
        {
            var mockVotesRepo = new Mock<IDeletableEntityRepository<Genre>>();
            mockVotesRepo.Setup(x => x.AllAsNoTracking())
                .Returns(this.TestData()
                .Where(g => g.Id == id).AsQueryable);

            var service = new GenresService(mockVotesRepo.Object);
            Assert.Equal(expectedResult, service.GetById<SingleGenreViewModel>(id).Id);
        }

        private List<Genre> TestData()
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

        private GenresService MockService(List<Genre> genres)
        {
            var mockVotesRepo = new Mock<IDeletableEntityRepository<Genre>>();
            mockVotesRepo.Setup(x => x.AllAsNoTracking()).Returns(genres.AsQueryable);

            // mockVotesRepo.Setup(x => x.AddAsync(It.IsAny<Genre>()))
            //    .Callback((Genre genre) => genres.Add(genre));
            var service = new GenresService(mockVotesRepo.Object);
            return service;
        }
    }
}
