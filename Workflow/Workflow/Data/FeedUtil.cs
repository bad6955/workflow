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
        }

        public static void CreateProjectFeedItem(string feedText, int userId, int projectId)
        {
            MySqlCommand cmd = new MySqlCommand("INSERT INTO ActivityFeed (FeedItemText, UserID, ProjectID) VALUES (@feedText, @userId, @projectId)");
            cmd.Parameters.AddWithValue("@feedText", feedText);
            cmd.Parameters.AddWithValue("@userId", userId);
            cmd.Parameters.AddWithValue("@projectId", projectId);
            DBConn conn = new DBConn();
            conn.ExecuteInsertCommand(cmd);
        }

        public static List<FeedItem> GetFeed(int userId)
        {
            string query = "SELECT FeedItemID, FeedItemText, ProjectID, Time FROM ActivityFeed WHERE UserID = @userId AND Dismissed = 0 ORDER BY Time";
            MySqlCommand cmd = new MySqlCommand(query);
            cmd.Parameters.AddWithValue("@userId", userId);
            DBConn conn = new DBConn();
            MySqlDataReader dr = conn.ExecuteSelectCommand(cmd);

            List<FeedItem> feedList = new List<FeedItem>();
            while (dr.Read())
            {
                FeedItem f = new FeedItem((int)dr["FeedItemID"], (string)dr["FeedItemText"], (DateTime)dr["Time"], (int)dr["ProjectID"]);
                feedList.Add(f);
            }
            conn.CloseConnection();
            return feedList;
        }

        public static void DismissItem(int feedItemId)
        {
            string query = "UPDATE ActivityFeed SET Dismissed = 1 WHERE FeedItemID = @feedItemId";

            MySqlCommand cmd = new MySqlCommand(query);
            cmd.Parameters.AddWithValue("@feedItemId", feedItemId);
            DBConn conn = new DBConn();
            conn.ExecuteInsertCommand(cmd);
        }
    }
}