using DevsTutorialCenterAPI.Models.DTOs;

namespace DevsTutorialCenterAPI.Services.Abstractions
{
    public interface IArticleService
    {
        Task<GetArticleDto> GetSingleArticle(string articleId);
    }
}
