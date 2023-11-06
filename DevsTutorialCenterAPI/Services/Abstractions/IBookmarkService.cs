namespace DevsTutorialCenterAPI.Services.Interfaces
{
    public interface IBookmarkService
    {
        Task BookmarkArticleAsync(string articleId, string userId);
        Task UnbookmarkArticleAsync(string articleId, string userId);
    }
}
