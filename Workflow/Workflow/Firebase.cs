using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Workflow
{
    public static class Firebase
    {
        public static FirebaseAuthProvider GetAuthProvider()
        {
            return new FirebaseAuthProvider(new FirebaseConfig("AIzaSyAEa4tI_IPceDWsVkIf86AgyFozzmeC6tI"));
        }

        public static bool CreateNewUser(String email, String pass, String displayName, bool verificationEmail)
        {
            FirebaseAuthLink auth = FirebaseUtil.AttemptCreateNewUser(email, pass, displayName, verificationEmail);
            if (auth != null)
            {
                if (auth.User != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public static bool LoginUser(String email, String pass)
        {
            FirebaseAuthLink auth = FirebaseUtil.AttemptLoginUser(email, pass);
            if (auth != null)
            {
                if (auth.User != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
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
    }
}