using BuyLogTracker.Api.Helpers;
using BuyLogTracker.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace BuyLogTracker.Api.Data.Repositories
{
    public class UserRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UserRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<User> CreateUser(User user)
        {
            var newUser = await _applicationDbContext.Users.AddAsync(user);
            await _applicationDbContext.SaveChangesAsync();
            return newUser.Entity;
        }

        public async Task<User?> GetUserById(int id)
        {
            return await _applicationDbContext
                .Users
                .Include(u => u.PurchaseHistory)
                .SingleOrDefaultAsync(u => u.Id == id);
        }

        public Task<List<User>> GetUsersByName(string searchString) 
        {
            return _applicationDbContext
                .Users
                .Where(u => u.Name.Contains(searchString, StringComparison.CurrentCultureIgnoreCase))
                .ToListAsync();
        }

        public Task<List<User>> GetUsersByPhone(string searchString)
        {
            return _applicationDbContext
                .Users
                .Where(u => u.Name.Contains(searchString))
                .ToListAsync();
        }

        public Task<List<User>> GetUsersByEmail(string searchString)
        {
            return _applicationDbContext
                .Users
                .Where(u => u.Email!.Contains(searchString, StringComparison.CurrentCultureIgnoreCase))
                .ToListAsync();
        }

        public Task<List<User>> GetUsersByPurchaseHistoryDescription(string searchString)
        {
            return _applicationDbContext
                .Users
                .Where(u => u.PurchaseHistory!.Any(ph => ph.Description.Contains(searchString, StringComparison.CurrentCultureIgnoreCase)))
                .ToListAsync();
        }

        public async Task<bool> DeleteUserById(int id)
        {
            var existingUser = await GetUserById(id);
            if (existingUser == null)
            {
                return false;
            }

            _applicationDbContext.Users.Remove(existingUser);
            await _applicationDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateUser(User updatedUser)
        {
            var existingUser = await GetUserById(updatedUser.Id);
            if (existingUser == null)
            {
                return false;
            }

            existingUser.Name = updatedUser.Name;
            existingUser.Phone = updatedUser.Phone;
            existingUser.Email = updatedUser.Email;
            await _applicationDbContext.SaveChangesAsync();
            return true;
        }
    }
}
