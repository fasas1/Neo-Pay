using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NeoPay.Entities;
using NeoPay.Entities.Dtos;
using NeoPay.IServices;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NeoPay.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IWalletService _walletService;
        private readonly string _secretKey;

        public UserService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            IWalletService walletService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _walletService = walletService;

            _secretKey = configuration.GetValue<string>("JwtSettings:Secret");

            if (string.IsNullOrEmpty(_secretKey))
                throw new Exception("JWT secret key is missing in configuration (JwtSettings:Secret).");
        }

        public async Task<IdentityResult> CreateUserAsync(RegistrationRequestDto model)
        {
            var existingUser = await _userManager.FindByEmailAsync(model.UserName);
            if (existingUser != null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "DuplicateUser",
                    Description = "User already exists."
                });
            }

            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.UserName, 
                FullName = model.Name
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // Assign default role
                await _userManager.AddToRoleAsync(user, "User");

                var currencies = new[] { "NGN", "USD", "GBP", "EUR" };

                foreach (var currency in currencies)
                {
                    var walletName = $"{currency} Wallet";
                    await _walletService.CreateWallet(user.Id, walletName, currency);
                }
            }

            return result;
        }

        public async Task<ApplicationUser> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.UserName);

            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                return new LoginResponseDto
                {
                    IsSuccessful = false,
                    Message = "Invalid Username or Password"
                };
            }

            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault() ?? "User";

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id), // Standard user identifier
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Email, user.Email ?? ""),
                    new Claim(ClaimTypes.Role, role),
                    new Claim("FullName", user.FullName ?? "")
                };


            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddDays(5),
                signingCredentials: creds
            );

            return new LoginResponseDto
            {
                IsSuccessful = true,
                Message = "Login successful",
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Role = role,
                FullName = user.FullName
            };
        }

        public async Task<IdentityResult> AssignRoleAsync(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return await _userManager.AddToRoleAsync(user, roleName);
        }
    }
}
