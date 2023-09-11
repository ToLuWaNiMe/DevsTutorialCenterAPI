using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Data.Repositories;
using DevsTutorialCenterAPI.Services.Abstractions;
using DevsTutorialCenterAPI.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevsTutorialCenterAPI.Services.Implementations
{
    public class LikesService : ILikesService
    {
        private readonly IRepository _repository;
        public LikesService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<LikesByArticleDto>> GetLikesByArticleAsync(string articleId)
        {
            // Retrieve likes by article ID using the repository
            var likes = await _repository.GetAllAsync<ArticlesLikes>();

            // Filter the likes by the specified articleId
            var likesWithArticleId = likes
                .Where(like => like.ArticleId == articleId)
                .Select(like => new LikesByArticleDto
                {
                    UserId = like.UserId,
                    ArticleId = like.ArticleId
                })
                .ToList();

            if (!likesWithArticleId.Any())
            {
                throw new Exception("Likes Not Found");
            }

            return likesWithArticleId;
        }




        public async Task<bool> LikeArticleAsync(string userId, string articleId)
        {
            try
            {


                var newLike = new ArticlesLikes
                {
                    ArticleId = articleId,
                    UserId = userId
                };

              await _repository.AddAsync<ArticlesLikes>(newLike);

                // Implement logic to add a like for the specified user and article
                // Return true if the like was successfully added, otherwise false
                // Remember to handle exceptions appropriately

                // Placeholder, replace with your implementation
                return false;
            }
            catch (Exception ex)
            {
                throw ;
            }
        }
    }
}
