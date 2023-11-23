using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevsTutorialCenterAPI.Data.Entities;

public class ArticleRead : BaseEntity
{
    public string UserId { get; set; }
    public string ArticleId { get; set; }

    [ForeignKey("ArticleId")]
    public Article Article { get; set; }
}