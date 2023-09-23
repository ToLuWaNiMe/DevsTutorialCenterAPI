using DevsTutorialCenterAPI.Data.Entities;
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

    public async Task<Comment> CreateCommentAsync(CreateCommentDto commentDto)
    {
        var comment = new Comment
        {
            Text = commentDto.Text,
            UserId = commentDto.UserId,
            ArticleId = commentDto.ArticleId,
        };

        await _repository.AddAsync(comment);
        return comment;
    }

    public async Task<bool> UpdateCommentAsync(string id, CommentDto commentDto)
    {
        var comment = await _repository.GetByIdAsync<Comment>(id);

        if (comment == null) throw new Exception("Comment not found");

        if (comment.UserId != commentDto.UserId) throw new Exception("You cannot edit this comment.");

        comment.Text = commentDto.Text;

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

    public async Task<IEnumerable<CommentDto>> GetCommentsByArticle(string articleId)
    {
        var comments = await _repository.GetAllAsync<Comment>();

        var articleComments = await comments.Where(c => c.ArticleId == articleId)
            .OrderByDescending(c => c.CreatedOn)
            .Select(c => new CommentDto
            {
                Id = c.Id,
                ArticleId = c.ArticleId,
                Text = c.Text,
                UserId = c.UserId,
                CreatedOn = c.CreatedOn,
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