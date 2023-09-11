
using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Services.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevsTutorialCenterAPI.Controllers
{
       // [Authorize]
        [ApiController]
        [Route("api/articles")]
        public class ArticleController : ControllerBase
        {
            private readonly IReportArticleService _reportArticleService;

            public ArticleController(IReportArticleService reportArticleService)
            {
                _reportArticleService = reportArticleService;
            }

            [HttpPatch("report")]
            public async Task<IActionResult> ReportArticle(string articleId,[FromForm] ReportArticleRequestDto request)
            {
                var response = await _reportArticleService.ReportArticleAsync(articleId, request);

                if (response.Code == 200)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response);
                }
            }
        }
    }

