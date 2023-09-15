using System.ComponentModel.DataAnnotations;

namespace DevsTutorialCenterAPI.Models.DTOs
{
    public class TagDto

    {
        [Required]
        public string Name { get; set; }
    }
}
