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

        public async Task<string> CreateTagAsync(CreateTagDto createTagDto)
        {

            try
            {

                var tag = new Tag { Name = createTagDto.Name };

                await _repository.AddAsync<Tag>(tag);
                return tag.Id;


            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);


            }


        }










    }

}
