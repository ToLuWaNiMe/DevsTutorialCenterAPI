using System.Threading.Tasks;
using DevsTutorialCenterAPI.Data.Entities;

namespace DevsTutorialCenterAPI.Services.Interfaces
{
    public interface ILikeService
    {
        Task LikeArticleAsync(string articleId, string userId);
        Task UnlikeArticleAsync(string articleId, string userId);
    }
}
