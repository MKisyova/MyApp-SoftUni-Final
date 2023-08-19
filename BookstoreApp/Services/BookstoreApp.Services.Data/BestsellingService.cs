namespace BookstoreApp.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BookstoreApp.Data.Common.Repositories;
    using BookstoreApp.Data.Models;

    public class BestsellingService : IBestsellingService
    {
        private readonly IDeletableEntityRepository<Book> booksRepository;
        private readonly IDeletableEntityRepository<BestsellingBook> bestsellingRepository;

        public BestsellingService(
            IDeletableEntityRepository<Book> booksRepository,
            IDeletableEntityRepository<BestsellingBook> bestsellingRepository)
        {
            this.booksRepository = booksRepository;
            this.bestsellingRepository = bestsellingRepository;
        }

        public async Task IncreaseBestsellingBooksValue(IEnumerable<int> bookIds)
        {
            foreach (var bookId in bookIds)
            {
                var bestsellingBook = this.bestsellingRepository.All()
                    .Where(x => x.BookId == bookId)
                    .FirstOrDefault();

                if (bestsellingBook == null)
                {
                    var book = this.booksRepository.AllAsNoTracking()
                        .Where(x => x.Id == bookId)
                        .FirstOrDefault();

                    bestsellingBook = new BestsellingBook
                    {
                        BookId = book.Id,
                    };

                    await this.bestsellingRepository.AddAsync(bestsellingBook);
                }

                bestsellingBook.SalesCount++;
            }

            await this.bestsellingRepository.SaveChangesAsync();
        }
    }
}
