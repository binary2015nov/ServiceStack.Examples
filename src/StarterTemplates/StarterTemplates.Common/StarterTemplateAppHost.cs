using ServiceStack;

namespace StarterTemplates.Common
{
	//ASP.NET Hosts
	public class StarterTemplateAppHost : AppHostBase
	{
		public StarterTemplateAppHost() : base("", typeof(HelloService).Assembly)
		{
			ServiceName = AppSettings.Get("ServiceName") ?? "StarterTemplate ASP.NET Host";
		}

		public override void Configure(Funq.Container container)
		{
			container.Register(new TodoRepository());
		}
	}

	//HttpListener Hosts
	public class StarterTemplateAppListenerHost : AppHostHttpListenerBase
	{
		public StarterTemplateAppListenerHost() : base("", typeof(HelloService).Assembly)
		{
			ServiceName = AppSettings.Get("ServiceName") ?? "StarterTemplate HttpListener";
		}

		public override void Configure(Funq.Container container)
		{
			container.Register(new TodoRepository());
		}
	}
}
