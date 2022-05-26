
using System;
using System.IO;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace EastlawUI_v2
{
    public class SendGridAPI
    {
       
        public static async Task Execute(string EmailTo, string HtmlContent, string Subject, string AttachmentFilePath)
        {
            try
            {
                
                var FromEmail = System.Configuration.ConfigurationManager.AppSettings["SendGridFromEmail"].ToString();
                var apiKey = System.Configuration.ConfigurationManager.AppSettings["SendGridAPIKey"].ToString();
                var client = new SendGridClient(apiKey);
                var From = new EmailAddress(FromEmail, "ZAA (Zeeshan Ali Adnan)");
                var To = new EmailAddress(EmailTo);
                var msg = SendGrid.Helpers.Mail.MailHelper.CreateSingleEmail(From, To, Subject, string.Empty, HtmlContent);
                // Adding Attachement 
                if (!string.IsNullOrEmpty(AttachmentFilePath))
                {
                    var FileNameWithExtension = System.IO.Path.GetFileName(AttachmentFilePath);
                    var bytes = System.IO.File.ReadAllBytes(AttachmentFilePath);
                    var file = Convert.ToBase64String(bytes);
                    msg.AddAttachment(FileNameWithExtension, file);
                }
                var response = await client.SendEmailAsync(msg);
                // WriteLogs("Status Code :" + response.StatusCode.ToString() + "\n Sending To : " + EmailTo, "Execute");
            }
            catch (Exception exc)
            {
                SendGridAPI.WriteLogs("EXCEPTION ::" + exc.Message == null ? exc.InnerException.Message : exc.Message, "Execute");
            }
        }

        public static void WriteLogs(string msg, string filename)
        {
            try
            {
                string FolderName = DateTime.Now.ToString("MMyyyy");
                string day = DateTime.Now.ToString("dd");

                if (!Directory.Exists(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "apilogs/" + FolderName + "")))
                    Directory.CreateDirectory(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "apilogs/" + FolderName + ""));


                if (!Directory.Exists(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "apilogs/" + FolderName + "")))
                    Directory.CreateDirectory(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "apilogs/" + FolderName + ""));

                if (!Directory.Exists(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "apilogs/" + FolderName + "/" + day + "")))
                    Directory.CreateDirectory(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "apilogs/" + FolderName + "/" + day + ""));

                StreamWriter sw = new StreamWriter(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "apilogs/" + FolderName + "/" + day + "/" + filename + ".txt"), true);
                //StreamWriter sw = new StreamWriter(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Logs/" + FolderName + "/"+RqstCode+".txt"), true);
                sw.WriteLine("------------------------------------" + DateTime.Now + "-------------------");
                sw.WriteLine(msg);
                sw.Flush();
                sw.Close();
            }
            catch (Exception ex)
            {
                StreamWriter sw = new StreamWriter(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "apilogs/LoggingError" + ".txt"), true);
                //StreamWriter sw = new StreamWriter(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Logs/" + FolderName + "/"+RqstCode+".txt"), true);
                sw.WriteLine("------------------- Error In Loging eroor -------------------" + DateTime.Now.AddMinutes(28) + "-------------------");
                sw.WriteLine(ex.Message);
                sw.Flush();
                sw.Close();
            }
        }
    }
}