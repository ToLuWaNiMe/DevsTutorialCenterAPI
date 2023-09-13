using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Models.DTOs;

namespace DevsTutorialCenterAPI.Services.Abstractions
{
    public interface IArticleService
    {
        Task<GetAllArticlesDto> GetSingleArticle(string articleId);
        Task<bool> DeleteArticleAsync(Article article);
        Task<Article> GetArticleById(string articleId);
    }
}
