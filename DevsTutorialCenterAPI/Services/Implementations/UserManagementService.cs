using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Data.Repositories;

namespace DevsTutorialCenterAPI.Services.Implementations;

public class UserManagementService
{
    private readonly IRepository _repository;
    public UserManagementService(IRepository repository)
    {
        _repository = repository;
    }

    public async Task SoftDeleteUser(AppUser appUser)
    {
        var user = await _repository.GetByIdAsync<AppUser>(appUser.Id) ?? 
            throw new ArgumentException();

        user.IsDeleted = "true";
        
        try
        {
            await _repository.UpdateAsync(user);
        }
        catch (Exception ex)
        {

        }
    }
}
