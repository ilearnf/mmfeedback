using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Mmfeedback;
using Mmfeedback.Controllers;
using Moq;
using Mmfeedback.Models.Entities;
using Mmfeedback.Models.Abstract;
using System.Web;

namespace Mmfeedback.Tests
{
	[TestFixture]
	public class HomeControllerTest
	{
		[Test]
		public void TestPaging ()
		{
			// Arrange
			var mock = new Mock<IReviewRepository> ();
			mock.Setup (x => x.Tags).Returns (new List<string> () {
				"calc",
				"shamgunov",
				"algo",
				"shur",
				"dm",
				"programming",
				"egorov",
				"okulovskiy",
				"misc",
				"cs",
				"algebra",
				"vernikov",
				"python",
				"blinov",
				"kornev",
				"magaz",
				"orazkimovich",
				"oop",
				"klepinin"
			}.AsQueryable());
			mock.Setup (x => x.Reviews).Returns (new List<Review> () {
				new Review () { Title = "title1", Description = "calc shamgunov", Id = 1,
					CommunityUrl = "url", CommunutyDiscussionsCount = 0, Author = null,
					Category = "", Tags = new string[]{ "calculus", "shamgunov" }, Date = "1.1.1"
				}, 
				new Review () { Title = "title2", Description = "dm shur", Id = 1,
					CommunityUrl = "url", CommunutyDiscussionsCount = 0, Author = null,
					Category = "", Tags = new string[]{ "dm", "shur" }, Date = "1.1.1" 
				},
				new Review () { Title = "title3", Description = "algo shur", Id = 1,
					CommunityUrl = "url", CommunutyDiscussionsCount = 0, Author = null,
					Category = "", Tags = new string[]{ "algo", "shur" }, Date = "1.1.1"
				},
				new Review () { Title = "title4", Description = "dm shur", Id = 1,
					CommunityUrl = "url", CommunutyDiscussionsCount = 0, Author = null,
					Category = "", Tags = new string[]{ "dm", "shur" }, Date = "1.1.1"
				},
				new Review () { Title = "title5", Description = "programming o", Id = 1,
					CommunityUrl = "url", CommunutyDiscussionsCount = 0, Author = null,
					Category = "", Tags = new string[]{ "programming", "okulovskiy" }, Date = "1.1.1"
				},
				new Review () { Title = "title", Description = "programming o", Id = 1,
					CommunityUrl = "url", CommunutyDiscussionsCount = 0, Author = null,
					Category = "", Tags = new string[]{ "programming", "okulovskiy" }, Date = "1.1.1"
				}, 
				new Review () { Title = "title", Description = "programming e", Id = 1,
					CommunityUrl = "url", CommunutyDiscussionsCount = 0, Author = null,
					Category = "", Tags = new string[]{ "programming", "egorov" }, Date = "1.1.1"
				},
				new Review () { Title = "title", Description = "misc e", Id = 1,
					CommunityUrl = "url", CommunutyDiscussionsCount = 0, Author = null,
					Category = "", Tags = new string[]{ "misc", "egorov" }, Date = "1.1.1"
				},
				new Review () { Title = "title", Description = "cs", Id = 1, PostId = 1,
					CommunityUrl = "url", CommunutyDiscussionsCount = 0, Author = "",
		            Category = "", Tags = new string[]{ "cs" }, Date = "1.1.1", AuthorId = "1"
				},
				new Review () { Title = "title", Description = "cs programming", Id = 1,
					CommunityUrl = "url", CommunutyDiscussionsCount = 0, Author = null,
					Category = "", Tags = new string[]{ "cs", "programming" }, Date = "1.1.1"
				},
				new Review () { Title = "title1", Description = "eng th", Id = 1,
					CommunityUrl = "url", CommunutyDiscussionsCount = 0, Author = null,
					Category = "", Tags = new string[]{ "english", "thousands" }, Date = "1.1.1"
				}, 
				new Review () { Title = "title1", Description = "algebra v", Id = 1,
					CommunityUrl = "url", CommunutyDiscussionsCount = 0, Author = null,
					Category = "", Tags = new string[]{ "algebra", "vernikov" }, Date = "1.1.1"
				},
				new Review () { Title = "title1", Description = "magaz", Id = 1,
					CommunityUrl = "url", CommunutyDiscussionsCount = 0, Author = null,
					Category = "", Tags = new string[]{ "magaz", "orazkimovich" }, Date = "1.1.1"
				},
				new Review () { Title = "title", Description = "p blinov", Id = 1,
					CommunityUrl = "url", CommunutyDiscussionsCount = 0, Author = null,
					Category = "", Tags = new string[]{ "python", "blinov" }, Date = "1.1.1"
				},
				new Review () { Title = "title", Description = "p k", Id = 1,
					CommunityUrl = "url", CommunutyDiscussionsCount = 0, Author = null,
					Category = "", Tags = new string[]{ "python", "kornev" }, Date = "1.1.1"
				},
				new Review () { Title = "title", Description = "cs p", Id = 1,
					CommunityUrl = "url", CommunutyDiscussionsCount = 0, Author = null,
					Category = "", Tags = new string[]{ "cs", "python" }, Date = "1.1.1"
				}, 
				new Review () { Title = "title", Description = "oop cs", Id = 1,
					CommunityUrl = "url", CommunutyDiscussionsCount = 0, Author = null,
					Category = "", Tags = new string[]{ "cs", "oop" }, Date = "1.1.1"
				},
				new Review () { Title = "title", Description = "oop klep", Id = 1,
					CommunityUrl = "url", CommunutyDiscussionsCount = 0, Author = null,
					Category = "", Tags = new string[]{ "oop", "klepinin" }, Date = "1.1.1"
				},
				new Review () { Title = "title", Description = "oop klep", Id = 1,
					CommunityUrl = "url", CommunutyDiscussionsCount = 0, Author = null,
					Category = "", Tags = new string[]{ "oop", "klepinin" }, Date = "1.1.1"
				},
				new Review () { Title = "title", Description = "algo shur", Id = 1,
					CommunityUrl = "url", CommunutyDiscussionsCount = 0, Author = null,
					Category = "", Tags = new string[]{ "algo", "shur" }, Date = "1.1.1"
				}
			}.AsQueryable ());
			var contextMock = new Mock<ControllerContext> ();
			contextMock.Setup (x => x.HttpContext.Request["X-Requested-With"]).Returns ("");
			var controller = new HomeController (mock.Object, 4);
			controller.ControllerContext = contextMock.Object;

			// Act
			var result = controller.Index();
			Assert.IsInstanceOf<ViewResult> (result);
			var viewResult = result as ViewResult;
			var reviews = (IEnumerable<Review>)viewResult.Model;
			// Assert
			var reviewsArray = reviews.ToArray();
			Assert.IsTrue(reviewsArray.Length == 4);
			Assert.AreEqual (reviewsArray[0].Title, "title1");
			Assert.AreEqual (reviewsArray[3].Title, "title4");
		}

		[Test]
		public void TestTagsFitting(){
			// Arrange
			var mock = new Mock<IReviewRepository> ();
			mock.Setup (x => x.Tags).Returns (new List<string> () {
				"calc",
				"shamgunov",
				"algo",
				"shur",
				"dm",
				"programming",
				"egorov",
				"okulovskiy",
				"misc",
				"cs",
				"algebra",
				"vernikov",
				"python",
				"blinov",
				"kornev",
				"magaz",
				"orazkimovich",
				"oop",
				"klepinin"
			}.AsQueryable());
			var controller = new HomeController (mock.Object);

			// Act
			var resultWithEmptyString = controller.GetFitTags("").Content;
			var resultWithFittedString = controller.GetFitTags ("alg").Content;
			var resultWithWrongString = controller.GetFitTags ("johny").Content;

			// Assert
			Assert.IsTrue(resultWithEmptyString.Split(',').Length == 5);
			Assert.IsTrue (resultWithFittedString.Split (',').Length == 2);
			Assert.AreEqual (resultWithWrongString, "");
			Assert.AreEqual (resultWithFittedString, "algo,algebra");
		}
	}
}
