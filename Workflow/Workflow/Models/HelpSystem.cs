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
        int step;
        string intro;

        public HelpSystem(int pid, int rid, int step,string intro)
        {
            this.pageID = pid;
            this.roleID = rid;
            this.step = step;
            this.intro = intro;
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

        public int Step
        {
            get { return step; }
            set { step = value; }
        }

        public string Intro
        {
            get { return intro; }
            set { intro = value; }
        }
    }
}