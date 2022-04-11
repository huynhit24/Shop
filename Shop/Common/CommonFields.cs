using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Shop.Common
{
    public class CommonFields
    {
        public static int id { get; set; }
        public static string seek { get; set; }

        public static string getStringSHA256Hash(string text)
        {
            using (var sha256 = new SHA256Managed())
            {
                return BitConverter.ToString(sha256.ComputeHash(Encoding.UTF8.GetBytes(text))).Replace("-", ""); // replace tránh SQL Injection
            }
        }
    }
}