using DevsTutorialCenterAPI.Data;
using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Data.Repositories;
using DevsTutorialCenterAPI.Models.DTOs;
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

        //public async Task<ArticleApproval>ApprovalArticleById(string articleId)
        //{
        //    var article = await _repository.GetByIdAsync<Article>(articleId);

        //    if (article == null)
        //    {
        //        throw new Exception("Article not found");
        //    }

        //    var articleApproval = await _devsTutorialCenterAPIContext.ArticleApprovals.FirstOrDefaultAsync(a => a.ArticleId == article.Id);
        //    if (articleApproval == null)
        //    {
        //        throw new Exception("Article cannot be approved yet");
        //    }

        //    if (articleApproval.Status == SD.in_review)
        //    {
        //        articleApproval.Status = SD.is_approved;
        //    }
            

        //    await _repository.UpdateAsync<ArticleApproval>(articleApproval);

        //    return articleApproval;
        //}



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

            if(articleApproval.Status != SD.in_review)
            {
                throw new Exception("Please review Article First");
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
                .FirstOrDefaultAsync(a => a.ArticleId == article.Id && a.Status == SD.is_published);

            if (reviewedApproval != null)
            {
                throw new Exception("Article has already been published successfully");
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

            if (articleApproval.Status == SD.is_published)
            {
                throw new Exception("Article has already been published");
            }

            if (articleApproval.Status == SD.in_review)
            {
                articleApproval.Status = SD.is_rejected;
            }

                
           
            await _repository.UpdateAsync<ArticleApproval>(articleApproval);

            return articleApproval;
        }

        public async Task<List<GetSingleArticleDto>> FetchArticleStatusListAsync(int number)
        {
            var articleApproval = await _repository.GetAllAsync2<ArticleApproval>();

            if (number is not 3 && number is not 4)
            {
                throw new Exception("Number must either be 3 or 4");
            }

            var result = new List<GetSingleArticleDto>();

            if(articleApproval.Count() != 0)
            {
                var publishedarticles = new List<GetSingleArticleDto>();
                var rejectedarticles = new List<GetSingleArticleDto>();
                foreach (var article in articleApproval)
                {
                    if(number == 3 && article.Status == number)
                    {
                        var pArticle = await _repository.GetByIdAsync<Article>(article.ArticleId); if (pArticle == null) continue;

                        var getPArticle = new GetSingleArticleDto
                        {
                            Id = pArticle.Id,
                            Title = pArticle.Title,
                            TagId = pArticle.TagId,
                            ImageUrl = pArticle.ImageUrl,
                            AuthorId = pArticle.AuthorId,
                            CreatedOn = pArticle.CreatedOn,
                            PublicId = pArticle.PublicId,
                            ReadCount = pArticle.ReadCount,
                            ReadTime = pArticle.ReadTime,
                            Text = pArticle.Text,

                        };
                        publishedarticles.Add(getPArticle);
                        result = publishedarticles;
                    }

                    if (number == 4 && article.Status == number)
                    {
                        var pArticle = await _repository.GetByIdAsync<Article>(article.ArticleId); if (pArticle == null) continue;

                        var getPArticle = new GetSingleArticleDto
                        {
                            Id = pArticle.Id,
                            Title = pArticle.Title,
                            TagId = pArticle.TagId,
                            ImageUrl = pArticle.ImageUrl,
                            AuthorId = pArticle.AuthorId,
                            CreatedOn = pArticle.CreatedOn,
                            PublicId = pArticle.PublicId,
                            ReadCount = pArticle.ReadCount,
                            ReadTime = pArticle.ReadTime,
                            Text = pArticle.Text,

                        };
                        rejectedarticles.Add(getPArticle);
                        result = rejectedarticles;
                    }

                }
                
            }

            return result;
        }
    }

}
