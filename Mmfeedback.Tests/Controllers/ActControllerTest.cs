using System;
using NUnit.Framework;
using Moq;
using Mmfeedback.Models.Entities;
using System.Collections.Generic;
using System.Linq;
using Mmfeedback.Controllers;
using Mmfeedback.Models.ViewModels;
using System.Web.Mvc;

namespace Mmfeedback.Tests
{
	[TestFixture]
	public class ActControllerTest
	{
		[Test]
		public void TestAddingReview(){
			// Arrange
			var pendingMock = new Mock<IPendingRepository>();
			pendingMock.Setup (x => x.Reviews).Returns (new List<PendingReview> ().AsQueryable ());
			var controller = new ActController (null, pendingMock.Object, null);

			// Act
			var result = controller.AddReview (new ReviewCreatorModel () {
				Form = new AddReviewForm () {
					Title = "title",
					Description = "desc",
					Tags = "t1,t2",
					Category = "c"
				}
			});

			// Assert
			Assert.That(result, Is.TypeOf(typeof(RedirectToRouteResult)));
			var redirect = result as RedirectToRouteResult;
			Assert.AreEqual ("Success", redirect.RouteValues ["action"]);
			pendingMock.Verify (x => x.Add (It.IsAny<PendingReview>()), Times.Once);
		}
	}
}

