namespace BookstoreApp.Services.Data
{
    using System.Threading.Tasks;

    public interface IVotesService
    {
        Task SetVoteAsync(int bookId, string userId, byte value);

        double GetAverageVote(int bookId);
    }
}
