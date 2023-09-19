﻿using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Data.Repositories;
using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Services.Abstractions;

namespace DevsTutorialCenterAPI.Services.Implementations;

public class CommentService : ICommentService
{
    private readonly IRepository _repository;

    public CommentService(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<Comment> CreateCommentAsync(CommentDto commentDTO)
    {
        var comment = new Comment
        {
            Text = commentDTO.Text,
            UserId = commentDTO.UserId
        };

        await _repository.AddAsync(comment);
        return comment;
    }

    public async Task<bool> UpdateCommentAsync(string Id, CommentDto commentDTO)
    {
        var comment = await _repository.GetByIdAsync<Comment>(Id);

        if (comment == null) throw new Exception("Comment not found");

        if (comment.UserId != commentDTO.UserId) throw new Exception("You cannot edit this comment.");

        comment.Text = commentDTO.Text;

        await _repository.UpdateAsync(comment);

        return true;
    }

    public async Task<bool> DeleteCommentAsync(string Id)
    {
        var comment = await _repository.GetByIdAsync<Comment>(Id);

        if (comment == null) throw new Exception("Comment not found");

        await _repository.DeleteAsync(comment);

        return true;
    }
}