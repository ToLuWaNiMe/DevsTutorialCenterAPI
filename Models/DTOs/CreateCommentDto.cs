namespace DevsTutorialCenterAPI.Models.DTOs;

public class CreateCommentDto
{
    public string Text { get; set; }
    public string UserId { get; set; }
    public string ArticleId { get; set; }
}