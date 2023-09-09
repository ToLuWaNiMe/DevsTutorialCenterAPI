using DevsTutorialCenterAPI.Models.DTOs;

namespace DevsTutorialCenterAPI.Data.Repositories
{
    public interface IArticleRepository
    {
        Task <IEnumerable<GetAllArticlesDto>> GetAll();
    }
}
