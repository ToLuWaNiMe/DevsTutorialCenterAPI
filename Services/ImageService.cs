using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DevsTutorialCenterAPI.Data.Repositories.interfaces;

namespace DevsTutorialCenterAPI.Services
{
    public class ImageService : IImageService
    {
        private readonly Cloudinary _cloudinary;

        public ImageService(IConfiguration config)
        {
            var acc = new Account
            (
                config["Cloudinary:CloudName"],
                config["Cloudinary:ApiKey"],
                config["Cloudinary:ApiSecret"]
            );

            _cloudinary = new Cloudinary(acc);
        }

        public async Task<ImageUploadResult> AddImageAsync(IFormFile file)
        {
            var uploadResult = new ImageUploadResult();
            try {

                

                if (file.Length > 0)
                {
                    using var stream = file.OpenReadStream();
                    var uploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(file.FileName, stream)
                    };
                    uploadResult = await _cloudinary.UploadAsync(uploadParams);
                }


            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
            
            return uploadResult;
        }

        public async Task<DeletionResult> DeleteImageAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);

            var result = await _cloudinary.DestroyAsync(deleteParams);

            return result;
        }
    }
}