using DevsTutorialCenterAPI.Data;
using DevsTutorialCenterAPI.Data.Repositories;
using DevsTutorialCenterAPI.Data.Repositories.interfaces;
using DevsTutorialCenterAPI.Services.Abstraction;
using DevsTutorialCenterAPI.Services.Abstractions;
using DevsTutorialCenterAPI.Services.Implementation;
using DevsTutorialCenterAPI.Services.Implementations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<ILikesService, LikesService>();
builder.Services.AddScoped<ICommentsLikesService, CommentsLikesService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "Fast api",
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement()
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[]{}
            }
        });
});



builder.Services.AddDbContext<DevsTutorialCenterAPIContext>(
    option => option.UseSqlite(builder.Configuration.GetConnectionString("default"))
);


builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddScoped<ITenantService, TenantService>();
builder.Services.AddScoped<ITagService, TagService>();
builder.Services.AddScoped<ITenantAuthService, TenantAuthService>();
builder.Services.AddScoped<IReportArticleService, ReportArticleService>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    var key = Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JWT:Key").Value);
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateLifetime = true,
        ValidateAudience = false,
        ValidateIssuer = false
    };

});




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

