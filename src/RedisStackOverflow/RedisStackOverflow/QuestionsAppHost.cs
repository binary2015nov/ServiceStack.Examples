using Funq;
using ServiceStack;
using ServiceStack.Redis;
using RedisStackOverflow.ServiceInterface;

namespace RedisStackOverflow
{
    /// <summary>
    /// Create your ServiceStack web service application with a singleton AppHost.
    /// </summary>  
    public class QuestionsAppHost : AppHostBase
    {
        /// <summary>
        /// Initializes a new instance of your ServiceStack application, with the specified name and assembly containing the services.
        /// </summary>
        public QuestionsAppHost() : base("Redis StackOverflow", typeof(QuestionsService).Assembly)
        {
            Config.DebugMode = true;
        }

        /// <summary>
        /// Configure the container with the necessary routes for your ServiceStack application.
        /// </summary>
        /// <param name="container">The built-in IoC used with ServiceStack.</param>
        public override void Configure(Container container)
        {
            //Show StackTrace in Web Service Exceptions

            //Register any dependencies you want injected into your services
            container.Register<IRedisClientsManager>(c => new PooledRedisClientManager());
            container.Register<RedisStackOverflow.ServiceInterface.IRepository>(c => new Repository(c.Resolve<IRedisClientsManager>()));
        }
    }
}