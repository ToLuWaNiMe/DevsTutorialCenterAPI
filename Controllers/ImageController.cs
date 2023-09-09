using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Data.Repositories.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DevsTutorialCenterAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/images")]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;
        private readonly IArticleRepository _articleRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public ImageController(IImageService imageService, IArticleRepository articleRepository, UserManager<IdentityUser> userManager)
        {
            _imageService = imageService;
            _articleRepository = articleRepository;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage([FromForm] ImageUploadRequestDto requestDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {

                    return BadRequest(new ResponseDto<object>
                    {

                        Code = 401,
                        Message = "Unauthorized",
                        Data = null,
                        Error = "User is not authenticated."

                    });
                } 
                // Get the currently authenticated user
                var user = await _userManager.GetUserAsync(User);

                if (user == null)
                {
                    return Unauthorized(new ResponseDto<object>
                    {
                        Code = 401,
                        Message = "Unauthorized",
                        Data = null,
                        Error = "User is not authenticated."
                    });
                }

                // Fetch the article by the provided articleId
                var article = await _articleRepository.GetByArticleId(requestDto.ArticleId);

                // Check if the article exists and if the user owns it
                if (article == null || article.UserId != user.Id)
                {
                    return StatusCode(403, new ResponseDto<object>
                    {
                        Code = 403,
                        Message = "Forbidden",
                        Data = null,
                        Error = "You are not authorized to upload an image for this article."
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
                    // Get the currently authenticated user
                    var user = await _userManager.GetUserAsync(User);

                    if (user == null)
                    {
                        return Unauthorized(new ResponseDto<object>
                        {
                            Code = 401,
                            Message = "Unauthorized",
                            Data = null,
                            Error = "User not found."
                        });
                    }

                // Check if the user uploaded the image with the given publicId
                var article = await _articleRepository.GetByPublicId(publicId);
                if (article == null || article.UserId != user.Id)
                    {
                        return Unauthorized(new ResponseDto<object>
                        {
                            Code = 401,
                            Message = "Unauthorized",
                            Data = null,
                            Error = "You are not authorized to delete this image."
                        });
                    }

                    var deleteResult = await _imageService.DeleteImageAsync(publicId);

                    if (deleteResult.Result == "ok")
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
                            Error = "Failed to delete image"
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

