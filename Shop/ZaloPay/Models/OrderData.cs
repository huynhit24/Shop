using Newtonsoft.Json;
using Shop.ZaloPay.Crypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static Shop.ZaloPay.Crypto.HmacHelper;

namespace Shop.ZaloPay.Models
{
    public class OrderData
    {
        public string Appid { get; set; }
        public string Apptransid { get; set; }
        public long Apptime { get; set; }
        public string Appuser { get; set; }
        public string Item { get; set; }
        public string Embeddata { get; set; }
        public long Amount { get; set; }
        public string Description { get; set; }
        public string Bankcode { get; set; }
        public string Mac { get; set; }

        public OrderData(long amount, string description = "", string bankcode = "", object embeddata = null, object item = null, string appuser = "")
        {
            Appid = "553";
            Apptransid = ZaloPayHelper.GenTransID();
            Apptime = Util.GetTimeStamp();
            Appuser = appuser;
            Amount = amount;
            Bankcode = bankcode;
            Description = description;
            Embeddata = JsonConvert.SerializeObject(embeddata);
            Item = JsonConvert.SerializeObject(item);
            Mac = ComputeMac();
        }

        public virtual string GetMacData()
        {
            return Appid + "|" + Apptransid + "|" + Appuser + "|" + Amount + "|" + Apptime + "|" + Embeddata + "|" + Item;
        }

        public string ComputeMac()
        {
            return Compute(ZaloPayHMAC.HMACSHA256, "9phuAOYhan4urywHTh0ndEXiV3pKHr5Q", GetMacData());
        }
    }

    public class QuickPayOrderData : OrderData
    {
        public string Paymentcode { get; set; }

        public QuickPayOrderData(long amount, string paymentcodeRaw, string description = "", object embeddata = null, object item = null, string appuser = "")
            : base(amount, description, "", embeddata, item, appuser)
        {
            Paymentcode = RSAHelper.Encrypt(paymentcodeRaw, "MFwwDQYJKoZIhvcNAQEBBQADSwAwSAJBAOfB6/x0b5UiLkU3pOdcnXIkuCSzmvlVhDJKv1j3yBCyvsgAHacVXd+7WDPcCJmjSEKlRV6bBJWYam5vo7RB740CAwEAAQ==");
            Mac = ComputeMac();
        }

        public override string GetMacData()
        {
            return base.GetMacData() + "|" + Paymentcode;
        }
    }
}