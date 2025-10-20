using NeoPay.Entities;

namespace NeoPay.Repository.IRepository
{
    public interface IWalletRepository :IRepository<Wallet>
    {
        Task<Wallet> GetWalletByNameAsync(string name);
        Task<IEnumerable<Wallet>> GetActiveWalletAsync();
        Task<IEnumerable<Wallet>> GetWalletsByUserAsync(string userId);


    }
}
