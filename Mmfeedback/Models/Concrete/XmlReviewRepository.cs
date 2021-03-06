﻿using System;
using Mmfeedback.Models.Abstract;
using Mmfeedback.Models.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Web;
using System.Reflection;
using kasthack.vksharp;

namespace Mmfeedback.Models.Concrete
{
	public class XmlReviewRepository : IReviewRepository
	{
		public IQueryable<Review> Reviews { get; }
		public IQueryable<string> Tags { get; }
		private static string _dbPath;
		private readonly XDocument _database;

		public XmlReviewRepository ()
		{
			
			_dbPath = HttpContext.Current.Server.MapPath ("~/App_LocalResources/Data.xml");
			_database = XDocument.Load (_dbPath);
			var data = _database
				.Descendants ("review")
				.Select (review => new Review () {
					Id = Int32.Parse(review.Element ("id").Value),
					PostId = Int32.Parse(review.Element ("postid").Value),
					Title = review.Element ("title").Value,
					Description = review.Element ("description").Value,
					Date = review.Element ("date").Value,
					CommunityUrl = review.Element ("communityurl").Value,
					CommunutyDiscussionsCount = (Int32.Parse(review.Element ("communutydiscussionscount").Value)),
					Author = review.Element ("author").Value,
					AuthorId = review.Element ("authorid").Value,
					Tags = review.Element ("tags").Value.Split(',')
			})
				.OrderByDescending(review => review.Id)
				.AsQueryable ();
			Reviews = data;
			Tags = _database
				.Descendants ("tags")
				.SelectMany (element => element.Value.Split(','))
				.Distinct ()
				.AsQueryable ();
		}

		public int UpdateCommunityDiscussionsCount(int id){
			int count;
			var element = _database
				.Descendants ("review")
				.Where (review => Int32.Parse (review.Element ("id").Value) == id)
				.Select (review => review)
				.FirstOrDefault ();
			var postId = Int32.Parse (element.Element ("postid").Value);
			var oldCount = Int32.Parse (element.Element ("communutydiscussionscount").Value);
			var api = new Api ();
			//api.AddToken (new Token ("22b299f8c56446504035cc2c561b95823a3a21b5afa2377b620e0e36aac1e8ac947520d0f12c673a6d8ea"));
			api.AddToken(new Token("cf54ae77fdfba85e3e141c865552d916191a636d0dd682b6472f3ba2a1b9883cecae7e2f7dab7273afcd3"));
			try{
				count = api.Wall.GetByIdSync(0, 
					new string[] { "-106361362_" + postId })[0].Comments.Count;
			}
			catch (IndexOutOfRangeException e){
				count = oldCount;
			}
			element.Element ("communutydiscussionscount").Value = count.ToString ();
			_database.Save (_dbPath);
			return count;
		}

		public void Add(Review review){
			var reviewElement = new XElement ("review");
			foreach (var property in typeof(Review).GetProperties()) {
				var propertyName = property.Name.ToLower ();
				if (propertyName != "tags" && propertyName != "id" 
					&& propertyName != "communutydiscussionscount" && propertyName != "postid")
					reviewElement.Add (new XElement (propertyName) { Value = (string)property.GetValue (review) });
				else if (propertyName == "tags")
					reviewElement.Add (new XElement ("tags") { Value = String.Join (",", review.Tags) });
				else
					reviewElement.Add (new XElement (propertyName) { Value = ((int)property.GetValue (review)).ToString() });
			}
			_database.Root.Add (reviewElement);
			_database.Save (_dbPath);
		}

		public Review Get(int id){
			return Reviews
				.ToList ()
				.Find (review => review.Id == id);
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

