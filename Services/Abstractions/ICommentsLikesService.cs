using DevsTutorialCenterAPI.Models.DTOs;

namespace DevsTutorialCenterAPI.Services.Abstractions;

public interface ICommentsLikesService
{
    Task<List<LikesByCommentsDto>> GetLikesByCommentsAsync(string commentId);
}