using System;
using System.Configuration;
using System.IO;
using ServiceStack.Logging;

namespace ServiceStack.Examples.Host.Web
{
	public class Global : System.Web.HttpApplication
	{
		protected void Application_Start(object sender, EventArgs e)
		{
			LogManager.LogFactory = new ConsoleLogFactory();

			new AppHost().Init();
		}
	}
}