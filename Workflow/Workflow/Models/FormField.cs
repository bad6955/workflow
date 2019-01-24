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
        string fieldTitle;
        string fieldText;

        public FormField(string fieldTitle)
        {
            this.fieldTitle = fieldTitle;
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

        public string FieldTitle
        {
            get { return fieldTitle; }
            set { fieldTitle = value; }
        }

        public string FieldText
        {
            get { return fieldText; }
            set { fieldText = value; }
        }
    }
}