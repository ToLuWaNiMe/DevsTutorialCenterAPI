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
    }
}
