using ServiceStack;
using ServiceStack.Redis;

namespace Backbone.Todos
{
    /// <summary>
    /// Create your ServiceStack web service application with a singleton AppHost.
    /// </summary>
    public class TodoAppHost : AppHostBase
    {
        // Initializes a new instance of your ServiceStack application, with the specified name and assembly containing the services.
        public TodoAppHost() : base("Backbone.js TODO", typeof(TodoService).Assembly)
        {
            //Configure ServiceStack Json web services to return idiomatic Json camelCase properties.
            Config.UseCamelCase = true;
        }

        // Configure the container with the necessary routes for your ServiceStack application.
        public override void Configure(Funq.Container container)
        {
            //Register Redis factory in Funq IoC. The default port for Redis is 6379.
            container.Register<IRedisClientsManager>(new BasicRedisClientManager());

            //Register user-defined REST Paths using the fluent configuration API
            Routes
              .Add<Todo>("/todos")
              .Add<Todo>("/todos/{Id}");
        }
    }
}