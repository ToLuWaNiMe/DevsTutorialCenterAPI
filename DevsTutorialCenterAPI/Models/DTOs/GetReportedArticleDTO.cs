namespace DevsTutorialCenterAPI.Models.DTOs
{
    public class GetReportedArticleDTO
    {
        public string ArticleId { get; set; }
        public string ArticleTitle { get; set; }

        public string ArticleText { get; set; }

        public string Author { get; set; }

        public int NoOfLikes { get; set; }

        public int NoOfComments { get; set; }

        public DateTime PublishedOn { get; set; }
    }
}
