using System;
using System.Web;
using Funq;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using ServiceStack.Northwind.ServiceInterface;

namespace ServiceStack.Northwind
{
	public class AppHost : AppHostBase
	{
		public AppHost() : base("Northwind Web Services", typeof(CustomersService).Assembly) { }

		public override void Configure(Container container)
		{
			container.Register<IDbConnectionFactory>(
				new OrmLiteConnectionFactory("~/App_Data/Northwind.sqlite".MapHostAbsolutePath(), SqliteDialect.Provider));

			//Use Redis Cache
			//container.Register<ICacheClient>(new PooledRedisClientManager());

			VCardFormat.Register(this);
		}
	}

	public class Global : HttpApplication
	{
		protected void Application_Start(object sender, EventArgs e)
		{
			(new AppHost()).Init();
		}
	}
}