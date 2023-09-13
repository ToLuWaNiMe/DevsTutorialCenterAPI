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


        public async Task<IEnumerable<CommentDto>> GetCommentsByArticle(string articleId)
        {
            var comments = await _repository.GetAllAsync<CommentDto>();

            var articleComments = await comments.Where(c => c.ArticleId == articleId).
                                  OrderByDescending(c => c.CreatedOn).ToListAsync();
            return articleComments;
        }
    }
}
