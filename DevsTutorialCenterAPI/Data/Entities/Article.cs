using DevsTutorialCenterAPI.Utilities;
using Humanizer;

namespace DevsTutorialCenterAPI.Data.Entities;

public class Article : BaseEntity
{
    public string Title { get; set; }
    public string Tag { get; set; }
    public string Text { get; set; }
    public string ImageUrl { get; set; }
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
    public DateTime? DeletedAt { get; set; }
    public int ReadTime { get; set; }

    public string UserId { get; set; }

    public string GetFormattedCreatedOn()
    {
        return CreatedOn.Humanize();
    }
    public string GetFormattedPublishedOn()
    {
        return PublishedOn.Humanize();
    }


}