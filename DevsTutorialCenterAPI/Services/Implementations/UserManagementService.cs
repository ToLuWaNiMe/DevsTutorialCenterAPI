using AutoMapper;
using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Data.Repositories;
using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Services.Abstractions;

namespace DevsTutorialCenterAPI.Services.Implementations;

public class UserManagementService : IUserManagementService
{
    private readonly IRepository _repository;
    private readonly IArticleService _articleService;
    private readonly IMapper _mapper;
    public UserManagementService(
        IRepository repository,
        IMapper mapper,
        IArticleService articleService)
    {
        _repository = repository;
        _mapper = mapper;
        _articleService = articleService;
    }

    public async Task<IEnumerable<AppUserDTO>> GetAllUsers()
    {
        var users = (await _repository.GetAllAsync<AppUser>())
            .Where( user => user.IsDeleted == false );

        var userDtoList = _mapper.Map<List<AppUserDTO>>(users);

        return userDtoList;
    }

    public async Task<AppUserDTO> GetUserById(string userId)
    {
        var existingUser = await _repository.GetByIdAsync<AppUser>(userId);

        if (existingUser == null || existingUser.IsDeleted == true)
            return null;

        var userDto = _mapper.Map<AppUserDTO>(existingUser);

        return userDto;
    }

    public async Task<bool> SoftDeleteUser(string id)
    {
        var user = await _repository.GetByIdAsync<AppUser>(id);

        if (user == null || user.IsDeleted)
            return false;

        user.IsDeleted = true;
        user.DeletedAt = DateTime.UtcNow;
        user.UpdatedOn = DateTime.UtcNow;

        await _repository.UpdateAsync(user);
        return true;
    }

    public async Task<bool> UpdateUser(string id, AppUserUpdateRequestDTO appUser)
    {
        var user = await _repository.GetByIdAsync<AppUser>(id);

        if (user == null || user.IsDeleted)
            return false;

        user.FirstName = appUser.FirstName;
        user.LastName = appUser.LastName;
        user.Email = appUser.Email;
        user.PhoneNumber = appUser.PhoneNumber;
        user.ImageUrl = appUser.ImageUrl;
        user.Stack = appUser.Stack;
        user.Squad = appUser.Squad;
        user.UpdatedOn = DateTime.UtcNow;

        await _repository.UpdateAsync(user);

        return true;
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
