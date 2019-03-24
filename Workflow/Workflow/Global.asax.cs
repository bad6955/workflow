using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Workflow.Utility;

namespace Workflow
{
    public class Global : System.Web.HttpApplication
    {
        public Global()
        {
            //Workflow.Utility.Log.Init();
        }

        protected void Application_Start(object sender, EventArgs e)
        {
           
        }

        public override void Dispose()
        {
            //Workflow.Utility.Log.Shutdown();
            base.Dispose();
        }

    }
}