using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Data.Repositories;
using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Services.Abstractions;
using DevsTutorialCenterAPI.Utilities;
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
        
        public async Task<GetAllArticlesDto> GetSingleArticle(string articleId)
        {
            var article = await _repository.GetByIdAsync<Article>(articleId);

            if (article == null)
            {
                throw new Exception($"Article with ID {articleId} not found.");
            }

            var articleDto = new GetAllArticlesDto
            {
                UserId = article.UserId,
                Title = article.Title,
                Tag = article.Tag,
                Text = article.Text,
                ImageUrl = article.ImageUrl,
                CreatedOn = article.CreatedOn
            };

            return articleDto;
        }

        public async Task<PaginatorResponseDto<IEnumerable<GetAllArticlesDto>>> GetAllArticles(FilterArticleDto filters)
        {

            var authorIdFilter = new List<Article>();
            var tagFilter = new List<Article>();
            var isRecommendedFilter = new List<Article>();
            var isSavedFilter = new List<Article>();
            var isReadFilter = new List<Article>();
            var isReportedFilter = new List<Article>();
            var isPublishedFilter = new List<Article>();

            var articles = await _repository.GetAllAsync<Article>();

            if (!string.IsNullOrEmpty(filters.AuthorId))
            {
                authorIdFilter = articles.Where(a => a.UserId == filters.AuthorId).ToList();
            }

            if (!string.IsNullOrEmpty(filters.Tag))
            {
                tagFilter = articles.Where(a => a.Tag == filters.Tag.ToUpper()).ToList();
            }

            if (filters.IsRecommended)
            {
                isRecommendedFilter = articles.Where(a => a.IsRecommended).ToList();
            }

            if (filters.IsSaved)
            {
                isSavedFilter = articles.Where(a => a.IsSaved).ToList();
            }

            if (filters.IsRead)
            {
                isReadFilter = articles.Where(a => a.IsRead).ToList();
            }

            if (filters.IsReported)
            {
                isReportedFilter = articles.Where(a => a.IsReported).ToList();
            }

            if (filters.IsPublished)
            {
                isPublishedFilter = articles.Where(a => a.IsPublished).ToList();
            }

            var articlesDto = articles.Select(a => new GetAllArticlesDto
            {
                Id = a.Id,
                UserId = a.UserId,
                Title = a.Title,
                Tag = a.Tag,
                Text = a.Text,
                ImageUrl = a.ImageUrl,
            });

            int pageNum = filters.Page ?? 1;
            int pageSize = filters.Size ?? 10;

            var skipAmount = (pageNum - 1) * pageSize;

            var paginatorResponse = Helper.Paginate(articlesDto, pageNum, pageSize);

            return paginatorResponse;
        }
    }
}

