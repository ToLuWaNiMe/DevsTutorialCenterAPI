using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Models.DTOs;

namespace DevsTutorialCenterAPI.Services.Interfaces
{
    public interface ITagService
    {
        Task<TagDto> Delete(string id);
        Task<Tag> GetByIdAsync<T>(string id);
        Task<Tag> UpdateAsync(string id, TagDto updatedTag);

        Task<TagDto> AddTagAsync(TagDto newTag);
    }
}
