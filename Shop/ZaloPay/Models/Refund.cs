using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shop.ZaloPay.Models
{
    public class Refund
    {
        [Key]
        public string Mrefundid { get; set; }
        public string Zptransid { get; set; }
        public long Amount { get; set; }
    }
}