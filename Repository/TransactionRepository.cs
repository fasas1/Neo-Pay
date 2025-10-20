using NeoPay.Data;
using NeoPay.Repository.IRepository;
using System.Transactions;

namespace NeoPay.Repository
{
    public class TransactionRepository: ITransactionRepository
    {
        private readonly ApplicationDbContext _db;

        public TransactionRepository(ApplicationDbContext db)
        {
            _db = db;  
        }

        public Task<Transaction> AddAsync(Transaction entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Transaction>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Transaction?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Transaction>> GetTransactionFromWalletAsync(Guid walletId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Transaction>> GetTransactionToWalletAsync(Guid walletId)
        {
            throw new NotImplementedException();
        }

        public Task<Transaction> GetTransactionWithDetailsAsync(Guid transactionId)
        {
            throw new NotImplementedException();
        }

        public Task<Transaction> UpdateAsync(Transaction entity)
        {
            throw new NotImplementedException();
        }
    }
}
