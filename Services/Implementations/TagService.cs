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






    }

}
