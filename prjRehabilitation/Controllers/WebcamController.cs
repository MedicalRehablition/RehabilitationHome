using Microsoft.AspNetCore.Mvc;
using prjRehabilitation.Models;
using prjRehabilitation.ViewModel;
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
                //var image = (Bitmap)Image.FromFile("D:\\github\\期末測試用\\testRehabilitationHome\\prjRehabilitation\\wwwroot\\images\\e24.png");//員工測試
                //var image = (Bitmap)Image.FromFile("D:\\github\\期末測試用\\testRehabilitationHome\\prjRehabilitation\\wwwroot\\images\\c10.png");//訪客測試
                dbClassContext db = new dbClassContext();
                CQrcodeinfo qr = new CQrcodeinfo();
                var result = reader.Decode(image);
                if (result != null)
                {
                    string end = result.ToString();
                    string head = end.Substring(0, 1);
                    int unkownid = Convert.ToInt32(end.Substring(1, end.Length - 1));

                    if (head == "e") //員工
                    {
                        Admin admin = db.Admins.FirstOrDefault(t => t.Fid == unkownid);
                        if (admin != null)
                        {
                            qr.word = head;  //英文字母，判斷為員工還是訪客
                            qr.Person = admin.FName;  //員工姓名
                            qr.Time = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
                            qr.Morningafternoon = qr.morningafternoon();

                            //把資料放進差勤資料庫(利用Morningafternoon來判斷存到哪個column)
                            EmployeeDuty empduty = new EmployeeDuty();
                            if (qr.Morningafternoon == "上班時間:")
                            {
                                empduty.Employeeid = unkownid;
                                empduty.Onduty = qr.Time;
                                //db.EmployeeDuties.Add(empduty);
                                //db.SaveChanges();
                            }
                            else if (qr.Morningafternoon == "下班時間:")
                            {
                                var gooffduty = db.EmployeeDuties.FirstOrDefault(t => t.Fid == unkownid && t.Onduty.Substring(0, 10) == DateTime.Now.ToString("yyyy-MM-dd"));
                                empduty.Offduty = qr.Time;//未完成規則建置
                                //db.EmployeeDuties.Add(empduty);
                                //db.SaveChanges();
                            };
                            db.EmployeeDuties.Add(empduty);
                            db.SaveChanges();
                        };
                        return Json(qr);
                    }
                    else if (head == "c") //訪客
                    {
                        Customer cust = db.Customers.FirstOrDefault(t => t.Fid == unkownid);
                        if (cust != null)
                        {
                            qr.word = head;
                            qr.Person = cust.FName;//找病人的姓名
                            qr.Time = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
                            qr.Morningafternoon = "來訪時間";
                            //----把資料放進訪客資料庫--
                            VisitorDateList vist = new VisitorDateList();
                            vist.Customerid = unkownid;
                            vist.Date = qr.Time;
                            db.VisitorDateLists.Add(vist);
                            db.SaveChanges();
                        }
                        return Json(qr);
                    };
                };
            };
            return Json(null);

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