using Microsoft.EntityFrameworkCore;
using NeoPay.Data;
using NeoPay.Entities;
using NeoPay.Repository.IRepository;


namespace NeoPay.Repository
{
    public class TransactionRepository: ITransactionRepository
    {
        private readonly ApplicationDbContext _db;

        public TransactionRepository(ApplicationDbContext db)
        {
            _db = db;  
        }

        public async Task<Transaction> AddAsync(Transaction entity)
        {
           await _db.Transactions.AddAsync(entity);
            await _db.SaveChangesAsync();
            return entity;
        } 

        public async Task<bool> DeleteAsync(Guid id)
        {
            var trnx = await _db.Transactions.FindAsync(id);
            if (trnx == null) return false;

            _db.Transactions.Remove(trnx);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _db.Transactions.AnyAsync(c => c.TransactionId == id);
        }
       public async Task<IEnumerable<Transaction>> GetAllAsync()
        {
            return await _db.Transactions.ToListAsync();
        }
        public async Task<Transaction?> GetByIdAsync(Guid id)
        {
            return await _db.Transactions.FindAsync(id);
        }
        public async Task<IEnumerable<Transaction>> GetTransactionFromWalletAsync(Guid walletId)
        {
             return await _db.Transactions
                   .Include(t => t.FromWallet)
                    .Where(t => t.FromWallet.WalletId == walletId)
                   .ToListAsync();
        }
        public async Task<IEnumerable<Transaction>> GetTransactionToWalletAsync(Guid walletId)
        {
            return await _db.Transactions
                    .Include(t => t.ToWallet)
                    .Where(t  => t.ToWallet.WalletId == walletId)
                    .ToListAsync();
        }
        public async Task<Transaction> GetTransactionWithDetailsAsync(Guid transactionId)
        {
             return await _db.Transactions
                          .Include(t => t.FromWallet)
                                .ThenInclude(w => w.User)
                          .Include(t => t.ToWallet)
                                .ThenInclude(w => w.User)
                          .FirstOrDefaultAsync(t => t.TransactionId == transactionId);
        }
        public async Task<Transaction> UpdateAsync(Transaction entity)
        {
            _db.Transactions.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }    
    }
}
