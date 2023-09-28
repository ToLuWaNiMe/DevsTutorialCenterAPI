using System.ComponentModel.DataAnnotations;

namespace DevsTutorialCenterAPI.Models.DTOs;

public class CreateArticleDto
{
    [Required] public string Title { get; set; }
    [Required] public string Tag { get; set; }
    [Required] public string Text { get; set; }
    [Required] public string ImageUrl { get; set; }
    public string PublicId { get; set; }
    public bool IsRecommended { get; set; } 
    public bool IsTrending { get; set; } 
    public bool IsPending { get; set; } 
    public bool IsDraft { get; set; } 
    public bool IsSaved { get; set; } 
    public bool IsRead { get; set; } 
    public bool IsReported { get; set; } 
    public bool IsPublished { get; set; } 
    public DateTime PublishedOn { get; set; }
    public DateTime CreatedOn { get; set; }
    public int ReadTime { get; set; }
    public string UserId { get; set; }
}