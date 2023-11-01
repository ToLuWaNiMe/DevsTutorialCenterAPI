using Microsoft.AspNetCore.Identity;

namespace DevsTutorialCenterAPI.Data.Entities
{
    public class AppUser : IdentityUser
    {

        public string Name { get; set; }

        public string Stack { get; set; }

        public string Squad { get; set; }

    }
}
