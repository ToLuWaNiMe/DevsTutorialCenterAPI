using DevsTutorialCenterAPI.Data.Entities;

namespace DevsTutorialCenterAPI.Services.Abstractions
{
    public interface ITagService
    {
        Task<IEnumerable<Tag>> GetAllTagAsync();
    }
}
