using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Data.Repositories;
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


        public async Task<Tag> Delete(Tag tag)
        {
            if (tag == null)
                throw new ArgumentNullException(nameof(tag));


            await _repository.DeleteAsync(tag);


            return tag;
        }

        public async Task<Tag> GetByIdAsync<T>(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));

            return await _repository.GetByIdAsync<Tag>(id);
        }

        public async Task<Tag> UpdateAsync(string id, Tag updatedTag)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));

            if (updatedTag == null)
                throw new ArgumentNullException(nameof(updatedTag));

            var existingTag = await GetByIdAsync<Tag>(id);

            if (existingTag == null)
                throw new InvalidOperationException($"Tag with ID {id} not found.");


            existingTag.Name = updatedTag.Name;
            existingTag.UpdatedOn = updatedTag.UpdatedOn;
            existingTag.CreatedOn = updatedTag.CreatedOn;




            await _repository.UpdateAsync(existingTag);

            return existingTag;
        }

        public async Task<Tag> AddTagAsync(Tag newTag)
        {
            if (newTag == null)
                throw new ArgumentNullException(nameof(newTag));

            await _repository.AddAsync(newTag);

            return newTag;
        }

    }

}
