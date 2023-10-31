namespace DevsTutorialCenterAPI.Data.Entities;

public class ArticleBookMark : BaseEntity
{
    public string UserId { get; set; }

    public Article Article { get; set; }
    public string ArticleId { get; set; }
}