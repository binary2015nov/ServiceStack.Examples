﻿using System;
using Funq;
using RedisStackOverflow.ServiceInterface;
using ServiceStack;
using ServiceStack.Configuration;
using ServiceStack.Logging;
using ServiceStack.Redis;
using IRepository = RedisStackOverflow.ServiceInterface.IRepository;

namespace RedisStackOverflow
{
    /// <summary>
    /// Create your ServiceStack web service application with a singleton AppHost.
    /// </summary>  
    public class AppHost : AppHostBase
    {
        /// <summary>
        /// Initializes a new instance of your ServiceStack application, with the specified name and assembly containing the services.
        /// </summary>
        public AppHost() : base("Redis StackOverflow", typeof(QuestionsService).Assembly)
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
            container.Register<IRepository>(c => new Repository(c.Resolve<IRedisClientsManager>()));
        }
    }

    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            if (ConfigUtils.GetAppSetting("log", false)) { LogManager.LogFactory = new ConsoleLogFactory(); }

            //Initialize your application
            (new AppHost()).Init();
        }
    }
}