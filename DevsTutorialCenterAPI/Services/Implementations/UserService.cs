using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Data.Repositories;
using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Services.Abstractions;

namespace DevsTutorialCenterAPI.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IRepository _repository;
        private readonly IArticleService _articleService;

        public UserService(IRepository repository, IArticleService articleService)
        {
            _repository = repository;
            _articleService = articleService;
        }

        public async Task<IEnumerable<AppUserDTO>> GetAllUsers()
        {
            var users = await _repository.GetAllAsync<AppUser>();

            var userDTOs = users.Select(u => new AppUserDTO
            {
                ID = u.Id,
                Email = u.Email,
                FirstName = u.FirstName,
                LastName = u.LastName,
                PhoneNumber = u.PhoneNumber,
                Stack = u.Stack,
                Squad = u.Squad
            }).ToList();

            return userDTOs;
        }

        public async Task<AppUserDTO> GetUserById(string userId)
        {
            var existingUser = await _repository.GetByIdAsync<AppUser>(userId);

            if (existingUser == null)
                return null;

            var userDTO = new AppUserDTO
            {
                ID = existingUser.Id,
                Email = existingUser.Email,
                FirstName = existingUser.FirstName,
                LastName = existingUser.LastName,
                PhoneNumber = existingUser.PhoneNumber,
                Stack = existingUser.Stack,
                Squad = existingUser.Squad
            };

            return userDTO;
        }

        public async Task<List<GetReadArticlesDto>> GetArticleReadByUser(string userId)
        {
            // Retrieve all ArticleRead entries for the given user
            var articleReadEntries = (await _repository.GetAllAsync2<ArticleRead>()).Where(a => a.UserId == userId);
            if (articleReadEntries == null) 
            throw new ArgumentNullException("User has read no articles");

            var getAllReadArticles = new List<GetReadArticlesDto>();

            foreach (var article in articleReadEntries)
            {
                var foundArticle = await _articleService.GetArticleById(article.ArticleId);
                var getReadArticle = new GetReadArticlesDto
                {
                    Title = foundArticle is not null ? foundArticle.Title : null,
                    Text = foundArticle is not null ? foundArticle.Text : null,
                    TagId = foundArticle is not null ? foundArticle.TagId : null,
                    ImageUrl = foundArticle is not null ? foundArticle.ImageUrl : null,


                };
                getAllReadArticles.Add(getReadArticle);
            }


            return getAllReadArticles;
        }

      
    }
}
