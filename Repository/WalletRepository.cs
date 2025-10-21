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
               return await _db.Wallets.FindAsync(w => w.WalletId == id);
        }

        public Task<IEnumerable<Wallet>> GetActiveWalletAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Wallet>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Wallet?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Wallet> GetWalletByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Wallet>> GetWalletsByUserAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<Wallet> UpdateAsync(Wallet entity)
        {
            throw new NotImplementedException();
        }
    }
}
