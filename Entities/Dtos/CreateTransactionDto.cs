using NeoPay.Enums;

namespace NeoPay.Entities.Dtos
{
    public class CreateTransactionDto
    {
        public Wallet FromWallet { get; set; }
        public Wallet ToWallet { get; set; }
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
   

    }
}
