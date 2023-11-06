namespace DevsTutorialCenterAPI.Models.DTOs
{
    public class LoginResponseDTO
    {
        public AppUserDTO User { get; set; }
        public string Token { get; set; }
    }
}
