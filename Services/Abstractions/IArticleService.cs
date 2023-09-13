using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Models.DTOs;

namespace DevsTutorialCenterAPI.Services.Abstractions
{
    public interface IArticleService
    {
        Task<CreateArticleDto> CreateArticleAsync(CreateArticleDto model);
        Task<GetAllArticlesDto> GetSingleArticle(string articleId);
    }
}
