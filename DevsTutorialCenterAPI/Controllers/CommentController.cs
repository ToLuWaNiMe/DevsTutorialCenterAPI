using System.Net;
using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DevsTutorialCenterAPI.Controllers;

[ApiController]
[Route("api/comments")]
//[Authorize]
public class CommentController : ControllerBase
{
    private readonly ICommentService _commentService;
    private readonly SignInManager<AppUser> _signInManager;

    public CommentController(ICommentService commentService, SignInManager<AppUser> signInManager)
    {
        _commentService = commentService;
        _signInManager = signInManager;
    }

    [HttpPost("add-comment/{articleId}")]
    public async Task<IActionResult> CreateComment(string articleId, [FromBody] CreateCommentDto commentDto)
    {
        var response = new ResponseDto<CreateCommentDto>();

        var user = await _signInManager.UserManager.GetUserAsync(User);

        if (!ModelState.IsValid)
        {
            response.Code = 400;
            response.Message = "Invalid model state";
            response.Data = null;
            response.Error = string.Empty;

            return BadRequest(response);
        }


        try
        {
            var createdComment = await _commentService.CreateCommentAsync(articleId, user.Id, commentDto);
            if (createdComment == null)
            {
                response.Code = 400;
                response.Message = "Comment not added";
                response.Data = null;
                response.Error = "";
                return BadRequest(response);
            }

            response.Code = 200;
            response.Message = "Ok";
            response.Data = createdComment;
            response.Error = "";
            response.IsSuccessful = true;
        }
        catch (Exception ex)
        {
            response.Code = 500;
            response.Message = "Error";
            response.Data = null;
            response.Error = ex.Message;

            return BadRequest(response);
        }

        return Ok(response);
    }

    [HttpPut("update-comment/{commentId}")]
    public async Task<IActionResult> UpdateComment( string commentId, [FromBody] CommentDto commentDto)
    {
        var response = new ResponseDto<UpdateCommentDto>();

        var user = await _signInManager.UserManager.GetUserAsync(User);

        if (!ModelState.IsValid)
        {
            response.Code = 400;
            response.Message = "Invalid model state";
            response.Data = null;
            response.Error = string.Empty;

            return BadRequest(response);
        }

        try
        {
            var updateComment = await _commentService.UpdateCommentAsync(commentId,user.Id, commentDto);
            if (updateComment == null)
            {
                response.Code = 400;
                response.Message = "Comment not updated";
                response.Data = null;
                response.Error = "";
                return BadRequest(response);
            }

            response.Code = 200;
            response.Message = "Ok";
            response.Data = updateComment;
            response.Error = "";
            response.IsSuccessful = true;
        }
        catch (Exception ex)
        {
            response.Code = 500;
            response.Message = "Error";
            response.Data = null;
            response.Error = ex.Message;

            return BadRequest(response);
        }

        return Ok(response);
    }

    [HttpDelete("delete-comment/{commentId}")]
    public async Task<IActionResult> DeleteComment(string commentId)
    {
        var response = new ResponseDto<object>();

        var user = await _signInManager.UserManager.GetUserAsync(User);

        try
        {
            var isDeleted = await _commentService.DeleteCommentAsync(commentId, user.Id);
            if (!isDeleted)
            {
                response.Code = 400;
                response.Message = "Comment not deleted";
                response.Data = null;
                response.Error = "";

                return BadRequest(response);
            }

            response.Code = 200;
            response.Message = "deleted successfully";
            response.Data = true;
            response.Error = "";
            response.IsSuccessful = true;
        }
        catch (Exception ex)
        {
            response.Code = 500;
            response.Message = "Error";
            response.Data = false;
            response.Error = ex.Message;

            return BadRequest(response);
        }

        return Ok(response);
    }

    [HttpGet("get-comments/{articleId}")]
    public async Task<IActionResult> GetCommentsByArticle(string articleId)
    {
        var response = new ResponseDto<object>();

        try
        {
            var comments = await _commentService.GetCommentsByArticle(articleId);
            
            response.Code = (int)HttpStatusCode.OK;
            response.Message = "Comments found";
            response.Data = comments;
            response.Error = "";
            response.IsSuccessful = true;

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

    [HttpGet("{commentId}/get-likes")]
    public async Task<IActionResult> GetLikesByCommentId([FromRoute] string commentId)
    {
        try
        {
            var likes = await _commentService.GetLikesByCommentsAsync(commentId);

            var response = new ResponseDto<List<LikesByCommentsDto>>();

            if (likes.Count == 0)
            {
                response.Code = 404;
                response.Message = "Not Found";
                response.Data = likes;
                response.Error = "Likes not found for the specified comment.";
                return NotFound(response);
            }

            response.Code = 200;
            response.Message = "OK";
            response.Data = likes;
            response.Error = "";
            response.IsSuccessful = true;

            return Ok(response);
        }
        catch (Exception ex)
        {
            var errorResponse = new ResponseDto<object>
            {
                Code = 500,
                Message = "Internal Server Error",
                Data = null,
                Error = ex.Message
            };

            return StatusCode(500, errorResponse);
        }
    }

    [HttpPost("like-comment/{commentId}")]

    public async Task<IActionResult> LikeAComment(string commentId)
    {
        var response = new ResponseDto<string>();

        var user = await _signInManager.UserManager.GetUserAsync(User);

        try
        {
            var like = await _commentService.LikeComment(commentId, user.Id);
           

            response.Code = 200;
            response.Message = "Comment liked";
            response.Data = like;
            response.Error = "";
            response.IsSuccessful = true;
        }
        catch (Exception ex)
        {
            response.Code = 500;
            response.Message = "Error";
            response.Data = null;
            response.Error = ex.Message;

            return BadRequest(response);
        }

        return Ok(response);
    }
}