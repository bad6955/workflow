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
        string fieldName;
        string fieldText;

        public FormField(string fieldName)
        {
            this.fieldName = fieldName;
        }

        public FormField(int formFieldId, int formId, string fieldText)
        {
            this.id = formFieldId;
            this.formId = formId;
            this.fieldText = fieldText;
        }

        public FormField(int formFieldId, int formId, string fieldName, string fieldText)
        {
            this.id = formFieldId;
            this.formId = formId;
            this.fieldName = fieldName;
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

        public string FieldName
        {
            get { return fieldName; }
            set { fieldName = value; }
        }

        public string FieldText
        {
            get { return fieldText; }
            set { fieldText = value; }
        }
    }
}