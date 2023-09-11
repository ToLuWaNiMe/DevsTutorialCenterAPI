using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevsTutorialCenterAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class LikesController : ControllerBase
    {
        private readonly ILikesService _likesService;

        public LikesController(ILikesService likesService)
        {
            _likesService = likesService;
        }

        [HttpGet("{articleId}")]
        public async Task<IActionResult> GetLikesByArticleAsync(string articleId)
        {
            try
            {
                var likes = await _likesService.GetLikesByArticleAsync(articleId);

                var response = new ResponseDto<List<LikesByArticleDto>>
                {
                    Code = 200,
                    Message = "Ok",
                    Data = likes,
                    Error = ""
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseDto<object>
                {
                    Code = 500,
                    Message = "Internal Server Error",
                    Data = null,
                    Error = ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddLikesByArticleAsync([FromBody] ArticlesLikes articlesLikes)
        {


           await _likesService.LikeArticleAsync(articlesLikes.ArticleId, articlesLikes.UserId);

            return Ok();
        }

    }
}
