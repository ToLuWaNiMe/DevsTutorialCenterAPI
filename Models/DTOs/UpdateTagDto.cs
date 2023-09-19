using System.ComponentModel.DataAnnotations;

namespace DevsTutorialCenterAPI.Models.DTOs;

public class UpdateTagDto
{
    [Required] public string Name { get; set; }
}