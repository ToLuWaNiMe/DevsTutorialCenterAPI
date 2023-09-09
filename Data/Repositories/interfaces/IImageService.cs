using CloudinaryDotNet.Actions;

namespace DevsTutorialCenterAPI.Data.Repositories.interfaces
{
    public interface IImageService
    {
      
        Task<ImageUploadResult> AddImageAsync(IFormFile file);

        Task<DeletionResult> DeleteImageAsync(string publicUrl);
    }
}