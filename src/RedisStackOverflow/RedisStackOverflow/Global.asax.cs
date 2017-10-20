using System;
using Funq;
using RedisStackOverflow.ServiceInterface;
using ServiceStack;
using ServiceStack.Configuration;
using ServiceStack.Logging;
using ServiceStack.Redis;

namespace RedisStackOverflow
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            if (ConfigUtils.GetAppSetting("log", false)) { LogManager.LogFactory = new ConsoleLogFactory(); }

            //Initialize your application
            new QuestionsAppHost().Init();
        }
    }
}