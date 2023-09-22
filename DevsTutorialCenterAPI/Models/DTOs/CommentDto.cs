﻿namespace DevsTutorialCenterAPI.Models.DTOs;

public class CommentDto
{
    public string Id { get; set; }
    public string Text { get; set; }
    public string UserId { get; set; }
    public string ArticleId { get; set; }
    public DateTime CreatedOn { get; set; }
}