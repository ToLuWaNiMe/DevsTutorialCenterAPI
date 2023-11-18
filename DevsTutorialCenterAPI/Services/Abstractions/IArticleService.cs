using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Models.DTOs;

namespace DevsTutorialCenterAPI.Services.Abstractions;

public interface IArticleService
{
   Task<PaginatorResponseDto<IEnumerable<GetAllArticlesDto>>> GetAllArticles(FilterArticleDto filters);
    Task<CreateArticleDtoReturn> CreateArticleAsync2(CreateArticleDto2 model);
    Task<IEnumerable<GetAllArticlesDto>> GetBookmarkedArticles(string userId);
    Task<GetSingleArticleDto> GetSingleArticle(string articleId, string userId);
    Task<CreateArticleDto> CreateArticleAsync(CreateArticleDto model);
    Task<Article> GetArticleById(string articleId);
    Task<UpdateArticleDto> UpdateArticleAsync(string articleId, UpdateArticleDto updatedArticle);
    Task<bool> SetArticleReportStatus(string articleId, string status);
    Task<bool> DeleteArticleAsync(string articleId);
    Task<List<LikesByArticleDto>> GetLikesByArticleAsync(string articleId);
    Task<AuthorsStatsDto> GetAuthorStatsAsync(FetchAuthorsStatsDto fetchAuthorsStatsDto);
    Task LogArticleReadAsync(string articleId, string userId);
    Task<IEnumerable<Article>> GetAllArticle();

    
    Task<bool> IsArticleBookmarkedByUser(string articleId, string userId);


    Task<List<GetPendingArticlesDto>> GetPendingArticles();

    Task<object> SoftDeleteArticle(string articleId);

    Task<List<AuthorDTO>> GetAuthorStats();
}