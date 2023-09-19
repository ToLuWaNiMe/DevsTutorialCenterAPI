using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Models.DTOs;

namespace DevsTutorialCenterAPI.Services.Abstractions
{
    public interface ITagService
    {
        Task<string> CreateTagAsync(CreateTagDto createTagDto);
        Task Delete(string id);
        Task<Tag> GetByIdAsync<T>(string id);
        Task<UpdateTagDto> UpdateAsync(string id, UpdateTagDto updatedTag);
    }
}
