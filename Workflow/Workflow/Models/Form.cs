using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Workflow.Models
{
    public class Form
    {
        int id;
        string name;
        int statusId;

        public Form(string name)
        {
            this.name = name;
        }

        public Form(int formId, string name)
        {
            this.id = formId;
            this.name = name;
        }

        public int FormId
        {
            get { return id; }
            set { id = value; }
        }

        public int StatusId
        {
            get { return statusId; }
            set { statusId = value; }
        }

        public string FormName
        {
            get { return name; }
            set { name = value; }
        }
    }
}