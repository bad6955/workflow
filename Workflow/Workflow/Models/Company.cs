using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Workflow.Models
{
    public class Company
    {
        int companyId;
        string companyName;
        
        public Company(string companyName)
        {
            this.companyName = companyName;
        }

        public Company(int companyId, string companyName)
        {
            this.companyId = companyId;
            this.companyName = companyName;
        }

        public string CompanyName
        {
            get { return companyName; }
            set { companyName = value; }
        }

        public int CompanyId
        {
            get { return companyId; }
            set { companyId = value; }
        }
    }
}