using System.Threading.Tasks;
using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Models.DTOs;

namespace DevsTutorialCenterAPI.Services.Interfaces
{
    public interface ILikeService
    {
        Task<string> LikeArticleAsync(string articleId, string userId);
        Task<string> UnlikeArticleAsync(string articleId, string userId);

        Task<List<AppUserLikeDTO>> GetLikesByArticle(string articleId);
    }
}
