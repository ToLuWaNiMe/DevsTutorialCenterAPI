using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevsTutorialCenterAPI.Data.Entities;

public class ArticlesLikes : BaseEntity
{
    public string UserId { get; set; }

    [ForeignKey("ArticleId")]
    [ValidateNever]
    public Article? Article { get; set; }
    public string ArticleId { get; set; }
}