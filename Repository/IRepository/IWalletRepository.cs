using NeoPay.Entities;

namespace NeoPay.Repository.IRepository
{
    public interface IWalletRepository :IRepository<Wallet>
    {
        Task<Wallet> GetWalletByNameAsync(string userId,string name);
        Task<IEnumerable<Wallet>> GetActiveWalletAsync();
        Task<IEnumerable<Wallet>> GetWalletsByUserAsync(string userId);
        Task<IEnumerable<Wallet>> GetWalletsByUserInfoAsync(string userId);
        Task <Wallet> GetWalletByIdAsync(Guid walletId);
        Task<Wallet?> GetWalletByCurrencyAsync(string userId, string currency);


    }
}
