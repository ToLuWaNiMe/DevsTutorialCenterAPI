using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevsTutorialCenterAPI.Data.Entities;

public class ArticleTag : BaseEntity
{
    public string Name { get; set; }

    [ForeignKey("ArticleId")]
    [ValidateNever]
    public Article? Article { get; set; }

    public DateTime? DeletedAt { get; set; }
}