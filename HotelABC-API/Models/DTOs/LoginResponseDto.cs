namespace HotelABC_API.Models.DTOs
{
    public class LoginResponseDto
    {
        public string Token { get; set; }
        public string? NameUser { get; set; }
        public List<string> Roles { get; set; }
    }
}
