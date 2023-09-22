﻿namespace DevsTutorialCenterAPI.Data.Entities;

public class Article : BaseEntity
{
    public string Title { get; set; }
    public string Tag { get; set; }
    public string Text { get; set; }
    public string ImageUrl { get; set; }
    public string PublicId { get; set; }
    public bool IsRecommended { get; set; }
    public bool IsTrending { get; set; }
    public bool IsSaved { get; set; }
    public bool IsRead { get; set; }
    public bool IsReported { get; set; }
    public bool IsPublished { get; set; }
    public string UserId { get; set; }
}