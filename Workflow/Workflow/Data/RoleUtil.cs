using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Workflow.Models;
using Role = Workflow.Models.Role;

namespace Workflow.Data
{
    public static class RoleUtil
    {
        public static List<Role> GetRoles()
        {
            string query = "SELECT RoleID, RoleName from Roles";

            MySqlCommand cmd = new MySqlCommand(query);
            DBConn conn = new DBConn();
            MySqlDataReader dr = conn.ExecuteSelectCommand(cmd);

            List <Role> roleList = new List<Role>();
            while (dr.Read())
            {
                Role r = new Role((int)dr["RoleID"], (string)dr["RoleName"]);
                roleList.Add(r);
            }
            conn.CloseConnection();
            return roleList;
        }
    }
}