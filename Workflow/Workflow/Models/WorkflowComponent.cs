using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Workflow.Models
{
    public class WorkflowComponent
    {
        int workflowid;
        int wfcomponentid;
        string componenttitle;
        string componenttext;

        public WorkflowComponent(int workflowId, string componenttitle, string componenttext)
        {
            this.workflowid = workflowId;
            this.componenttext = componenttext;
            this.componenttitle = componenttitle;
        }

        public int WorkflowId
        {
            get { return workflowid; }
            set { workflowid = value; }
        }

        public string ComponentTitle
        {
            get { return componenttitle; }
            set { componenttitle = value; }
        }

        public string ComponentText
        {
            get { return componenttext;  }
            set { componenttext = value; }
        }

    }
}