using Funq;
using ServiceStack;
using ServiceStack.Redis;

namespace Backbone.Todos
{
    // Create your ServiceStack web service application with a singleton AppHost.
    public class AppHost : AppHostBase
    {
        // Initializes a new instance of your ServiceStack application, with the specified name and assembly containing the services.
        public AppHost() : base("Backbone.js TODO", typeof(TodosService).Assembly)
        {
            //Configure ServiceStack Json web services to return idiomatic Json camelCase properties.
            Config.UseCamelCase = true;
        }

        // Configure the container with the necessary routes for your ServiceStack application.
        public override void Configure(Container container)
        {
            //Register Redis factory in Funq IoC. The default port for Redis is 6379.
            container.Register<IRedisClientsManager>(new BasicRedisClientManager("localhost:6379"));

            //Register user-defined REST Paths using the fluent configuration API
            Routes
              .Add<Todo>("/todos")
              .Add<Todo>("/todos/{Id}");
        }
    }
}