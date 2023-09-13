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
            var authorIdFilter = !string.IsNullOrEmpty(filters.AuthorId);
            var tagFilter = !string.IsNullOrEmpty(filters.Tag);
            var isRecommendedFilter = filters.IsRecommended != null;
            var isSavedFilter = filters.IsSaved != null;
            var isReadFilter = filters.IsRead != null;
            var isReportedFilter = filters.IsReported != null;
            var isPublishedFilter = filters.IsPublished != null;

            var articles = await _repository.GetAllAsync<Article>();

            if (authorIdFilter)
            {
                articles = articles.Where(a => a.UserId == filters.AuthorId);
            }

            if (tagFilter)
            {
                articles = articles.Where(a => a.Tag == filters.Tag.ToUpper());
            }

            if (isRecommendedFilter)
            {
                articles = articles.Where(a => a.IsRecommended);
            }

            if (isSavedFilter)
            {
                articles = articles.Where(a => a.IsSaved);
            }

            if (isReadFilter)
            {
                articles = articles.Where(a => a.IsRead);
            }

            if (isReportedFilter)
            {
                articles = articles.Where(a => a.IsReported);
            }

            if (isPublishedFilter)
            {
                articles = articles.Where(a => a.IsPublished);
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

            

            // Use the Helper class's Paginator method to paginate the result
            var paginatorResponse = Helper.Paginate(articlesDto, pageNum, pageSize);

            return paginatorResponse;
        }
    }
}

