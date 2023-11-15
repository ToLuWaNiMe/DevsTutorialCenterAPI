namespace DevsTutorialCenterAPI.Data.Entities;

public class BaseEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public DateTime CreatedOn { get; set; } 
    public DateTime UpdatedOn { get; set; } 

    public DateTime? DeletedAt { get; set; }
}