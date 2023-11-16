using DevsTutorialCenterAPI.Data;
using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Data.Repositories;
using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Models.Enums;
using DevsTutorialCenterAPI.Services.Abstractions;
using DevsTutorialCenterAPI.Utilities;
using static System.Net.Mime.MediaTypeNames;

namespace DevsTutorialCenterAPI.Services.Implementations;

public class ArticleService : IArticleService
{
    private readonly IRepository _repository;
    private readonly IArticleApprovalService _articleApprovalService;
    private readonly ITagService _tagService;
    private readonly DevsTutorialCenterAPIContext _db;

    public ArticleService(IRepository repository, IArticleApprovalService articleApprovalService, ITagService tagService, DevsTutorialCenterAPIContext db)
    {
        _repository = repository;
        _articleApprovalService = articleApprovalService;
        _tagService = tagService;
        _db = db;
    }


    public async Task<Article> GetArticleById(string articleId)
    {
        return await _repository.GetByIdAsync<Article>(articleId);
    }

    //DONE
    public async Task<UpdateArticleDto> UpdateArticleAsync(string articleId, UpdateArticleDto updatedArticle)
    {

        var existingArticle = await _repository.GetByIdAsync<Article>(articleId);

        if (existingArticle == null)
        {
            throw new Exception($"Article with ID {articleId} not found.");
        }

        existingArticle.Title = updatedArticle.Title ?? existingArticle.Title;
        existingArticle.TagId = updatedArticle.TagId ?? existingArticle.TagId;
        existingArticle.Text = updatedArticle.Text ?? existingArticle.Text;
        existingArticle.ImageUrl = updatedArticle.ImageUrl ?? existingArticle.ImageUrl;


        await _repository.UpdateAsync<Article>(existingArticle);

        var updatedArticleDto = new UpdateArticleDto
        {
            Title = existingArticle.Title,
            TagId = existingArticle.TagId,
            Text = existingArticle.Text,
            ImageUrl = existingArticle.ImageUrl
        };

        return updatedArticleDto;

    }

    //DONE (GOES WITH GETSINGLE ARTICLE)
    public async Task LogArticleReadAsync(string articleId, string userId)
    {
        var articleRead = new ArticleRead
        {
            UserId = userId,
            ArticleId = articleId
        };

        await _repository.AddAsync<ArticleRead>(articleRead);
    }

    public async Task<GetSingleArticleDto> GetSingleArticle(string articleId, string userId)
    {
        var article = await _repository.GetByIdAsync<Article>(articleId);

        if (article == null) throw new Exception($"Article with ID {articleId} not found.");

        var user = await _repository.GetByIdAsync<AppUser>(userId);

        if (user == null) throw new Exception($"User with ID {userId} not found.");

        await LogArticleReadAsync(article.Id, user.Id);

        var articleDto = new GetSingleArticleDto
        {
            Id = article.Id,
            PublicId = article.PublicId,
            AuthorId = article.AuthorId,
            Title = article.Title,
            TagId = article.TagId,
            Text = article.Text,
            ImageUrl = article.ImageUrl,
            ReadCount = article.ReadCount,
            ReadTime = article.ReadTime,
            CreatedOn = article.CreatedOn,

        };

        return articleDto;
    }


    //DONE
    public async Task<CreateArticleDto> CreateArticleAsync(CreateArticleDto model)
    {

        //var readtimeresult = Helper.CalculateReadingTime(model.Text);



        var tag = await _tagService.GetByIdAsync<ArticleTag>(model.TagId);

        if (tag == null)
        {
            throw new Exception("Tag  not found.");
        }

        var newArticle = new Article
        {
            Title = model.Title,
            TagId = tag.Id,
            Text = model.Text,
            ImageUrl = model.ImageUrl,

        };


        await _repository.AddAsync<Article>(newArticle);
        var articleApproval = new ArticleApproval
        {
            ArticleId = newArticle.Id,
            Status = SD.pending
        };

        await _articleApprovalService.ApproveAsync(articleApproval);

        var newArticleData = new CreateArticleDto
        {
            Title = newArticle.Title,
            TagId = newArticle.TagId,
            Text = newArticle.Text,
            ImageUrl = newArticle.ImageUrl
        };

        return newArticleData;
    }

    //public async Task<PaginatorResponseDto<IEnumerable<GetAllArticlesDto>>> GetAllArticles(FilterArticleDto filters)
    //{
    //    var authorIdFilter = !string.IsNullOrEmpty(filters.AuthorId);
    //    var tagFilter = !string.IsNullOrEmpty(filters.Tag);
    //    var isRecommendedFilter = filters.IsRecommended != null;
    //    var isSavedFilter = filters.IsSaved != null;
    //    var isReadFilter = filters.IsRead != null;
    //    var isReportedFilter = filters.IsReported != null;
    //    var isPublishedFilter = filters.IsPublished != null;
    //    var isTrendingFilter = filters.IsTrending != null;
    //    var isDraftFilter = filters.IsDraft != null;
    //    var isPendingFilter = filters.IsPending != null;



    //    var articles = await _repository.GetAllAsync<Article>();

    //    if (authorIdFilter) articles = articles.Where(a => a.UserId == filters.AuthorId);

    //    if (tagFilter) articles = articles.Where(a => a.Tag == filters.Tag.ToUpper());

    //    if (isRecommendedFilter) articles = articles.Where(a => a.IsRecommended);

    //    if (isSavedFilter) articles = articles.Where(a => a.IsSaved);

