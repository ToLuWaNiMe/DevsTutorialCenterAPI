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
            var articles = await _repository.GetAllAsync<Article>();

            if (!string.IsNullOrEmpty(filters.AuthorId))
            {
                articles = articles.Where(a => a.UserId == filters.AuthorId);
            }

            if (!string.IsNullOrEmpty(filters.Tag))
            {
                articles = articles.Where(a => a.Tag == (filters.Tag));
            }

            if (filters.IsRecommended is null)
            {
                articles = articles.Where(a => a.IsRecommended);
            }

            if (filters.IsSaved is null)
            {
                articles = articles.Where(a => a.IsSaved);
            }

            if (filters.IsRead is null)
            {
                articles = articles.Where(a => a.IsRead);
            }

            if (filters.IsReported is null)
            {
                articles = articles.Where(a => a.IsReported);
            }

            if (filters.IsPublished is null)
            {
                articles = articles.Where(a => a.IsPublished);
            }

            int pageNum = int.Parse(filters.Page);
            int pageSize = int.Parse(filters.Size);

            var skipAmount = (pageNum - 1) * pageSize;

            

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

            var paginatedArticles = result.Skip(skipAmount).Take(pageSize);

            return paginatedArticles;
        }

    }
}
