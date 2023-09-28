using Humanizer;

namespace DevsTutorialCenterAPI.Models.DTOs;

public class GetSingleArticleDto
{
    public string Id { get; set; }
    public string PublicId { get; set; }
    public string Title { get; set; }
    public string Tag { get; set; }
    public string Text { get; set; }
    public string ImageUrl { get; set; }
    public bool IsPublished { get; set; }
    public bool IsRead { get; set; }
    public bool IsRecommended { get; set; }
    public bool IsReported { get; set; }
    public bool IsSaved { get; set; }
    public string UserId { get; set; }
    public int ReadTime { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow.AddDays(-2);


    public string GetFormattedCreatedOn()
    {
        return CreatedOn.Humanize();
    }
}