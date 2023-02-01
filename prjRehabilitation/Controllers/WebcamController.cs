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
// Substring(抓取起始字串index(int),抓的字串長度(int)):指定抓取起點與抓取長度常用Substring(Int32, Int32)

//string b = "我要成為工程師";
//string c = b.Substring(3, 2);//從index=3開始抓，往後抓2個
//Console.WriteLine(c);
//Console.ReadKey();
//輸出結果:為工

//字串尋找
//概念一:直接搜尋文字
//String.Contains(string要查的字串):看括號內的值是否在字串內，傳回值為布林值boolean(T / F)常用
//string b = "我要成為工程師";
//bool c = b.Contains("我");//看"我"是否存在b字串裡
//Console.WriteLine(c);
//Console.ReadKey();
//輸出結果:true