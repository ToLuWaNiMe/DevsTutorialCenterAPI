using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Data.Repositories;
using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Services.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace DevsTutorialCenterAPI.Services.Implementations
{
    
    public class CommentService : ICommentService
    {
        private readonly IRepository _repository;

        public CommentService(IRepository repository)
        {
            _repository = repository;
        }
        public async Task<Comment> CreateCommentAsync(CommentDTO commentDTO)
        {

          //  var user = await _repository.GetByIdAsync<AppUser>(commentDTO.UserId);

            if (commentDTO == null)

            {
                throw new ArgumentNullException(nameof(commentDTO));
            }
            var comment = new Comment
            {
                Text = commentDTO.Text,
                UpdatedOn = DateTime.UtcNow,
                CreatedOn = DateTime.UtcNow,
            };

            await _repository.AddAsync(comment);            
            return comment;
        }
    }
}
