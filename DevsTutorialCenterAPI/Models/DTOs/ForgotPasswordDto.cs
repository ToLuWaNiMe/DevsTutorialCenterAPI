using System.ComponentModel.DataAnnotations;

namespace DevsTutorialCenterAPI.Models.DTOs
{
    public class ForgotPasswordDto
    {
        [Required(ErrorMessage = "Email is required!")]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}
