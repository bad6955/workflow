using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Workflow.Models;

namespace Workflow.Data
{
    public static class UserUtil
    {
        public static User CreateUser(string email, string firstName, string lastName)
        {
            User u = new User(email, firstName, lastName);
            string createQuery = "INSERT INTO Users (Email, FirstName, LastName) VALUES (@email,@firstName,@lastName)";

            SqlCommand cmd = new SqlCommand(createQuery);
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
            string createQuery = "INSERT INTO Users (GroupID, Token, Email, FirstName, LastName) VALUES (@groupId,@token,@email,@firstName,@lastName)";

            SqlCommand cmd = new SqlCommand(createQuery);
            cmd.Parameters.AddWithValue("@groupId", groupId);
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
            string createQuery = "SELECT ID, GroupID, Token, Email, FirstName, LastName from Users where Email = @email";

            SqlCommand cmd = new SqlCommand(createQuery);
            cmd.Parameters.AddWithValue("@email", email);
            DBConn conn = new DBConn();
            SqlDataReader dr = conn.ExecuteSelectCommand(cmd);
            User u = new User((int) dr["ID"], (int) dr["GroupID"], (string) dr["Token"], (string) dr["Email"], (string) dr["FirstName"], (string) dr["LastName"]);
            return u;
        }
    }
}