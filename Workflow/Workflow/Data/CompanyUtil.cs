using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Web;
using Workflow.Models;
using Workflow.Utility;

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
            conn.CloseConnection();
            return c;
        }

        public static Company GetCompany(int companyId)
        {
            string query = "SELECT CompanyID, CompanyName from Company WHERE CompanyID=@companyId";

            MySqlCommand cmd = new MySqlCommand(query);
            cmd.Parameters.AddWithValue("@companyId", companyId);
            DBConn conn = new DBConn();
            MySqlDataReader dr = conn.ExecuteSelectCommand(cmd);

            Company c = null;
            while (dr.Read())
            {
                c = new Company((int)dr["CompanyID"], (string)dr["CompanyName"]);
            }
            conn.CloseConnection();
            return c;
        }

        public static string GetCompanyName(int companyId)
        {
            return GetCompany(companyId).CompanyName;
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

        public static List<Company> GetClientCompanies()
        {
            string query = "SELECT CompanyID, CompanyName from Company where CompanyID != 1 AND CompanyID != -2";

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