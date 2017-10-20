using System;

//The entire C# source code for the ServiceStack + Redis TODO REST backend. There is no other .cs :)
namespace Backbone.Todos
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            //Initialize your ServiceStack AppHost
            new AppHost().Init();
        }
    }
}
