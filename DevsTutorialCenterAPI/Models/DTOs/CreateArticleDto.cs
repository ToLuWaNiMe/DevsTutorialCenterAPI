using System.ComponentModel.DataAnnotations;

namespace DevsTutorialCenterAPI.Models.DTOs;

public class CreateArticleDto
{
    [Required] public string Title { get; set; }
    [Required] public string Tag { get; set; }
    [Required] public string Text { get; set; }
    [Required] public string ImageUrl { get; set; }
    public string PublicId { get; set; }
    public bool IsRecommended { get; set; } = false;
    public bool IsTrending { get; set; } = false;
    public bool IsPending { get; set; } = false;
    public bool IsDraft { get; set; } = false;
    public bool IsSaved { get; set; } = false;
    public bool IsRead { get; set; } = false;
    public bool IsReported { get; set; } = false;
    public bool IsPublished { get; set; } = false;
    public DateTime PublishedOn { get; set; }
    public DateTime CreatedOn { get; set; }
    public string UserId { get; set; }
}