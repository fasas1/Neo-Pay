using NeoPay.Entities.Dtos;

namespace NeoPay.Services
{
    public interface IWalletService
    {
        Task <IEnumerable<WalletDto>> GetAllWalletAsync();
        Task <WalletDto> CreateWallet(string userId, string walletName, string currency = "NGN");
        Task <IEnumerable<WalletDto>> GetUserWalletsAsync(string userId);
    }
}
