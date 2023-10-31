namespace DevsTutorialCenterAPI.Data.Entities;

public class ArticleRead : BaseEntity
{
    public string UserId { get; set; }

    public Article Article { get; set; }
    public string ArticleId { get; set; }
}