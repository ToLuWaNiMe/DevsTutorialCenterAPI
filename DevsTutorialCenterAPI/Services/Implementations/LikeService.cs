using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Data.Repositories;
using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Services.Interfaces;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace DevsTutorialCenterAPI.Services.Implementations
{
    public class LikeService : ILikeService
    {
        private readonly IRepository _repository;

        public LikeService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task LikeArticleAsync(string articleId, string userId)
        {
            var article = await _repository.GetByIdAsync<Article>(articleId);

            if (article == null)
            {
                throw new ArgumentException("Article not found", nameof(articleId));
            }
            var user = await _repository.GetByIdAsync<AppUser>(userId);
            if (user == null) 
            {
                throw new ArgumentException("User not found", nameof(userId));
            }

            var like = await _repository.GetAllAsync<ArticlesLikes>();
            var existingLike = like.FirstOrDefault(al => al.ArticleId == articleId && al.UserId == userId);

            if (existingLike != null)
            {
                throw new InvalidOperationException("User has already liked the article");
            }

            var newLike = new ArticlesLikes
            {
                ArticleId = articleId,
                UserId = userId
            };

            await _repository.AddAsync<ArticlesLikes>(newLike);
        }

        public async Task UnlikeArticleAsync(string articleId, string userId)
        {
            var article = await _repository.GetByIdAsync<Article>(articleId);

            if (article == null)
            {
                throw new ArgumentException("Article not found", nameof(articleId));
            }

            var like = await _repository.GetAllAsync<ArticlesLikes>();
            var existingLike = like.FirstOrDefault(al => al.ArticleId == articleId && al.UserId == userId);

            if (existingLike == null)
            {
                return;
            }

            await _repository.DeleteAsync<ArticlesLikes>(existingLike);
        }
    }
}
