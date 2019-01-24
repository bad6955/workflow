using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Workflow.Models
{
    public class User
    {
        int id;
        int roleId;
        int companyId;
        string email;
        string firstName;
        string lastName;
        Firebase.Auth.User firebaseUser;

        public User(int id, int roleId, int companyId, string email, string firstName, string lastName)
        {
            this.id = id;
            this.roleId = roleId;
            this.companyId = companyId;
            this.email = email;
            this.firstName = firstName;
            this.lastName = lastName;
        }

        public User(int roleId, int companyId, string email, string firstName, string lastName)
        {
            this.roleId = roleId;
            this.companyId = companyId;
            this.email = email;
            this.firstName = firstName;
            this.lastName = lastName;
        }

        public int UserId
        {
            get { return id; }
            set { id = value; }
        }

        public int RoleId
        {
            get { return roleId; }
            set { roleId = value; }
        }

        public int CompanyId
        {
            get { return companyId; }
            set { companyId = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        public Firebase.Auth.User FirebaseUser
        {
            get { return firebaseUser; }
            set { firebaseUser = value; }
        }
    }
}