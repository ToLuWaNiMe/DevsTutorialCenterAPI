using DevsTutorialCenterAPI.Models.DTOs;

namespace DevsTutorialCenterAPI.Services.Abstractions;

public interface ILikesService
{
    Task<List<LikesByArticleDto>> GetLikesByArticleAsync(string articleId);
}