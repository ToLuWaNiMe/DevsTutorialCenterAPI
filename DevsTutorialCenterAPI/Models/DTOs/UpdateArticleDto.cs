namespace DevsTutorialCenterAPI.Models.DTOs
{
    public class UpdateArticleDto
    {
        public string Title { get; set; }
        public string TagId { get; set; }
        public string Text { get; set; }
        public string ImageUrl { get; set; }
    }
}
