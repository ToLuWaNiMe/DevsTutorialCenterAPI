using Microsoft.AspNetCore.Identity;

namespace DevsTutorialCenterAPI.Data.Entities
{
    public class AppUser : IdentityUser
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? ImageUrl { get; set; }
        public string? PublicId { get; set; }
        public bool IsReported { get; set; }

        public string Stack { get; set; }
        public string Squad { get; set; }

        public DateTime? DeletedAt { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedOn { get; set; } = DateTime.UtcNow;
    }
}
