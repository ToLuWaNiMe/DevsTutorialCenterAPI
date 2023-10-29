namespace DevsTutorialCenterAPI.Data.Entities;

public class ArticleApproval : BaseEntity
{
    public string ApprovalId { get; set; }
    public string ArticleId { get; set; }

    public int Status{ get; set; }
}