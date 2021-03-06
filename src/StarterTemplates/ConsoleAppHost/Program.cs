﻿using System;
using System.Diagnostics;
using Funq;
using ServiceStack;
using ServiceStack.Configuration;
using StarterTemplates.Common;

namespace ConsoleAppHost
{
    class Program
    {
        private static readonly string ListeningOn = AppSettings.Default.Get("ListeningOn");

        /// <summary>
        /// Create your ServiceStack http listener application with a singleton AppHost.
        /// </summary> 
        public class AppHost : AppHostHttpListenerBase
        {
            /// <summary>
            /// Initializes a new instance of your ServiceStack application, with the specified name and assembly containing the services.
            /// </summary>
            public AppHost() : base("StarterTemplate HttpListener", typeof(HelloService).Assembly) { }

            /// <summary>
            /// Configure the container with the necessary routes for your ServiceStack application.
            /// </summary>
            /// <param name="container">The built-in IoC used with ServiceStack.</param>
            public override void Configure(Container container)
            {
                container.Register(new TodoRepository());
            }
        }

        static void Main(string[] args)
        {
            var appHost = new AppHost();
            appHost.Init();
            appHost.Start(ListeningOn);

            Console.WriteLine("AppHost created at {0}, listening on {1}", appHost.CreatedAt, ListeningOn);


            Process.Start(ListeningOn);
            Console.ReadKey();
        }
    }
}
