﻿using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Models.DTOs;

namespace DevsTutorialCenterAPI.Services.Interfaces
{
    public interface ITagService
    {
        Task Delete(string id);
        Task<Tag> GetByIdAsync<T>(string id);
        Task<TagDto> UpdateAsync(string id, TagDto updatedTag);


    }
}
