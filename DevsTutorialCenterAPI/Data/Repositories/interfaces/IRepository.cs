using DevsTutorialCenterAPI.Data.Entities;

namespace DevsTutorialCenterAPI.Data.Repositories
{
    public interface IRepository
    {
        Task<IQueryable<T>> GetAllAsync<T>() where T : class;
        Task<T?> GetByIdAsync<T>(string id) where T : class;
        Task AddAsync<T>(T entity) where T : class;
        Task AddRangeAsync<T>(T entities) where T : class;
        Task UpdateAsync<T>(T entity) where T : class;
        Task DeleteAsync<T>(T entity) where T : class;
        Task DeleteRangeAsync<T>(T entities) where T : class;
        Task<IQueryable<T>> GetAllAsync2<T>() where T : class;
    }
}
