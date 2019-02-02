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