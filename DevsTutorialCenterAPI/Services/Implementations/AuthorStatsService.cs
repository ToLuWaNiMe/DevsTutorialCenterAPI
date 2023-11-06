//using DevsTutorialCenterAPI.Data.Entities;
//using DevsTutorialCenterAPI.Data.Repositories;
//using DevsTutorialCenterAPI.Models.DTOs;
//using DevsTutorialCenterAPI.Services.Interfaces;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace DevsTutorialCenterAPI.Services.Implementations
//{
//    public class AuthorStatsService : IAuthorStatsService
//    {
//        private readonly IRepository _repository;

//        public AuthorStatsService(IRepository repository)
//        {
//            _repository = repository;
//        }

//        public async Task<AuthorStatsDto> GetAuthorStatsAsync(string authorId)
//        {
//            var authorStats = new AuthorStatsDto
//            {
//                AuthorId = authorId,
//                TotalNumOfArticles = await GetTotalNumOfArticlesAsync(authorId),
//                TotalReportedArticles = await GetTotalReportedArticlesAsync(authorId),
//            };

//            return authorStats;
//        }

//        public async Task<AuthorsStatsDto> GetAuthorsStatsAsync(FetchAuthorsStatsDto fetchAuthorsStatsDto)
//        {
//            var authorStatsList = new List<AuthorStatsDto>();

//            foreach (var authorId in fetchAuthorsStatsDto.AuthorIdList)
//            {
//                var authorStats = new AuthorStatsDto
//                {
//                    AuthorId = authorId,
//                    TotalNumOfArticles = await GetTotalNumOfArticlesAsync(authorId),
//                    TotalReportedArticles = await GetTotalReportedArticlesAsync(authorId),
//                };

//                authorStatsList.Add(authorStats);
//            }

//            return new AuthorsStatsDto
//            {
//                AuthorStatsDtos = authorStatsList
//            };
//        }

//        private async Task<int> GetTotalNumOfArticlesAsync(string authorId)
//        {
//            var articles = await _repository.GetAllAsync<Article>();
//            var articlesCount = articles
//                .Where(article => article.AuthorId == authorId)
//                .Count();

//            return articlesCount;
//        }

//        private async Task<int> GetTotalReportedArticlesAsync(string authorId)
//        {
//            var articles = await _repository.GetAllAsync<Article>();
//            var reportedArticlesCount = articles
//                .Where(article => article.AuthorId == authorId && article.IsReported)
//                .Count();

//            return reportedArticlesCount;
//        }

//    }
//}
