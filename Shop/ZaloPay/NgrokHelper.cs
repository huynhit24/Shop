using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Shop.ZaloPay
{
    public class NgrokHelper
    {
        public static string PublicUrl { get; private set; }

        public static async Task Init()
        {
            try
            {
                var response = await HttpHelper.GetJson("http://localhost:4040/api/tunnels");
                var tunnels = response["tunnels"] as JArray;
                var tunnel = tunnels[0].ToObject<Dictionary<string, object>>();
                PublicUrl = tunnel["public_url"].ToString();
            }
            catch
            {
                PublicUrl = "";
            }
        }


        public static Dictionary<string, object> CreateEmbeddataWithPublicUrl(Dictionary<string, object> embeddata)
        {
            if (!string.IsNullOrEmpty(PublicUrl))
            {
                embeddata["callbackurl"] = PublicUrl + "/Callback";
            }
            return embeddata;
        }

        public static Dictionary<string, object> CreateEmbeddataWithPublicUrl()
        {
            return CreateEmbeddataWithPublicUrl(new Dictionary<string, object>());
        }
    }
}