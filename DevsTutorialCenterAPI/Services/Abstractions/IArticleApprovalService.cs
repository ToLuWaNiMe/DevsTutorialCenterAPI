using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Models.DTOs;

namespace DevsTutorialCenterAPI.Services.Abstractions
{
    public interface IArticleApprovalService
    {
        Task ApproveAsync(ArticleApproval articleApproval);
        Task <ArticleApproval> ArticleApprovalById(string articleid);
       // Task<ArticleApproval> ApprovalArticleById(string articleId);
        Task<ArticleApproval> PublishedArticle(string articleId);

        Task<ArticleApproval> ReviewArticle(string articleId);
        Task<ArticleApproval> RejectArticle(string articleId);

        Task<List<GetSingleArticleDto>> FetchArticleStatusListAsync(int number);
    }
}
