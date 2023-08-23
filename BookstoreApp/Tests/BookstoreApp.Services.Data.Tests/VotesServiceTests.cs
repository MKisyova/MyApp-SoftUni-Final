namespace BookstoreApp.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BookstoreApp.Data.Common.Repositories;
    using BookstoreApp.Data.Models;
    using Moq;
    using Xunit;

    public class VotesServiceTests
    {
        [Fact]
        public async Task WhenUserVotes2TimesOnly1VoteShouldBeCounted()
        {
            var votes = new List<Vote>();
            VotesService service = MockService(votes);

            await service.SetVoteAsync(1, "1", 1);
            await service.SetVoteAsync(1, "1", 3);
            await service.SetVoteAsync(1, "1", 5);

            Assert.Single(votes);
            Assert.Equal(5, votes.First().Value);
        }

        [Fact]
        public async Task When2UsersVoteForTheSameBookTheAverageVoteShouldBeCorrect()
        {
            var votes = new List<Vote>();
            VotesService service = MockService(votes);

            await service.SetVoteAsync(1, "1", 4);
            await service.SetVoteAsync(1, "1", 2);
            await service.SetVoteAsync(1, "2", 5);

            Assert.Equal(2, votes.Count);
            Assert.Equal(3.5, service.GetAverageVote(1));
        }

        private static VotesService MockService(List<Vote> votes)
        {
            var mockVotesRepo = new Mock<IRepository<Vote>>();
            mockVotesRepo.Setup(x => x.All()).Returns(votes.AsQueryable);
            mockVotesRepo.Setup(x => x.AddAsync(It.IsAny<Vote>()))
                .Callback((Vote vote) => votes.Add(vote));

            var service = new VotesService(mockVotesRepo.Object);
            return service;
        }
    }
}
