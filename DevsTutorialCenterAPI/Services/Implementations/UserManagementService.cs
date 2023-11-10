using AutoMapper;
using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Data.Repositories;
using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Services.Abstractions;

namespace DevsTutorialCenterAPI.Services.Implementations;

public class UserManagementService : IUserManagementService
{
    private readonly IRepository _repository;
    private readonly IMapper _mapper;
    public UserManagementService(
        IRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AppUserDTO>> GetAllUsers()
    {
        var users = (await _repository.GetAllAsync<AppUser>())
            .Where( user => user.IsDeleted == false );

        var userDtoList = _mapper.Map<List<AppUserDTO>>(users);

        return userDtoList;
    }

    public async Task<AppUserDTO> GetUserById(string userId)
    {
        var existingUser = await _repository.GetByIdAsync<AppUser>(userId);

        if (existingUser == null || existingUser.IsDeleted == true)
            return null;

        var userDto = _mapper.Map<AppUserDTO>(existingUser);

        return userDto;
    }

    public async Task<bool> SoftDeleteUser(string id)
    {
        var user = await _repository.GetByIdAsync<AppUser>(id);

        if (user == null || user.IsDeleted)
            return false;

        user.IsDeleted = true;
        user.DeletedAt = DateTime.UtcNow;
        user.UpdatedOn = DateTime.UtcNow;

        await _repository.UpdateAsync(user);
        return true;
    }

    public async Task<bool> UpdateUser(string id, AppUserUpdateRequestDTO appUser)
    {
        var user = await _repository.GetByIdAsync<AppUser>(id);

        if (user == null || user.IsDeleted)
            return false;

        user.FirstName = appUser.FirstName;
        user.LastName = appUser.LastName;
        user.Email = appUser.Email;
        user.PhoneNumber = appUser.PhoneNumber;
        user.ImageUrl = appUser.ImageUrl;
        user.Stack = appUser.Stack;
        user.Squad = appUser.Squad;
        user.UpdatedOn = DateTime.UtcNow;

        await _repository.UpdateAsync(user);

        return true;
    }
}
