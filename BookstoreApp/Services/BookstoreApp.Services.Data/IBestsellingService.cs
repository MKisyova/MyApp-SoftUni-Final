namespace BookstoreApp.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IBestsellingService
    {
        Task IncreaseBestsellingBooksValue(IEnumerable<int> bookIds);
    }
}
