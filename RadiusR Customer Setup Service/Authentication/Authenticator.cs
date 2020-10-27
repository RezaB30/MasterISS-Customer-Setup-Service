using RadiusR.DB;
using RadiusR_Customer_Setup_Service.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace RadiusR_Customer_Setup_Service.Authentication
{
    public static class Authenticator
    {
        public static UserIdentity Authenticate(string username, string hash)
        {
            // get user key fragment
            var keyFragment = KeyManager.GetKey(username);
            if (keyFragment == null)
                return null;
            // get user data
            UserIdentity identity;
            string passwordHash;
            using (RadiusREntities db = new RadiusREntities())
            {
                var user = db.CustomerSetupUsers.FirstOrDefault(u => u.Username == username);
                if (user == null || !user.IsEnabled)
                    return null;
                identity = new UserIdentity(user.Username, user.ID);
                passwordHash = user.Password;
            }

            // calculate hash
            var algorithm = SHA1.Create();
            var calculatedHash = string.Join("", algorithm.ComputeHash(Encoding.UTF8.GetBytes(passwordHash + keyFragment)).Select(b => b.ToString("x2")));
            // compare hashes
            if (calculatedHash == hash)
                return identity;
            return null;
        }

        public static UserIdentity Authenticate(CustomerSetupServiceRequestBase request)
        {
            return Authenticate(request.Username, request.Hash);
        }
    }
}