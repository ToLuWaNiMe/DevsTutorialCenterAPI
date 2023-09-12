using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Data.Repositories;
using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Services.Abstractions;

namespace DevsTutorialCenterAPI.Services.Implementations
{
    public class ArticleService : IArticleService
    {
        private readonly IRepository _repository;
        public ArticleService(IRepository repo)
        {
            _repository = repo;
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
