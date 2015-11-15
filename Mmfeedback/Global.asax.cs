
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mmfeedback.Infrastructure;
using Mmfeedback.Models.Abstract;
using Mmfeedback.Models.Concrete;
using Mmfeedback.Models.Entities;
using Nemiro.OAuth.Clients;
using Nemiro.OAuth;
using Mmfeedback.Infrastructure;
using System.Data.Entity;
using System.Web.Routing;
using System.Collections.Specialized;

namespace Mmfeedback
{
	public class MvcApplication : System.Web.HttpApplication
	{
		public static void RegisterRoutes (RouteCollection routes)
		{
			routes.IgnoreRoute ("{resource}.axd/{*pathInfo}");

			routes.MapRoute (
				null,
				"",
				new { controller = "Home", action = "Index" }
			);

			routes.MapRoute (
				null,
				"Page{page}",
				new { controller = "Home", action = "Index", page = UrlParameter.Optional },
				new { page = @"\d+" }
			);
				
			routes.MapRoute (
				null,
				"{search}/Page{page}",
				new { controller = "Home", action = "IndexSearch", search=UrlParameter.Optional }, 
				new { page = @"\d+" }
			);

			routes.MapRoute (
				null,
				"{controller}/{action}"
			);
		}

		public static void RegisterGlobalFilters (GlobalFilterCollection filters)
		{
			filters.Add (new HandleErrorAttribute ());
		}

		protected void Application_Start ()
		{
			AreaRegistration.RegisterAllAreas ();
			RegisterGlobalFilters (GlobalFilters.Filters);
			RegisterRoutes (RouteTable.Routes);

			ControllerBuilder.Current.SetControllerFactory (new NinjectControllerFactory ());
			Database.SetInitializer<ReviewsContext> (new ReviewsContextInitializer ());

			OAuthManager.RegisterClient (new VkontakteClient ("5126467", "teF9CazOv8SsJ3BMGVqa")
				
			);
		}
	}
}
