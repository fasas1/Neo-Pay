using Microsoft.EntityFrameworkCore;
using NeoPay.Data;
using NeoPay.Entities;
using NeoPay.Repository.IRepository;

namespace NeoPay.Repository
{
    public class WalletRepository : IWalletRepository
    {
        private readonly ApplicationDbContext _db;

        public WalletRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<Wallet> AddAsync(Wallet entity)
        {
                 _db.Wallets.Add(entity);
                await _db.SaveChangesAsync();
                return entity;
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var wallet = await _db.Wallets.FindAsync(id);
            if (wallet == null)
            {
                return false;
            }
               _db.Wallets.Remove(wallet);
                await _db.SaveChangesAsync();
                return true;
        }
        public async Task<bool> ExistsAsync(Guid id)
        {
               return await _db.Wallets.AnyAsync(w => w.WalletId == id);
        }

        public async Task<IEnumerable<Wallet>> GetActiveWalletAsync()
        {
            return await _db.Wallets
                        .Where(w => w.IsActive)
                        .ToListAsync();
        }
        public async Task<IEnumerable<Wallet>> GetAllAsync()
        {
              return await _db.Wallets.ToListAsync();
        }
        public async Task<Wallet?> GetByIdAsync(Guid id)
        {
            return await _db.Wallets.FindAsync(id);
        }
        public async Task<Wallet> GetWalletByNameAsync(string userId,string name)
        {
            return await _db.Wallets
                .FirstOrDefaultAsync(w => w.UserId == userId && w.Name.ToLower() == name.ToLower());
        }
         public async Task<IEnumerable<Wallet>> GetWalletsByUserInfoAsync(string userId)
        {
              return await _db.Wallets
                         .Where (w => w.UserId == userId)
                          .Include (w => w.User)
                         .Include(w => w.TransactionsFrom)
                         .Include(w => w.TransactionsTo)
                         .ToListAsync();
        }
        public async Task<IEnumerable<Wallet>> GetWalletsByUserAsync(string userId)
        {
              return await _db.Wallets
                          .Where(w => w.UserId == userId)
                          .Include(w => w.TransactionsFrom)
                          .Include(w => w.TransactionsTo)
                          .ToListAsync();
        }
       
        public async Task<Wallet> UpdateAsync(Wallet entity)
        {
            _db.Wallets.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<Wallet> GetWalletByIdAsync(Guid walletId)
        {
            return await _db.Wallets
                         .Include(w => w.TransactionsFrom)
                         .Include(w => w.TransactionsTo)
                         .FirstOrDefaultAsync(w => w.WalletId == walletId);
        }

        public async Task<Wallet?> GetWalletByCurrencyAsync(string userId, string currency)
        {
             return await _db.Wallets
                          .FirstOrDefaultAsync(w => w.UserId == userId && w.Currency == currency && w.IsActive);
        }
    }
}
