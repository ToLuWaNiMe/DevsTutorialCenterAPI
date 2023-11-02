using System.ComponentModel.DataAnnotations;

namespace DevsTutorialCenterAPI.Models.DTOs;

public class CreateTagDto
{
    [Required] 
    public string Name { get; set; }
}