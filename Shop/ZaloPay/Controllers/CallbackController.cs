using Newtonsoft.Json;
using Shop.ZaloPay;
using Shop.ZaloPay.DAL;
using Shop.ZaloPay.Models;
using Shop.ZaloPay.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.ZaloPay.Controllers
{
    public class CallbackController : Controller
    {
        [HttpPost]
        public JsonResult Index(CallbackRequest cbdata)
        {
            var result = new Dictionary<string, object>();

            try
            {
                var dataStr = Convert.ToString(cbdata.Data);
                var requestMac = Convert.ToString(cbdata.Mac);

                var isValidCallback = ZaloPayHelper.VerifyCallback(dataStr, requestMac);

                // kiểm tra callback hợp lệ (đến từ zalopay server)
                if (!isValidCallback)
                {
                    // callback không hợp lệ
                    result["returncode"] = -1;
                    result["returnmessage"] = "mac not equal";
                }
                else
                {
                    // thanh toán thành công
                    // merchant xử lý đơn hàng cho user
                    var data = JsonConvert.DeserializeObject<Dictionary<string, object>>(dataStr);
                    var apptransid = data["apptransid"].ToString();

                    using (var db = new ZaloPayDemoContext())
                    {
                        var orderDTO = db.Orders.SingleOrDefault(o => o.Apptransid.Equals(apptransid));
                        if (orderDTO != null)
                        {
                            orderDTO.Zptransid = data["zptransid"].ToString();
                            orderDTO.Channel = int.Parse(data["channel"].ToString());
                            orderDTO.Status = 1;

                            db.SaveChanges();
                        }
                    }

                    result["returncode"] = 1;
                    result["returnmessage"] = "success";

                    Storage.Set(apptransid, data);
                }

                // thông báo kết quả cho zalopay server
                return Json(result);
            }
            catch (Exception ex)
            {
                result["returncode"] = 0; // ZaloPay sẽ callback lại tối đa 3 lần
                result["returnmessage"] = ex.Message;
                return Json(result);
            }

        }
    }
}