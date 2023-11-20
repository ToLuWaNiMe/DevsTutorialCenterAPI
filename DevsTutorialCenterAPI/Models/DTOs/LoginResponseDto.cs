namespace DevsTutorialCenterAPI.Models.DTOs
{
    public class LoginResponseDto
    {
        public AppUserDto User { get; set; }
        public string Token { get; set; }
    }
}
