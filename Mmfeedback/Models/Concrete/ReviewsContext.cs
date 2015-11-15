using System;
using System.Data.Entity;
using Mmfeedback.Models.Entities;
using Mmfeedback.Infrastructure;
namespace Mmfeedback.Models.Concrete
{
	public class ReviewsContext : DbContext
	{
		public ReviewsContext () : base ("Db")
		{
		}

		public DbSet<Review> Reviews { get; set; }
		public DbSet<string> Tags { get; set; }
		public DbSet<PendingReview> PendingReviews { get; set; }

//		protected override void OnModelCreating(DbModelBuilder b){
//			var initializer = new ReviewsContextInitializer()<ReviewsContext> (b);
//			Database.SetInitializer (initializer);
//		}
	}
}