﻿using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Models.DTOs;

namespace DevsTutorialCenterAPI.Services.Abstractions
{
    public interface ICommentService
    {
        Task<Comment> CreateCommentAsync(CommentDTO commentDTO);
    }
}
