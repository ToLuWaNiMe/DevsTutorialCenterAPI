using System.ComponentModel.DataAnnotations;

namespace DevsTutorialCenterAPI.Models.DTOs;

public class RegistrationRequestDTO
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string PhoneNumber { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public  string Stack { get; set; }
    [Required]
    public string Squad { get; set; }
}