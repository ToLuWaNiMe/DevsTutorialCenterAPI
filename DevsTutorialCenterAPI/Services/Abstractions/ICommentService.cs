using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Models.DTOs;

namespace DevsTutorialCenterAPI.Services.Abstractions;

public interface ICommentService
{
    Task<IEnumerable<GetCommentDto>> GetCommentsByArticle(string articleId);
    Task<CreateCommentDto> CreateCommentAsync(string articleId, string userId, CreateCommentDto dto);
    Task<UpdateCommentDto> UpdateCommentAsync(string Id, string userId, CommentDto commentDTO);
    Task<bool> DeleteCommentAsync(string Id, string userId);
    Task<List<LikesByCommentsDto>> GetLikesByCommentsAsync(string commentId);

    Task<string> LikeComment(string commentId, string userId);

    Task<CommentLike> GetCommentLikeByUserId(string userId, string commentId);

   
}