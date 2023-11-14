namespace DevsTutorialCenterAPI.Models.DTOs;

public class GetCommentDto
{
    public string Id { get; set; }
    public string Text { get; set; }

    public string ArticleId { get; set; }

    public string UserId { get; set; }

}