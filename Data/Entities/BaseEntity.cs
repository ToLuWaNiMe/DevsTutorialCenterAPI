using System;
namespace DevsTutorialCenterAPI.Data.Entities
{
	public class BaseEntity
	{
		public string Id { get; set; } = Guid.NewGuid().ToString();
		public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
		public DateTime UpdatedOn { get; set; } = DateTime.UtcNow;
    }
}

