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
            var mockGenresRepo = new Mock<IDeletableEntityRepository<Genre>>();
            mockGenresRepo.Setup(x => x.AllAsNoTracking())
                .Returns(this.TestData()
                .Where(g => g.IsFiction).AsQueryable);

            var service = new GenresService(mockGenresRepo.Object);

            Assert.Equal(2, service.GetAllFiction<SingleGenreViewModel>().Count());
        }

        [Fact]
        public void GetAllNonFictionShouldReturnAllNonFictionGenres()
        {
            var mockGenresRepo = new Mock<IDeletableEntityRepository<Genre>>();
            mockGenresRepo.Setup(x => x.AllAsNoTracking())
                .Returns(this.TestData()
                .Where(g => g.IsFiction == false).AsQueryable);

            var service = new GenresService(mockGenresRepo.Object);

            Assert.Equal(2, service.GetAllNonfiction<SingleGenreViewModel>().Count());
        }

        [Theory]
        [InlineData(1, 2, 2)]
        [InlineData(1, 3, 3)]
        [InlineData(1, 4, 4)]
        public void GetAllWithPagingShouldReturnCorrectGenresCountPerPage(int page, int itemsPerPage, int expectedResult)
        {
            var mockGenresRepo = new Mock<IDeletableEntityRepository<Genre>>();
            mockGenresRepo.Setup(x => x.AllAsNoTracking())
                .Returns(this.TestData()
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage).AsQueryable);

            var service = new GenresService(mockGenresRepo.Object);
            Assert.Equal(expectedResult, service.GetAllWithPaging<SingleGenreInTableViewModel>(page, itemsPerPage).Count());
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [InlineData(3, 3)]
        public void GetByIdShouldReturnTheCorrectGenreById(int id, int expectedResult)
        {
            var mockGenresRepo = new Mock<IDeletableEntityRepository<Genre>>();
            mockGenresRepo.Setup(x => x.AllAsNoTracking())
                .Returns(this.TestData()
                .Where(g => g.Id == id).AsQueryable);

            var service = new GenresService(mockGenresRepo.Object);
            Assert.Equal(expectedResult, service.GetById<SingleGenreViewModel>(id).Id);
        }

        [Fact]
        public void GetAllGenresAsKeyValuePairShouldReturnAllAsKeyValuePair()
        {
            GenresService service = this.MockService(this.TestData());
            var expectedResult = this.TestData()
                .OrderBy(g => g.Name)
                .Select(g => new KeyValuePair<string, string>(g.Id.ToString(), g.Name));

            Assert.Equal(expectedResult, service.GetAllGenresAsKeyValuePair());
        }

        [Fact]
        public async Task CreateShouldCreateGenreSuccessfully()
        {
            var genres = new List<Genre>();
            var mockGenresRepo = new Mock<IDeletableEntityRepository<Genre>>();
            mockGenresRepo.Setup(x => x.AddAsync(It.IsAny<Genre>()))
               .Callback((Genre genre) => genres.Add(genre));

            var model = new CreateGenreInputModel
            {
                Name = "Action",
                IsFiction = true,
            };

            var service = new GenresService(mockGenresRepo.Object);
            await service.CreateAsync(model);

            Assert.Single(genres);
            Assert.Equal(model.Name, genres.FirstOrDefault().Name);
            Assert.Equal(model.IsFiction, genres.FirstOrDefault().IsFiction);
            mockGenresRepo.Verify(x => x.SaveChangesAsync(), Times.Once);
            mockGenresRepo.Verify(x => x.AddAsync(It.IsAny<Genre>()), Times.Once);
        }

        [Fact]
        public async Task UpdateShouldUpdateGenreSuccessfully()
        {
            var genreId = 1;
            var genres = new List<Genre>();
            var model = new EditGenreInputModel
            {
                Name = "Action",
                IsFiction = true,
            };

            var existingGenre = new Genre
            {
                Id = genreId,
                Name = "ActionChanged",
                IsFiction = false,
            };
            genres.Add(existingGenre);

            var mockGenresRepo = new Mock<IDeletableEntityRepository<Genre>>();
            mockGenresRepo.Setup(x => x.All())
            .Returns(genres.AsQueryable());

            var service = new GenresService(mockGenresRepo.Object);
            await service.UpdateAsync(genreId, model);

            Assert.Single(genres);
            Assert.Equal(model.Name, genres.FirstOrDefault().Name);
            Assert.Equal(model.IsFiction, genres.FirstOrDefault().IsFiction);
            mockGenresRepo.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteShouldDeleteGenreSuccessfully()
        {
            var genreId = 1;
            var genres = new List<Genre>()
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
            };

            var mockGenresRepo = new Mock<IDeletableEntityRepository<Genre>>();
            mockGenresRepo.Setup(x => x.All())
                .Returns(genres.AsQueryable);
            mockGenresRepo.Setup(x => x.Delete(It.IsAny<Genre>()))
                .Callback((Genre genre) => genres.Remove(genre));
            var service = new GenresService(mockGenresRepo.Object);

            await service.DeleteAsync(genreId);

            Assert.Single(genres);
            Assert.DoesNotContain(genres, genre => genre.Id == genreId);
            mockGenresRepo.Verify(x => x.SaveChangesAsync(), Times.Once);
            mockGenresRepo.Verify(x => x.Delete(It.IsAny<Genre>()), Times.Once);
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
            var mockGenresRepo = new Mock<IDeletableEntityRepository<Genre>>();
            mockGenresRepo.Setup(x => x.AllAsNoTracking()).Returns(genres.AsQueryable);

            var service = new GenresService(mockGenresRepo.Object);
            return service;
        }
    }
}
