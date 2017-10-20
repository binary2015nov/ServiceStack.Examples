using System;
using System.Web;
using ServiceStack.MovieRest.App_Start;

namespace ServiceStack.MovieRest
{
	public class Global : HttpApplication
	{
		protected void Application_Start(object sender, EventArgs e)
		{
			//Initialize your application
			(new MovieRestAppHost()).Init();
		}
	}
}