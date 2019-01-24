using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Workflow.Models
{
    public class User
    {
        int id;
        int groupId;
        string token;
        string email;
        string firstName;
        string lastName;
        Firebase.Auth.User firebaseUser;

        public User(int id, int groupId, string token, string email, string firstName, string lastName)
        {
            this.id = id;
            this.groupId = groupId;
            this.token = token;
            this.email = email;
            this.firstName = firstName;
            this.lastName = lastName;
        }

        public void setFirebaseUser(Firebase.Auth.User user)
        {
            this.firebaseUser = user;
        }

        //GETTERS
        public int getId()
        {
            return id;
        }

        public int getGroupId()
        {
            return groupId;
        }

        public string getToken()
        {
            return token;
        }

        public string getEmail()
        {
            return email;
        }

        public string getFirstName()
        {
            return firstName;
        }

        public string getLastName()
        {
            return lastName;
        }

        public Firebase.Auth.User getFirebaseUser()
        {
            return firebaseUser;
        }
    }
}