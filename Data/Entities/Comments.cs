using System;
namespace DevsTutorialCenterAPI.Data.Entities
{
	public class Comments
	{
		public Guid Id { get; set; }
		public string Text { get; set; }
        public string UserId { get; set; }
    }
}

