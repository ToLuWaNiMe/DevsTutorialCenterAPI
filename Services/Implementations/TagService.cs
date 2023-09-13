using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Data.Repositories;
using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DevsTutorialCenterAPI.Services.Implementations
{
    public class TagService : ITagService
    {
        private readonly IRepository _repository;

        public TagService(IRepository repository)
        {
            _repository = repository;
        }



        public async Task<IEnumerable<GetAllTagsDto>> GetAllTagAsync()
        {
            IQueryable<Tag> tags = await _repository.GetAllAsync<Tag>();

         
            var tagsDto = await tags
                .Select(tag => new GetAllTagsDto
                {
                    Id = tag.Id,
                    Name = tag.Name,
                })
                .ToListAsync();

            return tagsDto;
        }
    }
}
