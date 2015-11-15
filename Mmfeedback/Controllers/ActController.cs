using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mmfeedback.Models.Entities;
using Mmfeedback.Models.Abstract;
using Nemiro.OAuth;
using Mmfeedback.Models.ViewModels;
using kasthack.vksharp;
using System.Text;
using System.Net;

namespace Mmfeedback.Controllers
{
    public class ActController : Controller
    {
		private readonly IReviewRepository repository;
		private readonly IMessageRepository messagesRepository;
		private readonly IPendingRepository pendingRepository;

		public ActController(IReviewRepository newRepository, IPendingRepository newPendingRepository,
			IMessageRepository newMessageRepository)
		{
			repository = newRepository;
			pendingRepository = newPendingRepository;
			messagesRepository = newMessageRepository;
		}

		public ActionResult VkLogin(){
			var callbackUrl = Url.Action ("VkLoginCallback", "Act", null, null, Request.Url.Host);
			//var callbackUrl = "https://oauth.vk.com/blank.html";
			var u = OAuthWeb.GetAuthorizationUrl("VK", callbackUrl);
			return Redirect (OAuthWeb.GetAuthorizationUrl ("VK",  callbackUrl));
			//return Redirect ("https://oauth.vk.com/authorize?client_id=5126467&display=page" +
			//"&redirect_uri=http://127.0.0.1:8080/Act/VkLoginCallback&scope=email,status&response_type=code&v=5.40");
		}

		public ActionResult VkLoginCallback(){
			var result = OAuthWeb.VerifyAuthorization ();
//			var a = RouteData.Values ["access_token"];
//			var args = Request.Url.Query;
//			var rightIdx = args.IndexOf ('&') - 1;
//			var access_token = args.Substring(14, rightIdx);
			if (result.IsSuccessfully) {
				TempData["token"] = result.AccessTokenValue;
				TempData ["author"] = result.UserInfo.FullName;
				TempData ["authorId"] = result.UserId;
			}
			return Content (@"<script>window.close()</script>");
		}
		[HttpGet]
		public ViewResult AddReview(){
			var formModel = new ReviewCreatorModel (repository.Tags);
			return View ("AddForm", formModel);
		}

		[HttpPost]
		public ActionResult AddReview([Bind(Include = "Form")]ReviewCreatorModel model)
		{
			if (ModelState.IsValid) {
				var author = TempData.ContainsKey ("author") ? (string)TempData ["author"] : "";
				var authorId = TempData.ContainsKey ("authorId") ? (string)TempData ["authorId"] : "";
				var message = new StringBuilder ();
				message.AppendLine (model.Form.Title);
				message.Append (model.Form.Description);
				var review = new PendingReview () {
					Id = pendingRepository.GetNextId (),
					Title = model.Form.Title,
					Description = model.Form.Description,
					Tags = model.Form.Tags.Split (','),
					Author = author,
					AuthorId = authorId,
					Category = model.Form.Category,
					//PostId = postId
				};
				pendingRepository.Add (review);
				TempData.Remove ("author");
				TempData.Remove ("authorId");
			}
			return RedirectToAction("Success");
		}

		[HttpGet]
		public ActionResult SendMessage(){
			return View (new SendMessageForm());
		}

		[HttpPost]
		public ActionResult SendMessage(string title, string description){
			var author = TempData.ContainsKey ("author") ? (string)TempData ["author"] : "";
			var authorId = TempData.ContainsKey ("authorId") ? "vk.com/" + (string)TempData ["authorId"] : "anonym";
			var message = new Message () { 
				Title = title,
				Description = description,
				AuthorId = authorId,
				Author = ""
			};
			messagesRepository.Add (message);
			TempData.Remove ("author");
			TempData.Remove ("authorId");
			return RedirectToAction ("Success");
		}

		public ViewResult Success(){
			return View ();
		}
    }
}
