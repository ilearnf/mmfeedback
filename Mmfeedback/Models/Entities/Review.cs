using System;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Mmfeedback.Models.Entities
{
	public class Review
	{
		[HiddenInput(DisplayValue = false)]
		public int Id { get; set; }
		[HiddenInput(DisplayValue = false)]
		public int PostId { get; set; }
		public string Title { get; set; }
		[HiddenInput(DisplayValue = false)]
		public string Date { get; set; }
		[DataType(DataType.MultilineText)]
		public string Description { get; set; }
		[HiddenInput(DisplayValue = false)]
		public string[] Tags { get; set; }
		public string Category { get; set; }
		[HiddenInput(DisplayValue = false)]
		public string CommunityUrl { get; set; }
		[HiddenInput(DisplayValue = false)]
		public int CommunutyDiscussionsCount { get; set; }
		[HiddenInput(DisplayValue = false)]
		public string Author { get; set; }
		[HiddenInput(DisplayValue = false)]
		public string AuthorId { get; set; }
	}
}

