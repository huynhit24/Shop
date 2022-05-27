using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace Shop.ZaloPay.Crypto
{
    public enum ZaloPayHMAC
    {
        HMACMD5,
        HMACSHA1,
        HMACSHA256,
        HMACSHA512
    }

    public class HmacHelper
    {
        public static string Compute(ZaloPayHMAC algorithm = ZaloPayHMAC.HMACSHA256, string key = "", string message = "")
        {
            byte[] keyByte = System.Text.Encoding.UTF8.GetBytes(key);
            byte[] messageBytes = System.Text.Encoding.UTF8.GetBytes(message);
            byte[] hashMessage = null;

            switch (algorithm)
            {
                case ZaloPayHMAC.HMACMD5:
                    hashMessage = new HMACMD5(keyByte).ComputeHash(messageBytes);
                    break;
                case ZaloPayHMAC.HMACSHA1:
                    hashMessage = new HMACSHA1(keyByte).ComputeHash(messageBytes);
                    break;
                case ZaloPayHMAC.HMACSHA256:
                    hashMessage = new HMACSHA256(keyByte).ComputeHash(messageBytes);
                    break;
                case ZaloPayHMAC.HMACSHA512:
                    hashMessage = new HMACSHA512(keyByte).ComputeHash(messageBytes);
                    break;
                default:
                    hashMessage = new HMACSHA256(keyByte).ComputeHash(messageBytes);
                    break;
            }

            return BitConverter.ToString(hashMessage).Replace("-", "").ToLower();
        }
    }
}