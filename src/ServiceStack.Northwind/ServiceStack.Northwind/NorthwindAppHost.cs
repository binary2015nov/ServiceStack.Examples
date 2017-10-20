using Funq;
using ServiceStack.Data;
using ServiceStack.Northwind.ServiceInterface;
using ServiceStack.OrmLite;

namespace ServiceStack.Northwind
{
    public class NorthwindAppHost : AppHostBase
    {
        public NorthwindAppHost() : base("Northwind Web Services", typeof(CustomersService).Assembly) { }

        public override void Configure(Container container)
        {
            container.Register<IDbConnectionFactory>(
                new OrmLiteConnectionFactory("~/App_Data/Northwind.sqlite".MapHostAbsolutePath(), SqliteDialect.Provider));

            //Use Redis Cache
            //container.Register<ICacheClient>(new PooledRedisClientManager());

            VCardFormat.Register(this);
        }
    }
}