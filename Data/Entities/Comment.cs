using System;
namespace DevsTutorialCenterAPI.Data.Entities
{
	public class Comment : BaseEntity
	{
        public string Text { get; set; }
        public string ArticleId { get; set; }
        public string UserId { get; set; }
    }
}

