﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RadiusR_Customer_Setup_Service.Authentication
{
    public class UserIdentity
    {
        public string Username { get; private set; }

        public int UserId { get; private set; }

        public UserIdentity(string username, int userId)
        {
            Username = username;
            UserId = userId;
        }
    }
}