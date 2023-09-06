using BuyLogTracker.Api.Data.Repositories;

namespace BuyLogTracker.Api.Services
{
    public class UserPurchaseService
    {
        private readonly UserRepository _user;
        private readonly PurchaseHistoryRepository _purchaseHistory;

        public UserPurchaseService(UserRepository userRepository, PurchaseHistoryRepository purchaseHistoryRepository)
        {
            _user = userRepository;
            _purchaseHistory = purchaseHistoryRepository;
        }
    }
}
