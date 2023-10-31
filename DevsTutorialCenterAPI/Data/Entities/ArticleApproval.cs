using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevsTutorialCenterAPI.Data.Entities;

public class ArticleApproval : BaseEntity
{
    //public string ApprovalId { get; set; }
    public string ArticleId { get; set; }

    [ForeignKey("ArticleId")]
    [ValidateNever]
    public Article? Article { get; set; }
    public  int Status{ get; set; }
}