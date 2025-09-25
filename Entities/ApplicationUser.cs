using Microsoft.AspNetCore.Identity;

namespace NeoPay.Entities
{
    public class ApplicationUser :IdentityUser
    {
        public string FullName { get; set; }
        public Wallet Wallet { get; set; }
    }
}
