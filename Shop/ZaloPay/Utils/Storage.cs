using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.ZaloPay.Utils
{
    public class Storage
    {
        private static Dictionary<string, object> storage = new Dictionary<string, object>();

        private Storage() { }

        public static void Set(string key, object payload)
        {
            storage[key] = payload;
        }

        public static object Get(string key)
        {
            return storage.FirstOrDefault(p => p.Key == key).Value;
        }

        public static bool Remove(string key)
        {
            return storage.Remove(key);
        }
    }
}