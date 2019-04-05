using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Workflow.Models
{
    public class Form
    {
        int id;
        int projectId;
        string name;
        string formData;
        int statusId;
        int approvalRequiredId;
        string approved;
        string denied;
        string denialReason;
        int submission;
        int templateId;
        string filePath;
        string localPath;
        string approverIDs;

        public Form(string name)
        {
            this.name = name;
        }

        public Form(int formId, string name)
        {
            this.id = formId;
            this.name = name;
        }

        public Form(int formId, string name, string formData, int projectId, int approvalRequiredId, int statusId, int submission, string approved, string denied, string denialReason, int formTemplateId)
        {
            this.id = formId;
            this.name = name;
            this.formData = formData;
            this.projectId = projectId;
            this.approvalRequiredId = approvalRequiredId;
            this.statusId = statusId;
            this.submission = submission;
            this.approved = approved;
            this.denied = denied;
            this.denialReason = denialReason;
            this.templateId = formTemplateId;
        }

        public Form(int formId, string name, string formData, int projectId, int submission, string approved, string denied, string denialReason, int formTemplateId)
        {
            this.id = formId;
            this.name = name;
            this.formData = formData;
            this.projectId = projectId;
            this.submission = submission;
            this.approved = approved;
            this.denied = denied;
            this.denialReason = denialReason;
            this.templateId = formTemplateId;
        }

        public Form(int formId, string name, string formData, int projectId, int approvalRequiredId, int statusId, int submission, string approved, string denied, string denialReason, int formTemplateId, string filePath, string localPath)
        {
            this.id = formId;
            this.name = name;
            this.formData = formData;
            this.projectId = projectId;
            this.approvalRequiredId = approvalRequiredId;
            this.statusId = statusId;
            this.submission = submission;
            this.approved = approved;
            this.denied = denied;
            this.denialReason = denialReason;
            this.templateId = formTemplateId;
            this.filePath = filePath;
            this.localPath = localPath;
        }

        public Form(int formId, string name, string formData, int projectId, int submission, string approved, string denied, string denialReason, int formTemplateId, string filePath, string localPath)
        {
            this.id = formId;
            this.name = name;
            this.formData = formData;
            this.projectId = projectId;
            this.submission = submission;
            this.approved = approved;
            this.denied = denied;
            this.denialReason = denialReason;
            this.templateId = formTemplateId;
            this.filePath = filePath;
            this.localPath = localPath;
        }

        public Form(int formId, string name, string formData, string approverIDs)
        {
            this.id = formId;
            this.name = name;
            this.formData = formData;
            this.approverIDs = approverIDs;
        }

        public Form(int formId, string name, string formData)
        {
            this.id = formId;
            this.name = name;
            this.formData = formData;
        }

        public Form(int formId, string name, string formData, int projId)
        {
            this.id = formId;
            this.name = name;
            this.formData = formData;
            this.projectId = projId;
        }

        public Form(int formId, string name, int projId)
        {
            this.id = formId;
            this.name = name;
            this.projectId = projId;
        }

        public int FormId
        {
            get { return id; }
            set { id = value; }
        }

        public int ProjectId
        {
            get { return projectId; }
            set { projectId = value; }
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

        public string FormData
        {
            get { return formData; }
            set { formData = value; }
        }

        public int Submission
        {
            get { return submission; }
            set { submission = value; }
        }

        public string Approved
        {
            get { return approved; }
            set { approved = value; }
        }

        public string Denied
        {
            get { return denied; }
            set { denied = value; }
        }

        public string DenialReason
        {
            get { return denialReason; }
            set { denialReason = value; }
        }


        public int FormTemplateId
        {
            get { return templateId; }
            set { templateId = value; }
        }

        public string FilePath
        {
            get { return filePath; }
            set { filePath = value; }
        }

        public string LocalPath
        {
            get { return localPath; }
            set { localPath = value; }
        }

        public string ApproverIDs
        {
            get { return approverIDs; }
            set { approverIDs = value; }
        }
    }
}