using DevsTutorialCenterAPI.Data.Entities;

namespace DevsTutorialCenterAPI.Services.Abstractions
{
    public interface IArticleService
    {
        Task<Article> GetArticleByPublicId(string publicId);

        Task<Article> GetArticleById(string articleId);

        Task UpdateArticleAsync(Article article);
    }
}