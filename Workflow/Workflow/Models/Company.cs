using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Workflow.Models
{
    public class Company
    {
        string companyName;
        
        public Company(string companyName)
        {
            this.companyName = companyName;
        }

        public string GetCompanyName()
        {
            return companyName;
        }
    }
}