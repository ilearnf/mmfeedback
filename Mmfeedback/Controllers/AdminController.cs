﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mmfeedback.Models.Abstract;
using Mmfeedback.Models.Concrete;
using Mmfeedback.Models.Entities;
using kasthack.vksharp;
using kasthack.vksharp.DataTypes.ResponseEntities;
using System.Text;
using Nemiro.OAuth;
using System.Net;
using Newtonsoft.Json;

namespace Mmfeedback.Controllers
{
    public class AdminController : Controller
    {
		private IReviewRepository repository;
		private IPendingRepository pendingRepository;
		private IMessageRepository messageRepository;
		private static string token { get; set; }

		public AdminController(IReviewRepository newRepository, IPendingRepository newPendingRepository, 
		IMessageRepository newMessageRepository){
			repository = newRepository;
			pendingRepository = newPendingRepository;
			messageRepository = newMessageRepository;
			token = "22b299f8c56446504035cc2c561b95823a3a21b5afa2377b620e0e36aac1e8ac947520d0f12c673a6d8ea";
		}

		public ActionResult VkLogin(){
			var callbackUrl = Url.Action ("VkLoginCallback", "Admin", null, null, Request.Url.Host);
			return Redirect (OAuthWeb.GetAuthorizationUrl ("VK", callbackUrl));
		}

		public ActionResult VkLoginCallback(){
			var result = OAuthWeb.VerifyAuthorization ();
			if (result.IsSuccessfully) {
				TempData ["id"] = result.UserId;
			}
			return Content (@"<script>window.close()</script>");
		}

		[HttpGet]
		public ViewResult Login(){
			return View ();
		}

		[HttpPost]
		public ActionResult Login(string password){
			var id = TempData.ContainsKey ("id") ? TempData ["id"] : "";
			if (password != "111") {
				TempData ["logged"] = false;
				return Redirect (Url.Action ("ThrowError"));
			}
			TempData ["logged"] = true;
			return Redirect (Url.Action ("Index"));
		}

		public ActionResult Logout(){
			TempData ["logged"] = false;
			return RedirectToAction ("Index", "Home");
		}

        public ActionResult Index()
		{
			var logged = TempData.ContainsKey ("logged") ? (bool)TempData ["logged"] : false;
			if (logged)
				return View (pendingRepository.Reviews);
			else
				return RedirectToAction ("ThrowError");
        }

		public ActionResult GetMessages(){
			var logged = TempData.ContainsKey ("logged") ? (bool)TempData ["logged"] : false;
			if (logged)
				return View (messageRepository.Items.AsEnumerable());
			else
				return RedirectToAction ("ThrowError");
		}

		[HttpPost]
		public ActionResult PostReview(int id, string description, string tags, string author="",
			string authorId="", string title="", string category="")
		{
			var api = new Api ();
			api.AddToken (new Token (token));
			var message = new StringBuilder ();
			message.AppendLine (title);
			message.Append (description);
			var postRequest = api.Wall.PostSync (ownerId: -106361362, message: message.ToString());
			var review = new Review () {
				Id = repository.GetNextId(),
				PostId = postRequest.PostId,
				Title = title,
				Description = description,
				Tags = tags.Split (','),
				CommunityUrl = "https://vk.com/moiseyfomin?w=wall-106361362_" + postRequest.PostId,
				CommunutyDiscussionsCount = 0,
				Category = category,
				Date = DateTime.Now.ToShortDateString(),
				Author = author,
				AuthorId = authorId
			};
			pendingRepository.Delete (id);
			repository.Add (review);
			return Content ("accepted");
		}

		[HttpPost]
		public ActionResult DeclineReview(int id){
			pendingRepository.Delete (id);
			return Content ("declined");
		}

		public ActionResult ThrowError(){
			return View ();
		}
    }
}