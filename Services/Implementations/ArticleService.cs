using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Data.Repositories;
using DevsTutorialCenterAPI.Models.DTOs;
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

        public async Task<IEnumerable<GetAllArticlesDto>> GetAllArticles(FilterArticleDto filters)
        {
            // Get all articles from the repository
            var articles = await _repository.GetAllAsync<Article>();

            // Apply filters based on FilterArticleDto properties
            if (!string.IsNullOrEmpty(filters.AuthorId))
            {
                articles = articles.Where(a => a.UserId == filters.AuthorId);
            }

            if (!string.IsNullOrEmpty(filters.Tag))
            {
                articles = articles.Where(a => a.Tag.Contains(filters.Tag));
            }

            if (filters.IsRecommended)
            {
                articles = articles.Where(a => a.IsRecommended);
            }

            if (filters.IsSaved)
            {
                articles = articles.Where(a => a.IsSaved);
            }

            if (filters.IsRead)
            {
                articles = articles.Where(a => a.IsRead);
            }

            if (filters.IsReported)
            {
                articles = articles.Where(a => a.IsReported);
            }

            if (filters.IsPublished)
            {
                articles = articles.Where(a => a.IsPublished);
            }

            // You can add more filtering based on other properties as needed

            // Project the filtered articles into GetAllArticlesDto
            var result = articles.Select(a => new GetAllArticlesDto
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
                IsSaved = a.IsSaved
            }).ToList();

            return result;
        }

    }
}
