﻿using DevsTutorialCenterAPI.Models.DTOs;
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
        
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] CommentDTO commentDTO)
        {
            var response = new ResponseDto<object>();

            if(!ModelState.IsValid) 
            {
                response.Code = 400;
                response.Message = "Invalid model state";
                response.Data = null;
                response.Error = string.Empty;
                
                return BadRequest(response);
            }



            try
            {
               
                var createdComment = await _commentService.CreateCommentAsync(commentDTO);
                if(createdComment == null) 
                {
                    response.Code = 400;
                    response.Message = "Comment not added";
                    response.Data = null;
                    response.Error = "";
                    return BadRequest(response);
                }

                response.Code = 200;
                response.Message = "Ok";
                response.Data = new { commentId = createdComment.Id };
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

        //[Authorize]
        [HttpPut("update/{commentId}")]
        public async Task <IActionResult> UpdateComment([FromRoute]string commentId, [FromBody] CommentDTO commentDTO)
        {
            var response = new ResponseDto<object>();

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

                var updateComment = await _commentService.UpdateCommentAsync(commentId, commentDTO);
                if (!updateComment )
                {
                    response.Code = 400;
                    response.Message = "Comment not updated";
                    response.Data = null;
                    response.Error = "";
                    return BadRequest(response);
                }

                response.Code = 200;
                response.Message = "Ok";
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
