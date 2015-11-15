using System;
using Ninject;
using Moq;
using System.Web.Mvc;
using Mmfeedback.Models.Abstract;
using Mmfeedback.Models.Entities;
using System.Collections.Generic;
using System.Linq;
using Mmfeedback.Models.Concrete;

namespace Mmfeedback.Infrastructure
{
	public class NinjectControllerFactory : DefaultControllerFactory
	{
		private IKernel ninjectKernel;

		public NinjectControllerFactory ()
		{
			ninjectKernel = new StandardKernel ();
			AddBindings ();
		}

		protected override IController GetControllerInstance (System.Web.Routing.RequestContext requestContext,
			Type controllerType)
		{
			return controllerType == null ? null : (IController)ninjectKernel.Get (controllerType);
		}

		private void AddBindings(){
//			var mock = new Mock<IReviewRepository> ();
//			mock.Setup (x => x.Tags).Returns (new List<string> () {
//				"calc",
//				"shamgunov",
//				"algo",
//				"shur",
//				"dm",
//				"programming",
//				"egorov",
//				"okulovskiy",
//				"misc",
//				"cs",
//				"algebra",
//				"vernikov",
//				"python",
//				"blinov",
//				"kornev",
//				"magaz",
//				"orazkimovich",
//				"oop",
//				"klepinin"
//			}.AsQueryable());
//			mock.Setup (x => x.Reviews).Returns (new List<Review> () {
//				new Review () { Title = "title", Description = "calc shamgunov", Id = 1,
//					CommunityUrl = "url", CommunutyDiscussionsCount = 0, Author = null,
//					Category = "", Tags = new string[]{ "calculus", "shamgunov" }, Date = "1.1.1"
//				}, 
//				new Review () { Title = "title", Description = "dm shur", Id = 1,
//					CommunityUrl = "url", CommunutyDiscussionsCount = 0, Author = null,
//					Category = "", Tags = new string[]{ "dm", "shur" }, Date = "1.1.1" 
//				},
//				new Review () { Title = "title", Description = "algo shur", Id = 1,
//					CommunityUrl = "url", CommunutyDiscussionsCount = 0, Author = null,
//					Category = "", Tags = new string[]{ "algo", "shur" }, Date = "1.1.1"
//				},
//				new Review () { Title = "title", Description = "dm shur", Id = 1,
//					CommunityUrl = "url", CommunutyDiscussionsCount = 0, Author = null,
//					Category = "", Tags = new string[]{ "dm", "shur" }, Date = "1.1.1"
//				},
//				new Review () { Title = "title", Description = "programming o", Id = 1,
//					CommunityUrl = "url", CommunutyDiscussionsCount = 0, Author = null,
//					Category = "", Tags = new string[]{ "programming", "okulovskiy" }, Date = "1.1.1"
//				},
//				new Review () { Title = "title", Description = "programming o", Id = 1,
//					CommunityUrl = "url", CommunutyDiscussionsCount = 0, Author = null,
//					Category = "", Tags = new string[]{ "programming", "okulovskiy" }, Date = "1.1.1"
//				}, 
//				new Review () { Title = "title", Description = "programming e", Id = 1,
//					CommunityUrl = "url", CommunutyDiscussionsCount = 0, Author = null,
//					Category = "", Tags = new string[]{ "programming", "egorov" }, Date = "1.1.1"
//				},
//				new Review () { Title = "title", Description = "misc e", Id = 1,
//					CommunityUrl = "url", CommunutyDiscussionsCount = 0, Author = null,
//					Category = "", Tags = new string[]{ "misc", "egorov" }, Date = "1.1.1"
//				},
//				new Review () { Title = "title", Description = "cs", Id = 1,
//					CommunityUrl = "url", CommunutyDiscussionsCount = 0, Author = null,
//					Category = "", Tags = new string[]{ "cs" }, Date = "1.1.1"
//				},
//				new Review () { Title = "title", Description = "cs programming", Id = 1,
//					CommunityUrl = "url", CommunutyDiscussionsCount = 0, Author = null,
//					Category = "", Tags = new string[]{ "cs", "programming" }, Date = "1.1.1"
//				},
//				new Review () { Title = "title1", Description = "eng th", Id = 1,
//					CommunityUrl = "url", CommunutyDiscussionsCount = 0, Author = null,
//					Category = "", Tags = new string[]{ "english", "thousands" }, Date = "1.1.1"
//				}, 
//				new Review () { Title = "title1", Description = "algebra v", Id = 1,
//					CommunityUrl = "url", CommunutyDiscussionsCount = 0, Author = null,
//					Category = "", Tags = new string[]{ "algebra", "vernikov" }, Date = "1.1.1"
//				},
//				new Review () { Title = "title1", Description = "magaz", Id = 1,
//					CommunityUrl = "url", CommunutyDiscussionsCount = 0, Author = null,
//					Category = "", Tags = new string[]{ "magaz", "orazkimovich" }, Date = "1.1.1"
//				},
//				new Review () { Title = "title", Description = "p blinov", Id = 1,
//					CommunityUrl = "url", CommunutyDiscussionsCount = 0, Author = null,
//					Category = "", Tags = new string[]{ "python", "blinov" }, Date = "1.1.1"
//				},
//				new Review () { Title = "title", Description = "p k", Id = 1,
//					CommunityUrl = "url", CommunutyDiscussionsCount = 0, Author = null,
//					Category = "", Tags = new string[]{ "python", "kornev" }, Date = "1.1.1"
//				},
//				new Review () { Title = "title", Description = "cs p", Id = 1,
//					CommunityUrl = "url", CommunutyDiscussionsCount = 0, Author = null,
//					Category = "", Tags = new string[]{ "cs", "python" }, Date = "1.1.1"
//				}, 
//				new Review () { Title = "title", Description = "oop cs", Id = 1,
//					CommunityUrl = "url", CommunutyDiscussionsCount = 0, Author = null,
//					Category = "", Tags = new string[]{ "cs", "oop" }, Date = "1.1.1"
//				},
//				new Review () { Title = "title", Description = "oop klep", Id = 1,
//					CommunityUrl = "url", CommunutyDiscussionsCount = 0, Author = null,
//					Category = "", Tags = new string[]{ "oop", "klepinin" }, Date = "1.1.1"
//				},
//				new Review () { Title = "title", Description = "oop klep", Id = 1,
//					CommunityUrl = "url", CommunutyDiscussionsCount = 0, Author = null,
//					Category = "", Tags = new string[]{ "oop", "klepinin" }, Date = "1.1.1"
//				},
//				new Review () { Title = "title", Description = "algo shur", Id = 1,
//					CommunityUrl = "url", CommunutyDiscussionsCount = 0, Author = null,
//					Category = "", Tags = new string[]{ "algo", "shur" }, Date = "1.1.1"
//				}
//			}.AsQueryable ());
//			mock.Setup (x => x.PendingReviews).Returns (new List<PendingReview> () {
//				new PendingReview () { Title = "title", Description = "desc", Tags = new string[] { "t", "f" },
//					Category = "ddd", Author = "author author", PostId = -1
//				},
//				new PendingReview () { Title = "title", Description = "ddd", Tags = new string[] { "ddd" },
//					Category = "ddd", Author = "Author", PostId = 3
//				}
//			}.AsQueryable());
			ninjectKernel.Bind<IReviewRepository> ().To<XmlReviewRepository> ();
			ninjectKernel.Bind<IPendingRepository> ().To<XmlPendingReviewRepository> ();
			ninjectKernel.Bind<IMessageRepository> ().To<MessageRepository> ();

			//ninjectKernel.Bind<IReviewRepository> ().ToConstant(mock.Object);
			//ninjectKernel.Bind<IReviewRepository> ().To<DbProductRepository>();
		}
	}
}

