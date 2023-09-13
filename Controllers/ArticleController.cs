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

        [HttpPost("report-article")]
        public async Task<ActionResult<ResponseDto<object>>> ReportArticle([FromBody] ReportArticleRequestDto request, string articleId)
        {
            // validate to check if model is valid
            if(!ModelState.IsValid)
            {
                return BadRequest(new ResponseDto<object>
                {
                    Data = null,
                    Message = "Validation failed",
                    Code = 500,


                }) ;   
            }



            var response = await _reportArticleService.AddArticleReportAsync(request, articleId);

            return Ok(new ResponseDto<object>
            {
                Data = response,
                Code = 200,
                Message = "Ok",
                Error = ""
            });

            
        }
    }
}

/*
        -  Add a new entity for ReportArticle
            - Id
            - text
            - articleId
            - reportedBy

        -  Add it to Dbcontext
        -  run a migration to add it to the database
        -  Your dto should be of this shape
            - text
            - articleId
            - reportedBy
 */
