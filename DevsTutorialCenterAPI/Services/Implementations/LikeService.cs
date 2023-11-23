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

        public async Task<List<AppUserLikeDTO>> GetLikesByArticle(string articleId)
        {
            var article = await _repository.GetByIdAsync<Article>(articleId);

            if (article == null) throw new Exception("Article not found");

            var articleLikes = (await _repository.GetAllAsync2<ArticleLike>()).Where(al => al.ArticleId == articleId);

            if(articleLikes == null)
            {
                throw new Exception("No likes for this article");
            }

            var getAppUserLikes = new List<AppUserLikeDTO>();

            foreach(var art in articleLikes)
            {
                var user = await _repository.GetByIdAsync<AppUser>(art.UserId);

                if (user.DeletedAt != null) continue;

                if (user == null) continue;

                var getAppUserLike = new AppUserLikeDTO
                {
                    UserId = user.Id,
                    Name = $"{user.FirstName} {user.LastName}"
                };

                getAppUserLikes.Add(getAppUserLike);
            }

            return getAppUserLikes;
        }

        public async Task<string> LikeArticleAsync(string articleId, string userId)
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

            var like = await _repository.GetAllAsync<ArticleLike>();
            var existingLike = like.FirstOrDefault(al => al.ArticleId == article.Id && al.UserId == user.Id);

            if (existingLike != null)
            {
                throw new InvalidOperationException("User has already liked the article");
            }

            var newLike = new ArticleLike
            {
                ArticleId = articleId,
                UserId = userId
            };

            await _repository.AddAsync<ArticleLike>(newLike);

            var likesforArticle = (await _repository.GetAllAsync<ArticleLike>()).Where(al => al.ArticleId == article.Id);

           return  likesforArticle.Count().ToString();
        }

        public async Task<string> UnlikeArticleAsync(string articleId, string userId)
        {
            var article = await _repository.GetByIdAsync<Article>(articleId);

            if (article == null)
            {
                throw new ArgumentException("Article not found", nameof(articleId));
            }

            var like = await _repository.GetAllAsync<ArticleLike>();
            var existingLike = like.FirstOrDefault(al => al.ArticleId == articleId && al.UserId == userId);

            if (existingLike == null)
            {
                throw new ArgumentException("You haven't liked this article", nameof(articleId));
            }

            await _repository.DeleteAsync<ArticleLike>(existingLike);

            var likesforArticle = (await _repository.GetAllAsync<ArticleLike>()).Where(al => al.ArticleId == article.Id);

            return likesforArticle.Count().ToString();
        }
    }
}
