using System;

namespace RestIntro
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            //Initialize your application
            new RestIntroAppHost().Init();
        }
    }
}