using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Models.DTOs;

namespace DevsTutorialCenterAPI.Services.Abstractions
{
    public interface ICommentService
    {
        Task<Comment> CreateCommentAsync(CommentDTO commentDTO);
        Task<bool> UpdateCommentAsync(string Id, CommentDTO commentDTO);
       Task<bool> DeleteCommentAsync(string Id);
    }
}
