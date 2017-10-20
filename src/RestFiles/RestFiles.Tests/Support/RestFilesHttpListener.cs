using ServiceStack;
using RestFiles.ServiceInterface;

namespace RestFiles.Tests
{
	public class RestFilesHttpListener : AppHostHttpListenerBase
	{
		public const string ListeningOn = "http://localhost:8081/";

		public RestFilesHttpListener() : base("HttpListener Hosts for Unit Tests", typeof(FilesService).Assembly) { }

		public override void Configure(Funq.Container container)
		{
			var config = new AppConfig
			{
				RootDirectory = "~/App_Data/files/".MapAbsolutePath(),
				TextFileExtensions = ".txt,.sln,.proj,.cs,.config,.asax".Split(','),
			};
			container.Register(config);
		}
	}
}