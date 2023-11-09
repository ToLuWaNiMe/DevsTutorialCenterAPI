using DevsTutorialCenterAPI.Data;
using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Data.Repositories;
using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Services.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace DevsTutorialCenterAPI.Services.Implementations;

public class TagService : ITagService
{
    private readonly DevsTutorialCenterAPIContext _context;
    private readonly IRepository _repository;

    public TagService(IRepository repository, DevsTutorialCenterAPIContext context)
    {
        _repository = repository;
        _context = context;
    }

    public async Task<string> CreateTagAsync(CreateTagDto createTagDto)
    {
        try
        {
            var tag = new ArticleTag { Name = createTagDto.Name };

            await _repository.AddAsync<ArticleTag>(tag);
            return tag.Name;
        }

        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<ArticleTag> GetByIdAsync<T>(string id)
    {
        if (string.IsNullOrEmpty(id))
            throw new ArgumentNullException(nameof(id));

        return await _repository.GetByIdAsync<ArticleTag>(id);
    }


    public async Task<UpdateTagDto> UpdateAsync(string id, UpdateTagDto updatedTagDto)
    {
        var existingTag = await _repository.GetByIdAsync<ArticleTag>(id);

        if (existingTag == null)
            throw new InvalidOperationException($"Tag with ID {id} not found.");

        existingTag.Name = updatedTagDto.Name;

        await _repository.UpdateAsync<ArticleTag>(existingTag);
        return new UpdateTagDto
        {
            Name = existingTag.Name
        };
    }


    public async Task<IEnumerable<GetAllTagsDto>> GetAllTagAsync()
    {
        var tags = await _repository.GetAllAsync<ArticleTag>();

        var tagsDto = await tags
            .Select(tag => new GetAllTagsDto
            {
                Id = tag.Id,
                Name = tag.Name
            })
            .ToListAsync();

        return tagsDto;
    }
}