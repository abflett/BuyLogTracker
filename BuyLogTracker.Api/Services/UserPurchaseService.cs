using BuyLogTracker.Api.Data.Repositories;
using BuyLogTracker.Api.DTOs;
using BuyLogTracker.Api.Helpers;
using BuyLogTracker.Api.Models;

namespace BuyLogTracker.Api.Services
{
    public class UserPurchaseService
    {
        private readonly UserRepository _userRepository;
        private readonly PurchaseHistoryRepository _purchaseHistory;

        public UserPurchaseService(UserRepository userRepository, PurchaseHistoryRepository purchaseHistoryRepository)
        {
            _userRepository = userRepository;
            _purchaseHistory = purchaseHistoryRepository;
        }

        public async Task<List<User>> Users()
        {
            return await _userRepository.Users();
        }

        public async Task<List<PurchaseHistory>> PurchaseHistories()
        {
            return await _purchaseHistory.PurchaseHistories();
        }

        public async Task<bool> AddUserPurchase(UserPurchaseDTO userPurchase)
        {
            User? user = await _userRepository.GetExistingUser(userPurchase.Name, PhoneNumber.FormatPhoneNumberForStorage(userPurchase.Phone))
                ?? await _userRepository.CreateUser(new User()
                {
                    Name = userPurchase.Name,
                    Phone = userPurchase.Phone,
                    Email = userPurchase.Email,
                });

            if (user != null)
            {
                var history = await _purchaseHistory.CreatePurchaseHistory(new PurchaseHistory
                {
                    Description = userPurchase.Description,
                    User = user
                });
                return history != null;
            }

            return false;
        }

        public async Task<bool> AddPurchaseToUser(PurchaseDTO purchase)
        {
            var user = await _userRepository.GetUserById(purchase.Id);
            if (user == null)
            {
                return false;
            }
            var purchaseHistory = new PurchaseHistory { Description = purchase.Description, User = user };
            await _purchaseHistory.CreatePurchaseHistory(purchaseHistory);
            return true;
        }

        public async Task<User?> CreateUser(UserDTO user)
        {
            User newUser = new()
            {
                Name = user.Name,
                Email = user.Email,
                Phone = user.Phone,
            };
            return await _userRepository.CreateUser(newUser);
        }

        public async Task<List<User>> FindUsersByName(string searchString)
        {
            return await _userRepository.GetUsersByName(searchString);
        }

        public async Task<List<User>> FindUsersByPhone(string searchString)
        {
            List<User> users = await _userRepository.GetUsersByPhone(PhoneNumber.FormatPhoneNumberForStorage(searchString));
            foreach (var user in users)
            {
                user.Phone = PhoneNumber.FormatPhoneNumberForDisplay(user.Phone);
            }
            return users;
        }

        public async Task<List<User>> FindUsersByEmail(string searchString)
        {
            return await _userRepository.GetUsersByEmail(searchString);
        }

        public async Task<List<User>> FindUsersByPurchaseHistoryDescription(string searchString)
        {
            return await _userRepository.GetUsersByPurchaseHistoryDescription(searchString);
        }

        public async Task<User?> UserById(int userId)
        {
            return await _userRepository.GetUserById(userId);
        }

        public async Task<bool> DeleteUser(int userId)
        {
            return await _userRepository.DeleteUserById(userId);
        }

        public async Task<bool> UpdateUser(User updatedUser)
        {
            return await _userRepository.UpdateUser(updatedUser);
        }

        public async Task<bool> DeletePurchaseHistory(int purchaseHistoryId)
        {
            return await _purchaseHistory.DeletePurchaseHistoryById(purchaseHistoryId);
        }

        public async Task<bool> UpdatePurchaseHistory(PurchaseHistory updatedPurchaseHistory)
        {
            return await _purchaseHistory.UpdatePurchaseHistory(updatedPurchaseHistory);
        }
    }
}