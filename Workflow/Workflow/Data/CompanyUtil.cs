using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Workflow.Models;

namespace Workflow.Data
{
    public static class CompanyUtil
    {
        public static Company CreateCompany(string companyName)
        {
            Company c = new Company(companyName);
            SqlCommand cmd = new SqlCommand("INSERT INTO Company (CompanyName) VALUES (@companyName)");
            
            DBConn conn = new DBConn();
            int id = conn.ExecuteInsertStatement(cmd);
            return c;
        }
    }
}