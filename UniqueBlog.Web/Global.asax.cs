using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using UniqueBlog.Controllers.DtoMapperManager;
using UniqueBlog.Controllers.MEF;
using UniqueBlog.Controllers.RouteConfig;

namespace UniqueBlog.Web
{
	public class Global: System.Web.HttpApplication
	{

		protected void Application_Start(object sender, EventArgs e)
		{
			AreaRegistration.RegisterAllAreas();

			//注册路由
			GlobalRoutesConfig.RegisterRoutes(RouteTable.Routes);
			//初始化MEF
			MefConfiguration.RegisterMef();
			//注册AutoMapper
			MapperManager.RegisterTypeMapper();
		}

		protected void Session_Start(object sender, EventArgs e)
		{

		}

		protected void Application_BeginRequest(object sender, EventArgs e)
		{

		}

		protected void Application_AuthenticateRequest(object sender, EventArgs e)
		{

		}

		protected void Application_Error(object sender, EventArgs e)
		{

		}

		protected void Session_End(object sender, EventArgs e)
		{

		}

		protected void Application_End(object sender, EventArgs e)
		{

		}
	}
}