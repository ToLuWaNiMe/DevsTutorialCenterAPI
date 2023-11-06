//using System.Threading.Tasks;
//using DevsTutorialCenterAPI.Data.Repositories;
//using DevsTutorialCenterAPI.Models.DTOs;
//using DevsTutorialCenterAPI.Services.Interfaces;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;

//namespace DevsTutorialCenterAPI.Controllers
//{
//    [Authorize]
//    [ApiController]
//    [Route("api/authors")]
//    public class AuthorController : ControllerBase
//    {
//        private readonly IAuthorStatsService _authorStatsService;
//        private readonly IRepository _repository;

//        public AuthorController(IAuthorStatsService authorStatsService, IRepository repository)
//        {
//            _authorStatsService = authorStatsService;
//            _repository = repository;
//        }

//        [HttpGet("{authorId}/stats")]
//        public async Task<IActionResult> GetAuthorStats(string authorId)
//        {
//            var authorStats = await _authorStatsService.GetAuthorStatsAsync(authorId);

//            if (authorStats == null)
//            {
//                return NotFound($"Author with ID {authorId} not found.");
//            }

//            return Ok(authorStats);
//        }
//    }
//}
