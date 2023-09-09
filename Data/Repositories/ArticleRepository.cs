using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Data.Repositories.interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DevsTutorialCenterAPI.Data.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly DevsTutorialCenterAPIContext _context;

        public ArticleRepository(DevsTutorialCenterAPIContext context)
        {
            _context = context;
        }

        public async Task<Article> GetByPublicId(string publicId)
        {
            
            return await _context.Articles.SingleOrDefaultAsync(a => a.PublicId == publicId);
        }

        public async Task<Article> GetByArticleId(string articleId)
        {

            return await _context.Articles.SingleOrDefaultAsync(a => a.Id == articleId);
           
        }


    }
}
