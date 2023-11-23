using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Data.Repositories;
using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Services.Abstractions;

namespace DevsTutorialCenterAPI.Services.Implementations;

public class ReportArticleService : IReportArticleService
{
    private readonly IRepository _repository;

    public ReportArticleService(IRepository repository)
    {
        _repository = repository;
    }

    public Task<List<GetReportedArticleDTO>> GetReportedArticlesAsync()
    {
        throw new NotImplementedException();
    }

    //public async Task<object> AddArticleReportAsync(ReportArticleRequestDto request, string articleId)
    //{
    //    try
    //    {
    //        var article = await _repository.GetByIdAsync<Article>(articleId);

    //        if (article == null) throw new Exception("This article cannot be found");


    //        if (article.IsReported) throw new Exception("This article has already been reported");


    //        var reportedarticle = new ReportArticle
    //        {
    //            ReportText = request.ReportText,
    //            ArticleId = article.Id,
    //            ReportedBy = request.ReportedBy
    //        };

    //        await _repository.AddAsync(reportedarticle);

    //        article.IsReported = true;

    //        await _repository.UpdateAsync(article);

    //        return reportedarticle;

    //        // add new article report to db
    //        // update isReported property of article in db
    //    }
    //    catch (Exception ex)
    //    {
    //        throw new Exception(ex.ToString());
    //    }
    //}
}