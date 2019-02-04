using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Workflow.Models
{
    public class FormField
    {
        int id;
        int formId;
        int statusId;
        int roleId;
        string fieldValue;
        string fieldText;

        public FormField()
        {
        }

        public FormField(int formFieldId, int formId, string fieldText)
        {
            this.id = formFieldId;
            this.formId = formId;
            this.fieldText = fieldText;
        }

        public FormField(int formFieldId, int formId, string fieldValue, string fieldText)
        {
            this.id = formFieldId;
            this.formId = formId;
            this.fieldValue = fieldValue;
            this.fieldText = fieldText;
        }

        public int FormFieldId
        {
            get { return id; }
            set { id = value; }
        }

        public int FormId
        {
            get { return formId; }
            set { formId = value; }
        }

        public int StatusId
        {
            get { return statusId; }
            set { statusId = value; }
        }

        public int RoleId
        {
            get { return roleId; }
            set { roleId = value; }
        }

        public string FieldValue
        {
            get { return fieldValue; }
            set { fieldValue = value; }
        }

        public string FieldText
        {
            get { return fieldText; }
            set { fieldText = value; }
        }
    }
}