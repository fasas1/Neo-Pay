using AutoMapper;
using NeoPay.Entities;
using NeoPay.Entities.Dtos;
using NeoPay.Repository.IRepository;

namespace NeoPay.Services
{
    public class WalletService : IWalletService
    {
        private readonly IWalletRepository _walletRepo;
        private readonly IMapper _mapper;
        public WalletService(IWalletRepository walletRepo, IMapper mapper)
        {
            _walletRepo = walletRepo;
            _mapper = mapper;
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

        public async Task<IEnumerable<WalletDto>> GetAllWalletAsync()
        {
           var wallets = await _walletRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<WalletDto>>(wallets);
        }  
        public async Task<IEnumerable<WalletDto>> GetUserWalletsAsync(string userId)
        {
            var wallets = await _walletRepo.GetWalletsByUserAsync(userId);
            return _mapper.Map<IEnumerable<WalletDto>>(wallets);
        }

        public async Task<WalletDto?> GetWalletByCurrencyAsync(string userId, string currency)
        {
                var wallet = await _walletRepo.GetWalletByCurrencyAsync(userId, currency);
                    if(wallet == null)
                     {
                        return null;
                      }
                    return _mapper.Map<WalletDto>(wallet);
        }

        public async Task<WalletDto> GetWalletsByIdAsync(Guid walletId)
        {
            var wallet = await _walletRepo.GetWalletByIdAsync(walletId);
                 return _mapper.Map<WalletDto>(wallet);
        }
    }
}
