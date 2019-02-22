using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace Workflow
{
    public class Global : System.Web.HttpApplication
    {
        public Global()
        {
            Log.Init();
        }

        protected void Application_Start(object sender, EventArgs e)
        {
           
        }

        public override void Dispose()
        {
            Log.Shutdown();
            base.Dispose();
        }

    }
}