    //    if (isReadFilter) articles = articles.Where(a => a.IsRead);

    //    if (isReportedFilter) articles = articles.Where(a => a.IsReported);

    //    if (isPublishedFilter) articles = articles.Where(a => a.IsPublished);
    //    if (isTrendingFilter) articles = articles.Where(a => a.IsTrending);
    //    if (isDraftFilter) articles = articles.Where(a => a.IsDraft);
    //    if (isPendingFilter) articles = articles.Where(a => a.IsPending);

    //    var articlesDto = articles.Select(a => new GetAllArticlesDto
    //    {
    //        Id = a.Id,
    //        UserId = a.UserId,
    //        Title = a.Title,
    //        Tag = a.Tag,
    //        Text = a.Text,
    //        ImageUrl = a.ImageUrl,
    //        CreatedOn = a.CreatedOn,
    //        ReadTime = a.ReadTime,
    //        PublishedOn = a.PublishedOn,

    //    });

    //    var pageNum = filters.Page ?? 1;
    //    var pageSize = filters.Size ?? 10;

    //    var skipAmount = (pageNum - 1) * pageSize;

    //    var paginatorResponse = Helper.Paginate(articlesDto, pageNum, pageSize);

    //    return paginatorResponse;
    //}


    public async Task<bool> SetArticleReportStatus(string articleId, string status)
    {
        if (status != ArticleStatusReportEnum.Approved.ToString().ToLower() && status != ArticleStatusReportEnum.Declined.ToString().ToLower())
        {
            throw new Exception("Invalid status provided");
        }

        var article = await _repository.GetByIdAsync<Article>(articleId);

        if (article == null)
        {
            throw new Exception("Article not found");
        }

        //if(article.IsReported && status == ArticleStatusReportEnum.Approved.ToString().ToLower())
        //{
        //    throw new Exception("Article already approved");
        //}

        //if (!article.IsReported && status == ArticleStatusReportEnum.Declined.ToString().ToLower())
        //{
        //    throw new Exception("Article already declined");
        //}


        //if (status == ArticleStatusReportEnum.Approved.ToString().ToLower())
        //{
        //    article.IsReported = true;
        //}


        //else if (status == ArticleStatusReportEnum.Declined.ToString().ToLower())
        //{
        //    article.IsReported= false;
        //}

        await _repository.UpdateAsync<Article>(article);

        return true;
    }


    public async Task<bool> DeleteArticleAsync(string articleId)
    {
        var article = await _repository.GetByIdAsync<Article>(articleId);

        await _repository.DeleteAsync(article);
        return true;
    }

    public async Task<List<LikesByArticleDto>> GetLikesByArticleAsync(string articleId)
    {
        // Retrieve likes by article ID using the repository
        var likes = await _repository.GetAllAsync<ArticlesLikes>();

        // Filter the likes by the specified articleId
        var likesWithArticleId = likes
            .Where(like => like.ArticleId == articleId)
            .Select(like => new LikesByArticleDto
            {
                UserId = like.UserId,
                ArticleId = like.ArticleId
            })
            .ToList();

        if (!likesWithArticleId.Any()) return new List<LikesByArticleDto>();

        return likesWithArticleId;
    }

    public async Task<AuthorsStatsDto> GetAuthorStatsAsync(FetchAuthorsStatsDto fetchAuthorsStatsDto)
    {
        var authorStats = new List<AuthorStatsDto>();

        foreach (var authorId in fetchAuthorsStatsDto.AuthorIdList)
        {
            var query = await _repository.GetAllAsync<Article>();
            query = query.Where(a => a.AuthorId == authorId);

            var authorStat = new AuthorStatsDto
            {
                AuthorId = authorId,
                TotalNumOfArticles = query.Count(),
                // TotalReportedArticles = query.Where(a => a.IsReported).Count()
            };

            authorStats.Add(authorStat);
        }

        return new AuthorsStatsDto
        {
            AuthorStatsDtos = authorStats
        };
    }

    public async Task<IEnumerable<Article>> GetAllArticle()
    {
        var articles = await _repository.GetAllAsync<Article>();
        return articles;
    }

    public async Task<bool> IsArticleBookmarkedByUser(string articleId, string userId)
    {

        var bookmarkedArticle = await _repository.GetAllAsync<ArticleBookMark>();

        bool isBookmarked = bookmarkedArticle.Any(b => b.ArticleId == articleId && b.UserId == userId);

        return isBookmarked;
    }

    public async Task<List<GetPendingArticlesDto>> GetPendingArticles()
    {
        //var filter = (await _repository.GetAllAsync<ArticleApproval>()).Where(x => x.Status == SD.pending);
        var articleApprovals = await _repository.GetAllAsync<ArticleApproval>();

        var filter = articleApprovals.Where(x => x.Status == 1);

        if (filter == null)
        {
            throw new ArgumentNullException("No pending articles");
        }

        var getAllPendingArticles = new List<GetPendingArticlesDto>();

        foreach (var article in filter)
        {
            var foundArticle = await GetArticleById(article.ArticleId);
            var getPendingArticle = new GetPendingArticlesDto
            {
                Title = foundArticle is not null ? foundArticle.Title : null,
                Text = foundArticle is not null ? foundArticle.Text : null,
                TagId = foundArticle is not null ? foundArticle.TagId : null,
                ImageUrl = foundArticle is not null ? foundArticle.ImageUrl : null,
            };

            getAllPendingArticles.Add(getPendingArticle);
        }


        return getAllPendingArticles;
    }

}