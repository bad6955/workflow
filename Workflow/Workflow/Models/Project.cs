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
        int coachId;
        string notes;
        string name;

        public Project(int workflowId, int companyId, int statusId, int coachId, string name, string notes)
        {
            this.workflowId = workflowId;
            this.companyId = companyId;
            this.statusId = statusId;
            this.coachId = coachId;
            this.name = name;
            this.notes = notes;
        }

        public Project(int id, int workflowId, int companyId, int statusId, int coachId, string name, string notes)
        {
            this.id = id;
            this.workflowId = workflowId;
            this.companyId = companyId;
            this.statusId = statusId;
            this.coachId = coachId;
            this.name = name;
            this.notes = notes;
        }

        public int ProjectId
        {
            get { return id; }
            set { id = value; }
        }

        public int WorkflowId
        {
            get { return workflowId; }
            set { workflowId = value; }
        }

        public int CompanyId
        {
            get { return companyId; }
            set { companyId = value; }
        }

        public int StatusId
        {
            get { return statusId; }
            set { statusId = value; }
        }

        public int CoachId
        {
            get { return coachId; }
            set { coachId = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Notes
        {
            get { return notes; }
            set { notes = value; }
        }
    }
}