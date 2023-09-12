using DevsTutorialCenterAPI.Models.DTOs;

namespace DevsTutorialCenterAPI.Services.Abstractions
{
    public interface IArticleService
    {
        Task<IEnumerable<GetAllArticlesDto>> GetAllArticles(FilterArticleDto filters);
    }
}
