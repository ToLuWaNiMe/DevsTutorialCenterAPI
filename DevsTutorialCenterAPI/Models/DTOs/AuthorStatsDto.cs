namespace DevsTutorialCenterAPI.Models.DTOs
{
    public class AuthorStatsDto
    {
        public string AuthorId { get; set; }
        public int TotalNumOfArticles { get; set; }
        public int TotalReportedArticles { get; set; }
      
    }

    public class AuthorsStatsDto
    {
        public IEnumerable<AuthorStatsDto> AuthorStatsDtos { get; set; }
    }
}
