using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Data.Repositories;
using DevsTutorialCenterAPI.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevsTutorialCenterAPI.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IRepository _repository;

        public ArticleService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<Article> GetArticleByPublicId(string publicId)
        {
            var article = await _repository.GetByIdAsync<Article>(publicId);


            return article;

        }

        public async Task<Article> GetArticleById(string articleId)
        {
            return await _repository.GetByIdAsync<Article>(articleId);
        }

        public async Task UpdateArticleAsync(Article article)
        {
            if (article == null)
            {
                throw new ArgumentNullException(nameof(article));
            }

            await _repository.UpdateAsync(article);
        }


    }
}
