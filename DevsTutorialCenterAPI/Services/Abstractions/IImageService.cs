using CloudinaryDotNet.Actions;

namespace DevsTutorialCenterAPI.Services.Abstractions;

public interface IImageService
{
    Task<ImageUploadResult> AddImageAsync(IFormFile file);

    Task<DeletionResult> DeleteImageAsync(string publicUrl);
}