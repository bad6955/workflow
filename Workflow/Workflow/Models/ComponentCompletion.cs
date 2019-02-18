using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Workflow.Models
{
    public class ComponentCompletion
    {
        int wfcomponentid;
        int projectid;
        int completionid;
        int workflowid;

        public ComponentCompletion(int wfid, int projid, int completionid)
        {
            wfcomponentid = wfid;
            projectid = projid;
            this.completionid = completionid;
        }

        public int WorkflowId
        {
            get { return workflowid; }
            set { workflowid = value; }
        }

        public int WorkflowComponent
        {
            get { return wfcomponentid; }
            set { wfcomponentid = value; }
        }

        public int CompletionID
        {
            get { return completionid; }
            set { completionid = value; }
        }
        public int ProjectID
        {
            get { return projectid; }
            set { ProjectID = value; }
        }

    }
}