using NeoPay.Enums;
using System.Transactions;
using TransactionStatus = NeoPay.Enums.TransactionStatus;

namespace NeoPay.Entities
{
    public class Transaction
    {
        public Guid TransactionId { get; set; }
        public Wallet FromWallet { get; set; }
        public Wallet ToWallet { get; set; }
        public TransactionType Type { get; set; }                                                                                                                                                
        public TransactionStatus Status { get; set; } = TransactionStatus.Pending;
        public decimal  Amount { get; set; }   
        public string  Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? CompletedAt { get; set; }

    }
}
