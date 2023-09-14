﻿using DevsTutorialCenterAPI.Models.DTOs;

namespace DevsTutorialCenterAPI.Services.Abstractions
{
    public interface IArticleService
    {
        Task<GetAllArticlesDto> GetSingleArticle(string articleId);

        Task<bool> SetArticleReportStatus(string articleId, string status);
    }
}
