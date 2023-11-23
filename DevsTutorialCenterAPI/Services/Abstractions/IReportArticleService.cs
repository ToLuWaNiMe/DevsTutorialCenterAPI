using DevsTutorialCenterAPI.Models.DTOs;

namespace DevsTutorialCenterAPI.Services.Abstractions;

public interface IReportArticleService
{
    //Task<object> AddArticleReportAsync(ReportArticleRequestDto request, string articleId);

    Task<List<GetReportedArticleDTO>> GetReportedArticlesAsync();
    Task<List<GetReportedAuthorsDTO>> GetReportedAuthorsAsync();
}