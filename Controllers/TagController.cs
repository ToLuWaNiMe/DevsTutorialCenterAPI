using DevsTutorialCenterAPI.Data.Entities;

using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DevsTutorialCenterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagRepository;

        public TagController(ITagService tagRepository)
        {
            _tagRepository = tagRepository;
        }




        [HttpPut("{id}")]
        public async Task<ResponseDto<Tag>> UpdateTag(string id, Tag updatedtag)
        {
            var tag = await _tagRepository.GetByIdAsync<Tag>(id.ToString());

            try
            {

                if (tag == null)
                {

                    return new ResponseDto<Tag>
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "Tag not found.",

                        Data = null
                    };
                }

                await _tagRepository.UpdateAsync(id, updatedtag);
                return new ResponseDto<Tag>
                {
                    Code = StatusCodes.Status204NoContent,
                    Message = "Tag deleted successfully.",
                    Data = tag
                };
            }


            catch (Exception ex)
            {

                return new ResponseDto<Tag>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Error = ex.Message,
                    Message = ex.Message,
                    Data = null
                };
            }



        }

        [HttpDelete("{id}")]
        public async Task<ResponseDto<Tag>> DeleteTag(string id)
        {
            try
            {
                var tag = await _tagRepository.GetByIdAsync<Tag>(id.ToString());

                if (tag == null)
                {

                    return new ResponseDto<Tag>
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "Tag not found.",

                        Data = null
                    };
                }

                await _tagRepository.Delete(tag);


                return new ResponseDto<Tag>
                {
                    Code = StatusCodes.Status204NoContent,
                    Message = "Tag deleted successfully.",
                    Data = tag
                };
            }
            catch (Exception ex)
            {

                return new ResponseDto<Tag>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Error = ex.Message,
                    Message = ex.Message,
                    Data = null
                };
            }
        }




        [HttpPost]
        public async Task<ActionResult<ResponseDto<Tag>>> AddTag(Tag newTag)
        {
            try
            {



                await _tagRepository.AddTagAsync(newTag);

                return new ResponseDto<Tag>
                {
                    Code = StatusCodes.Status201Created,
                    Message = "Tag added successfully.",
                    Data = newTag
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<Tag>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Error = ex.Message,
                    Message = "Error adding the tag.",
                    Data = null
                };
            }
        }

    }
}
