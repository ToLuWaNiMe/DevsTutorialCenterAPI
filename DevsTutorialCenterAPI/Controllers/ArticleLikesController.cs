using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Data.Repositories;
using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DevsTutorialCenterAPI.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/article-likes")]
    public class ArticlesLikeController : ControllerBase
    {
        private readonly ILikeService _likeService;
        private readonly IRepository _repository;

        public ArticlesLikeController(ILikeService likeService, IRepository repository)
        {
            _likeService = likeService;
            _repository = repository;
        }

        [HttpPost("like")]
        public async Task<IActionResult> LikeArticle([FromBody] LikesByArticleDto dto)
        {




            if (!ModelState.IsValid)
            {
                return BadRequest(new ResponseDto<object>
                {
                    Data = null,
                    Code = 500,
                    Error = "Invalid data",
                    Message = "Failed"
                });
            }

                // Proceed to like the article if the user and article exist
                await _likeService.LikeArticleAsync(dto.ArticleId, dto.UserId);

                var response = new ResponseDto<string>
                {
                    Code = 200,
                    Message = "Article liked successfully",
                    Data = "Article liked successfully",
                    Error = string.Empty
                };

                return Ok(response);
            
             
            
               
            
        }

        [HttpDelete("unlike")]
        public async Task<IActionResult> UnlikeArticle([FromBody] LikesByArticleDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResponseDto<object>
                {
                    Data = null,
                    Code = 500,
                    Error = "Invalid data",
                    Message = "Failed"
                });
            }

            // Proceed to unlike the article if the user and article exist
            await _likeService.UnlikeArticleAsync(dto.ArticleId, dto.UserId);

            var response = new ResponseDto<string>
            {
                Code = 200,
                Message = "Article Unliked successfully",
                Error = string.Empty
            };

            return Ok(response);





        }

    }
}
