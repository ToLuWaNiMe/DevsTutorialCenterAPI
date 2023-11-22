using DevsTutorialCenterAPI.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DevsTutorialCenterAPI.Data;

public class DevsTutorialCenterAPIContext : IdentityDbContext<AppUser>
{
    public DevsTutorialCenterAPIContext(DbContextOptions<DevsTutorialCenterAPIContext> options) :
        base(options)
    {
    }

    public DbSet<Article> Articles { get; set; }
    public DbSet<Comment> Comments { get; set; }

    public DbSet<ReportedArticle> ReportedArticles { get; set; }
    public DbSet<ArticleTag> Tags { get; set; }
    public DbSet<ArticleBookMark> ArticleBookMarks { get; set; }
    public DbSet<ArticleRead> ArticleReads { get; set; }

    public DbSet<ArticleApproval> ArticleApprovals { get; set; }
    public DbSet<AppUser> AppUsers { get; set; }

    public DbSet<ArticleLike> ArticlesLikes { get; set; }
    public DbSet<CommentLike> CommentsLikes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AppUser>().Property(x => x.ImageUrl).IsRequired(false);
        modelBuilder.Entity<AppUser>().Property(x => x.PublicId).IsRequired(false);

        modelBuilder.Entity<Article>().Property(x => x.ImageUrl).IsRequired(false);
        modelBuilder.Entity<Article>().Property(x => x.PublicId).IsRequired(false);

        base.OnModelCreating(modelBuilder);
    }
}