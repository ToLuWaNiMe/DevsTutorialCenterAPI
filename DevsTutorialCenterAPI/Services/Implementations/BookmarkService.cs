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

        public async Task<ArticlesBookmarkDto> BookmarkArticleAsync(string articleId, string userId)
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
            var bookmarks = await _repository.GetAllAsync<ArticleBookMark>();
            var existingBookmark = bookmarks.FirstOrDefault(ab => ab.ArticleId == article.Id && ab.UserId == user.Id);
            
            if (existingBookmark != null)
            {
                throw new InvalidOperationException("User has already bookmarked the article");
            }

            var newBookmark = new ArticleBookMark
            {
                ArticleId = article.Id,
                UserId = user.Id
            };

            await _repository.AddAsync<ArticleBookMark>(newBookmark);
            return new ArticlesBookmarkDto { ArticleId = newBookmark.ArticleId, UserId = user.Id };
        }

        public async Task UnbookmarkArticleAsync(string articleId, string userId)
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
            var bookmarks = await _repository.GetAllAsync<ArticleBookMark>();
            var existingBookmark = bookmarks.FirstOrDefault(ab => ab.ArticleId == article.Id && ab.UserId == user.Id);

            if (existingBookmark == null)
            {
                
                return;
            }

            await _repository.DeleteAsync<ArticleBookMark>(existingBookmark);
        }
    }
}
