using System;

namespace RestFiles
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            //Initialize your application
            new RestFilesAppHost().Init();
        }
    }
}