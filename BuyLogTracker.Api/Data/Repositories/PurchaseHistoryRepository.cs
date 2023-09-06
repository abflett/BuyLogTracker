namespace BuyLogTracker.Api.Data.Repositories
{
    public class PurchaseHistoryRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public PurchaseHistoryRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
    }
}
