namespace DevsTutorialCenterAPI.Models.DTOs
{
    public class RegistrationRequestDTO
    {
        public string Email { get; set; }
        public string Name { get; set; }

        public string PhoneNumber { get; set; }
        public string Password { get; set; }

        //public string RoleName { get; set; }

        public  string Stack { get; set; }

        public string Squad { get; set; }
    }
}
