using DevsTutorialCenterAPI.Data.Entities;

namespace DevsTutorialCenterAPI.Services.Abstractions
{
    public interface IJwtTokenGeneratorService
    {
        string GenerateToken(AppUser appUser, IEnumerable<string> roles);
    }
}