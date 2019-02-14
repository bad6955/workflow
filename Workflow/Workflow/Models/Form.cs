using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Workflow.Models
{
    public class Form
    {
        int id;
        int workflowId;
        string name;
        int statusId;
        int approvalRequiredId;

        public Form(string name)
        {
            this.name = name;
        }

        public Form(int formId, string name)
        {
            this.id = formId;
            this.name = name;
        }

        public Form(int formId, int workflowId, string name, int approvalRequiredId, int statusId)
        {
            this.id = formId;
            this.workflowId = workflowId;
            this.name = name;
            this.approvalRequiredId = approvalRequiredId;
            this.statusId = statusId;
        }

        public int FormId
        {
            get { return id; }
            set { id = value; }
        }

        public int WorkflowId
        {
            get { return workflowId; }
            set { workflowId = value; }
        }

        public int StatusId
        {
            get { return statusId; }
            set { statusId = value; }
        }

        public int ApprovalRequiredId
        {
            get { return approvalRequiredId; }
            set { approvalRequiredId = value; }
        }

        public string FormName
        {
            get { return name; }
            set { name = value; }
        }
    }
}