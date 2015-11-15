using System;
using Mmfeedback.Models.Entities;
using System.Linq;
using System.Xml.Linq;
using System.Web;
using Mmfeedback.Models.Abstract;

namespace Mmfeedback.Models.Concrete
{
	public class MessageRepository : IMessageRepository
	{
		public IQueryable<Message> Items { get; }
		private static string _dbPath;
		private readonly XDocument _database;

		public MessageRepository ()
		{
			_dbPath = HttpContext.Current.Server.MapPath ("~/App_LocalResources/Messages.xml");
			_database = XDocument.Load (_dbPath);
			var data = _database
				.Descendants ("message")
				.Select (message => new Message () {
					Title = message.Element ("title").Value,
					Description = message.Element ("description").Value,
					Author = message.Element ("author").Value,
					AuthorId = message.Element ("authorid").Value,
				})
				.AsQueryable ();
			Items = data;
		}

		public void Add(Message message){
			var messageElement = new XElement ("message");
			foreach (var property in typeof(Message).GetProperties()) {
				var propertyName = property.Name.ToLower ();
				messageElement.Add (new XElement (propertyName) { Value = (string)property.GetValue (message) });
			}
			_database.Root.Add (messageElement);
			_database.Save (_dbPath);
		}
	}
}

