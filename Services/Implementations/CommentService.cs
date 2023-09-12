using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Data.Repositories;
using DevsTutorialCenterAPI.Services.Abstractions;

namespace DevsTutorialCenterAPI.Services.Implementations
{
    public class CommentService : ICommentService
    {
        private readonly IRepository _repository;
        public CommentService(IRepository repository)
        {

        }
        public async Task<bool> DeleteCommentAsync(string Id, string userId)
        {
            var comment = await _repository.GetByIdAsync<Comments>(Id);

            if (comment == null)
            {
                throw new Exception("Comment not found");
            }

            if (comment.UserId != userId)
            {
                throw new Exception("You cannot delete this comment.");
            }

            await _repository.DeleteAsync<Comments>(comment);

            return true;
        }
    }
}
