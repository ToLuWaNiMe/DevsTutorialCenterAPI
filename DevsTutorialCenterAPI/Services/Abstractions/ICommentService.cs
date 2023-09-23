﻿using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Models.DTOs;

namespace DevsTutorialCenterAPI.Services.Abstractions;

public interface ICommentService
{
    Task<IEnumerable<CommentDto>> GetCommentsByArticle(string articleId);
    Task<Comment> CreateCommentAsync(CreateCommentDto commentDTO);
    Task<bool> UpdateCommentAsync(string Id, CommentDto commentDTO);
    Task<bool> DeleteCommentAsync(string Id);
    Task<List<LikesByCommentsDto>> GetLikesByCommentsAsync(string commentId);
}