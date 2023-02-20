using System.Net.Mail;
using System.Net;
using System.Text;

namespace prjRehabilitation.Models
{
    public class CSendmal
    {
        public void SendByGmail(string email, string content, string title)
        {
            try
            {
                // 使用 Google Mail Server 發信
                string GoogleID = "yeee880726@gmail.com"; //Google 發信帳號
                string TempPwd = "dkyzsdpffgrgount"; //應用程式密碼
                string ReceiveMail = email; //接收信箱

                string SmtpServer = "smtp.gmail.com";
                int SmtpPort = 587;
                MailMessage mms = new MailMessage();
                mms.From = new MailAddress(GoogleID);
                mms.Subject = title; //主旨
                mms.Body = content;//內容
                mms.IsBodyHtml = true;
                mms.SubjectEncoding = Encoding.UTF8;
                mms.To.Add(new MailAddress(ReceiveMail));
                using (SmtpClient client = new SmtpClient(SmtpServer, SmtpPort))
                {
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential(GoogleID, TempPwd);//寄信帳密 
                    client.Send(mms); //寄出信件
                }
            }
            catch { }

        }
    }
}
