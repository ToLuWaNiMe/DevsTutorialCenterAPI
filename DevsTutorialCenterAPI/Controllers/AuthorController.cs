using System.Threading.Tasks;
using DevsTutorialCenterAPI.Data.Repositories;
using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Services.Abstractions;
using DevsTutorialCenterAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevsTutorialCenterAPI.Controllers
{
   // [Authorize]
    [ApiController]
    [Route("api/authors")]
    public class AuthorController : ControllerBase
    {
       // private readonly IAuthorStatsService _authorStatsService;
        private readonly IRepository _repository;
        private readonly IArticleService _articleService;

        public AuthorController( IRepository repository, IArticleService articleService)
        {
            //_authorStatsService = authorStatsService;
            _repository = repository;
            _articleService = articleService;
        }

        [HttpGet("author-stats")]
        public async Task<IActionResult> GetAuthorStats()
        {
            var authorStats = await _articleService.GetAuthorStats();

            if (authorStats == null)
            {
                return BadRequest(new ResponseDto<List<AuthorDTO>>
                {
                    Code = 500,
                    Data = null,
                    Message = "No authors found",
                    Error = "Failed"
                });
            }

            return Ok(new ResponseDto<List<AuthorDTO>>
            {
                Code = 200,
                Data = authorStats,
                Message = "Authors found",
                Error = "",
                IsSuccessful = true
            });
        }
    }
}
