using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Configuration;
using System.IO;
using System.Net;


public class Email{
        public static void SendMail(string ToEmail, string Content, string Subject, string FromDisplayName, string AttachmentFilePath)
        {
            try
            {
                SmtpClient smtpClinet = new SmtpClient();
                MailMessage msg = new MailMessage();
                msg.Body = Content;
                msg.To.Add(new MailAddress(ToEmail));
                msg.From = new MailAddress(ConfigurationSettings.AppSettings["fromEmail"].ToString(), FromDisplayName);
                msg.Subject = Subject;
                msg.IsBodyHtml = true;
                if (AttachmentFilePath != "")
                    msg.Attachments.Add(new System.Net.Mail.Attachment(AttachmentFilePath));

                SmtpClient client = new SmtpClient();
                client.Host = ConfigurationSettings.AppSettings["smtp"].ToString();
                client.Credentials = new System.Net.NetworkCredential(ConfigurationSettings.AppSettings["fromEmail"].ToString(), ConfigurationSettings.AppSettings["fromEmailPassword"].ToString());


                client.Send(msg);
                msg.Dispose();


            }
            catch (Exception ex)
            {
            Email.WriteLogs("Email Sending Failed Error Message: " + ex.Message, "Email Sending Failed");
        }
        }
        public int SendMailTest(string ToEmail, string FromEmail, string SMTP, string Content, string Subject, string FromDisplayName, string UserName, string Password)
        {
            try
            {
                SmtpClient smtpClinet = new SmtpClient();
                MailMessage msg = new MailMessage();
                msg.Body = Content;
                msg.To.Add(new MailAddress(ToEmail));
                msg.From = new MailAddress(FromEmail, FromDisplayName);
                msg.Subject = Subject;
                msg.IsBodyHtml = true;
                SmtpClient client = new SmtpClient();
                client.Host = SMTP;
                client.Credentials = new System.Net.NetworkCredential(UserName, Password);


                client.Send(msg);
                msg.Dispose();

                return 1;

            }
            catch (Exception ex)
            {
                return 0;

            }
        }
        public static void AWSSimpleEmailTest(string ToEmail, string Content, string Subject, string FromDisplayName, string AttachmentFilePath)
    {
        // Replace sender@example.com with your "From" address. 
        // This address must be verified with Amazon SES.
        String FROM = "notification @eastlaw.pk";
        String FROMNAME = "Notification - Eastlaw.pk";

        // Replace recipient@example.com with a "To" address. If your account 
        // is still in the sandbox, this address must be verified.
        String TO = ToEmail;// "umar.mughal@gmail.com";

        // Replace smtp_username with your Amazon SES SMTP user name.
        String SMTP_USERNAME = "AKIAW2ZEV4TBIU4N3UUG";

        // Replace smtp_password with your Amazon SES SMTP user name.
        String SMTP_PASSWORD = "BNQ6p8WUhGCxNY1392BWIIEpXw6ZWFQFOBMQh1G1sovq";

        // (Optional) the name of a configuration set to use for this message.
        // If you comment out this line, you also need to remove or comment out
        // the "X-SES-CONFIGURATION-SET" header below.
        String CONFIGSET = "elnotification";

        // If you're using Amazon SES in a region other than US West (Oregon), 
        // replace email-smtp.us-west-2.amazonaws.com with the Amazon SES SMTP  
        // endpoint in the appropriate AWS Region.
        String HOST = "email-smtp.ap-south-1.amazonaws.com";

        // The port you will connect to on the Amazon SES SMTP endpoint. We
        // are choosing port 587 because we will use STARTTLS to encrypt
        // the connection.
        int PORT = 587;

        // The subject line of the email
        String SUBJECT = Subject;// "Amazon SES test (SMTP interface accessed using C#)";

        // The body of the email
        String BODY = Content;
            //"<h1>Amazon SES Test</h1>" +
            //"<p>This email was sent through the " +
            //"<a href='https://aws.amazon.com/ses'>Amazon SES</a> SMTP interface " +
            //"using the .NET System.Net.Mail library.</p>";

        // Create and build a new MailMessage object
        MailMessage message = new MailMessage();
        message.IsBodyHtml = true;
        message.From = new MailAddress(FROM, FROMNAME);
        message.To.Add(new MailAddress(TO));
        message.Subject = SUBJECT;
        message.Body = BODY;
        if (AttachmentFilePath != "")
            message.Attachments.Add(new System.Net.Mail.Attachment(AttachmentFilePath));
        // Comment or delete the next line if you are not using a configuration set
        message.Headers.Add("X-SES-CONFIGURATION-SET", CONFIGSET);

        using (var client = new System.Net.Mail.SmtpClient(HOST, PORT))
        {
            // Pass SMTP credentials
            client.Credentials =
                new NetworkCredential(SMTP_USERNAME, SMTP_PASSWORD);

            // Enable SSL encryption
            client.EnableSsl = true;

            // Try to send the message. Show status in console.
            try
            {
               // Console.WriteLine("Attempting to send email...");
                client.Send(message);
                //Console.WriteLine("Email sent!");
            }
            catch (Exception ex)
            {
                Email.WriteLogs("Email Sending Failed Error Message: " + ex.Message, "Email Sending Failed");
                // Console.WriteLine("The email was not sent.");
                //  Console.WriteLine("Error message: " + ex.Message);
            }
        }
        
    }
        public static void AWSSimpleEmail(string ToEmail, string Content, string Subject, string FromDisplayName, string AttachmentFilePath)
    {
        try
        {
            // Replace sender@example.com with your "From" address. 
            // This address must be verified with Amazon SES.
            string FROM = ConfigurationManager.AppSettings["fromEmail"].ToString();
            string FROMNAME = ConfigurationManager.AppSettings["formName"].ToString();

            // Replace recipient@example.com with a "To" address. If your account 
            // is still in the sandbox, this address must be verified.
            string TO = ToEmail;// "umar.mughal@gmail.com";

            // Replace smtp_username with your Amazon SES SMTP user name.
            string SMTP_USERNAME = ConfigurationManager.AppSettings["userNameEmail"].ToString();

            // Replace smtp_password with your Amazon SES SMTP user name.
            string SMTP_PASSWORD = ConfigurationManager.AppSettings["fromEmailPassword"].ToString();

            // (Optional) the name of a configuration set to use for this message.
            // If you comment out this line, you also need to remove or comment out
            // the "X-SES-CONFIGURATION-SET" header below.
            //string CONFIGSET = "elnotification";

            // If you're using Amazon SES in a region other than US West (Oregon), 
            // replace email-smtp.us-west-2.amazonaws.com with the Amazon SES SMTP  
            // endpoint in the appropriate AWS Region.
            string HOST = ConfigurationManager.AppSettings["smtp"].ToString();

            // The port you will connect to on the Amazon SES SMTP endpoint. We
            // are choosing port 587 because we will use STARTTLS to encrypt
            // the connection.
            int PORT = Convert.ToInt16(ConfigurationManager.AppSettings["port"].ToString());

            // The subject line of the email
            string SUBJECT = Subject;// "Amazon SES test (SMTP interface accessed using C#)";

            // The body of the email
            string BODY = Content;
            //"<h1>Amazon SES Test</h1>" +
            //"<p>This email was sent through the " +
            //"<a href='https://aws.amazon.com/ses'>Amazon SES</a> SMTP interface " +
            //"using the .NET System.Net.Mail library.</p>";

            // Create and build a new MailMessage object
            MailMessage message = new MailMessage();
            message.IsBodyHtml = true;
            message.From = new MailAddress(FROM, FROMNAME);
            message.To.Add(new MailAddress(TO));
            message.Subject = SUBJECT;
            message.Body = BODY;
            if (AttachmentFilePath != "")
                message.Attachments.Add(new Attachment(AttachmentFilePath));
            // Comment or delete the next line if you are not using a configuration set
            //message.Headers.Add("X-SES-CONFIGURATION-SET", CONFIGSET);

            using (var client = new SmtpClient(HOST, PORT))
            {
                // Pass SMTP credentials
                client.Credentials =
                    new NetworkCredential(SMTP_USERNAME, SMTP_PASSWORD);

                // Enable SSL encryption
                client.EnableSsl = true;

                // Try to send the message. Show status in console.
                try
                {
                    // Console.WriteLine("Attempting to send email...");
                    client.Send(message);
                    //Console.WriteLine("Email sent!");
                }
                catch (Exception ex)
                {
                    // Email.WriteLogs("Email Sending Failed Error Message: " + ex.Message, "Email Sending Failed");
                    // Console.WriteLine("The email was not sent.");
                    //  Console.WriteLine("Error message: " + ex.Message);
                }
            }
        }
        catch (Exception ex)
        { }

    }

    public static void WriteLogs(string msg, string filename)
    {
        try
        {
            string FolderName = DateTime.Now.ToString("MMyyyy");

            if (!Directory.Exists(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Logs/" + FolderName + "")))
                Directory.CreateDirectory(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Logs/" + FolderName + ""));

            StreamWriter sw = new StreamWriter(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Logs/" + FolderName + "/" + filename + ".txt"), true);
            //StreamWriter sw = new StreamWriter(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Logs/" + FolderName + "/"+RqstCode+".txt"), true);
            sw.WriteLine("------------------------------------" + DateTime.Now + "-------------------");
            sw.WriteLine(msg);
            sw.Flush();
            sw.Close();
        }
        catch (Exception ex)
        {
            StreamWriter sw = new StreamWriter(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Logs/LoggingError" + ".txt"), true);
            //StreamWriter sw = new StreamWriter(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Logs/" + FolderName + "/"+RqstCode+".txt"), true);
            sw.WriteLine("------------------- Error In Loging eroor -------------------" + DateTime.Now.AddMinutes(28) + "-------------------");
            sw.WriteLine(ex.Message);
            sw.Flush();
            sw.Close();
        }
    }
}
