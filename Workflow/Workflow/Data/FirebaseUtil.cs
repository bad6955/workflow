using Firebase.Auth;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace Workflow.Data
{
    public static class FirebaseUtil
    {
        public static FirebaseAuthProvider GetAuthProvider()
        {
            string API_KEY = ConfigurationManager.AppSettings.Get("FirebaseAPIKey");
            return new FirebaseAuthProvider(new FirebaseConfig(API_KEY));
        }

        public static User CreateNewUser(string email, string pass, string displayName, bool verificationEmail)
        {
            FirebaseAuthLink auth = FirebaseFunctions.AttemptCreateNewUser(email, pass, displayName, verificationEmail);
            if (auth != null)
            {
                return auth.User;
            }
            return null;
        }

        public static void DeleteUserAccess(string email)
        {
            
        }

        public static User LoginUser(string email, string pass)
        {
            FirebaseAuthLink auth = FirebaseFunctions.AttemptLoginUser(email, pass);
            if (auth != null)
            {
                return auth.User;
            }
            return null;
        }

        public static bool ForgotPassword(string email)
        {
            return FirebaseFunctions.ResetPassword(email);
        }
    }

    public static class FirebaseFunctions
    {
        public static FirebaseAuthLink AttemptCreateNewUser(string email, string pass, string displayName, bool verificationEmail)
        {
            var authProvider = FirebaseUtil.GetAuthProvider();
            Task<FirebaseAuthLink> task = authProvider.CreateUserWithEmailAndPasswordAsync(email, pass, displayName, verificationEmail);
            try
            {
                task.Wait();
                return task.Result;
            }
            catch(Exception e)
            {
                if(e.InnerException.Message.Contains("Reason: EmailExists"))
                {
                    //email exists error
                }
            }
            return null;
        }

        public static void AttemptDeleteUser(string email)
        {
            //Task<FirebaseAuthLink> task = FirebaseUtil.GetAuthProvider().SignInWithEmailAndPasswordAsync
        }


        public static FirebaseAuthLink AttemptLoginUser(string email, string pass)
        {
            var authProvider = FirebaseUtil.GetAuthProvider();
            Task<FirebaseAuthLink> task = authProvider.SignInWithEmailAndPasswordAsync(email, pass);
            try
            {
                task.Wait();
                return task.Result;
            }
            catch (Exception e)
            {
            }
            return null;
        }

        public static bool ResetPassword(string email)
        {
            var authProvider = FirebaseUtil.GetAuthProvider();
            Task task = authProvider.SendPasswordResetEmailAsync(email);
            try
            {
                task.Wait();
                return true;
            }
            catch (Exception e)
            {
            }
            return false;
        }


    }
}