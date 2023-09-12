using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Data.Entities;

namespace DevsTutorialCenterAPI.Services.Abstractions
{
    public interface IArticleService
    {
      

        Task<Article> GetArticleById(string articleId);

        Task UpdateArticleAsync(Article article);
        
        Task<GetAllArticlesDto> GetSingleArticle(string articleId);

    }
}
