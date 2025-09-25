using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NeoPay.Entities;


namespace NeoPay.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions option) : base(option) { }

        public DbSet<ApplicationUser>  ApplicationUser { get; set; }
    }
}
