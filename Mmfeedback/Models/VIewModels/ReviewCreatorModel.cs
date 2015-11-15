using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;

namespace Mmfeedback.Models.ViewModels
{
	public class ReviewCreatorModel
	{
		public AddReviewForm Form { get; set; }
		public IEnumerable<string> Tags { get; set; }
		public IEnumerable<SelectListItem> Categories { get; }
		// TODO Add review types

		public ReviewCreatorModel(){
			Tags = null;
			Categories = null;
		}

		public ReviewCreatorModel (IEnumerable<string> tags)
		{
			Form = new AddReviewForm ();
			Tags = tags;
			Categories = new List<string> () {
				"Официальный",
				"На преподавателя",
				"На курс",
				"На направление",
			}.Select (category => new SelectListItem () { Text = category, Value = category });
		}
	}
}

