namespace DevsTutorialCenterAPI.Models.DTOs
{
    public class GetReportedAuthorsDTO
    {
        public string Name { get; set; }

        public string Stack { get; set; }

        public int NoOfArticles { get; set; }

        public int NoOfReports { get; set; }
        public string? ImageUrl { get;  set; }
    }
}
