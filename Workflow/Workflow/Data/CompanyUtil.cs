using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
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
            MySqlCommand cmd = new MySqlCommand("INSERT INTO Company (CompanyName) VALUES (@companyName)");
            cmd.Parameters.AddWithValue("@companyName", companyName);
            DBConn conn = new DBConn();
            conn.ExecuteInsertCommand(cmd);
            return c;
        }

        public static List<Company> GetCompanies()
        {
            string query = "SELECT CompanyID, CompanyName from Company";

            MySqlCommand cmd = new MySqlCommand(query);
            DBConn conn = new DBConn();
            MySqlDataReader dr = conn.ExecuteSelectCommand(cmd);

            List<Company> companyList = new List<Company>();
            while (dr.Read())
            {
                Company c = new Company((int)dr["CompanyID"], (string)dr["CompanyName"]);
                companyList.Add(c);
            }
            conn.CloseConnection();
            return companyList;
        }
    }
}