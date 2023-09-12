using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Models.DTOs;

namespace DevsTutorialCenterAPI.Services.Interfaces
{
    public interface ITagService
    {
        Task CreateTagAsync(Tag tag);
        //Task<IEnumerable<Tag>> GetAllTagAsync();
    }
}
