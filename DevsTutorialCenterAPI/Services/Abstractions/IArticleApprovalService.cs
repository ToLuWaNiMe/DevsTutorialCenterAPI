using DevsTutorialCenterAPI.Data.Entities;

namespace DevsTutorialCenterAPI.Services.Abstractions
{
    public interface IArticleApprovalService
    {
        Task ApproveAsync(ArticleApproval articleApproval);
        Task <ArticleApproval> ArticleApprovalById(string articleid);
        Task<ArticleApproval> PublishedArticle(string articleId);

        Task<ArticleApproval> ReviewArticle(string articleId);
        Task<IEnumerable<ArticleApproval>> PendingArticles();
    }
}
