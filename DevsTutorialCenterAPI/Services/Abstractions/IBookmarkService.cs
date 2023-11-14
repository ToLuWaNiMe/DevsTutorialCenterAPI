using DevsTutorialCenterAPI.Models.DTOs;

namespace DevsTutorialCenterAPI.Services.Interfaces
{
    public interface IBookmarkService
    {
        Task<ArticlesBookmarkDto> BookmarkArticleAsync(string articleId, string userId);
        Task UnbookmarkArticleAsync(string articleId, string userId);
    }
}
