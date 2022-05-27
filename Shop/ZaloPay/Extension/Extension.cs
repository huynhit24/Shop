using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Shop.ZaloPay.Extension
{
    public static class ObjectExtension
    {
        public static Dictionary<string, string> AsParams(this object source, BindingFlags bindingAttr = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
        {
            return source.GetType().GetProperties(bindingAttr).ToDictionary
            (
                propInfo => propInfo.Name.ToLower(),
                propInfo => propInfo.GetValue(source, null)?.ToString()
            );
        }
    }

    public static class DictionaryExtension
    {
        public static object GetOrDefault(this Dictionary<string, object> source, string key, object defaultValue)
        {
            return source.ContainsKey(key) ? source[key] : defaultValue;
        }
    }
}