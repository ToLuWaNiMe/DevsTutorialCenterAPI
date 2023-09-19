﻿using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Data.Repositories;
using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Services.Abstractions;

namespace DevsTutorialCenterAPI.Services.Implementations;

public class LikesService : ILikesService
{
    private readonly IRepository _repository;

    public LikesService(IRepository repository)
    {
        _repository = repository;
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