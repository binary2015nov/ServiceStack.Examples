using System;
using System.Configuration;
using System.IO;
using ServiceStack.Configuration;
using ServiceStack.Logging;

namespace ServiceStack.Examples.Host.Web
{
	public class Global : System.Web.HttpApplication
	{
		protected void Application_Start(object sender, EventArgs e)
		{
			LogManager.LogFactory = new ConsoleLogFactory();

			//if (File.Exists(@"C:\src\appsettings.license.txt"))
			//	Licensing.RegisterLicenseFromFile(@"C:\src\appsettings.license.txt");
			//else if (string.IsNullOrEmpty(ConfigUtils.GetNullableAppSetting("servicestack:license")))
			//	throw new ConfigurationErrorsException("A valid license key is required for this demo");

			new AppHost().Init();
		}
	}
}