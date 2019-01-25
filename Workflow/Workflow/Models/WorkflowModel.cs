using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Workflow.Models
{
    public class WorkflowModel
    {
        int id;
        string workflowName;

        public WorkflowModel(string workflowName)
        {
            this.workflowName = workflowName;
        }

        public WorkflowModel(int workflowId, string workflowName)
        {
            this.workflowName = workflowName;
            this.id = workflowId;
        }

        public int WorkflowId
        {
            get { return id; }
            set { id = value; }
        }

        public string WorkflowName
        {
            get { return workflowName; }
            set { workflowName = value; }
        }
    }
}