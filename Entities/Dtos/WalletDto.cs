namespace NeoPay.Entities.Dtos
{
    public class WalletDto
    {
        public Guid WalletId { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public string Currency { get; set; } = "NGN";
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
    }
}
