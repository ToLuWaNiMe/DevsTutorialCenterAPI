using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevsTutorialCenterAPI.Controllers
{
    [ApiController]
    [Route("api/images")]
  //  [Authorize]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;

        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage([FromForm] ImageUploadRequestDto requestDto)
        {
            try
            {
                var uploadResult = await _imageService.AddImageAsync(requestDto.Photo);

                if (uploadResult.Error != null)
                {
                    return BadRequest(new ResponseDto<object>
                    {
                        Code = 400,
                        Message = "Error",
                        Data = null,
                        Error = uploadResult.Error.Message
                    });
                }

                return Ok(new ResponseDto<object>
                {
                    Code = 200,
                    Message = "Ok",
                    Data = new
                    {
                        PublicId = uploadResult.PublicId,
                        Url = uploadResult.SecureUri.AbsoluteUri
                    },
                    Error = "",
                    IsSuccessful = true
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseDto<object>
                {
                    Code = 400,
                    Message = "Error",
                    Data = null,
                    Error = "Failed to upload image: " + ex.Message
                });
            }
        }

        [HttpDelete("{publicId}")]
        public async Task<IActionResult> DeleteImage(string publicId)
        {
            try
            {
                var deletionResult = await _imageService.DeleteImageAsync(publicId);

                if (deletionResult.Result == "ok")
                {
                    return Ok(new ResponseDto<object>
                    {
                        Code = 200,
                        Message = "Ok",
                        Data = "",
                        Error = "",
                        IsSuccessful = true
                    });
                }

                return BadRequest(new ResponseDto<object>
                {
                    Code = 400,
                    Message = "Error",
                    Data = null,
                    Error = "Failed to delete image from Cloudinary."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseDto<object>
                {
                    Code = 400,
                    Message = "Error",
                    Data = null,
                    Error = "Failed to delete image: " + ex.Message
                });
            }
        }
    }
}
