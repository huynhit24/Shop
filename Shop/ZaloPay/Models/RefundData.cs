using Shop.ZaloPay.Crypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static Shop.ZaloPay.Crypto.HmacHelper;

namespace Shop.ZaloPay.Models
{
    public class RefundData
    {
        public string Appid { get; set; }
        public string Zptransid { get; set; }
        public long Amount { get; set; }
        public string Description { get; set; }
        public long Timestamp { get; set; }
        public string Mrefundid { get; set; }
        public string Mac { get; set; }

        public RefundData(long amount, string zptransid, string description = "")
        {
            Appid = "553";
            Zptransid = zptransid;
            Amount = amount;
            Description = description;
            Mrefundid = ZaloPayHelper.GenTransID();
            Timestamp = Util.GetTimeStamp();
            Mac = ComputeMac();
        }

        public string GetMacData()
        {
            return Appid + "|" + Zptransid + "|" + Amount + "|" + Description + "|" + Timestamp;
        }

        public string ComputeMac()
        {
            return HmacHelper.Compute(ZaloPayHMAC.HMACSHA256, "9phuAOYhan4urywHTh0ndEXiV3pKHr5Q", GetMacData());
        }
    }
}