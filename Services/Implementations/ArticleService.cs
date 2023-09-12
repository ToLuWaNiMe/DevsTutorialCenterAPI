using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Data.Repositories;
using DevsTutorialCenterAPI.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevsTutorialCenterAPI.Services.Implementations
{
    public class ArticleService : IArticleService
    {
        private readonly IRepository _repository;

        public ArticleService(IRepository repository)
        {
            _repository = repository;
        }

      

        public async Task<Article> GetArticleById(string articleId)
        {
            return await _repository.GetByIdAsync<Article>(articleId);
        }

        public async Task UpdateArticleAsync(Article article)
        {
            if (article == null)
            {
                throw new ArgumentNullException(nameof(article));
            }

            await _repository.UpdateAsync(article);
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
    }
}
