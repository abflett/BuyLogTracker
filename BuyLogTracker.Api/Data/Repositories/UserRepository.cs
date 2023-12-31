﻿using BuyLogTracker.Api.Models;
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

        public async Task<List<User>> Users ()
        {
            return await _applicationDbContext.Users.Include(u => u.PurchaseHistory).ToListAsync();
        }

        public async Task<User?> GetExistingUser(string name, string phoneNumber)
        {
            return await _applicationDbContext.Users.FirstOrDefaultAsync(u => u.Name == name && u.Phone == phoneNumber);
        }

        public async Task<User?> CreateUser(User user)
        {
            User? newUser = null;
            try
            {
                var entityEntry = await _applicationDbContext.Users.AddAsync(user);
                newUser = entityEntry.Entity;
            }
            catch (Exception)
            {
                return newUser;
            }
            await _applicationDbContext.SaveChangesAsync();
            return newUser;
        }

        public async Task<User?> GetUserById(int id)
        {
            return await _applicationDbContext
                .Users
                .Include(u => u.PurchaseHistory)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public Task<List<User>> GetUsersByName(string searchString) 
        {
            return _applicationDbContext
                .Users
                .Where(u => u.Name.ToUpper().Contains(searchString.ToUpper()))
                .ToListAsync();
        }

        public Task<List<User>> GetUsersByPhone(string searchString)
        {
            return _applicationDbContext
                .Users
                .Where(u => u.Phone.Contains(searchString))
                .ToListAsync();
        }

        public Task<List<User>> GetUsersByEmail(string searchString)
        {
            return _applicationDbContext
                .Users
                .Where(u => u.Email!.ToUpper().Contains(searchString.ToUpper()))
                .ToListAsync();
        }

        public Task<List<User>> GetUsersByPurchaseHistoryDescription(string searchString)
        {
            return _applicationDbContext
                .Users
                .Where(u => u.PurchaseHistory!.Any(ph => ph.Description.ToUpper().Contains(searchString.ToUpper())))
                .ToListAsync();
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

            try
            {
                await _applicationDbContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                return false;
            }
            return true;
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
    }
}
