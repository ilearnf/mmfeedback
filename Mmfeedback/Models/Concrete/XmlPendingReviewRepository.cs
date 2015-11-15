using System;
using Mmfeedback.Models.Abstract;
using System.Xml.Linq;
using System.Linq;
using Mmfeedback.Models.Entities;
using System.Web;

namespace Mmfeedback.Models.Concrete
{
	public class XmlPendingReviewRepository : IPendingRepository
	{
		public IQueryable<PendingReview> Reviews { get; }
		private static string _dbPath;
		private readonly XDocument _database;

		public XmlPendingReviewRepository ()
		{
			_dbPath = HttpContext.Current.ApplicationInstance.Server.MapPath ("~/App_LocalResources/PendingData.xml");
			_database = XDocument.Load (_dbPath);
			var data = _database
				.Descendants ("review")
				.Select (review => new PendingReview () {
					Id = Int32.Parse(review.Element ("id").Value),
					Title = review.Element ("title").Value,
					Description = review.Element ("description").Value,
					Author = review.Element ("author").Value,
					AuthorId = review.Element ("authorid").Value,
					Tags = review.Element ("tags").Value.Split(',')
				})
				.AsQueryable ();
			Reviews = data;
		}

		public void Add(PendingReview review){
			var reviewElement = new XElement ("review");
			foreach (var property in typeof(PendingReview).GetProperties()) {
				var propertyName = property.Name.ToLower ();
				if (propertyName != "tags" && propertyName != "id")
					reviewElement.Add (new XElement (propertyName) { Value = (string)property.GetValue (review) });
				else if (propertyName == "tags")
					reviewElement.Add (new XElement ("tags") { Value = String.Join (",", review.Tags) });
				else
					reviewElement.Add (new XElement ("id") { Value = ((int)property.GetValue(review)).ToString() });
			}
			_database.Root.Add (reviewElement);
			_database.Save (_dbPath);
		}

		public PendingReview Get(int id){
			return Reviews
				.ToList ()
				.Find (review => review.Id == id);
		}

		public void Delete(int id){
			_database
				.Root
				.Elements ("review")
				.Where (review => Int32.Parse (review.Element ("id").Value) == id)
				.Remove ();
			_database.Save (_dbPath);
		}

		public int GetNextId(){
			return _database
				.Descendants ("review")
				.OrderByDescending (review => Int32.Parse (review.Element ("id").Value))
				.Select(review => Int32.Parse(review.Element("id").Value))
				.FirstOrDefault () + 1;
		}
	}
}

