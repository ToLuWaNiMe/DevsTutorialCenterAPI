using DevsTutorialCenterAPI.Models.DTOs;

namespace DevsTutorialCenterAPI.Services.Interfaces
{
    public interface IBookmarkService
    {
        Task<ArticlesBookmarkDto> BookmarkArticleAsync(ArticlesBookmarkDto dto);
        Task UnbookmarkArticleAsync(string articleId, string userId);
    }
}
