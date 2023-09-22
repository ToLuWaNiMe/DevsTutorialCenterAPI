namespace DevsTutorialCenterAPI.Data.Entities;

public class Tenant : BaseEntity
{
    public string Name { get; set; }
    public string Identity { get; set; }
    public string Password { get; set; }
}