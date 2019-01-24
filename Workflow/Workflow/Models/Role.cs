using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Workflow.Models
{
    public class Role
    {
        int id;
        string roleName;

        public Role(int id, string roleName)
        {
            this.id = id;
            this.roleName = roleName;
        }

        public int RoleId
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public string RoleName
        {
            get { return this.roleName; }
            set { this.roleName = value; }
        }
    }
}