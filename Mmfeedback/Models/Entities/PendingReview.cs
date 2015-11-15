using System;

namespace Mmfeedback.Models.Entities
{
	public class PendingReview
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string[] Tags { get; set; }
		public string Category { get; set; }
		public string Author { get; set; }
		public string AuthorId { get; set; }

		public PendingReview ()
		{
		}
	}
}

