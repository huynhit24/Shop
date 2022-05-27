using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Shop.ZaloPay.Crypto;
using Shop.ZaloPay.Models;
using Shop.ZaloPay.Extension;
using static Shop.ZaloPay.Crypto.HmacHelper;
using Newtonsoft.Json.Linq;

namespace Shop.ZaloPay
{
    public class ZaloPayHelper
    {
        private static long uid = Util.GetTimeStamp();

        public static bool VerifyCallback(string data, string requestMac)
        {
            try
            {
                string mac = HmacHelper.Compute(ZaloPayHMAC.HMACSHA256, "Iyz2habzyr7AG8SgvoBCbKwKi3UzlLi3", data);

                return requestMac.Equals(mac);
            }
            catch
            {
                return false;
            }
        }

        public static bool VerifyRedirect(Dictionary<string, object> data)
        {
            try
            {
                string reqChecksum = data["checksum"].ToString();
                string checksum = ZaloPayMacGenerator.Redirect(data);

                return reqChecksum.Equals(checksum);
            }
            catch
            {
                return false;
            }
        }

        public static string GenTransID()
        {
            return DateTime.Now.ToString("yyMMdd") + "_" + "553" + "_" + (++uid);
        }

        public static Task<Dictionary<string, object>> CreateOrder(Dictionary<string, string> orderData)
        {
            return HttpHelper.PostFormAsync("https://sandbox.zalopay.com.vn/v001/tpe/createorder", orderData);
        }

        public static Task<Dictionary<string, object>> CreateOrder(OrderData orderData)
        {
            return CreateOrder(orderData.AsParams());
        }

        public static Task<Dictionary<string, object>> QuickPay(Dictionary<string, string> orderData)
        {
            return HttpHelper.PostFormAsync("https://sandbox.zalopay.com.vn/v001/tpe/submitqrcodepay", orderData);
        }

        public static Task<Dictionary<string, object>> QuickPay(QuickPayOrderData orderData)
        {
            return QuickPay(orderData.AsParams());
        }

        public static Task<Dictionary<string, object>> GetOrderStatus(string apptransid)
        {
            var data = new Dictionary<string, string>();
            data.Add("appid", "553");
            data.Add("apptransid", apptransid);
            data.Add("mac", ZaloPayMacGenerator.GetOrderStatus(data));

            return HttpHelper.PostFormAsync("https://sandbox.zalopay.com.vn/v001/tpe/getstatusbyapptransid", data);
        }

        public static Task<Dictionary<string, object>> Refund(Dictionary<string, string> refundData)
        {
            return HttpHelper.PostFormAsync("https://sandbox.zalopay.com.vn/v001/tpe/partialrefund", refundData);
        }

        public static Task<Dictionary<string, object>> Refund(RefundData refundData)
        {
            return Refund(refundData.AsParams());
        }

        public static Task<Dictionary<string, object>> GetRefundStatus(string mrefundid)
        {
            var data = new Dictionary<string, string>();
            data.Add("appid", "553");
            data.Add("mrefundid", mrefundid);
            data.Add("timestamp", Util.GetTimeStamp().ToString());
            data.Add("mac", ZaloPayMacGenerator.GetRefundStatus(data));

            return HttpHelper.PostFormAsync("https://sandbox.zalopay.com.vn/v001/tpe/getpartialrefundstatus", data);
        }

        public static Task<Dictionary<string, object>> GetBankList()
        {
            var data = new Dictionary<string, string>();
            data.Add("appid", "553");
            data.Add("reqtime", Util.GetTimeStamp().ToString());
            data.Add("mac", ZaloPayMacGenerator.GetBankList(data));

            return HttpHelper.PostFormAsync("https://sbgateway.zalopay.vn/api/getlistmerchantbanks", data);
        }

        public static List<BankDTO> ParseBankList(Dictionary<string, object> banklistResponse)
        {
            var banklist = new List<BankDTO>();
            var bankMap = (banklistResponse["banks"] as JObject);

            foreach (var bank in bankMap)
            {
                var bankDTOs = bank.Value.ToObject<List<BankDTO>>();
                foreach (var bankDTO in bankDTOs)
                {
                    banklist.Add(bankDTO);
                }
            }

            return banklist;
        }
    }
}