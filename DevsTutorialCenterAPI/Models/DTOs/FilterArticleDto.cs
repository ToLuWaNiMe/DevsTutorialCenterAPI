namespace DevsTutorialCenterAPI.Models.DTOs
{
    public class FilterArticleDto
    {
        public string? AuthorId { get; set; }
        public int? Page { get; set; }
        public int? Size { get; set; }
        public bool IsDeleted { get; set; }
        public bool? IsRecentlyAdded { get; set; }
        public bool? IsTopRead { get; set; }
        public string? TagId { get; set; }
    }
}
