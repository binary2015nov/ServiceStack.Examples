using ServiceStack;
using ServiceStack.Redis;

namespace Backbone.Todos
{
    // Define your ServiceStack web service request (i.e. Request DTO).
    public class Todo
    {
        public long Id { get; set; }
        public string Content { get; set; }
        public int Order { get; set; }
        public bool Done { get; set; }
    }

    // Create your ServiceStack rest-ful web service implementation. 
    public class TodoService : Service
    {
        public object Get(Todo todo)
        {
            //Return a single Todo if the id is provided.
            if (todo.Id != 0)
                return Redis.As<Todo>().GetById(todo.Id);

            //Return all Todos items.
            return Redis.As<Todo>().GetAll();
        }

        // Handles creating and updating the Todo items.
        public Todo Post(Todo todo)
        {
            var redis = Redis.As<Todo>();

            //Get next id for new todo
            if (todo.Id == 0)
                todo.Id = redis.GetNextSequence();

            redis.Store(todo);

            return todo;
        }

        // Handles creating and updating the Todo items.
        public Todo Put(Todo todo)
        {
            return Post(todo);
        }

        // Handles Deleting the Todo item
        public Todo Delete(Todo todo)
        {
            Redis.As<Todo>().DeleteById(todo.Id);
            return todo;
        }
    }

    // Create your ServiceStack web service application with a singleton AppHost.
    public class TodosAppHost : AppHostBase
    {
        // Initializes a new instance of your ServiceStack application, with the specified name and assembly containing the services.
        public TodosAppHost() : base("Backbone.js TODO", typeof(TodoService).Assembly)
        {
            //Configure ServiceStack Json web services to return idiomatic Json camelCase properties.
            Config.UseCamelCase = true;
        }

        // Configure the container with the necessary routes for your ServiceStack application.
        public override void Configure(Funq.Container container)
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