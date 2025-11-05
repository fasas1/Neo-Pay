using AutoMapper;
using NeoPay.Entities;
using NeoPay.Entities.Dtos;

namespace NeoPay
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Transaction, TransactionDto>().ReverseMap();
            CreateMap<Wallet, WalletDto>().ReverseMap();
            CreateMap<Wallet, CreateWalletDto>().ReverseMap();
            CreateMap<ApplicationUser, RegistrationRequestDto>().ReverseMap();
        }
    }
}
