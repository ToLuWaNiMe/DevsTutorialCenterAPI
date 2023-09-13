using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DevsTutorialCenterAPI.Controllers
{
    [Authorize]
    [Route("api/comments")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
       private readonly ICommentService _commentService;
       public CommentsController(ICommentService commentService)
       {
           _commentService = commentService;
       }
       // DELETE endpoint for deleting a comment
       [Authorize]
       [HttpDelete("{commentId}")]
       public async Task<IActionResult> DeleteComment(string commentId)
       {
           var response = new ResponseDto<object>();

           try
           {
               var isDeleted = await _commentService.DeleteCommentAsync(commentId);
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
               response.Data = "";
               response.Error = "";
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

}

