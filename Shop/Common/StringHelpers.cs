using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Shop.Common
{
    public static class StringHelpers
    {
        public static string ToSeoUrl(this string url) // định nghĩa phương thức mở rộng Extend cho lớp String
        {
            // đặt url thành chữ thường
            string encodedUrl = (url ?? "").ToLower();

            // thay thế dấu & bằng dấu "va"
            encodedUrl = Regex.Replace(encodedUrl, @"\&+", "va");

            // remove characters
            encodedUrl = encodedUrl.Replace(@"à | á | ạ | ả | ã | â | ầ | ấ | ậ | ẩ | ẫ | ă | ằ | ắ | ặ | ẳ | ẵ", "a");

            // loại bỏ các ký tự không hợp lệ
            //encodedUrl = Regex.Replace(encodedUrl, @"[^a-z0-9]","-");

            // Loại bỏ các bản sao
            encodedUrl = Regex.Replace(encodedUrl, @"-+", "-");

            // Loại bỏ khoảng trắng các ký tự đầu và cuối
            encodedUrl = encodedUrl.Trim('-');

            return encodedUrl;
        }

        public static string convertToUnSign3(this string url)
        {
            // đặt url thành chữ thường
            string encodedUrl = (url ?? "").ToLower();

            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = encodedUrl.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D').Replace(" ","-");
        }

        public static string SeoLink(this string url)
        {
            string seolink = (url ?? "").ToLower();
            seolink = Regex.Replace(seolink, @"\&+", "va");
            seolink = url.Replace(" ", "-").Replace("%", "-").Replace("$", "-").Replace("#", "-").Replace("@","-").Replace("!","-").Replace("~","-").Replace("^","-")
                                .Replace("&","-").Replace("*","-").Replace("(","-").Replace(")","-").Replace("_","-").Replace("+","-").Replace("=","-").Replace("{","-")
                                .Replace("}","-").Replace("|","-").Replace("\\","-").Replace(":","-").Replace(";","-").Replace("\"","-").Replace("'","-")
                                .Replace("<","-").Replace(">","-").Replace(",","-").Replace(".","-").Replace("?","-").Replace("/","-").Replace("`","-");
            seolink = seolink.Replace(@"à | á | ạ | ả | ã | â | ầ | ấ | ậ | ẩ | ẫ | ă | ằ | ắ | ặ | ẳ | ẵ", "a");
            seolink = seolink.Replace(@"đ", "d");
            seolink = seolink.Replace(@"ê | ế | ề | ễ | ệ", "e");
            seolink = seolink.Replace(@"ư | ứ | ừ | ữ | ự", "u");
            seolink = seolink.Replace(@"ô | ố | ồ | ỗ | ộ", "o");
            seolink = seolink.Replace(@"í | ì | ị | ĩ", "d");
            return seolink.ToLower();
        }
    }
}