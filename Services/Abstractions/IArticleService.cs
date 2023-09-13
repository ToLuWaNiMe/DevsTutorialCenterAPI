using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Models.DTOs;

namespace DevsTutorialCenterAPI.Services.Abstractions
{
    public interface IArticleService
    {
        public Task<CreateArticleDto> CreateArticleAsync(CreateArticleDto model);
        public Task<GetAllArticlesDto> GetSingleArticle(string articleId);
    }
}
