using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Models.DTOs;

namespace DevsTutorialCenterAPI.Services.Abstraction
{
    public interface IReportArticleService
    {
        Task<ResponseDto<ReportArticleRequestDto>> ReportArticleAsync(string articleId, ReportArticleRequestDto request);
    }
}
  

