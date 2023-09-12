using DevsTutorialCenterAPI.Data.Entities;

namespace DevsTutorialCenterAPI.Services.Interfaces
{
    public interface ITagService
    {
        Task<Tag> Delete(Tag tag);
        Task<Tag> GetByIdAsync<T>(string id);
        Task<Tag> UpdateAsync(string Name, Tag updatedTag);

        Task<Tag> AddTagAsync(Tag newTag);
    }
}
