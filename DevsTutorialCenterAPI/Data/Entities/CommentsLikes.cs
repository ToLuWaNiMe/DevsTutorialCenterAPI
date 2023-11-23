namespace DevsTutorialCenterAPI.Data.Entities;

public class CommentLike : BaseEntity
{
    public string UserId { get; set; }
    public string CommentId { get; set; }
    
    public AppUser User { get; set; }
    public Comment Comment { get; set; }
}