namespace DevsTutorialCenterAPI.Services.Abstractions
{
    public interface ICommentService
    {
        Task<bool> DeleteCommentAsync(string Id, string userId);
    }
}
