using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Models.DTOs;

namespace DevsTutorialCenterAPI.Services.Abstractions;

public interface IArticleService
{
    Task<PaginatorResponseDto<IEnumerable<GetAllArticlesDto>>> GetAllArticles(FilterArticleDto filters);
    Task<GetSingleArticleDto> GetSingleArticle(string articleId, string userId);
    Task<CreateArticleDto> CreateArticleAsync(CreateArticleDto model);
    Task<Article> GetArticleById(string articleId);
    Task<UpdateArticleDto> UpdateArticleAsync(string articleId, UpdateArticleDto updatedArticle);
    Task<bool> SetArticleReportStatus(string articleId, string status);
    Task<bool> DeleteArticleAsync(string articleId);
    Task<List<LikesByArticleDto>> GetLikesByArticleAsync(string articleId);
    Task<AuthorsStatsDto> GetAuthorStatsAsync(FetchAuthorsStatsDto fetchAuthorsStatsDto);
    Task LogArticleReadAsync(string articleId, string userId);
}