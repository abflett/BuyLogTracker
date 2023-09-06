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


//AddPurchase(): Combining user creation and purchase history addition into one method is a practical choice for simplicity, as discussed earlier.

//CreateUser(): This method can be useful for scenarios where you only need to create a new user without adding purchase history.

//AddPurchaseToExistingUser(): Good for adding purchase history to an existing user, following the challenge requirements.

//FindUsersByName(), FindUsersByPhone(), FindUsersByEmail(): These methods can help you search for users based on name or phone number, which can be useful for query functionalities.

//FindUsersByPurchaseHistoryDescription(): Useful for searching users based on purchase history descriptions.

//UserById(): Fetching user details and associated purchase history by user ID is essential for detailed user information retrieval.

//DeleteUser()

//UpdateUser()