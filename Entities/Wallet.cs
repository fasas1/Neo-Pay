namespace NeoPay.Entities
{
    public class Wallet
    {
        public Guid WalletId { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public string Currency { get; set; } = "NGN";
        public DateTime CreatedAt { get; set; }
        public bool IsActive{ get; set; }

        public ICollection<Transaction> TransactionsFrom { get; set; } = new List<Transaction>();
        public ICollection<Transaction> TransactionsTo { get; set; } = new List<Transaction>();
    }
}
