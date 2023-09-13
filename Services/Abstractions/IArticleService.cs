using DevsTutorialCenterAPI.Models.DTOs;

namespace DevsTutorialCenterAPI.Services.Abstractions
{
    public interface IArticleService
    {
        Task<IEnumerable<GetAllArticlesDto>> GetAllArticles();
        Task<GetAllArticlesDto> GetSingleArticle(string articleId);
        Task<PaginatorResponseDto<IEnumerable<GetAllArticlesDto>>> GetAllArticles(FilterArticleDto filters);
    }
}
