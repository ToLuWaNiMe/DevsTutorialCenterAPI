using DevsTutorialCenterAPI.Data;
using DevsTutorialCenterAPI.Data.Repositories;
using DevsTutorialCenterAPI.Data.Repositories.interfaces;
using DevsTutorialCenterAPI.Services.Abstraction;
using DevsTutorialCenterAPI.Services.Abstractions;
using DevsTutorialCenterAPI.Services.Implementation;
using DevsTutorialCenterAPI.Services.Implementations;
using DevsTutorialCenterAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<ILikesService, LikesService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DevsTutorialCenterAPIContext>(
    option => option.UseSqlite(builder.Configuration.GetConnectionString("default"))
);
builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddScoped<ITenantService, TenantService>();
builder.Services.AddScoped<ITagService, TagService>();
builder.Services.AddScoped<ITenantAuthService, TenantAuthService>();
builder.Services.AddScoped<IReportArticleService, ReportArticleService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

Seeder.SeedeMe(app);

app.MapControllers();

app.Run();

