using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DevsTutorialCenterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        
        public async Task<IActionResult> GetCommentsByArticle(string articleId)
        {
           
            var response = new ResponseDto<object>();

            try
            {
                var comments = await _commentService.GetCommentsByArticle(articleId);

                var noOfComments = comments.Count();

                if (!comments.Any())
                {
                    response.Code = (int)HttpStatusCode.NoContent;
                    response.Message = $"no comments found for {articleId}";
                    response.Data = null;
                    response.Error = "";

                    return BadRequest(response);
                }

                response.Code = (int)HttpStatusCode.OK;
                response.Message = $"{noOfComments} Comments found";
                response.Data = comments;
                response.Error = "";
            }
            catch (Exception ex)
            {
                response.Code = (int)HttpStatusCode.InternalServerError;
                response.Message = "Error";
                response.Data = null;
                response.Error = ex.Message;

                return BadRequest(response);
            }

            return Ok(response);
        }

    }
}