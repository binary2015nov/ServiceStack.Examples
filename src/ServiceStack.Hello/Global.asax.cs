using System;

namespace ServiceStack.Hello
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            //Initialize your application
            (new HelloAppHost()).Init();
        }
    }
}
