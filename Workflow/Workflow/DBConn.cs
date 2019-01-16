using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Workflow
{
    public class DBConn
    {
        //The 'DBConnString' value still needs to be filled out for our DB in the Web.Config file
        String connStr = ConfigurationManager.ConnectionStrings["DBConnString"].ConnectionString;
    }
}