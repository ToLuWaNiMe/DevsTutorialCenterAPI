﻿using DevsTutorialCenterAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevsTutorialCenterAPI.Data;

public class DevsTutorialCenterAPIContext : DbContext
{
    public DevsTutorialCenterAPIContext(DbContextOptions<DevsTutorialCenterAPIContext> options) :
        base(options)
    {
    }

    public DbSet<Article> Articles { get; set; }
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<Comment> Comments { get; set; }

    public DbSet<ReportArticle> ReportedArticles { get; set; }
    public DbSet<ArticleTag> Tags { get; set; }
    public DbSet<ArticleBookMark> ArticleBookMarks { get; set; }
    public DbSet<ArticleRead> ArticleReads { get; set; }

    public DbSet<ArticleApproval> ArticleApprovals { get; set; }
    public DbSet<UserExtraInfo> UserExtraInfos{ get; set; }

    public DbSet<ArticlesLikes> ArticlesLikes { get; set; }
    public DbSet<CommentsLikes> CommentsLikes { get; set; }
}