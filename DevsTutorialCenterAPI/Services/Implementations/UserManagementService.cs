using AutoMapper;
using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Data.Repositories;
using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Services.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace DevsTutorialCenterAPI.Services.Implementations;

public class UserManagementService : IUserManagementService
{
    private readonly IRepository _repository;
    private readonly IArticleService _articleService;
    private readonly IMapper _mapper;
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    public UserManagementService(
        IRepository repository,
        IMapper mapper,
        IArticleService articleService,
        UserManager<AppUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _repository = repository;
        _mapper = mapper;
        _articleService = articleService;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<IEnumerable<AppUserDTO>> GetAllUsers()
    {
        var users = (await _repository.GetAllAsync2<AppUser>())
            .Where( user => user.DeletedAt == null );

        var userDtoList = new List<AppUserDTO>();

        foreach ( var user in users )
        {
            var userRole = await _userManager.GetRolesAsync(user);

            var userDTO = new AppUserDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                ImageUrl = user.ImageUrl,
                Squad = user.Squad,
                Stack = user.Stack,
                RoleName = userRole is not null ? userRole : null

            };

            userDtoList.Add(userDTO);   
        }

        return userDtoList;
    }

    public async Task<AppUserDTO> GetUserById(string userId)
    {
        var existingUser = await _repository.GetByIdAsync<AppUser>(userId);

        if (existingUser == null || existingUser.DeletedAt is not null)
            return null;

        var userDto = _mapper.Map<AppUserDTO>(existingUser);

        return userDto;
    }

    public async Task<object> SoftDeleteUser(string id)
    {
        var user = await _repository.GetByIdAsync<AppUser>(id);

        if (user == null || !(user.DeletedAt is null))
            return false;

        user.DeletedAt = DateTime.UtcNow;
        user.UpdatedOn = DateTime.UtcNow;

        await _repository.UpdateAsync(user);
        return new { deletedAt = user.DeletedAt };
    }

    public async Task<AppUserUpdateRequestDTO> UpdateUser(string id, AppUserUpdateRequestDTO appUser)
    {
        var user = await _repository.GetByIdAsync<AppUser>(id);

        if (user == null || user.DeletedAt is not null)
            throw new Exception("User not found");

        user.FirstName = appUser.FirstName;
        user.LastName = appUser.LastName;
        user.Email = appUser.Email;
        user.PhoneNumber = appUser.PhoneNumber;
        user.ImageUrl = appUser.ImageUrl;
        user.Stack = appUser.Stack;
        user.Squad = appUser.Squad;
        user.UpdatedOn = DateTime.UtcNow;

        await _repository.UpdateAsync<AppUser>(user);

        var updatedUser = new AppUserUpdateRequestDTO
        {
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            PhoneNumber = user.PhoneNumber,
            ImageUrl = user.ImageUrl,
            Squad = user.Squad,
            Stack = user.Stack
        };

        return updatedUser;
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
                Title = foundArticle?.Title,
                Text = foundArticle?.Text,
                TagId = foundArticle?.TagId,
                ImageUrl = foundArticle?.ImageUrl,


            };
            getAllReadArticles.Add(getReadArticle);
        }


        return getAllReadArticles;
    }
}
