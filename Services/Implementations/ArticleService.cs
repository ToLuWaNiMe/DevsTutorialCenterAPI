using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Data.Repositories;
using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Models.Enums;
using DevsTutorialCenterAPI.Services.Abstractions;

namespace DevsTutorialCenterAPI.Services.Implementations
{
    public class ArticleService : IArticleService
    {
        private readonly IRepository _repository;
        public ArticleService(IRepository repository) 
        {
            _repository = repository;
        }

        public async Task<GetAllArticlesDto> GetSingleArticle(string articleId)
        {
            var article = await _repository.GetByIdAsync<Article>(articleId);

            if (article == null)
            {
                throw new Exception($"Article with ID {articleId} not found.");
            }

            var articleDto = new GetAllArticlesDto
            {
                Id = article.Id,
                PublicId = article.PublicId,
                UserId = article.UserId,
                Title = article.Title,
                Tag = article.Tag,
                Text = article.Text,
                ImageUrl = article.ImageUrl,
                IsPublished = article.IsPublished,
                IsRead = article.IsRead,
                IsRecommended = article.IsRecommended,
                IsReported = article.IsReported,
                IsSaved = article.IsSaved,
                CreatedOn = article.CreatedOn
            };

            return articleDto;
        }


        public async Task<bool> SetArticleReportStatus(string articleId, string status)
        {
            if (status != ArticleStatusReportEnum.Approved.ToString().ToLower() && status != ArticleStatusReportEnum.Declined.ToString().ToLower())
            {
                throw new Exception("Invalid status provided");
            }

            var article = await _repository.GetByIdAsync<Article>(articleId);

            if (article == null)
            {
                throw new Exception("Article not found");
            }

            if(article.IsReported && status == ArticleStatusReportEnum.Approved.ToString().ToLower())
            {
                throw new Exception("Article already approved");
            }

            if (!article.IsReported && status == ArticleStatusReportEnum.Declined.ToString().ToLower())
            {
                throw new Exception("Article already declined");
            }


            if (status == ArticleStatusReportEnum.Approved.ToString().ToLower())
            {
                article.IsReported = true;
            }


            else if (status == ArticleStatusReportEnum.Declined.ToString().ToLower())
            {
                article.IsReported= false;
            }

            await _repository.UpdateAsync(article);

            return true;
        }
    }
}
