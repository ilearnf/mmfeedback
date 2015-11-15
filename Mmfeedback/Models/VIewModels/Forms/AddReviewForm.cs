using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Mmfeedback
{
	public class AddReviewForm
	{
		[Required]
		public string Title { get; set; }
		[DataType(DataType.MultilineText), Required]
		public string Description { get; set; }
		[Required]
		public string Tags { get; set; }
		[Required]
		public string Category { get; set; }
		[HiddenInput(DisplayValue = false)]
		public string Author { get; set; }
	}
}

