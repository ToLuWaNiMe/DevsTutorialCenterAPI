﻿using Humanizer;

namespace DevsTutorialCenterAPI.Models.DTOs;

public class GetAllArticlesDto
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Tag { get; set; }
    public string Text { get; set; }
    public string ImageUrl { get; set; }
    public string UserId { get; set; }
    public DateTime PublishedOn { get; set; }
    public int ReadTime { get; set; }

    public DateTime CreatedOn { get; set; } = DateTime.UtcNow.AddDays(-2);


   
}