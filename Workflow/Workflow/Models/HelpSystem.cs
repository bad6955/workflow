using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Workflow.Models
{
    public class HelpSystem
    {
        int pageID;
        int roleID;
        string text;

        public HelpSystem(int pid, int rid, string text)
        {
            this.pageID = pid;
            this.roleID = rid;
            this.text = text;
        }

        public int PageID
        {
            get { return pageID; }
            set { pageID = value; }
        }

        public int RoleID
        {
            get { return roleID; }
            set { roleID = value; }
        }

        public string Text
        {
            get { return text; }
            set { text = value; }
        }
    }
}