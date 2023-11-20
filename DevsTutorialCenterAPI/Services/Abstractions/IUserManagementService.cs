using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Models.DTOs;

namespace DevsTutorialCenterAPI.Services.Abstractions
{
    public interface IUserManagementService
    {
        Task<IEnumerable<AppUserDto>> GetAllUsers();
        Task<AppUserDto> GetUserById(string userId);
        Task<bool> SoftDeleteUser(string Id);
        Task<bool> UpdateUser(string id, AppUserUpdateRequestDTO appUser);
        Task<List<GetReadArticlesDto>> GetArticleReadByUser(string userId);
    }
}