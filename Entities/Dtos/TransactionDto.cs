using NeoPay.Enums;

namespace NeoPay.Entities.Dtos
{
    public class TransactionDto
    {
        public Guid TransactionId { get; set; }
        //public int? FromWalletId { get; set; }
        //public Wallet FromWallet { get; set; }

        //public int? ToWalletId { get; set; }
        //public Wallet ToWallet { get; set; }
        public TransactionType Type { get; set; }
        public TransactionStatus Status { get; set; } = TransactionStatus.Pending;

        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? CompletedAt { get; set; }
    }
}
