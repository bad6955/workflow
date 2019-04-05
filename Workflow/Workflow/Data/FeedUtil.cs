using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Workflow.Models;
using Workflow.Utility;

namespace Workflow.Data
{
    public static class FeedUtil
    {
        public static void CreateFeedItem(string feedText, int userId)
        {
            MySqlCommand cmd = new MySqlCommand("INSERT INTO ActivityFeed (FeedItemText, UserID) VALUES (@feedText, @userId)");
            cmd.Parameters.AddWithValue("@feedText", feedText);
            cmd.Parameters.AddWithValue("@userId", userId);
            DBConn conn = new DBConn();
            conn.ExecuteInsertCommand(cmd);
            conn.CloseConnection();
            UserUtil.SendEmail(userId, feedText);
        }

        public static void CreateProjectFeedItem(string feedText, int userId, int projectId)
        {
            MySqlCommand cmd = new MySqlCommand("INSERT INTO ActivityFeed (FeedItemText, UserID, ProjectID) VALUES (@feedText, @userId, @projectId)");
            cmd.Parameters.AddWithValue("@feedText", feedText);
            cmd.Parameters.AddWithValue("@userId", userId);
            cmd.Parameters.AddWithValue("@projectId", projectId);
            DBConn conn = new DBConn();
            conn.ExecuteInsertCommand(cmd);
            conn.CloseConnection();
            UserUtil.SendEmail(userId, feedText);

        }

        public static void CreateProjectFormFeedItem(string feedText, int userId, int projectId, int formId)
        {
            MySqlCommand cmd = new MySqlCommand("INSERT INTO ActivityFeed (FeedItemText, UserID, ProjectID, FormID) VALUES (@feedText, @userId, @projectId, @formId)");
            cmd.Parameters.AddWithValue("@feedText", feedText);
            cmd.Parameters.AddWithValue("@userId", userId);
            cmd.Parameters.AddWithValue("@projectId", projectId);
            cmd.Parameters.AddWithValue("@formId", formId);
            DBConn conn = new DBConn();
            conn.ExecuteInsertCommand(cmd);
            conn.CloseConnection();
            UserUtil.SendEmail(userId, feedText);

        }

        public static List<FeedItem> GetFeed(int userId)
        {
            string query = "SELECT FeedItemID, FeedItemText, ProjectID, FormID, Time FROM ActivityFeed WHERE UserID = @userId ORDER BY Time";
            MySqlCommand cmd = new MySqlCommand(query);
            cmd.Parameters.AddWithValue("@userId", userId);
            DBConn conn = new DBConn();
            MySqlDataReader dr = conn.ExecuteSelectCommand(cmd);

            List<FeedItem> feedList = new List<FeedItem>();
            while (dr.Read())
            {
                FeedItem f = new FeedItem((int)dr["FeedItemID"], (string)dr["FeedItemText"], (DateTime)dr["Time"], (int)dr["ProjectID"], (int)dr["FormID"]);
                feedList.Add(f);
            }
            conn.CloseConnection();
            return feedList;
        }
    }
}