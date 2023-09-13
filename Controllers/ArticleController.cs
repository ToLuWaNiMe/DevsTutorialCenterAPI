using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DevsTutorialCenterAPI.Controllers
{
    [Route("articles")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [Authorize]
        [HttpPost("create-article")]
        public async Task<IActionResult> CreateArticle([FromBody]CreateArticleDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdArticle = await _articleService.CreateArticleAsync(model);
            if (createdArticle != null) 
            {
                var response = new ResponseDto<CreateArticleDto>
                {
                    Code = (int)HttpStatusCode.OK,
                    Data = createdArticle,
                    Message = "Article Created Successfully",
                    Error = string.Empty
                };

                return Ok(response);
            }
            else
            {
                var response = new ResponseDto<CreateArticleDto>
                {
                    Code = (int)HttpStatusCode.BadRequest,
                    Data = null,
                    Message = "Failed to create new Article",
                    Error = string.Empty
                };
               
                return BadRequest(response);
            }

        }
    }
}
