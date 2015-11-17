using System;
using Mmfeedback.Models.Entities;
using System.Linq;
using System.Collections.Generic;

namespace Mmfeedback.Models.Abstract
{
	public interface IReviewRepository
	{
		IQueryable<Review> Reviews { get; }
		IQueryable<string> Tags { get; }

		void Add (Review review);
		Review Get(int id);
		int GetNextId();
		int UpdateCommunityDiscussionsCount (int id);
	}
}

