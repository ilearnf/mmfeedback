using System;
using NUnit.Framework;
using Mmfeedback.Controllers;
using System.Web.Mvc;
using Mmfeedback.Models.Abstract;
using Moq;
using Mmfeedback.Models.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mmfeedback.Tests
{
	[TestFixture]
	public class AdminControllerTest
	{
		[Test]
		public void TestLogin(){
			// Arrange
			var mock = new Mock<IReviewRepository>();
			var pendingMock = new Mock<IPendingRepository>();
			var messageMock = new Mock<IMessageRepository>();
			mock.Setup (x => x.Reviews).Returns (new List<Review> ().AsQueryable());
			pendingMock.Setup (x => x.Reviews).Returns (new List<PendingReview> ().AsQueryable ());
			messageMock.Setup (x => x.Items).Returns (new List<Message> ().AsQueryable ());
			var context = new Mock<ControllerContext> ();
			var session = new Mock<HttpSessionStateBase> ();
			context.Setup (x => x.HttpContext.Session).Returns (session.Object);
			context.SetupGet (x => x.HttpContext.Session ["logged"]).Returns (false);
			context.SetupSet (x => x.HttpContext.Session ["logged"] = It.IsAny<bool> ()).Callback (
				(string name, object value) => {
					context.SetupGet (x => x.HttpContext.Session ["logged"]).Returns ((bool)value);
				});
			var controller = new AdminController(mock.Object, pendingMock.Object, messageMock.Object);
			controller.ControllerContext = context.Object;

			// Act
			var resultPassed = controller.Login("111");
			var resultFailed = controller.Login ("112");

			// Assert
			Assert.That(resultFailed, Is.TypeOf(typeof(RedirectToRouteResult)));
			Assert.That(resultPassed, Is.TypeOf(typeof(RedirectToRouteResult)));
			var resultPassedRedirect = resultPassed as RedirectToRouteResult;
			var resultFailedRedirect = resultFailed as RedirectToRouteResult;
			Assert.AreEqual (resultFailedRedirect.RouteValues["action"], "ThrowError");
			Assert.AreEqual (resultPassedRedirect.RouteValues["action"], "Index");
		}

		[Test]
		public void TestAddReview(){
			// Arrange
			var mock = new Mock<IReviewRepository>();
			var pendingMock = new Mock<IPendingRepository>();
			var messageMock = new Mock<IMessageRepository>();
			mock.Setup (x => x.Reviews).Returns (new List<Review> ().AsQueryable());
			mock.Setup (x => x.GetNextId ()).Returns (0);
			pendingMock.Setup (x => x.Reviews).Returns (new List<PendingReview> ().AsQueryable ());
			messageMock.Setup (x => x.Items).Returns (new List<Message> ().AsQueryable ());
			var controller = new AdminController(mock.Object, pendingMock.Object, messageMock.Object);

			// Act
			var result = controller.PostReview(0, "desc", "t1,t2");

			// Assert
			Assert.IsInstanceOf(typeof(ContentResult), result);
			mock.Verify (x => x.Add (It.IsAny<Review>()), Times.Once);
		}


	}
}

