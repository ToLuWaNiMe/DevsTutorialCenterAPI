using DevsTutorialCenterAPI.Data;
using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Data.Repositories;
using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Services.Abstractions;

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

        public async Task CreateTagAsync(CreateTagDto createTagDto)
        {
            var tag = new Tag { Name = createTagDto.Name };
            await _repository.AddAsync(tag);
            
        }







    }
}
