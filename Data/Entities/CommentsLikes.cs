using System;
namespace DevsTutorialCenterAPI.Data.Entities
{
	public class CommentsLikes: BaseEntity
	{
		public string UserId { get; set; }
		public string CommentId { get; set; }
	}
}

