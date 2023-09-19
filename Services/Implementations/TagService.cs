using DevsTutorialCenterAPI.Data;
using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Data.Repositories;
using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Services.Abstractions;
using DevsTutorialCenterAPI.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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


        public async Task Delete(string id)
        {
            var existingTag = await _repository.GetByIdAsync<Tag>(id);

            if (existingTag == null)
                throw new InvalidOperationException($"Tag with ID {id} not found.");


            await _repository.DeleteAsync(existingTag);
        }


        public async Task<Tag> GetByIdAsync<T>(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));

            return await _repository.GetByIdAsync<Tag>(id);
        }


        public async Task<UpdateTagDto> UpdateAsync(string id, UpdateTagDto updatedTagDto)
        {
            var existingTag = await _repository.GetByIdAsync<Tag>(id);

            if (existingTag == null)
                throw new InvalidOperationException($"Tag with ID {id} not found.");

            existingTag.Name = updatedTagDto.Name;

            await _repository.UpdateAsync(existingTag);
            return new UpdateTagDto
            {
                Name = existingTag.Name,
            };
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