namespace DevsTutorialCenterAPI.Data.Entities;

public class ReportedArticle : BaseEntity
{
    public string ArticleId { get; set; }
    public string ReportedBy { get; set; }
    public string ReportText { get; set; }
    public Article Article { get; set; }
}