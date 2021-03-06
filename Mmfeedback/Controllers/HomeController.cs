﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Mmfeedback.Models.Abstract;
using Mmfeedback.Models.Entities;
using Mmfeedback.Infrastructure;
using System.Web.Hosting;
using System.Dynamic;

namespace Mmfeedback.Controllers
{
	public class HomeController : Controller
	{
		private readonly IReviewRepository repository;
		private readonly int _itemsPerPage;

		public HomeController (IReviewRepository newRepository, int itemsPerPage = 10){
			repository = newRepository;
			_itemsPerPage = itemsPerPage;
		}

		public ActionResult Index (int page=0)
		{
			ViewBag.Categories = new List<string> () {
				"",
				"Официальный",
				"На преподавателя",
				"На курс",
				"На направление",
			};
			if (Request.IsAjaxRequest())
				return PartialView ("NewContent", 
					repository.Reviews
					.Skip (page * _itemsPerPage)
					.Take (_itemsPerPage));
			return View (repository.Reviews.Take(_itemsPerPage));
		}

		public ContentResult UpdateCommunityDiscussionsCount(int id){
			var count = repository.UpdateCommunityDiscussionsCount (id);
			return Content (count.ToString ());
		}

		public ContentResult GetFitTags(string start){
			if (start == "")
				return Content(String.Join (",", repository.Tags.AsEnumerable ().Take(5)));
			return Content(string.Join (",", repository.Tags
				.AsEnumerable ()
				.Where (x => x.StartsWith (start))
				.Take (5)));
		}

		public ActionResult IndexSearch (string searchQuery="", int page=0)
		{
			if (searchQuery == "")
				return Index (page);
			if (Request.IsAjaxRequest ()) {
				var tags = searchQuery.Split (new char[] { ',', ' ' });
				return PartialView ("NewContent", repository.Reviews
					.AsEnumerable ()
					.Where (review => {
					    foreach (var tag in review.Tags) {
						    if (tags.Contains (tag))
							    return true;
					    }
					    return false;
				    })
					.Skip (page * _itemsPerPage)
					.Take (_itemsPerPage));
			}
			return View ("Index", repository.Reviews.Take(_itemsPerPage));
		}
	}
}

