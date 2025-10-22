using NeoPay.Entities.Dtos;
using NeoPay.Repository.IRepository;

namespace NeoPay.Services
{
    public class WalletService : IWalletService
    {
        private readonly IWalletRepository _walletRepo;
        public WalletService(IWalletRepository walletRepo)
        {
            _walletRepo = walletRepo;
        }
        public async Task<WalletDto> CreateWallet(string userId, string walletName, string currency = "NGN")
        {
            var wallet = new Wallet
            {
                UserId = userId,
                Currency = currency,
                Name = walletName,
                Balance = 0,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };
             var newWallet = await _walletRepo.AddAsync(wallet);
            return _mapper.Map<WalletDto>(newWallet);
        }

        public Task<IEnumerable<WalletDto>> GetAllWalletAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<WalletDto>> GetUserWalletsAsync(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
