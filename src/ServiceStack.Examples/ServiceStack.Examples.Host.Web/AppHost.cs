using System;
using ServiceStack.Configuration;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.Sqlite;
using ServiceStack.Examples.ServiceInterface;
using ServiceStack.Examples.ServiceInterface.Support;
using ServiceStack.Examples.ServiceModel;

namespace ServiceStack.Examples.Host.Web
{
    /// <summary>
    /// An example of a AppHost to have your services running inside a web server.
    /// </summary>
    public class AppHost : AppHostBase
    {
        public AppHost() : base("ServiceStack Examples", typeof(GetFactorialService).Assembly)
        {
            Config.DebugMode = true;
        }

        public override void Configure(Funq.Container container)
        {
            Plugins.Add(new SoapFormat());
            Plugins.Add(new CorsFeature());

            container.Register(c => new ExampleConfig(AppSettings));

            var appConfig = container.Resolve<ExampleConfig>();

            container.Register<IDbConnectionFactory>(c =>
                new OrmLiteConnectionFactory(
                    appConfig.ConnectionString.MapHostAbsolutePath(),
                    SqliteOrmLiteDialectProvider.Instance));

            ConfigureDatabase.Init(container.Resolve<IDbConnectionFactory>());

            //If you give Redis a try, you won't be disappointed. This however requires Redis to be installed.
            //container.Register<ICacheClient>(c => new BasicRedisClientManager());
        }
    }
}