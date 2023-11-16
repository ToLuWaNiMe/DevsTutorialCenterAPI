using DevsTutorialCenterAPI.Data;
using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Data.Repositories;
using DevsTutorialCenterAPI.Models.Enums;
using DevsTutorialCenterAPI.Services.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace DevsTutorialCenterAPI.Services.Implementations
{
    public class ArticleApprovalService : IArticleApprovalService
    {
        private readonly IRepository _repository;
        private readonly DevsTutorialCenterAPIContext _devsTutorialCenterAPIContext;

        public ArticleApprovalService(IRepository repository, DevsTutorialCenterAPIContext devsTutorialCenterAPIContext)
        {
            _repository = repository;
            _devsTutorialCenterAPIContext = devsTutorialCenterAPIContext;
        }
        public async Task ApproveAsync(ArticleApproval articleApproval)
        {
             await _repository.AddAsync<ArticleApproval>(articleApproval);
        }

        public async Task<ArticleApproval> ArticleApprovalById(string id)
        {
            var articleApproval = await _repository.GetByIdAsync<ArticleApproval>(id);

            return articleApproval;
        }

        public async Task<IEnumerable<ArticleApproval>> PendingArticles()
        {
            var pendingApprovals = await _devsTutorialCenterAPIContext.ArticleApprovals.Where(a => a.Status == SD.pending).ToListAsync();

            return pendingApprovals;
        }

        public async Task<ArticleApproval> PublishedArticle(string articleId)
        {
            var article = await _repository.GetByIdAsync<Article>(articleId);

            if(article == null)
            {
                throw new Exception("Article not found");
            }

            var articleApproval = await _devsTutorialCenterAPIContext.ArticleApprovals.FirstOrDefaultAsync(a => a.ArticleId == article.Id);
            if(articleApproval == null)
            {
                throw new Exception("Article cannot be published yet");
            }

            if(articleApproval.Status != SD.is_approved)
            {
                throw new Exception("Please Approve Article First");
            }
            articleApproval.Status = SD.is_published;

            await _repository.UpdateAsync<ArticleApproval>(articleApproval);

            return articleApproval;
        }

        public async Task<ArticleApproval> ReviewArticle(string articleId)
        {
            var article = await _repository.GetByIdAsync<Article>(articleId);

            if (article == null)
            {
                throw new Exception("Article not found");
            }

            var existingApproval = await _devsTutorialCenterAPIContext.ArticleApprovals
                .FirstOrDefaultAsync(a => a.ArticleId == article.Id && a.Status == SD.in_review);

            if (existingApproval != null)
            {
                throw new Exception("Article is already in review");
            }

            var newApproval = new ArticleApproval
            {
                ArticleId = article.Id,
                Status = SD.in_review
            };

            await _repository.AddAsync<ArticleApproval>(newApproval);

            return newApproval;
        }
        
    }

}
