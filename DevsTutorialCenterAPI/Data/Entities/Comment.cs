namespace DevsTutorialCenterAPI.Data.Entities;

public class Comment : BaseEntity
{
    public string Text { get; set; }
    public string UserId { get; set; }
    public string ArticleId { get; set; }

    public AppUser User { get; set; }
    public Article Article { get; set; }
}