using System;
using Mmfeedback.Models.Entities;

namespace Mmfeedback.Models.ViewModels
{
	public class SendMessageForm
	{
		public Message Message { get; set; }

		public SendMessageForm(){
			Message = new Message ();
		}
	}
}

