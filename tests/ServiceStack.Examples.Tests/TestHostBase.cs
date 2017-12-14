using System;
using Funq;
using NUnit.Framework;
using ServiceStack.Configuration;
using ServiceStack.Data;
using ServiceStack.Host;
using ServiceStack.Logging;
using ServiceStack.OrmLite;
using ServiceStack.Testing;
using ServiceStack.Web;
using ServiceStack.Examples.ServiceInterface;
using ServiceStack.Examples.ServiceInterface.Support;

namespace ServiceStack.Examples.Tests
{
	[TestFixture]
	public class TestHostBase
	{
		protected const string InMemoryDb = ":memory:";
		private static ILog log;

		public readonly ServiceStackHost appHost;

		public TestHostBase()
		{
			LogManager.LogFactory = new DebugLogFactory();
			log = LogManager.GetLogger(GetType());

			appHost = new BasicAppHost(typeof(GetFactorialService).Assembly) {
				ConfigureContainer = Configure
			}.Init();
		}

		[OneTimeTearDown]
		public void TestFixtureTearDown()
		{
			appHost.Dispose();
		}

		protected IDbConnectionFactory ConnectionFactory
		{
			get
			{
				return appHost.Container.Resolve<IDbConnectionFactory>();
			}
		}		

		public void Configure(Container container)
		{
			container.Register<IAppSettings>(c => new AppSettings());

			container.Register<IDbConnectionFactory>(c =>
				new OrmLiteConnectionFactory(
					InMemoryDb,
					SqliteDialect.Provider));

			ConfigureDatabase.Init(container.Resolve<IDbConnectionFactory>());

			log.InfoFormat("TestAppHost Created: " + DateTime.Now);
		}
		
		/// <summary>
		/// Process a webservice in-memory
		/// </summary>
		/// <typeparam name="TResponse"></typeparam>
		/// <param name="requestDto"></param>
		/// <returns></returns>
		public TResponse Send<TResponse>(object requestDto)
		{
			return Send<TResponse>(requestDto, new BasicRequest());
		}

		public TResponse Send<TResponse>(object requestDto, IRequest request)
		{
			return (TResponse)appHost.ServiceController.Execute(requestDto,
				request);
		}
	}
}