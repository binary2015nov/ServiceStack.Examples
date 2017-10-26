using System;
using Funq;
using NUnit.Framework;
using ServiceStack.Configuration;
using ServiceStack.Data;
using ServiceStack.Examples.ServiceInterface;
using ServiceStack.Examples.ServiceInterface.Support;
using ServiceStack.Logging;
using ServiceStack.OrmLite;
using ServiceStack.Examples.ServiceModel;

namespace ServiceStack.Examples.Tests.Integration
{
	public class IntegrationTestAppHost : AppHostHttpListenerBase
	{
		public IntegrationTestAppHost() : base("ServiceStack Examples", typeof(MovieRestService).Assembly) { }

		public override void Configure(Container container)
		{
			container.Register(c => new ExampleConfig(c.Resolve<IAppSettings>()));
			var appConfig = container.Resolve<ExampleConfig>();

			container.Register<IDbConnectionFactory>(c =>
				 new OrmLiteConnectionFactory(
					":memory:",			//Use an in-memory database instead
					SqliteDialect.Provider));
		}
	}

	public class IntegrationTestBase
	{
		private const string BaseUrl = "http://127.0.0.1:8081/";

		private ServiceStackHost appHost;

		[OneTimeSetUp]
		public void TestFixtureSetUp()
		{
			LogManager.LogFactory = new ConsoleLogFactory();
			appHost = new IntegrationTestAppHost()
				.Init()
				.Start(BaseUrl);
		}

		[OneTimeTearDown]
		public void TestFixtureTearDown()
		{
			appHost.Dispose();
		}

		[SetUp]
		public void OnEachMethodBefore()
		{
			ConfigureDatabase.Init(appHost.Resolve<IDbConnectionFactory>());
		}

		public void SendToEachEndpoint<TRes>(object request, Action<TRes> validate)
		{
			SendToEachEndpoint(request, null, validate);
		}

		/// <summary>
		/// Run the request against each Endpoint
		/// </summary>
		/// <typeparam name="TRes"></typeparam>
		/// <param name="request"></param>
		/// <param name="validate"></param>
		/// <param name="httpMethod"></param>
		public void SendToEachEndpoint<TRes>(object request, string httpMethod, Action<TRes> validate)
		{
			using (var jsonClient = new JsonServiceClient(BaseUrl))
			{
				jsonClient.HttpMethod = httpMethod;
				var jsonResponse = jsonClient.Send<TRes>(request);
				if (validate != null) validate(jsonResponse);
			}
		}

	}
}
