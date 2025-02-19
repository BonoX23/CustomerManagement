namespace Domain.DTOs
{
    public class AuthResponseDto
    {
        public string Token { get; set; }
        public string UserName { get; set; }
        public int ExpireIn { get; set; }
    }
}
