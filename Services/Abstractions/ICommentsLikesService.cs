using System.Collections.Generic;
using System.Threading.Tasks;
using DevsTutorialCenterAPI.Models.DTOs;

namespace DevsTutorialCenterAPI.Services.Abstractions
{
    public interface ICommentsLikesService
    {
        Task<List<LikesByCommentsDto>> GetLikesByCommentsAsync(string commentId);

    }
}
