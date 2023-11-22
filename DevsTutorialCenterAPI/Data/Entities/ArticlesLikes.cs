using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevsTutorialCenterAPI.Data.Entities;

public class ArticleLike : BaseEntity
{
    public string UserId { get; set; }
    public string ArticleId { get; set; }
    
    [ForeignKey("ArticleId")]
    [ValidateNever]
    public Article Article { get; set; }
    public AppUser User { get; set; }
}