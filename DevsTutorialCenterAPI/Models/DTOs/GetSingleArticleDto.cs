using Humanizer;

namespace DevsTutorialCenterAPI.Models.DTOs;

public class GetSingleArticleDto
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Text { get; set; }
    public string AuthorId { get; set; }
    public string TagId { get; set; }
    public int ReadCount { get; set; }
    public string ImageUrl { get; set; }
    public string PublicId { get; set; }
    public string ReadTime { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedOn { get; set; } 


    public string GetFormattedCreatedOn()
    {
        return CreatedOn.Humanize();
    }
}