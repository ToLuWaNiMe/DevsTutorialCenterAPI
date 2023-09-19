using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace DevsTutorialCenterAPI.Controllers;

// [Authorize]
[ApiController]
[Route("api/images")]
public class ImageController : ControllerBase
{
    private readonly IArticleService _articleService;
    private readonly IImageService _imageService;


    public ImageController(IImageService imageService, IArticleService articleService)
    {
        _imageService = imageService;
        _articleService = articleService;
    }

    [HttpPost]
    public async Task<IActionResult> UploadImage([FromForm] ImageUploadRequestDto requestDto)
    {
        try
        {
            var article = await _articleService.GetArticleById(requestDto.ArticleId);


            if (article == null)
                return StatusCode(403, new ResponseDto<object>
                {
                    Code = 403,
                    Message = "Error",
                    Data = null,
                    Error = "No article was found with the given id."
                });

            var uploadResult = await _imageService.AddImageAsync(requestDto.Photo);

            if (uploadResult.Error != null)
                return BadRequest(new ResponseDto<object>
                {
                    Code = 400,
                    Message = "Error",
                    Data = null,
                    Error = uploadResult.Error.Message
                });


            article.PublicId = uploadResult.PublicId;
            article.ImageUrl = uploadResult.SecureUrl.AbsoluteUri;

            await _articleService.UpdateArticleAsync(article);

            return Ok(new ResponseDto<object>
            {
                Code = 200,
                Message = "Ok",
                Data = new
                {
                    uploadResult.PublicId,
                    Url = uploadResult.SecureUrl.AbsoluteUri
                },
                Error = ""
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
                return Ok(new ResponseDto<object>
                {
                    Code = 200,
                    Message = "Ok",
                    Data = "",
                    Error = ""
                });
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