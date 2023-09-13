using DevsTutorialCenterAPI.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DevsTutorialCenterAPI.Utilities
{
    public static class Helper
    {
        public static PaginatorResponseDto<IEnumerable<T>> Paginate<T>(IQueryable<T> items, int pageNum, int pageSize)
        {
            var totalCount = items.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            if (pageNum <= 0 || pageNum > totalPages)
            {
                return null;
            }

            var skipAmount = (pageNum - 1) * pageSize;
            var paginatedItems = items.Skip(skipAmount).Take(pageSize).ToList();

            return new PaginatorResponseDto<IEnumerable<T>>
            {
                PageItems = paginatedItems,
                PageSize = pageSize,
                CurrentPage = pageNum,
                NumberOfPages = totalPages,
                TotalCount = totalCount,
                PreviousPage = pageNum > 1 ? pageNum - 1 : -1, // Set to -1 for the first page
            };
        }
    }
}
