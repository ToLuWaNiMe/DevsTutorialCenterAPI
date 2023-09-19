namespace DevsTutorialCenterAPI.Models.DTOs
{
    public class FilterArticleDto
    {
        public string? AuthorId { get; set; }
        public string? Tag { get; set; }
        public int? Page { get; set; }
        public int? Size { get; set; }
        public bool IsRecommended { get; set; }
        public bool IsSaved { get; set; }
        public bool IsRead { get; set; }
        public bool IsReported { get; set; }
        public bool IsPublished { get; set; }

    }
}
