using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Data.Repositories;
using DevsTutorialCenterAPI.Services.Abstractions;

namespace DevsTutorialCenterAPI.Services.Implementations
{
    public class ArticleApprovalService : IArticleApprovalService
    {
        private readonly IRepository _repository;

        public ArticleApprovalService(IRepository repository)
        {
            _repository = repository;
        }
        public async Task ApproveAsync(ArticleApproval articleApproval)
        {
             await _repository.AddAsync<ArticleApproval>(articleApproval);
        }
    }
}
