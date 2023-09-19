using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Models.DTOs;

namespace DevsTutorialCenterAPI.Services.Abstractions
{
    public interface IArticleService
    {
        Task<PaginatorResponseDto<IEnumerable<GetAllArticlesDto>>> GetAllArticles(FilterArticleDto filters);
        Task<GetSingleArticleDto> GetSingleArticle(string articleId);
        Task<CreateArticleDto> CreateArticleAsync(CreateArticleDto model);
    }
}
