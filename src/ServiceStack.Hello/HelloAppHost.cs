namespace ServiceStack.Hello
{
    /// <summary>
    /// Create your ServiceStack web service application with a singleton HelloAppHost.
    /// </summary>        
    public class HelloAppHost : AppHostBase
    {
        /// <summary>
        /// Initializes a new instance of your ServiceStack application, with the specified name and assembly containing the services.
        /// </summary>
        public HelloAppHost() : base("Hello Web Services", typeof(HelloService).Assembly) { }

        /// <summary>
        /// Configure the container with the necessary routes for your ServiceStack application.
        /// </summary>
        /// <param name="container">The built-in IoC used with ServiceStack.</param>
        public override void Configure(Funq.Container container)
        {
            //Register user-defined REST-ful urls. You can access the service at the url similar to the following.
            //http://localhost:62577/servicestack/hello or http://localhost:62577/servicestack/hello/John%20Doe
            //You can change /servicestack/ to a custom path in the web.config.
            Routes
                .Add<Hello>("/hello")
                .Add<Hello>("/hello/{Name}");
        }
    }
}