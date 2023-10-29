namespace DevsTutorialCenterAPI.Data.Entities;

public class ArticleTag : BaseEntity
{
    public string Name { get; set; }

    public DateTime DeletedAt { get; set; }
}