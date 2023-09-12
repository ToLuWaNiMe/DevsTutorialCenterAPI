using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Models.DTOs;

namespace DevsTutorialCenterAPI.Services.Abstractions
{
    public interface ITagService
    {
        Task CreateTagAsync(CreateTagDto createTagDto);
    }
}
