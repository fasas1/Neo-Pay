using Microsoft.EntityFrameworkCore;

namespace NeoPay.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions option) : base(option) { }
    }
}
