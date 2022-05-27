using QRCoder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.ZaloPay
{
    public class QRCodeHelper
    {
        private static readonly QRCodeGenerator qrCodeGenerator = new QRCodeGenerator();

        public static Base64QRCode CreateQRBase64Code(string text)
        {
            var qrCodeData = qrCodeGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
            var qrCode = new Base64QRCode(qrCodeData);
            return qrCode;
        }

        public static string CreateQRCodeBase64Image(string text)
        {
            var qrCode = CreateQRBase64Code(text);
            return "data:image/png;base64," + qrCode.GetGraphic(5);
        }
    }
}