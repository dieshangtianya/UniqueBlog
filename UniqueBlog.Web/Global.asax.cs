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
using UniqueBlog.Infrastructure.Log;
using System.Reflection;

namespace UniqueBlog.Web
{
	public class Global: System.Web.HttpApplication
	{
        private static readonly ILog logger = LoggerFactory.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		protected void Application_Start(object sender, EventArgs e)
		{
			AreaRegistration.RegisterAllAreas();

			//注册路由
			GlobalRoutesConfig.RegisterRoutes(RouteTable.Routes);
			//初始化MEF
			MVCMEFConfiguration.RegisterMef();
			//注册AutoMapper
			MapperManager.RegisterTypeMapper();
            //配置日志
            LogConfiguration.ConfigLog();
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
            Exception exception = Server.GetLastError();
            logger.Fatal("There is an fatal error happen when the web application is running", exception);
		}

		protected void Session_End(object sender, EventArgs e)
		{

		}

		protected void Application_End(object sender, EventArgs e)
		{

		}
	}
}