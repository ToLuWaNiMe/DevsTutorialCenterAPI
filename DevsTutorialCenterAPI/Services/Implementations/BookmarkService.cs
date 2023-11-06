using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Data.Repositories;
using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DevsTutorialCenterAPI.Services.Implementations
{
    public class BookmarkService : IBookmarkService
    {
        private readonly IRepository _repository;

        public BookmarkService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task BookmarkArticleAsync(string articleId, string userId)
        {
            var article = await _repository.GetByIdAsync<Article>(articleId);

            if (article == null)
            {
                throw new ArgumentException("Article not found", nameof(articleId));
            }

            var bookmarks = await _repository.GetAllAsync<ArticleBookMark>();
            var existingBookmark = bookmarks.FirstOrDefault(ab => ab.ArticleId == articleId && ab.UserId == userId);

            if (existingBookmark != null)
            {
                throw new InvalidOperationException("User has already bookmarked the article");
            }

            var newBookmark = new ArticleBookMark
            {
                ArticleId = articleId,
                UserId = userId
            };

            await _repository.AddAsync(newBookmark);

        }

        public async Task UnbookmarkArticleAsync(string articleId, string userId)
        {
            var article = await _repository.GetByIdAsync<Article>(articleId);

            if (article == null)
            {
                throw new ArgumentException("Article not found", nameof(articleId));
            }

            var bookmarks = await _repository.GetAllAsync<ArticleBookMark>();
            var existingBookmark = bookmarks.FirstOrDefault(ab => ab.ArticleId == articleId && ab.UserId == userId);

            if (existingBookmark == null)
            {
                
                return;
            }

            await _repository.DeleteAsync(existingBookmark);
        }
    }
}
