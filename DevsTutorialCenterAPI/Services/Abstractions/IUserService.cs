﻿using DevsTutorialCenterAPI.Models.DTOs;

namespace DevsTutorialCenterAPI.Services.Abstractions
{
    public interface IUserService
    {
        Task<IEnumerable<AppUserDTO>> GetAllUsers();
        Task<AppUserDTO> GetUserById(string userId);
    }
}
