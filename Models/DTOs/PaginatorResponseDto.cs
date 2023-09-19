namespace DevsTutorialCenterAPI.Models.DTOs;

public class PaginatorResponseDto<T>
{
    public T? PageItems { get; set; }
    public int PageSize { get; set; }
    public int CurrentPage { get; set; }
    public int NumberOfPages { get; set; }
    public int? PreviousPage { get; set; }
    public int? NextPage { get; set; }
    public int TotalCount { get; internal set; }
}