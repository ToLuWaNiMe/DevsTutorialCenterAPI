using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Data.Repositories;
using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Services.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace DevsTutorialCenterAPI.Services.Implementations;

public class CommentsLikesService : ICommentsLikesService
{
    private readonly IRepository _repository;

    public CommentsLikesService(IRepository repository)
    {
        _repository = repository;
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