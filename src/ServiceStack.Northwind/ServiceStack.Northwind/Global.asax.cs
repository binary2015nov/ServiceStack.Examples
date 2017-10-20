using System;
using System.Web;

namespace ServiceStack.Northwind
{
	public class Global : HttpApplication
	{
		protected void Application_Start(object sender, EventArgs e)
		{
			(new NorthwindAppHost()).Init();
		}
	}
}