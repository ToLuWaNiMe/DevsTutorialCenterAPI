using System.ComponentModel.DataAnnotations;

namespace DevsTutorialCenterAPI.Models.DTOs;

public class FetchAuthorsStatsDto
{
    [Required]
    public IEnumerable<string> AuthorIdList { get; set; }
}