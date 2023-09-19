namespace DevsTutorialCenterAPI.Data.Entities;

public class ArticlesLikes : BaseEntity
{
    public string UserId { get; set; }
    public string ArticleId { get; set; }
}