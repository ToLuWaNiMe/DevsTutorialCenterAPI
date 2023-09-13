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

        public async Task<CreateArticleDto> CreateArticleAsync(CreateArticleDto model)
        {
            string[] allowedTags = { "JAVA", ".NET", "NODE" };
            if (!allowedTags.Contains(model.Tag, StringComparer.OrdinalIgnoreCase))
            {
                throw new ArgumentException("Invalid tag. Tag must either one of: JAVA, .NET, NODE.");
            }
            var newArticle = new Article
            {
                Title = model.Title,
                Tag = model.Tag,
                Text = model.Text,
                IsRecommended = model.IsRecommended,
                IsTrending = model.IsTrending,
                UserId = model.UserId,
            };

            await _repository.AddAsync(newArticle);

            var newArticleData = new CreateArticleDto
            {
                Title = newArticle.Title,
                Tag = newArticle.Tag,
                IsRecommended = newArticle.IsRecommended,
                IsTrending = newArticle.IsTrending,
                UserId = newArticle.UserId,
                Text = newArticle.Text,
            };

            return newArticleData;

        }
    }
}
