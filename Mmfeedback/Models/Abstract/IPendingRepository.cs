using System;
using System.Linq;
using Mmfeedback.Models.Entities;

namespace Mmfeedback
{
	public interface IPendingRepository
	{
		IQueryable<PendingReview> Reviews { get; }

		void Add (PendingReview review);
		PendingReview Get(int id);
		void Delete(int id);
		int GetNextId();
	}
}

