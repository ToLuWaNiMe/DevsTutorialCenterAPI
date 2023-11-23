using DevsTutorialCenterAPI.Data;
using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Data.Repositories;
using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Services.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace DevsTutorialCenterAPI.Services.Implementations;

public class CommentService : ICommentService
{
    private readonly IRepository _repository;
    private readonly DevsTutorialCenterAPIContext _db;

    public CommentService(IRepository repository, DevsTutorialCenterAPIContext db)
    {
        _repository = repository;
        _db = db;
    }

    public async Task<CreateCommentDto> CreateCommentAsync(string articleId, string userId, CreateCommentDto dto)
    {
        var article = await _repository.GetByIdAsync<Article>(articleId);

        if (article == null) throw new Exception("Article not found");

        var user = await _repository.GetByIdAsync<AppUser>(userId);

        if (user == null) throw new Exception("User not found");

        var comment = new Comment
        {
            Text = dto.Text,
            UserId = user.Id,
            ArticleId = article.Id,
        };

        await _repository.AddAsync<Comment>(comment);

        var commentDto = new CreateCommentDto
        {
            Text = dto.Text,
           
           
        };
        return commentDto;
    }

    public async Task<UpdateCommentDto> UpdateCommentAsync(string id, string userId,  CommentDto commentDto)
    {
        var comment = await _repository.GetByIdAsync<Comment>(id);

        if (comment == null) throw new Exception("Comment not found");

        var user = await _repository.GetByIdAsync<AppUser>(userId);

        if (user == null) throw new Exception("User not found");

        if (comment.UserId != user.Id) throw new Exception("You cannot edit this comment.");

        comment.Text = commentDto.Text;

        await _repository.UpdateAsync<Comment>(comment);

        return new UpdateCommentDto
        {
            Text = comment.Text

        };
            
    }

    public async Task<bool> DeleteCommentAsync(string Id, string userId)
    {
        var comment = await _repository.GetByIdAsync<Comment>(Id);

        if (comment == null) throw new Exception("Comment not found");

        var user = await _repository.GetByIdAsync<AppUser>(userId);

        if (user == null) throw new Exception("User not found");

        if (comment.UserId != user.Id) throw new Exception("You cannot delete this comment.");

        await _repository.DeleteAsync(comment);

        return true;
    }

    public async Task<IEnumerable<GetCommentDto>> GetCommentsByArticle(string articleId)
    {
        var comments = await _repository.GetAllAsync<Comment>();
        if (comments == null) throw new Exception("No comments for this article");

        var articleComments = await comments.Where(c => c.ArticleId == articleId)
            .OrderByDescending(c => c.CreatedOn)
            .Select(c => new GetCommentDto
            {
                Id = c.Id,
                Text = c.Text,
                UserId = c.UserId,
                ArticleId = c.ArticleId
               
            })
            .ToListAsync();

        return articleComments;
    }

    public async Task<List<LikesByCommentsDto>> GetLikesByCommentsAsync(string commentId)
    {
        try
        {
            var likesQuery = await _repository.GetAllAsync<CommentLike>();
            var likes = await likesQuery
                .Where(like => like.CommentId == commentId)
                .Select(like => new LikesByCommentsDto
                {
                    UserId = like.UserId,
                    CommentId = like.CommentId
                })
                .ToListAsync();

            return likes;
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to retrieve likes by comments.", ex);
        }
    }

    public async Task<string> LikeComment(string commentId, string userId)
    {
        var comment = await _repository.GetByIdAsync<Comment>(commentId);

        if (comment == null) throw new Exception("Comment not found");

        var user = await _repository.GetByIdAsync<AppUser>(userId);

        if (user == null) throw new Exception("User not found");

        var commentLikeCheck = await GetCommentLikeByUserId(user.Id, comment.Id);

        if(commentLikeCheck != null)
        {
            throw new Exception("You have already liked this comment");
        }

        var commentLike = new CommentLike
        {
            UserId = user.Id,
            CommentId = comment.Id
        };

        await _repository.AddAsync<CommentLike>(commentLike);

        var likes = (await _repository.GetAllAsync2<CommentLike>()).Where(cl => cl.CommentId == comment.Id);

        return likes.Count().ToString();


    }

    public async Task<CommentLike> GetCommentLikeByUserId(string userId, string commentId)
    {
        return await _db.CommentsLikes.FirstOrDefaultAsync(cl => cl.UserId == userId && cl.CommentId == commentId);
    }

   
}