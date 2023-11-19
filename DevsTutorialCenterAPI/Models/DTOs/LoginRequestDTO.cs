using System.ComponentModel.DataAnnotations;

namespace DevsTutorialCenterAPI.Models.DTOs
{
    public class LoginRequestDTO
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
