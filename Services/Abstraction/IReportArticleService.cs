using DevsTutorialCenterAPI.Models.DTOs;

namespace DevsTutorialCenterAPI.Services.Abstraction;

public interface IReportArticleService
{
    Task<object> AddArticleReportAsync(ReportArticleRequestDto request, string articleId);
}