using RadiusR.DB;
using RadiusR_Customer_Setup_Service.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using RezaB.API.WebService;

namespace RadiusR_Customer_Setup_Service.Authentication
{
    public static class Authenticator
    {
        public static UserIdentity Authenticate(BaseRequest<SHA256> request)
        {
            // get user data
            UserIdentity identity;
            string passwordHash;
            using (RadiusREntities db = new RadiusREntities())
            {
                var user = db.CustomerSetupUsers.FirstOrDefault(u => u.Username == request.Username);
                if (user == null || !user.IsEnabled)
                    return null;
                identity = new UserIdentity(user.Username, user.ID, user.Password);
                passwordHash = user.Password;
            }
            if (request.HasValidHash(passwordHash, Properties.Settings.Default.CacheDuration))
            {
                return identity;
            }
            return null;
        }
    }
}