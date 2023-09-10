using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevsTutorialCenterAPI.Controllers
{
    [ApiController]
    [Route("api/comments")]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }
        
        
        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] CommentDTO commentDTO)
        {
            if(commentDTO == null) 
            {
                var response = new ResponseDto<object>
                {
                Code = 400,
                Message = "Error",
                Data = null,
                Error = "Comment is so empty."
                };
                return BadRequest(response);
            }

            if (string.IsNullOrEmpty(commentDTO.UserId)) 
            { 
                return BadRequest("User ID cannot be empty");
            }

            try
            {
               
                var createdComment = await _commentService.CreateCommentAsync(commentDTO);
                var response = new ResponseDto<object>
                
                {
                    Code = 200,
                    Message = "Ok",
                    Data = new { commentId = createdComment.ToString() },
                    Error = ""
                };
                return Ok(response);
            }
            catch (Exception)
            {
                var response = new ResponseDto<object> 
                {
                Code=400,
                Message= "Error",
                Data = null,
                Error="Failed to add comment"
                };
                return BadRequest("Failed to add comment.");
            }
            
        }

    }
}
