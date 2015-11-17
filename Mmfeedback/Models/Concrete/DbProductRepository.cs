using System;
using Mmfeedback.Models.Entities;
using Mmfeedback.Models.Abstract;
using Mmfeedback.Models.Concrete;
using System.Linq;

namespace Mmfeedback
{
	public class DbProductRepository : IReviewRepository
	{
		private ReviewsContext db = new ReviewsContext();

		public IQueryable<Review> Reviews { get { return db.Reviews; } }
		public IQueryable<string> Tags { get { return db.Tags; } }

		public virtual void Add(Review review){
		}
		public virtual Review Get(int id){
			return new Review ();
		}
		public virtual int GetNextId(){
			return 1;
		}
		public int UpdateCommunityDiscussionsCount(int id){
			return 1;
		}
	}
}

