using System;

namespace Backbone.Todos
{  
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            //Initialize your ServiceStack AppHost
            new TodoAppHost().Init();
        }
    }
}
