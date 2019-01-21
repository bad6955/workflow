using Firebase.Auth;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace Workflow
{
    public static class Firebase
    {
        public static FirebaseAuthProvider GetAuthProvider()
        {
            String API_KEY = ConfigurationManager.AppSettings.Get("FirebaseAPIKey");
            return new FirebaseAuthProvider(new FirebaseConfig(API_KEY));
        }

        public static User CreateNewUser(String email, String pass, String displayName, bool verificationEmail)
        {
            FirebaseAuthLink auth = FirebaseUtil.AttemptCreateNewUser(email, pass, displayName, verificationEmail);
            if (auth != null)
            {
                return auth.User;
            }
            return null;
        }

        public static User LoginUser(String email, String pass)
        {
            FirebaseAuthLink auth = FirebaseUtil.AttemptLoginUser(email, pass);
            if (auth != null)
            {
                return auth.User;
            }
            return null;
        }

        public static bool ForgotPassword(String email)
        {
            return FirebaseUtil.ResetPassword(email);
        }
    }

    public static class FirebaseUtil
    {
        public static FirebaseAuthLink AttemptCreateNewUser(String email, String pass, String displayName, bool verificationEmail)
        {
            var authProvider = Firebase.GetAuthProvider();
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


        public static FirebaseAuthLink AttemptLoginUser(String email, String pass)
        {
            var authProvider = Firebase.GetAuthProvider();
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

        public static bool ResetPassword(String email)
        {
            var authProvider = Firebase.GetAuthProvider();
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