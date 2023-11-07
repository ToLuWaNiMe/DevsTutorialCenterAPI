﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevsTutorialCenterAPI.Data.Entities;

public class ArticleBookMark : BaseEntity
{
    public string UserId { get; set; }

    public string ArticleId { get; set; }

    [ForeignKey("ArticleId")]
    [ValidateNever]
    public Article? Article { get; set; }
}