using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NeoPay.Entities;


namespace NeoPay.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions option) : base(option) { }

        public DbSet<ApplicationUser>  ApplicationUser { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Wallet> Wallets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Wallet <-> User (One-to-Many)
            modelBuilder.Entity<Wallet>()
                .HasOne(w => w.User)
                .WithMany(u => u.Wallets)
                .HasForeignKey(w => w.UserId);

            modelBuilder.Entity<Wallet>()
                .HasIndex(w => new { w.UserId, w.Currency })
                .IsUnique();

            // Transaction <-> Wallet (FromWallet)
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.FromWallet)
                .WithMany(w => w.TransactionsFrom)
                .HasForeignKey("FromWalletId")
                .OnDelete(DeleteBehavior.Restrict);

            // Transaction <-> Wallet (ToWallet)
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.ToWallet)
                .WithMany(w => w.TransactionsTo)
                .HasForeignKey("ToWalletId")
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
