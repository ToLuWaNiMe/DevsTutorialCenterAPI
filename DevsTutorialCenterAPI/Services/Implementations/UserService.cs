using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Data.Repositories;
using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Services.Abstractions;

namespace DevsTutorialCenterAPI.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IRepository _repository;

        public UserService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<AppUserDTO>> GetAllUsers()
        {
            var users = await _repository.GetAllAsync<AppUser>();

            var userDTOs = users.Select(u => new AppUserDTO
            {
                ID = u.Id,
                Email = u.Email,
                FirstName = u.FirstName,
                LastName = u.LastName,
                PhoneNumber = u.PhoneNumber,
                Stack = u.Stack,
                Squad = u.Squad
            }).ToList();

            return userDTOs;
        }

        public async Task<AppUserDTO> GetUserById(string userId)
        {
            var existingUser = await _repository.GetByIdAsync<AppUser>(userId);

            if (existingUser == null)
                return null;

            var userDTO = new AppUserDTO
            {
                ID = existingUser.Id,
                Email = existingUser.Email,
                FirstName = existingUser.FirstName,
                LastName = existingUser.LastName,
                PhoneNumber = existingUser.PhoneNumber,
                Stack = existingUser.Stack,
                Squad = existingUser.Squad
            };

            return userDTO;
        }
    }
}
