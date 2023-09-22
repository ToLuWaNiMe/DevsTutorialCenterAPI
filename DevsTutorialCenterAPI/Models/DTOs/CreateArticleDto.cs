using System.ComponentModel.DataAnnotations;

namespace DevsTutorialCenterAPI.Models.DTOs;

public class CreateArticleDto
{
    [Required] public string Title { get; set; }

    [Required] public string Tag { get; set; }

    [Required] public string Text { get; set; }

    [Required] public bool IsTrending { get; set; }

    [Required] public bool IsRecommended { get; set; }

    [Required] public string UserId { get; set; }
}