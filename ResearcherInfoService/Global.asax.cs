using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace ResearcherInfoService
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //comment
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
