using DevsTutorialCenterAPI.Data.Entities;

namespace DevsTutorialCenterAPI.Services.Abstractions
{
    public interface IArticleApprovalService
    {
        Task ApproveAsync(ArticleApproval articleApproval);
    }
}
