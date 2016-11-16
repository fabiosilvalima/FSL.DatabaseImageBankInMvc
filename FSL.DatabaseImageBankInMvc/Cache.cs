using System;
using System.Configuration;
using System.Web.Configuration;

namespace FSL.DatabaseImageBankInMvc
{
    public class Cache
    {
        public Cache()
        {
            _config = ConfigurationManager.GetSection("system.web/caching/outputCacheSettings") as OutputCacheSettingsSection;
        }
        
        private OutputCacheSettingsSection _config;
        
        private OutputCacheProfile GetProfile(string profile)
        {
            return !string.IsNullOrEmpty(profile) ? _config.OutputCacheProfiles[profile] : new OutputCacheProfile("default");
        }
        
        private object GetFromCache(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new NullReferenceException("id is null");
            if (System.Web.HttpRuntime.Cache != null)
            {
                lock (this)
                {
                    return System.Web.HttpRuntime.Cache[id];
                }
            }

            return null;
        }
        
        public Cache Insert(string id, string profile, object obj)
        {
            if (System.Web.HttpRuntime.Cache != null)
            {
                if (string.IsNullOrEmpty(id))
                {
                    throw new ArgumentNullException("id", "id is null");
                }

                if (string.IsNullOrEmpty(profile))
                {
                    throw new ArgumentNullException("profile", string.Format("profile is null for id {0}", id));
                }

                var objProfile = GetProfile(profile);
                if (objProfile == null)
                {
                    throw new NullReferenceException(string.Format("profile is null for id {0} and profile {1}", id, profile));
                }

                lock (this)
                {
                    System.Web.HttpRuntime.Cache.Insert(id, obj, null, DateTime.Now.AddSeconds(objProfile.Duration), TimeSpan.Zero);
                }
            }

            return this;
        }
        
        public bool NotExists(string id)
        {
            return GetFromCache(id) == null;
        }
        
        public Cache Remove(string id)
        {
            if (System.Web.HttpRuntime.Cache != null)
            {
                lock (this)
                {
                    System.Web.HttpRuntime.Cache.Remove(id);
                }
            }

            return this;
        }
        
        public object Get(string id)
        {
            return GetFromCache(id);
        }
    }
}