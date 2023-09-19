namespace DevsTutorialCenterAPI.Data.Entities
{
    public class ReportArticle : BaseEntity
    {
       
        public string ArticleId { get; set; }
        public string ReportedBy { get; set; }
        public string ReportText{ get; set; }
       
           
          
    }
}
