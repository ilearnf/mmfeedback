using System;
using System.Data.Entity;
using Mmfeedback.Models.Entities;
using Mmfeedback.Models.Abstract;
using Mmfeedback.Models.Concrete;
using System.Linq;
using System.Collections.Generic;

namespace Mmfeedback.Infrastructure
{
	public class ReviewsContextInitializer : DropCreateDatabaseIfModelChanges<ReviewsContext>
	{
		protected override void Seed(ReviewsContext context){
			var reviews = new List<Review> {
				new Review { Title = "title", Description = "description", Id = 1,
					CommunityUrl = "url", CommunutyDiscussionsCount = 0, Author = null,
					Category = "", Tags = new string[]{ "tag1", "tag2" }
				}, 
				new Review () { Title = "title", Description = "description", Id = 1,
					CommunityUrl = "url", CommunutyDiscussionsCount = 0, Author = null,
					Category = "", Tags = new string[]{ "tag1", "tag2" }
				}
			};
			reviews.ForEach(r => context.Reviews.Add(r));
			context.SaveChanges ();
		}
		public ReviewsContextInitializer ()
		{
		}
	}
}

