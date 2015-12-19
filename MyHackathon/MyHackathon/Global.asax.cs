using MyHackathon.Models.ClientModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MyHackathon
{
	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			GlobalConfiguration.Configure(WebApiConfig.Register);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);

			Database.SetInitializer(new CreateDatabaseIfNotExists<DBMapper>());

			CreateDefaultAdmin();
		}

		public void CreateDefaultAdmin()
		{
			using (var db = new DBMapper())
			{
				var mail = "Admin@admin.ru";
				var password = "123456";
				if (db.Users.FirstOrDefault(l => l.Mail == mail) != null)
				{
					return;
				}
				db.Users.Add(new Models.ClientModels.User() { Mail = mail, Password = password, Role = Roles.Admin & Roles.Professor & Roles.Student }); 
			}
		}
	}
}
