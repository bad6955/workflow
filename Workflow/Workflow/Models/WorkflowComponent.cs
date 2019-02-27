using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Workflow.Data;

namespace Workflow.Models
{
    public class WorkflowComponent
    {
        int workflowId;
        int wfComponentId;
        int formId;
        string componentTitle;
        string componentText;
        public WorkflowComponent(int wfComponentId, int workflowId, string componentTitle, string componentText)
        {
            this.workflowId = workflowId;
            this.componentText = componentText;
            this.componentTitle = componentTitle;
            this.wfComponentId = wfComponentId;
        }

        public WorkflowComponent(int wfComponentId, int workflowId, string componentTitle)
        {
            this.workflowId = workflowId;
            this.componentTitle = componentTitle;
            this.wfComponentId = wfComponentId;
        }

        public WorkflowComponent(int workflowId, string componentTitle, int formId)
        {
            this.workflowId = workflowId;
            this.componentTitle = componentTitle;
            this.formId = formId;
        }

        public WorkflowComponent(int workflowId, string componentTitle)
        {
            this.workflowId = workflowId;
            this.componentTitle = componentTitle;
        }

        public WorkflowComponent(int workflowId)
        {
            this.workflowId = workflowId;
        }

        public int WorkflowId
        {
            get { return workflowId; }
            set { workflowId = value; }
        }

        public string ComponentTitle
        {
            get { return componentTitle; }
            set { componentTitle = value; }
        }

        public string ComponentText
        {
            get { return componentText;  }
            set { componentText = value; }
        }

        public int WFComponentID
        {
            get { return wfComponentId; }
            set { wfComponentId = value;  }
        }

        public int FormID
        {
            get { return formId; }
            set { formId = value; }
        }
    }
}