using DevsTutorialCenterAPI.Data.Entities;

namespace DevsTutorialCenterAPI.Services.Abstractions
{
    public interface ITagService
    {
        Task CreateTagAsync(Tag tag);
    }
}
