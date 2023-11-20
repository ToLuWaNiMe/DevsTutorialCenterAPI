using DevsTutorialCenterAPI.Models.DTOs;

namespace DevsTutorialCenterAPI.Services.Abstractions;

public interface IUserManagementService
{
    Task<IEnumerable<AppUserDto>> GetAllUsers();
    Task<AppUserDto> GetUserById(string userId);
    Task<object> SoftDeleteUser(string Id);
    Task<AppUserUpdateRequestDTO> UpdateUser(string id, AppUserUpdateRequestDTO appUser);
    Task<List<GetReadArticlesDto>> GetArticleReadByUser(string userId);
}