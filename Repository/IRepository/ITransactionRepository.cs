

using NeoPay.Entities;

namespace NeoPay.Repository.IRepository
{
    public interface ITransactionRepository:IRepository<Transaction>
    {
      
        Task<Transaction> GetTransactionWithDetailsAsync(Guid transactionId);
        Task<IEnumerable<Transaction>> GetTransactionFromWalletAsync(Guid walletId);
        Task<IEnumerable<Transaction>> GetTransactionToWalletAsync(Guid walletId);
    }
}
