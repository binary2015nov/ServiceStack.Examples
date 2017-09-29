using System;
using Funq;
using ServiceStack.Configuration;
using ServiceStack.Data;
using ServiceStack.Examples.ServiceInterface;
using ServiceStack.Examples.ServiceInterface.Support;
using ServiceStack.Formats;
using ServiceStack.Logging;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.Sqlite;

namespace ServiceStack.Examples.Host.Web
{
    /// <summary>
    /// An example of a AppHost to have your services running inside a web server.
    /// </summary>
    public class AppHost : AppHostBase
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(AppHost));

        public AppHost() : base("ServiceStack Examples", typeof(GetFactorialService).Assembly)
        {
            //Permit modern browsers (e.g. Firefox) to allow sending of any REST HTTP Method
            Config.DebugMode = true;
        }

        public override void Configure(Container container)
        {
            Plugins.Add(new CorsFeature());

            container.Register<IAppSettings>(new AppSettings());
            container.Register(c => new ExampleConfig(c.Resolve<IAppSettings>()));

            var appConfig = container.Resolve<ExampleConfig>();

            container.Register<IDbConnectionFactory>(c =>
                new OrmLiteConnectionFactory(
                    appConfig.ConnectionString.MapHostAbsolutePath(),
                    SqliteOrmLiteDialectProvider.Instance));

            ConfigureDatabase.Init(container.Resolve<IDbConnectionFactory>());

            //If you give Redis a try, you won't be disappointed. This however requires Redis to be installed.
            //container.Register<ICacheClient>(c => new BasicRedisClientManager());

            Logger.InfoFormat("AppHost Configured: {0}", DateTime.Now);
        }
    }

}