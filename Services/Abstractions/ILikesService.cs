using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Models.DTOs;

namespace DevsTutorialCenterAPI.Services.Abstractions
{
    public interface ILikesService
    {
        Task<List<LikesByArticleDto>> GetLikesByArticleAsync(string articleId);
        Task<bool> LikeArticleAsync(string userId, string articleId);

    }
}
