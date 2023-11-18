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

        public async Task<ArticleApproval>ApprovalArticleById(string articleId)
        {
            var article = await _repository.GetByIdAsync<Article>(articleId);

            if (article == null)
            {
                throw new Exception("Article not found");
            }

            var articleApproval = await _devsTutorialCenterAPIContext.ArticleApprovals.FirstOrDefaultAsync(a => a.ArticleId == article.Id);
            if (articleApproval == null)
            {
                throw new Exception("Article cannot be approved yet");
            }

            if (articleApproval.Status == SD.in_review)
            {
                articleApproval.Status = SD.is_approved;
            }
            

            await _repository.UpdateAsync<ArticleApproval>(articleApproval);

            return articleApproval;
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

            var reviewedApproval = await _devsTutorialCenterAPIContext.ArticleApprovals
                .FirstOrDefaultAsync(a => a.ArticleId == article.Id && a.Status == SD.is_approved);

            if (reviewedApproval != null)
            {
                throw new Exception("Article has already been reviewed successfully");
            }

            var newApproval = await _devsTutorialCenterAPIContext.ArticleApprovals
                .FirstOrDefaultAsync(a => a.ArticleId == article.Id);

            newApproval.Status = SD.in_review;

            await _repository.UpdateAsync<ArticleApproval>(newApproval);

            return newApproval;
        }


        public async Task<ArticleApproval> RejectArticle(string articleId)
        {
            var article = await _repository.GetByIdAsync<Article>(articleId);

            if (article == null)
            {
                throw new Exception("Article not found");
            }

            var articleApproval = await _devsTutorialCenterAPIContext.ArticleApprovals
                .FirstOrDefaultAsync(a => a.ArticleId == articleId);

            if(articleApproval.Status == SD.is_rejected)
            {
                throw new Exception("Article has already been rejected");
            }

            if (articleApproval.Status == SD.in_review)
            {
                articleApproval.Status = SD.is_rejected;
            }

                
           
            await _repository.UpdateAsync<ArticleApproval>(articleApproval);

            return articleApproval;
        }


    }

}
