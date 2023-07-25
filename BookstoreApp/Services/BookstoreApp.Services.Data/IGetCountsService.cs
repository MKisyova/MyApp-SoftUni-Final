namespace BookstoreApp.Services.Data
{
    using BookstoreApp.Web.ViewModels.Home;

    public interface IGetCountsService
    {
        IndexViewModel GetCounts();
    }
}
