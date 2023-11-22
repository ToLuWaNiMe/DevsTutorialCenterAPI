using DevsTutorialCenterAPI.Models.DTOs;

namespace DevsTutorialCenterAPI.Utilities;

public static class Helper
{
    public static PaginatorResponseDto<IEnumerable<T>> Paginate<T>(IQueryable<T> items, int pageNum, int pageSize)
    {
        var totalCount = items.Count();
        var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

        if (pageNum <= 0 || pageNum > totalPages) pageNum = totalPages;

        var skipAmount = (pageNum - 1) * pageSize;
        var paginatedItems = items.Skip(skipAmount).Take(pageSize).ToList();

        return new PaginatorResponseDto<IEnumerable<T>>
        {
            PageItems = paginatedItems,
            PageSize = pageSize,
            CurrentPage = pageNum,
            NumberOfPages = totalPages,
            TotalCount = totalCount,
            PreviousPage = pageNum > 1 ? pageNum - 1 : null,
            NextPage = totalPages == pageNum ? null : pageNum + 1
        };
    }


    public static string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public static bool VerifyPassword(string hashedPassword, string inputPassword)
    {
        return BCrypt.Net.BCrypt.Verify(inputPassword, hashedPassword);
    }

    public static int CalculateReadingTime(string text)
    {
        // Assuming an average reading speed of 200 words per minute
        const int WordsPerMinute = 200;

        // Calculate the number of words in the text (you may need a more accurate word count algorithm)
        int wordCount = text.Split(new char[] { ' ', '.', ',', '!', '?' }, StringSplitOptions.RemoveEmptyEntries)
            .Length;

        // Calculate the reading time in minutes
        return (int)Math.Ceiling((double)wordCount / WordsPerMinute);
    }
}