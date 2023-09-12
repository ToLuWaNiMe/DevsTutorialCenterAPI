using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DevsTutorialCenterAPI.Models.DTOs;
using Microsoft.AspNetCore.Identity;
using DevsTutorialCenterAPI.Services.Abstractions;

namespace DevsTutorialCenterAPI.Controllers
{
   // [Authorize]
    [ApiController]
    [Route("api/images")]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;
        private readonly IArticleService _articleService;
     

        public ImageController(IImageService imageService, IArticleService articleService )
        {
            _imageService = imageService;
            _articleService = articleService;
         
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage([FromForm] ImageUploadRequestDto requestDto)
        {
            try
            {
                // Fetch the article by the provided articleId
                var article = await _articleService.GetArticleById(requestDto.ArticleId);

                // Check if the article exists
                if (article == null)
                {
                    return StatusCode(403, new ResponseDto<object>
                    {
                        Code = 403,
                        Message = "Error",
                        Data = null,
                        Error = "No article was found with the given id."
                    });
                }

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

                // Assign the publicId to the article's PublicId property
                article.PublicId = uploadResult.PublicId;

                // Update the article in the repository (assuming you have an Update method)
                await _articleService.UpdateArticleAsync(article);

                return Ok(new ResponseDto<object>
                {
                    Code = 200,
                    Message = "Ok",
                    Data = new
                    {
                        PublicId = uploadResult.PublicId,
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
                // Check if an article with the given public ID exists
                var article = await _articleService.GetArticleByPublicId(publicId);

                if (article == null)
                {
                    return StatusCode(400, new ResponseDto<object>
                    {
                        Code = 400,
                        Message = "Error",
                        Data = null,
                        Error = "No article was found with the given public ID."
                    });
                }

                // Delete the image by public ID
                var deleteImageResult = await _imageService.DeleteImageAsync(publicId);

                if (deleteImageResult.Result == "ok")
                {
                    return Ok(new ResponseDto<object>
                    {
                        Code = 200,
                        Message = "Ok",
                        Data = null,
                        Error = ""
                    });
                }
                else
                {
                    return BadRequest(new ResponseDto<object>
                    {
                        Code = 400,
                        Message = "Error",
                        Data = null,
                        Error = "Failed to delete the image."
                    });
                }
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

