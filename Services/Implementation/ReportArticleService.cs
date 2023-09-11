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

        public async Task<ResponseDto<ReportArticleRequestDto>> ReportArticleAsync(string articleId, ReportArticleRequestDto request)
        {
            try
            {
                // Find the article by its ID
                var article = await _repository.GetByIdAsync<Article>(articleId);

                if (article == null)
                {
                    return new ResponseDto<ReportArticleRequestDto>
                    {
                        Code = 400,
                        Message = "Error",
                        Error = "Article not found."
                    };
                }

                // Implement the reporting logic here
                article.IsReported = request.IsReported;
                

                // Update the article in the repository
               await  _repository.UpdateAsync(article);

                return new ResponseDto<ReportArticleRequestDto>
                {
                    Code = 200,
                    Message = "Ok",
                    Data = request
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<ReportArticleRequestDto>
                {
                    Code = 400,
                    Message = "Error",
                    Error = ex.Message
                };
            }
        }
    }
}
