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

        public async Task<ArticlesBookmarkDto> BookmarkArticleAsync(ArticlesBookmarkDto dto)
        {
            var article = await _repository.GetByIdAsync<Article>(dto.ArticleId);

            if (article == null)
            {
                throw new ArgumentException("Article not found", nameof(dto.ArticleId));
            }
            var user = await _repository.GetByIdAsync<AppUser>(dto.UserId);
            if (user == null) 
            {
                throw new ArgumentException("User not found", nameof(dto.UserId));
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
