using Microsoft.AspNetCore.Mvc;
using prjRehabilitation.Models;
using prjRehabilitation.ViewModel;
using prjRehabilitation.ViewModel.Eric;
using System.Net.Mail;
using System.Text.Json;

namespace prjRehabilitation.Controllers.Api
{
    public class CalendarAjaxController : Controller
    {
        public Customer getCustomerIfSession1()
        {
            string json = HttpContext.Session.GetString(CDictionary.SK_CUSTOMER_User);
            Customer customer = null;

            if (!string.IsNullOrEmpty(json))
            {
                customer = JsonSerializer.Deserialize<Customer>(json);
            }
            return customer;
        }

        //public Admin getAdminIfSession()
        //{
        //    string json = HttpContext.Session.GetString(CDictionary.SK_ADMIN_User);
        //    Admin admin = null;

        //    if (!string.IsNullOrEmpty(json))
        //    {
        //        admin = JsonSerializer.Deserialize<Admin>(json);
        //    }
        //    return admin;
        //}

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SendMailByGmail(CCalendarViewModel vm)
        {
            if (vm.content == null || vm.date == null) return Content("請輸入完整資料");
            if (vm.fRecorder == null) return Content("請登入後申請");
            dbClassContext db = new dbClassContext();
            Customer getCusSession = getCustomerIfSession1();
            string getResident = db.PatientInfos.Where(_ => _.FCustomerid == getCusSession.Fid).FirstOrDefault().FName;//先抓出來免得不是字造成連續發信問題
            string getDate = vm.date;   //先抓出來免得不是字造成連續發信問題
            List<string> MailList = new List<string>();

            MailList.Add(getCusSession.FEmail);//新增收件人進去
            string Subject = "會客申請";
            string Body = "您好，您想申請在"+getDate+"與"+"\r\n住民："+ getResident + "\r\n會面\r\n內容為：\r\n"+"\r\n"+ vm.content+ "\r\n       收到申請了，請等候通知。";
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("yeeee880726@gmail.com", "測試郵件", System.Text.Encoding.UTF8);
            msg.To.Add(string.Join(",", MailList.ToArray()));//收件人
            msg.Subject = Subject; //主旨
            msg.SubjectEncoding = System.Text.Encoding.UTF8;
            msg.Body = Body;//內容
            msg.IsBodyHtml = true;
            msg.BodyEncoding = System.Text.Encoding.UTF8;
            msg.Priority = MailPriority.Normal;
            SmtpClient MySmtp = new SmtpClient("smtp.gmail.com", 587);
            //寄件人
            MySmtp.Credentials = new System.Net.NetworkCredential("yeeee880726@gmail.com", "dkyzsdpffgrgount");
            MySmtp.EnableSsl = true;
            MySmtp.Send(msg);
            return Content("已發送郵件, 請等候通知。");
        }


    }
}
