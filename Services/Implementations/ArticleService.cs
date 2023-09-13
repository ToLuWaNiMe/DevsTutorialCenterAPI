using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Data.Repositories;
using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Services.Abstractions;
using Humanizer;
using Microsoft.EntityFrameworkCore;

namespace DevsTutorialCenterAPI.Services.Implementations
{
    public class ArticleService : IArticleService
    {
        private readonly IRepository _repository;
        public ArticleService(IRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<GetAllArticlesDto>> GetAllArticles()
        {
            var articles = await _repository.GetAllAsync<Article>();

            return articles.Select(a => new GetAllArticlesDto
            {
                Id = a.Id,
                PublicId = a.PublicId,
                UserId = a.UserId,
                Title = a.Title,
                Tag = a.Tag,
                Text = a.Text,
                ImageUrl = a.ImageUrl,
                IsPublished = a.IsPublished,
                IsRead = a.IsRead,
                IsRecommended = a.IsRecommended,
                IsReported = a.IsReported,
                IsSaved = a.IsSaved,
                CreatedOn = a.CreatedOn
            }).ToList();
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
