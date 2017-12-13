using System;
using Funq;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using RestIntro.ServiceInterface;
using RestIntro.ServiceModel;

namespace RestIntro
{
    /// <summary>
    /// Create your ServiceStack web service application with a singleton AppHost.
    /// </summary> 
    public class RestIntroAppHost : AppHostBase
    {
        /// <summary>
        /// Initializes a new instance of your ServiceStack application, with the specified name and assembly containing the services.
        /// </summary>
        public RestIntroAppHost() : base("REST Intro", typeof(CustomerService).Assembly) { }

        /// <summary>
        /// Configure the container with the necessary routes for your ServiceStack application.
        /// </summary>
        /// <param name="container">The built-in IoC used with ServiceStack.</param>
        public override void Configure(Container container)
        {
            container.Register<IDbConnectionFactory>(
                new OrmLiteConnectionFactory(
                "~/RestIntro.sqlite".MapHostAbsolutePath(),
                SqliteDialect.Provider));

            using (var db = container.Resolve<IDbConnectionFactory>().Open())
            {
                db.DropAndCreateTable<Customer>();
            }
        }
    }

    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            //Initialize your application
            new RestIntroAppHost().Init();
        }
    }
}