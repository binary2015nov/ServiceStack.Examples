using Funq;
using ServiceStack;

namespace StarterTemplates.Common
{
	//ASP.NET Hosts
	public class StarterTemplateAppHost : AppHostBase
	{
		public StarterTemplateAppHost() : base(string.Empty, typeof(HelloService).Assembly)
		{
			ServiceName = AppSettings.Get("ServiceName") ?? "StarterTemplate ASP.NET Host";
		}

		public override void Configure(Container container)
		{
			container.Register(new TodoRepository());
		}
	}

	//HttpListener Hosts
	public class StarterTemplateAppListenerHost : AppHostHttpListenerBase
	{
		public StarterTemplateAppListenerHost() : base(string.Empty, typeof(HelloService).Assembly)
		{
			ServiceName = AppSettings.Get("ServiceName") ?? "StarterTemplate HttpListener";
		}

		public override void Configure(Container container)
		{
			container.Register(new TodoRepository());
		}
	}
}
