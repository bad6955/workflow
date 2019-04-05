using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Web;
using Workflow.Models;
using Workflow.Utility;
using System.Net.Mail;

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
            string createQuery = "SELECT UserID, RoleID, CompanyID, Token, Email, FirstName, LastName, InvalidLoginCt, AdminPanelToggle from Users where Email = @email";

            MySqlCommand cmd = new MySqlCommand(createQuery);
            cmd.Parameters.AddWithValue("@email", email);
            DBConn conn = new DBConn();
            MySqlDataReader dr = conn.ExecuteSelectCommand(cmd);
            User u = null;
            while (dr.Read())
            {
                u = new User((int)dr["UserID"], (int)dr["RoleID"], (int)dr["CompanyID"], (string)dr["Email"], (string)dr["FirstName"], (string)dr["LastName"], (int)dr["InvalidLoginCt"], (int)dr["AdminPanelToggle"]);
            }
            conn.CloseConnection();
            return u;
        }

        public static User SendEmail(int userId, string text)
        {
            User u = GetUser(userId);
            MailMessage mail = new MailMessage("TeamLondonVCSystem@rit.edu", u.Email);
            SmtpClient client = new SmtpClient();
            client.Port = 25;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Host = "mail.rit.edu";
            mail.Subject = "ACTION REQUIRED - Venture Creations Notification";
            mail.Body = text;
            //client.Send(mail);
            return u;
        }

        public static List<User> GetUsers()
        {
            string createQuery = "SELECT UserID, RoleID, CompanyID, Token, Email, FirstName, LastName, InvalidLoginCt, AdminPanelToggle from Users";

            MySqlCommand cmd = new MySqlCommand(createQuery);
            DBConn conn = new DBConn();
            MySqlDataReader dr = conn.ExecuteSelectCommand(cmd);
            List<User> users = new List<User>();
            while (dr.Read())
            {
                User u = new User((int)dr["UserID"], (int)dr["RoleID"], (int)dr["CompanyID"], (string)dr["Email"], (string)dr["FirstName"], (string)dr["LastName"], (int)dr["InvalidLoginCt"], (int)dr["AdminPanelToggle"]);
                users.Add(u);
            }
            conn.CloseConnection();
            return users;
        }

        public static User GetUser(int userId)
        {
            string createQuery = "SELECT UserID, RoleID, CompanyID, Token, Email, FirstName, LastName, InvalidLoginCt, AdminPanelToggle from Users where UserID = @userId";

            MySqlCommand cmd = new MySqlCommand(createQuery);
            cmd.Parameters.AddWithValue("@userId", userId);
            DBConn conn = new DBConn();
            MySqlDataReader dr = conn.ExecuteSelectCommand(cmd);
            User u = null;
            while (dr.Read())
            {
                u = new User((int)dr["UserID"], (int)dr["RoleID"], (int)dr["CompanyID"], (string)dr["Email"], (string)dr["FirstName"], (string)dr["LastName"], (int)dr["InvalidLoginCt"], (int)dr["AdminPanelToggle"]);
            }
            conn.CloseConnection();
            return u;
        }

        public static bool DoesUserExist(string email)
        {
            User u = GetUser(email);
            if (u != null)
            {
                return true;
            }
            return false;
        }

        public static List<User> GetLockedUsers()
        {
            string createQuery = "SELECT UserID, RoleID, CompanyID, Token, Email, FirstName, LastName, InvalidLoginCt, AdminPanelToggle from Users where InvalidLoginCt >= 5";

            MySqlCommand cmd = new MySqlCommand(createQuery);
            DBConn conn = new DBConn();
            MySqlDataReader dr = conn.ExecuteSelectCommand(cmd);
            List<User> lockedUsers = new List<User>();
            while (dr.Read())
            {
                User u = new User((int)dr["UserID"], (int)dr["RoleID"], (int)dr["CompanyID"], (string)dr["Email"], (string)dr["FirstName"], (string)dr["LastName"], (int)dr["InvalidLoginCt"], (int)dr["AdminPanelToggle"]);
                lockedUsers.Add(u);
            }
            conn.CloseConnection();
            return lockedUsers;
        }

        public static void InvalidLogin(User user)
        {
            user.InvalidLoginCt++;
            string invalidQuery = "UPDATE Users SET InvalidLoginCt = @invalidLoginCt where Email = @email";

            MySqlCommand cmd = new MySqlCommand(invalidQuery);
            cmd.Parameters.AddWithValue("@invalidLoginCt", user.InvalidLoginCt);
            cmd.Parameters.AddWithValue("@email", user.Email);
            DBConn conn = new DBConn();
            conn.ExecuteInsertCommand(cmd);
            conn.CloseConnection();
        }

        public static void ValidLogin(User user)
        {
            string validQuery = "UPDATE Users SET InvalidLoginCt = 0 where Email = @email";

            MySqlCommand cmd = new MySqlCommand(validQuery);
            cmd.Parameters.AddWithValue("@email", user.Email);
            DBConn conn = new DBConn();
            conn.ExecuteInsertCommand(cmd);
            conn.CloseConnection();
        }

        public static void AdminPanelToggle(User user, int toggle)
        {
            string validQuery = "UPDATE Users SET AdminPanelToggle = @toggle where Email = @email";

            MySqlCommand cmd = new MySqlCommand(validQuery);
            cmd.Parameters.AddWithValue("@email", user.Email);
            cmd.Parameters.AddWithValue("@toggle", toggle);
            DBConn conn = new DBConn();
            conn.ExecuteInsertCommand(cmd);
            conn.CloseConnection();
        }

        public static List<User> GetCoaches()
        {
            string createQuery = "SELECT UserID, RoleID, CompanyID, Token, Email, FirstName, LastName, InvalidLoginCt, AdminPanelToggle from Users where RoleID = 2";

            MySqlCommand cmd = new MySqlCommand(createQuery);
            DBConn conn = new DBConn();
            MySqlDataReader dr = conn.ExecuteSelectCommand(cmd);
            List<User> coachList = new List<User>();
            while (dr.Read())
            {
                User u = new User((int)dr["UserID"], (int)dr["RoleID"], (int)dr["CompanyID"], (string)dr["Email"], (string)dr["FirstName"], (string)dr["LastName"], (int)dr["InvalidLoginCt"], (int)dr["AdminPanelToggle"]);
                coachList.Add(u);
            }
            conn.CloseConnection();
            return coachList;
        }

        public static User GetCoach(int coachID)
        {
            string createQuery = "SELECT UserID, RoleID, CompanyID, Token, Email, FirstName, LastName, InvalidLoginCt, AdminPanelToggle from Users where UserID = (@coachID)";

            MySqlCommand cmd = new MySqlCommand(createQuery);
            cmd.Parameters.AddWithValue("@coachID", coachID);
            DBConn conn = new DBConn();
            MySqlDataReader dr = conn.ExecuteSelectCommand(cmd);
            User coach = null;
            while (dr.Read())
            {
                coach = new User((int)dr["UserID"], (int)dr["RoleID"], (int)dr["CompanyID"], (string)dr["Email"], (string)dr["FirstName"], (string)dr["LastName"], (int)dr["InvalidLoginCt"], (int)dr["AdminPanelToggle"]);
            }
            conn.CloseConnection();
            return coach;
        }

        public static string GetCoachName(int coachId)
        {
            return GetCoach(coachId).FullName;
        }

        public static List<User> GetClients(int companyId)
        {
            string createQuery = "SELECT UserID, RoleID, CompanyID, Token, Email, FirstName, LastName, InvalidLoginCt, AdminPanelToggle from Users where CompanyID = @companyId";

            MySqlCommand cmd = new MySqlCommand(createQuery);
            cmd.Parameters.AddWithValue("@companyId", companyId);
            DBConn conn = new DBConn();
            MySqlDataReader dr = conn.ExecuteSelectCommand(cmd);
            List<User> clientList = new List<User>();
            while (dr.Read())
            {
                User u = new User((int)dr["UserID"], (int)dr["RoleID"], (int)dr["CompanyID"], (string)dr["Email"], (string)dr["FirstName"], (string)dr["LastName"], (int)dr["InvalidLoginCt"], (int)dr["AdminPanelToggle"]);
                clientList.Add(u);
            }
            conn.CloseConnection();
            return clientList;
        }
    }
}