
namespace DevsTutorialCenterAPI.Models.DTOs
{
    public class ImageUploadRequestDto
    {
        public IFormFile Photo { get; set; }
        public string ArticleId { get; set; }
    }
}
