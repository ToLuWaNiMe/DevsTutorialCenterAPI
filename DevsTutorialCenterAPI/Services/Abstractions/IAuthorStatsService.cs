using DevsTutorialCenterAPI.Models.DTOs;
using System.Threading.Tasks;

namespace DevsTutorialCenterAPI.Services.Interfaces
{
    public interface IAuthorStatsService
    {
        Task<AuthorStatsDto> GetAuthorStatsAsync(string authorId);
        Task<AuthorsStatsDto> GetAuthorsStatsAsync(FetchAuthorsStatsDto fetchAuthorsStatsDto);
    }
}
