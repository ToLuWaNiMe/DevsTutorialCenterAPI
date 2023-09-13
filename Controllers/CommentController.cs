using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DevsTutorialCenterAPI.Controllers
{
    [Route("api/comments")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet("{articleId}")]
        public async Task<IActionResult> GetCommentsByArticle(string articleId)
        {
           
            var response = new ResponseDto<object>();

            try
            {
                var comments = await _commentService.GetCommentsByArticle(articleId);
      

                response.Code = (int)HttpStatusCode.OK;
                response.Message = $"Comments found";
                response.Data = comments;
                response.Error = "";

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Code = (int)HttpStatusCode.InternalServerError;
                response.Message = "Error";
                response.Data = null;
                response.Error = ex.Message;

                return BadRequest(response);
            }

        }

    }
}