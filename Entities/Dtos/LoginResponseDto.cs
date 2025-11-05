namespace NeoPay.Entities.Dtos
{
    public class LoginResponseDto
    {
        public string  UserName { get; set; }
        public string FullName { get; set; }
        public bool  IsSuccessful { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
    }
}
