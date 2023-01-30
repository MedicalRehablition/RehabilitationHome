using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Net.NetworkInformation;

namespace prjRehabilitation.Controllers
{
    public class WebcamController : Controller
    {
        public IActionResult Camera()
        {
            return View();
        }

        [HttpPost]
        public IActionResult decode(string imageData)
        {
            //IBarcodeReader reader = new BarcodeReader();
            var reader = new ZXing.Windows.Compatibility.BarcodeReader();
            //BarcodeReader r = new BarcodeReader();
            if (imageData != null)
            {
                var image = (Bitmap)new ImageConverter().ConvertFrom(Convert.FromBase64String(imageData.Split(',')[1]));
                //var image = (Bitmap)Image.FromFile("D:\\C#\\QRcode\\qrcodePractice\\qrcodeform\\images\\ZxingQrcode.png");//測試
                //var image = (Bitmap)Image.FromFile("D:\\github\\期末測試用\\RehabilitationHome-gimmywu-patch-1\\prjRehabilitation\\wwwroot\\images\\ZxingQrcodewithbackground.png");//圖片有背景測試
                //byte[] image = Convert.FromBase64String(imageData.Split(',')[1]);
                var result = reader.Decode(image);
                if (result != null)
                {
                    string decodedText = result.Text;
                    return Json(new { decodedText });
                }
            }
            return Json(new { decodedText = "no pic" });

        }

    }
}
