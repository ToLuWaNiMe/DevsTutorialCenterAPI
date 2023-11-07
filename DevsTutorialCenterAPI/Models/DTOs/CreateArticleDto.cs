using System.ComponentModel.DataAnnotations;

namespace DevsTutorialCenterAPI.Models.DTOs;

public class CreateArticleDto
{
    [Required] public string Title { get; set; }
    [Required] public string TagId { get; set; }
    [Required] public string Text { get; set; }
    [Required] public string ImageUrl { get; set; }
}