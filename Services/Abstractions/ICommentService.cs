using DevsTutorialCenterAPI.Data.Entities;

namespace DevsTutorialCenterAPI.Services.Abstractions
{
    public interface ICommentService
    {
        Task<IEnumerable<Comment>> GetCommentsByArticle(string ArticleId);
    }
}
