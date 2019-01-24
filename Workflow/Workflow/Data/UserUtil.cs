using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Web;
using Workflow.Models;

namespace Workflow.Data
{
    public static class UserUtil
    {
        public static User CreateUser(int roleId, int companyId, string email, string firstName, string lastName)
        {
            User u = new User(roleId, companyId, email, firstName, lastName);
            string createQuery = "INSERT INTO Users (RoleID, CompanyID, Email, FirstName, LastName) VALUES (@roleId,@companyId,@email,@firstName,@lastName)";

            MySqlCommand cmd = new MySqlCommand(createQuery);
            cmd.Parameters.AddWithValue("@roleId", roleId);
            cmd.Parameters.AddWithValue("@companyId", companyId);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@firstName", firstName);
            cmd.Parameters.AddWithValue("@lastName", lastName);
            DBConn conn = new DBConn();
            conn.ExecuteInsertCommand(cmd);

            return u;
        }

        /*
        public static User CreateUser(int groupId, string token, string email, string firstName, string lastName)
        {
            User u = new User(groupId, token, email, firstName, lastName);
            string createQuery = "INSERT INTO Users (RoleID, Token, Email, FirstName, LastName) VALUES (@roleId,@token,@email,@firstName,@lastName)";

            SqlCommand cmd = new SqlCommand(createQuery);
            cmd.Parameters.AddWithValue("@roleId", roleId);
            cmd.Parameters.AddWithValue("@token", token);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@firstName", firstName);
            cmd.Parameters.AddWithValue("@lastName", lastName);
            cmd.Prepare();

            DBConn conn = new DBConn();
            conn.ExecuteInsertCommand(cmd);

            return u;
        }
        */

        public static User GetUser(string email)
        {
            string createQuery = "SELECT UserID, RoleID, CompanyID, Token, Email, FirstName, LastName from Users where Email = @email";

            MySqlCommand cmd = new MySqlCommand(createQuery);
            cmd.Parameters.AddWithValue("@email", email);
            DBConn conn = new DBConn();
            MySqlDataReader dr = conn.ExecuteSelectCommand(cmd);
            User u = null;
            while (dr.Read())
            {
                u = new User((int)dr["UserID"], (int)dr["RoleID"], (int)dr["CompanyID"], (string)dr["Email"], (string)dr["FirstName"], (string)dr["LastName"]);
            }
            conn.CloseConnection();
            return u;
        }
    }
}