using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Data.Repositories;
using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Services.Interfaces;

namespace DevsTutorialCenterAPI.Services.Implementations
{
    public class TagService : ITagService
    {
        private readonly IRepository _repository;

        public TagService(IRepository repository)
        {
            _repository = repository;
        }


        public async Task<TagDto> Delete(string id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            // Find the existing tag by ID
            var existingTag = await _repository.GetByIdAsync<Tag>(id);

            if (existingTag == null)
                throw new InvalidOperationException($"Tag with ID {id} not found.");

            // Delete the existing tag from the repository
            await _repository.DeleteAsync(existingTag);

            // Return the deleted tag DTO if needed
            return new TagDto
            {
                Id = existingTag.Id,
                // Map other properties as needed
            };
        }


        public async Task<Tag> GetByIdAsync<T>(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));

            return await _repository.GetByIdAsync<Tag>(id);
        }

        public async Task<Tag> UpdateAsync(string id, TagDto updatedTag)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));

            if (updatedTag == null)
                throw new ArgumentNullException(nameof(updatedTag));

            var existingTag = await GetByIdAsync<Tag>(id);

            if (existingTag == null)
                throw new InvalidOperationException($"Tag with ID {id} not found.");

            existingTag.Name = updatedTag.Name;

            await _repository.UpdateAsync(existingTag);

            return existingTag;
        }


        public async Task<TagDto> AddTagAsync(TagDto newTagDto)
        {
            if (newTagDto == null)
                throw new ArgumentNullException(nameof(newTagDto));


            var newTagEntity = new Tag
            {

                Name = newTagDto.Name,

            };


            await _repository.AddAsync(newTagEntity);


            var addedTagDto = new TagDto
            {

                Id = newTagEntity.Id,
                Name = newTagEntity.Name,

            };

            return addedTagDto;
        }



    }

}
