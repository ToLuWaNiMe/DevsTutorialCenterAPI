using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Data.Repositories;
using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Services.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DevsTutorialCenterAPI.Services.Implementation
{
    public class ReportArticleService : IReportArticleService
    {
        private readonly IRepository _repository;

        public ReportArticleService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<object> AddArticleReportAsync(ReportArticleRequestDto request, string articleId)
        {
            
            try
            {

                var article = await _repository.GetByIdAsync<Article>(articleId);

                if (article == null)
                {
                    throw new Exception("This article cannot be found");
                }


                if (article.IsReported == true)
                {
                    throw new Exception("This article has already been reported");
                }

                
                var reportedarticle = new ReportArticle() 
                {
                    ReportText = request.ReportText,
                    ArticleId = article.Id,
                    ReportedBy = request.ReportedBy
                
                };

                await _repository.AddAsync<ReportArticle>(reportedarticle);

                article.IsReported = true;

                await _repository.UpdateAsync<Article>(article);

                return reportedarticle;

                // add new article report to db
                // update isReported property of article in db



            }
            catch (Exception ex)
            {
               throw new Exception(ex.ToString());
            }
        }
    }
}
