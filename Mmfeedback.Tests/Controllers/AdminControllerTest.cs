using System;
using NUnit.Framework;
using Mmfeedback.Controllers;
using System.Web.Mvc;
using Mmfeedback.Models.Abstract;
using Moq;
using Mmfeedback.Models.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Mmfeedback.Tests
{
	[TestFixture]
	public class AdminControllerTest
	{
		[Test]
		public void TestLogin(){
			// Arrange
			var controller = new AdminController(null, null, null);

			// Act
			var resultPassed = controller.Login("111");
			var resultFailed = controller.Login ("112");

			// Assert
			Assert.IsInstanceOf(typeof(RedirectResult), resultFailed);
			Assert.IsInstanceOf(typeof(RedirectResult), resultPassed);
			var resultPassedRedirect = resultPassed as RedirectResult;
			var resultFailedRedirect = resultFailed as RedirectResult;
			Assert.AreEqual (resultFailedRedirect.Url, "/Admin/ThrowError");
			Assert.AreEqual (resultPassedRedirect.Url, "/Admin/Index");
		}

		[Test]
		public void TestVkLogin(){
			// Arrange
			var controller = new AdminController(null, null, null);

			// Act
			var result = controller.VkLogin();

			// Assert
			Assert.IsInstanceOf(typeof(RedirectResult), result);
		}

		[Test]
		public void TestAddReview(){
			// Arrange
			var mock = new Mock<IReviewRepository>();
			mock.Setup (x => x.Reviews).Returns (new List<Review> ().AsQueryable());
			var controller = new AdminController(mock.Object, null, null);

			// Act
			var result = controller.PostReview(0, "desc", "t1,t2");

			// Assert
			Assert.IsInstanceOf(typeof(ContentResult), result);
			mock.Verify (x => x.Add (It.Is<Review>), Times.Once);
		}


	}
}

