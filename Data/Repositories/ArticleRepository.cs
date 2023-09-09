using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DevsTutorialCenterAPI.Data.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly DevsTutorialCenterAPIContext _context;
        public ArticleRepository(DevsTutorialCenterAPIContext context) 
        {
            _context = context;
        }
        public async Task<IEnumerable<GetAllArticlesDto>> GetAll()
        {
            var articles = await _context.Articles.Select(a => new GetAllArticlesDto
            {
                PublicId = a.PublicId,
                UserId = a.UserId,
                Title = a.Title,
                Tag = a.Tag,
                Text = a.Text,
                ImageUrl = a.ImageUrl,
                IsPublished = a.IsPublished,
                IsRead = a.IsRead,
                IsRecommended = a.IsRecommended,
                IsReported = a.IsReported,
                IsSaved = a.IsSaved
            }).ToListAsync();

            return articles;
        }
    }
}
