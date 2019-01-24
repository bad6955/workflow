using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Workflow.Models
{
    public class Project
    {
        int id;
        int workflowId;
        int companyId;
        int statusId;
        int VCId;
        string notes;

        public Project(int id, int workflowId, int companyId, int statusId, int VCId, string notes)
        {
            this.id = id;
            this.workflowId = workflowId;
            this.companyId = companyId;
            this.statusId = statusId;
            this.VCId = VCId;
            this.notes = notes;
        }

        public int getId()
        {
            return id;
        }

        public int getWorkflowId()
        {
            return workflowId;
        }

        public int getCompanyId()
        {
            return companyId;
        }

        public int getStatusId()
        {
            return statusId;
        }

        public int getVCId()
        {
            return VCId;
        }

        public string getNotes()
        {
            return notes;
        }
    }
}