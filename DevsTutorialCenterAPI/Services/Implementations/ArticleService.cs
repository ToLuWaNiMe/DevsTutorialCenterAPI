using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Data.Repositories;
using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Models.Enums;
using DevsTutorialCenterAPI.Services.Abstractions;
using DevsTutorialCenterAPI.Utilities;

namespace DevsTutorialCenterAPI.Services.Implementations;

public class ArticleService : IArticleService
{
    private readonly IRepository _repository;

    public ArticleService(IRepository repository)
    {
        _repository = repository;
    }


    public async Task<Article> GetArticleById(string articleId)
    {
        return await _repository.GetByIdAsync<Article>(articleId);
    }

    public async Task UpdateArticleAsync(Article article)
    {
        if (article == null) throw new ArgumentNullException(nameof(article));

        await _repository.UpdateAsync(article);
    }


    public async Task<GetSingleArticleDto> GetSingleArticle(string articleId)
    {
        var article = await _repository.GetByIdAsync<Article>(articleId);

        if (article == null) throw new Exception($"Article with ID {articleId} not found.");

        var articleDto = new GetSingleArticleDto
        {
            Id = article.Id,
            PublicId = article.PublicId,
            UserId = article.UserId,
            Title = article.Title,
            Tag = article.Tag,
            Text = article.Text,
            ImageUrl = article.ImageUrl,
            IsPublished = article.IsPublished,
            IsRead = article.IsRead,
            IsRecommended = article.IsRecommended,
            IsReported = article.IsReported,
            IsSaved = article.IsSaved,
            CreatedOn = article.CreatedOn
        };

        return articleDto;
    }

    public async Task<CreateArticleDto> CreateArticleAsync(CreateArticleDto model)
    {
        string[] allowedTags = { "JAVA", ".NET", "NODE" };
        if (!allowedTags.Contains(model.Tag, StringComparer.OrdinalIgnoreCase))
            throw new ArgumentException("Invalid tag. Tag must either one of: JAVA, .NET, NODE.");
        var newArticle = new Article
        {
            Title = model.Title,
            Tag = model.Tag,
            Text = model.Text,
            IsRecommended = model.IsRecommended,
            IsTrending = model.IsTrending,
            UserId = model.UserId
        };

        await _repository.AddAsync(newArticle);

        var newArticleData = new CreateArticleDto
        {
            Title = newArticle.Title,
            Tag = newArticle.Tag,
            IsRecommended = newArticle.IsRecommended,
            IsTrending = newArticle.IsTrending,
            UserId = newArticle.UserId,
            Text = newArticle.Text
        };

        return newArticleData;
    }

    public async Task<PaginatorResponseDto<IEnumerable<GetAllArticlesDto>>> GetAllArticles(FilterArticleDto filters)
    {
        var authorIdFilter = !string.IsNullOrEmpty(filters.AuthorId);
        var tagFilter = !string.IsNullOrEmpty(filters.Tag);
        var isRecommendedFilter = filters.IsRecommended != null;
        var isSavedFilter = filters.IsSaved != null;
        var isReadFilter = filters.IsRead != null;
        var isReportedFilter = filters.IsReported != null;
        var isPublishedFilter = filters.IsPublished != null;

        var articles = await _repository.GetAllAsync<Article>();

        if (authorIdFilter) articles = articles.Where(a => a.UserId == filters.AuthorId);

        if (tagFilter) articles = articles.Where(a => a.Tag == filters.Tag.ToUpper());

        if (isRecommendedFilter) articles = articles.Where(a => a.IsRecommended);

        if (isSavedFilter) articles = articles.Where(a => a.IsSaved);

        if (isReadFilter) articles = articles.Where(a => a.IsRead);

        if (isReportedFilter) articles = articles.Where(a => a.IsReported);

        if (isPublishedFilter) articles = articles.Where(a => a.IsPublished);

        var articlesDto = articles.Select(a => new GetAllArticlesDto
        {
            Id = a.Id,
            UserId = a.UserId,
            Title = a.Title,
            Tag = a.Tag,
            Text = a.Text,
            ImageUrl = a.ImageUrl
        });

        var pageNum = filters.Page ?? 1;
        var pageSize = filters.Size ?? 10;

        var skipAmount = (pageNum - 1) * pageSize;

        var paginatorResponse = Helper.Paginate(articlesDto, pageNum, pageSize);

        return paginatorResponse;
    }


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

            if(article.IsReported && status == ArticleStatusReportEnum.Approved.ToString().ToLower())
            {
                throw new Exception("Article already approved");
            }

            if (!article.IsReported && status == ArticleStatusReportEnum.Declined.ToString().ToLower())
            {
                throw new Exception("Article already declined");
            }


            if (status == ArticleStatusReportEnum.Approved.ToString().ToLower())
            {
                article.IsReported = true;
            }


            else if (status == ArticleStatusReportEnum.Declined.ToString().ToLower())
            {
                article.IsReported= false;
            }

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
}