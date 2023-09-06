using BuyLogTracker.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace BuyLogTracker.Api.Data.Repositories
{
    public class PurchaseHistoryRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public PurchaseHistoryRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<PurchaseHistory?> CreatePurchaseHistory(PurchaseHistory purchaseHistory)
        {
            try
            {
                var entityEntry = await _applicationDbContext.PurchaseHistories.AddAsync(purchaseHistory);
                await _applicationDbContext.SaveChangesAsync();
                return entityEntry.Entity;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<PurchaseHistory?> GetPurchaseHistoryById(int id)
        {
            return await _applicationDbContext
                .PurchaseHistories
                .FirstOrDefaultAsync(ph => ph.Id == id);
        }

        public async Task<bool> UpdatePurchaseHistory(PurchaseHistory updatedPurchaseHistory)
        {
            var existingPurchaseHistory = await GetPurchaseHistoryById(updatedPurchaseHistory.Id);
            if (existingPurchaseHistory == null)
            {
                return false;
            }

            existingPurchaseHistory.Description = updatedPurchaseHistory.Description;

            try
            {
                await _applicationDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeletePurchaseHistoryById(int id)
        {
            var existingPurchaseHistory = await GetPurchaseHistoryById(id);
            if (existingPurchaseHistory == null)
            {
                return false;
            }

            _applicationDbContext.PurchaseHistories.Remove(existingPurchaseHistory);
            await _applicationDbContext.SaveChangesAsync();
            return true;
        }
    }
}
