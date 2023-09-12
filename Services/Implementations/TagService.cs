using DevsTutorialCenterAPI.Data;
using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Data.Repositories;
using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Services.Interfaces;

namespace DevsTutorialCenterAPI.Services.Implementations
{
    public class TagService : ITagService
    {
        private readonly IRepository _repository;
        private readonly DevsTutorialCenterAPIContext _context;
           
        public TagService(IRepository repository, DevsTutorialCenterAPIContext context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task CreateTagAsync(Tag tag)
        {
            await _repository.AddAsync <Tag>(tag);
        }







        //public async Task<IEnumerable<Tag>> GetAllTagAsync()
        //{
        //    return await _repository.GetAllAsync<Tag>();
        //}
    }
}
