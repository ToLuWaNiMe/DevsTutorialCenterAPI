namespace DevsTutorialCenterAPI.Models.DTOs
{
    public class FilterArticleDto
    {
        public string ArticleId { get; set; }
        public string Tag { get; set; }
        public string Page { get; set; }
        public string Size { get; set; }
        public bool IsRecommended { get; set; }
        public bool IsSaved { get; set; }
        public bool IsRead { get; set; }
        public bool IsReported { get; set; }
        public bool IsPublished { get; set; }

    }
}
