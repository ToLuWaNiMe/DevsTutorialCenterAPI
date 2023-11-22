using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevsTutorialCenterAPI.Data.Entities;

public class ArticleApproval : BaseEntity
{
    public string ArticleId { get; set; }
    public  int Status{ get; set; }

    [ForeignKey("ArticleId")]
    public Article Article { get; set; }
}