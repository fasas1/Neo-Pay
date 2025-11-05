using Microsoft.AspNetCore.Identity;
using NeoPay.Entities;
using NeoPay.Entities.Dtos;

namespace NeoPay.IServices
{
    public interface IUserService
    {
        Task<IdentityResult> CreateUserAsync(RegistrationRequestDto model);
        Task<LoginResponseDto> LoginAsync(LoginRequestDto model);
        Task<ApplicationUser> GetUserByEmailAsync(string email);
        Task<IdentityResult> AssignRoleAsync(string userId, string roleName);
    }
}
