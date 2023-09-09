using DevsTutorialCenterAPI.Data.Entities;

namespace DevsTutorialCenterAPI.Data.Repositories.interfaces
{
    public interface IArticleRepository
    {

        Task<Article> GetByPublicId(string publicId);

        Task<Article> GetByArticleId(string articleId);
    }
}