using System;
using System.Linq;
using System.Collections.Generic;
using Mmfeedback.Models.Entities;

namespace Mmfeedback.Models.Abstract
{
	public interface IMessageRepository
	{
		IQueryable<Message> Items { get; }

		void Add (Message review);
	}
}

