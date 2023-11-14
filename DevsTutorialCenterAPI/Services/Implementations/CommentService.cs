﻿using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Data.Repositories;
using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Services.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace DevsTutorialCenterAPI.Services.Implementations;

public class CommentService : ICommentService
{
    private readonly IRepository _repository;

    public CommentService(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<CreateCommentDto> CreateCommentAsync(string articleId, string userId, CreateCommentDto dto)
    {
        var article = await _repository.GetByIdAsync<Article>(articleId);

        if (article == null) throw new Exception("Article not found");

        var user = await _repository.GetByIdAsync<AppUser>(userId);

        if (user == null) throw new Exception("User not found");

        var comment = new Comment
        {
            Text = dto.Text,
            UserId = user.Id,
            ArticleId = article.Id,
        };

        await _repository.AddAsync<Comment>(comment);

        var commentDto = new CreateCommentDto
        {
            Text = dto.Text,
           
           
        };
        return commentDto;
    }

    public async Task<UpdateCommentDto> UpdateCommentAsync(string id, string userId,  CommentDto commentDto)
    {
        var comment = await _repository.GetByIdAsync<Comment>(id);

        if (comment == null) throw new Exception("Comment not found");

        var user = await _repository.GetByIdAsync<AppUser>(userId);

        if (user == null) throw new Exception("User not found");

        if (comment.UserId != user.Id) throw new Exception("You cannot edit this comment.");

        comment.Text = commentDto.Text;

        await _repository.UpdateAsync<Comment>(comment);

        return new UpdateCommentDto
        {
            Text = comment.Text

        };
            
    }

    public async Task<bool> DeleteCommentAsync(string Id, string userId)
    {
        var comment = await _repository.GetByIdAsync<Comment>(Id);

        if (comment == null) throw new Exception("Comment not found");

        var user = await _repository.GetByIdAsync<AppUser>(userId);

        if (user == null) throw new Exception("User not found");

        if (comment.UserId != user.Id) throw new Exception("You cannot delete this comment.");

        await _repository.DeleteAsync(comment);

        return true;
    }

    public async Task<IEnumerable<GetCommentDto>> GetCommentsByArticle(string articleId)
    {
        var comments = await _repository.GetAllAsync<Comment>();
        if (comments == null) throw new Exception("No comments for this article");

        var articleComments = await comments.Where(c => c.ArticleId == articleId)
            .OrderByDescending(c => c.CreatedOn)
            .Select(c => new GetCommentDto
            {
                Id = c.Id,
                Text = c.Text,
                UserId = c.UserId,
                ArticleId = c.ArticleId
               
            })
            .ToListAsync();

        return articleComments;
    }

    public async Task<List<LikesByCommentsDto>> GetLikesByCommentsAsync(string commentId)
    {
        try
        {
            var likesQuery = await _repository.GetAllAsync<CommentsLikes>();
            var likes = await likesQuery
                .Where(like => like.CommentId == commentId)
                .Select(like => new LikesByCommentsDto
                {
                    UserId = like.UserId,
                    CommentId = like.CommentId
                })
                .ToListAsync();

            return likes;
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to retrieve likes by comments.", ex);
        }
    }
}