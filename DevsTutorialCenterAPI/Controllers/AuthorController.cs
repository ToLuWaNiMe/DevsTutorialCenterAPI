using DevsTutorialCenterAPI.Data.Repositories;
using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace DevsTutorialCenterAPI.Controllers;

[ApiController]
[Route("api/authors")]
public class AuthorController : ControllerBase
{
    private readonly IRepository _repository;
    private readonly IArticleService _articleService;

    public AuthorController(IRepository repository, IArticleService articleService)
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

    [HttpGet("{authorId}/articles")]
    public async Task<IActionResult> GetAuthorArticles([FromRoute] string authorId)
    {
        var articles = await _articleService.GetAuthorArticles(authorId);

        return Ok(ResponseDto<object>.Success(articles));
    }
}