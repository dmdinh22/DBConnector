using System;
using System.Runtime.Caching;

namespace DBConnector.Tools
{
    public sealed class ObjCache
    {
        private static readonly ObjCache instance = new ObjCache();

        static ObjCache() { }

        private ObjCache() { }

        public static ObjCache Instance
        {
            get { return instance; }
        }

        readonly ObjectCache cache = MemoryCache.Default;

        public T Get<T>(string key)
        {
            if (cache.Contains(key)) { return (T)cache[key]; }
            return default(T);
        }

        public void Set<T>(string key, T value, int expire_in_hours = 1)
        {
            cache.Set(key, value, DateTime.Now.AddHours(expire_in_hours));
        }

        public bool HasCache(string key)
        {
            return cache.Contains(key);
        }
    }
}
