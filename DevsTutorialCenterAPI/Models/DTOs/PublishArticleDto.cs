namespace DevsTutorialCenterAPI.Models.DTOs
{
    public class PublishArticleDto
    {
        public string ArticleId { get; set; }
        public DateTime PublishedOn { get; set; }
        public bool IsPublished { get; set; }
    }
}
