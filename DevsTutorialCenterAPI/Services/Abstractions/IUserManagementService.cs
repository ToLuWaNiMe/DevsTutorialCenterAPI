using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Models.DTOs;

namespace DevsTutorialCenterAPI.Services.Abstractions
{
    public interface IUserManagementService
    {
        Task<IEnumerable<AppUserDTO>> GetAllUsers();
        Task<AppUserDTO> GetUserById(string userId);
        Task<bool> SoftDeleteUser(string Id);
        Task<bool> UpdateUser(string id, AppUserUpdateRequestDTO appUser);
    }
}