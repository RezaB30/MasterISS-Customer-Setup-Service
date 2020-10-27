using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;

namespace RadiusR_Customer_Setup_Service.Authentication
{
    public static class KeyManager
    {
        private static MemoryCache _cache = MemoryCache.Default;
        private const string _namePrefix = "KEYS_";
        private static TimeSpan _cacheDuration = TimeSpan.FromHours(25);

        public static string GenerateKey(string username)
        {
            var key = GetKey(username);
            if(key == null)
            {
                key = Guid.NewGuid().ToString();
                _cache.Set(_namePrefix + username, key, GetPolicy());
            }
            return key;
        }

        public static string GetKey(string username)
        {
            var key = _cache.Get(_namePrefix + username) as string;
            return key;
        }

        private static CacheItemPolicy GetPolicy()
        {
            return new CacheItemPolicy()
            {
                AbsoluteExpiration = DateTimeOffset.Now.Add(_cacheDuration),
            };
        }
    }
}