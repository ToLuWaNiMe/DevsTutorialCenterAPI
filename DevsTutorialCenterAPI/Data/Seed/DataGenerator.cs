using Bogus;
using DevsTutorialCenterAPI.Data.Entities;

namespace DevsTutorialCenterAPI.Data.Seed;

public class DataGenerator
{
    public readonly IEnumerable<Article> Articles;
    public readonly IEnumerable<AppUser> Users;
    public readonly IEnumerable<ArticleTag> Tags;
    public readonly IEnumerable<ArticleApproval> ArticleApprovals;
    public readonly IEnumerable<ArticleRead> ArticleReads;
    public readonly IEnumerable<ArticleLike> ArticleLikes;
    public readonly IEnumerable<Comment> Comments;
    public readonly IEnumerable<ReportedArticle> ReportedArticles;
    private readonly string[] _stacks;

    public DataGenerator()
    {
        Randomizer.Seed = new Random(123);
        _stacks = new[] { ".NET", "JAVA", "NODE" };
        
        Users = GenerateUsers(10);
        Tags = GenerateTags();
        Articles = GenerateArticles(100);
        ArticleApprovals = GenerateArticleApproval();
        ArticleReads = GenerateArticleReads(90);
        ArticleLikes = GenerateArticleLikes(50);
        Comments = GenerateComments(90);
        ReportedArticles = GenerateReportedArticle(10);
    }

    private IEnumerable<AppUser> GenerateUsers(int count)
    {
        return new Faker<AppUser>()
            .RuleFor(u => u.FirstName, f => f.Name.FirstName())
            .RuleFor(u => u.LastName, f => f.Name.LastName())
            .RuleFor(u => u.Squad, f => f.Random.Number(1, 10).ToString("000"))
            .RuleFor(u => u.Stack, f => f.PickRandom(_stacks))
            .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName))
            .RuleFor(u => u.ImageUrl, f => f.Image.PlaceholderUrl(200, 200, "people"))
            .RuleFor(u => u.Stack, f => f.PickRandom(_stacks))
            .Generate(count);
    }

    private IEnumerable<ArticleTag> GenerateTags()
    {
        return _stacks.Select(stack => new ArticleTag { Name = stack }).ToList();
    }

    private IEnumerable<Article> GenerateArticles(int count)
    {
        return new Faker<Article>()
            .RuleFor(a => a.Title, f => f.Lorem.Sentence())
            .RuleFor(a => a.Text, f => f.Lorem.Paragraphs(f.Random.Number(5, 10)))
            .RuleFor(a => a.AuthorId, f => f.PickRandom(Users).Id)
            .RuleFor(a => a.TagId, f => f.PickRandom(Tags).Id)
            .RuleFor(a => a.ReadCount, f => f.Random.Number(0, 100))
            .RuleFor(a => a.ImageUrl, "https://res.cloudinary.com/dj8irij6m/image/upload/c_pad,b_auto:predominant,fl_preserve_transparency/v1694085406/Sq016/Card_Dummy_Image.jpg?_s=public-apps")
            .RuleFor(a => a.ReadTime, f => f.Random.Number(1, 10) + " mins")
            .Generate(count);
    }

    private IEnumerable<ArticleApproval> GenerateArticleApproval()
    {
        return Articles.Select(article => new Faker<ArticleApproval>()
            .RuleFor(a => a.ArticleId, f => article.Id)
            .RuleFor(a => a.Status, f => f.Random.Number(1, 4))
            .Generate());
    }

    private IEnumerable<ArticleLike> GenerateArticleLikes(int count)
    {
        return new Faker<ArticleLike>()
            .RuleFor(a => a.ArticleId, f => f.PickRandom(Articles).Id)
            .RuleFor(a => a.UserId, f => f.PickRandom(Users).Id)
            .Generate(count);
    }

    private IEnumerable<ArticleRead> GenerateArticleReads(int count)
    {
        return new Faker<ArticleRead>()
            .RuleFor(a => a.ArticleId, f => f.PickRandom(Articles).Id)
            .RuleFor(a => a.UserId, f => f.PickRandom(Users).Id)
            .Generate(count);
    }

    private IEnumerable<Comment> GenerateComments(int count)
    {
        return new Faker<Comment>()
            .RuleFor(a => a.ArticleId, f => f.PickRandom(Articles).Id)
            .RuleFor(a => a.UserId, f => f.PickRandom(Users).Id)
            .RuleFor(a => a.Text, f => f.Lorem.Paragraphs(f.Random.Number(1, 3)))
            .Generate(count);
    }
    
    private IEnumerable<ReportedArticle> GenerateReportedArticle(int count)
    {
        return new Faker<ReportedArticle>()
            .RuleFor(a => a.ArticleId, f => f.PickRandom(Articles).Id)
            .RuleFor(a => a.ReportedBy, f => f.PickRandom(Users).Id)
            .RuleFor(a => a.ReportText, f => f.Lorem.Paragraphs(f.Random.Number(1, 3)))
            .Generate(count);
    }
}