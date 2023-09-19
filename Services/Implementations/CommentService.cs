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
                _repository = repository;
            }
            public async Task<bool> DeleteCommentAsync(string Id)
            {
            var comment = await _repository.GetByIdAsync<Comment>(Id);

                if (comment == null)
                {
                    throw new Exception("Comment not found");
                }

                await _repository.DeleteAsync(comment);

                return true;
            }
        }

    }

