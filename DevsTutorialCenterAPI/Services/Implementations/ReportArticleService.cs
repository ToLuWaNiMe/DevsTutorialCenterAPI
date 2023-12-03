using DevsTutorialCenterAPI.Data;
using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Data.Repositories;
using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Models.Enums;
using DevsTutorialCenterAPI.Services.Abstractions;
using DevsTutorialCenterAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DevsTutorialCenterAPI.Services.Implementations;

public class ReportArticleService : IReportArticleService
{
    private readonly IRepository _repository;
    private readonly DevsTutorialCenterAPIContext _devsTutorialCenterAPIContext;
    private readonly ILikeService _likeService;
    private readonly ICommentService _commentService;
    public ReportArticleService(IRepository repository, DevsTutorialCenterAPIContext devsTutorialCenterAPIContext, ILikeService likeService, ICommentService commentService)
    {
        _repository = repository;
        _devsTutorialCenterAPIContext = devsTutorialCenterAPIContext;
        _likeService = likeService;
        _commentService = commentService;
    }

    public async Task<List<GetReportedArticleDTO>> GetReportedArticlesAsync()
    {
        var reportedArticles = await _repository.GetAllAsync2<ReportedArticle>();

        if(reportedArticles.Count() == 0) { throw new Exception("No reported articles found");}

        var getReportedArticles = new List<GetReportedArticleDTO>();

        foreach(var reportedArticle in reportedArticles)
        {
            var article = await _repository.GetByIdAsync<Article>(reportedArticle.ArticleId);
            if (article == null) continue;

            var articleApproval = await _devsTutorialCenterAPIContext.ArticleApprovals.FirstOrDefaultAsync(a => a.ArticleId == article.Id) ;

            if (articleApproval == null) continue;

            var articleLikes = await _likeService.GetLikesByArticle(article.Id);
            if (articleLikes == null) continue;

            var articleComments = await _commentService.GetCommentsByArticle(article.Id);
            if (articleComments == null) continue;

            var author = await _repository.GetByIdAsync<AppUser>(article.AuthorId); if (author == null) continue;

            var getReportedArticle = new GetReportedArticleDTO
            {
                ArticleId = article.Id,
                ArticleText = article.Text,
                ArticleTitle = article.Title,
                Author = $"{author.FirstName} {author.LastName}",
                NoOfComments = articleComments.Count(),
                NoOfLikes = articleLikes.Count(),
                PublishedOn = articleApproval.CreatedOn
            };

            getReportedArticles.Add(getReportedArticle);    
        }

        return getReportedArticles;
    }

    public async Task<List<GetReportedAuthorsDTO>> GetReportedAuthorsAsync()
    {
        var reportedArticles = await _repository.GetAllAsync2<ReportedArticle>();

        if (reportedArticles.Count() == 0) { throw new Exception("No reported articles found"); }

        var getReportedAuthors = new List<GetReportedAuthorsDTO>();

        foreach(var reportedArticle in reportedArticles)
        {
            var article = await _repository.GetByIdAsync<Article>(reportedArticle.ArticleId);
            if (article == null) continue;

            var author = await _repository.GetByIdAsync<AppUser>(article.AuthorId); if (author == null) continue;

            var articlesByAuthor = (await _repository.GetAllAsync2<Article>()).Where(a => a.AuthorId == author.Id);

            var reportedAuthorTimes = new List<ReportedArticle>();

            foreach(var articleReported in articlesByAuthor)
            {
                var rArticle = await _devsTutorialCenterAPIContext.ReportedArticles.FirstOrDefaultAsync(r => r.ArticleId == articleReported.Id);
                if (rArticle == null) continue;
                reportedAuthorTimes.Add(rArticle);
            }

            var reportedAuthor = new GetReportedAuthorsDTO
            {
                Name = $"{author.FirstName} {author.LastName}",
                ImageUrl = author.ImageUrl,
                Stack = author.Stack,
                NoOfArticles = articlesByAuthor.Count(),
                NoOfReports = reportedAuthorTimes.Count()
            };

            getReportedAuthors.Add(reportedAuthor);
        }

        return getReportedAuthors;
    }

    public async Task<ReportedArticle> ReportArticle(CreateReportDTO createReportDTO, string userId)
    {
        var article = await _repository.GetByIdAsync<Article>(createReportDTO.ArticleId);

        if(article == null)
        {
            throw new Exception("This article cannot be found");
        }

        var user = await _repository.GetByIdAsync<AppUser>(userId);

        if(user == null)
        {
            throw new Exception("This user record not found");
        }

        var articleApproval = await _devsTutorialCenterAPIContext.ArticleApprovals.FirstOrDefaultAsync(a => a.ArticleId == article.Id);
        if (articleApproval == null) throw new Exception("Not in approval list");
        if (articleApproval.Status != ApprovalStatusConstant.IsPublished)
        {
            throw new Exception("This article has not been published yet");
        }
            

        var reportedArticle = new ReportedArticle
        {
            ArticleId = article.Id,
            ReportText = createReportDTO.Text,
            ReportedBy = $"{user.FirstName} {user.LastName}",
            CreatedOn = DateTime.UtcNow,
            UpdatedOn = DateTime.UtcNow

        };

        await _repository.AddAsync<ReportedArticle>(reportedArticle);

        return reportedArticle;
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