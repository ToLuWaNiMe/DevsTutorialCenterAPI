using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Data.Repositories;
using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DevsTutorialCenterAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/article-likes")]
    public class ArticlesLikeController : ControllerBase
    {
        private readonly ILikeService _likeService;
        private readonly IRepository _repository;
        private readonly SignInManager<AppUser> _signInManager;

        public ArticlesLikeController(ILikeService likeService, IRepository repository, SignInManager<AppUser> signInManager)
        {
            _likeService = likeService;
            _repository = repository;
            _signInManager = signInManager;
        }

        [HttpPost("like/{articleId}")]
        public async Task<IActionResult> LikeArticle(string articleId)
        {

            var user = await _signInManager.UserManager.GetUserAsync(User);


            if (!ModelState.IsValid)
            {
                return BadRequest(new ResponseDto<string>
                {
                    Data = null,
                    Code = 500,
                    Error = "Invalid data",
                    Message = "Failed"
                });
            }

                // Proceed to like the article if the user and article exist
                var data = await _likeService.LikeArticleAsync(articleId, user.Id);

                var response = new ResponseDto<string>
                {
                    Code = 200,
                    Message = "Article liked successfully",
                   Data = data,
                    Error = string.Empty,
                    IsSuccessful = true
                };

                return Ok(response);
            
             
            
               
            
        }

        [HttpDelete("unlike/{articleId}")]
        public async Task<IActionResult> UnlikeArticle(string articleId)
        {
            var user = await _signInManager.UserManager.GetUserAsync(User);
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
            var data = await _likeService.UnlikeArticleAsync(articleId, user.Id);

            var response = new ResponseDto<string>
            {
                Code = 200,
                Data = data,
                Message = "Article Unliked successfully",
                Error = string.Empty,
                IsSuccessful = true
            };

            return Ok(response);





        }

        [HttpGet("get-likes-for-article/{articleId}")]

        public async Task<IActionResult> GetLikesForArticleId(string articleId)
        {
            var data = await _likeService.GetLikesByArticle(articleId);

            if (!ModelState.IsValid)
            {
                return BadRequest(new ResponseDto<List<AppUserLikeDTO>>
                {
                    Data = null,
                    Code = 500,
                    Error = "Invalid data",
                    Message = "Failed"
                });
            }

            var response = new ResponseDto<List<AppUserLikeDTO>>
            {
                Code = 200,
                Data = data,
                Message = "Likes retrieved successfully",
                Error = string.Empty,
                IsSuccessful = true
            };

            return Ok(response);
        }

    }
}
