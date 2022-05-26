using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using System.Configuration;
using System.Net;

namespace UsersUtility
{
   public class UsersActions
    {
       EastLawBL.Users objusr = new EastLawBL.Users();
      public void SendPreExpiryNotificationToUser()
       {
           try
           {
               DataTable dt = new DataTable();
               dt = objusr.GetUserPreExpiryList();
               if(dt != null)
               {
                   if(dt.Rows.Count > 0)
                   {
                       for (int a = 0; a < dt.Rows.Count; a++)
                       {
                           DateTime dtExpiryOn = DateTime.Parse(dt.Rows[a]["FormatedExpire"].ToString());
                           double NoOfdays = (dtExpiryOn - DateTime.Now).TotalDays;
                           int roundNoofDays = (int)NoOfdays;
                           //if(NoOfdays == 3 || NoOfdays > 1)
                           if (NoOfdays >= 1 && NoOfdays <= 7)
                           {
                               DataTable dtPreExpNotiSent = objusr.CheckPreExpiryNotificationSent(int.Parse(dt.Rows[a]["ID"].ToString()));
                               if (dtPreExpNotiSent != null)
                               {
                                   if (dtPreExpNotiSent.Rows.Count == 0)
                                   {
                                       string emailcontent = PreExpiryNotificationEmailContent(int.Parse(dt.Rows[a]["ID"].ToString()), dt.Rows[a]["FullName"].ToString(), dt.Rows[a]["EmailID"].ToString(), String.Format("{0:dddd, MMMM d, yyyy}", dtExpiryOn), roundNoofDays.ToString());
                                       objusr.AddUserNotificationLog(int.Parse(dt.Rows[a]["ID"].ToString()), "Pre Expiry Notification", emailcontent);
                                       Email.SendMail(dt.Rows[a]["EmailID"].ToString(), emailcontent, "Account Expiry Notification", "Eastlaw - Notification", "");
                                   }
                               }
                           }
                       }
                   }
               }
           }
           catch { }
       }

      public void SendExpiredNotificationToUser()
      {
          try
          {
              DataTable dt = new DataTable();
              dt = objusr.GetUserPreExpiryList();
              if (dt != null)
              {
                  if (dt.Rows.Count > 0)
                  {
                      for (int a = 0; a < dt.Rows.Count; a++)
                      {
                          DateTime dtExpiryOn = DateTime.Parse(dt.Rows[a]["FormatedExpire"].ToString());
                          double NoOfdays = (dtExpiryOn.Date - DateTime.Now.Date).TotalDays;
                          //if(NoOfdays == 3 || NoOfdays > 1)
                          if (NoOfdays < 1)
                          {
                              string emailcontent = ExpiredNotificationEmailContent(int.Parse(dt.Rows[a]["ID"].ToString()), dt.Rows[a]["FullName"].ToString(), dt.Rows[a]["EmailID"].ToString());
                              int chk = objusr.UpdateUserStatus(int.Parse(dt.Rows[a]["ID"].ToString()), "Expired");
                              objusr.AddUserNotificationLog(int.Parse(dt.Rows[a]["ID"].ToString()), "Account Expired Notification", emailcontent);
                              Email.SendMail(dt.Rows[a]["EmailID"].ToString(), emailcontent, "Account Expired ", "Eastlaw - Notification","");
                              Email.SendMail("registration@eastlaw.pk", emailcontent, "Account Expired ", "Eastlaw - Notification","");

                              SendExpireSMS(int.Parse(dt.Rows[a]["ID"].ToString()),dt.Rows[a]["FullName"].ToString(),dt.Rows[a]["PhoneNo"].ToString());
                              
                          }
                      }
                  }
              }
          }
          catch { }
      }

      string PreExpiryNotificationEmailContent(int UserID,string Name, string EmailID,string DOE,string NoofDays)
      {
          try
          {
              string file = "";


              string html = "";

              //file =(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "MailTemplate/PreExpiryNotificationComp.html"));
              file = (Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "MailTemplate/PreExpiryNotification.html"));
              
              StreamReader sr = new StreamReader(file);
              FileInfo fi = new FileInfo(file);

              if (System.IO.File.Exists(file))
              {
                  html += sr.ReadToEnd();
                  sr.Close();
              }


              html = html.Replace("##FullName##", Name);
              html = html.Replace("##COMPPKG##", "http://eastlaw.pk/Member/Complementary-Subscription?nval=" + EncryptDecryptHelper.Encrypt(UserID.ToString()));
              html = html.Replace("##DOE##", DOE);
              html = html.Replace("##DAYSE##", NoofDays);
              //html = html.Replace("##ClickLink##", "<a href='" + ConfigurationSettings.AppSettings["websiteUrl"].ToString() + "Member/Forget-Password?ac=" + EncryptDecryptHelper.Encrypt(EmailID) + "' target='_blank'>Reset your password now </a>");
              //html = html.Replace("##FullLink##", ConfigurationSettings.AppSettings["websiteUrl"].ToString() + "Member/Forget-Password?ac=" + EncryptDecryptHelper.Encrypt(EmailID));
              // html = html.Replace("##Pwd##", Session["Pwd"].ToString());


              return html;
          }
          catch
          {
              return "";
          }



      }
      string ExpiredNotificationEmailContent(int UserID,string Name, string EmailID)
      {
          try
          {
              string file = "";


              string html = "";

              //file = (Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "MailTemplate/AccountExpiredNotificationComp.html"));
              file = (Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "MailTemplate/AccountExpiredNotification.html"));
              
              StreamReader sr = new StreamReader(file);
              FileInfo fi = new FileInfo(file);

              if (System.IO.File.Exists(file))
              {
                  html += sr.ReadToEnd();
                  sr.Close();
              }


              html = html.Replace("##FullName##", Name);
              html = html.Replace("##COMPPKG##", "http://eastlaw.pk/Member/Complementary-Subscription?nval="+EncryptDecryptHelper.Encrypt(UserID.ToString()));
              //html = html.Replace("##ClickLink##", "<a href='" + ConfigurationSettings.AppSettings["websiteUrl"].ToString() + "Member/Forget-Password?ac=" + EncryptDecryptHelper.Encrypt(EmailID) + "' target='_blank'>Reset your password now </a>");
              //html = html.Replace("##FullLink##", ConfigurationSettings.AppSettings["websiteUrl"].ToString() + "Member/Forget-Password?ac=" + EncryptDecryptHelper.Encrypt(EmailID));
              // html = html.Replace("##Pwd##", Session["Pwd"].ToString());


              return html;
          }
          catch
          {
              return "";
          }



      }

      void SendExpireSMS(int UserID,string Name, string MobileNo)
      {
          try
          {
              //string smstxt = "Dear " + Name + ", Thank you for using EastLaw.pk. Kindly note that your"
              //+ "Subscription Package has Expired. Please login to place order for new Subscription Package.";

              string smstxt = "Dear " + Name + ", Thank you for using EastLaw.pk. Kindly note that your"
              + "Subscription Package has Expired. Please login to purchase the most suitable Subscription Package at specially discounted price. Helpline#. 03-111-116-670";

              string mobilenumber = MobileNo;
              //string url = "http://bulksms.com.pk/api/sms.php?username=923214264174&password=5974&sender=eastlaw.pk&mobile=923214264174&message=" + smstxt + "";
              string url = "http://bulksms.com.pk/api/sms.php?username=923228451969&password=1813&sender=eastlaw.pk&mobile=" + mobilenumber + "&message=" + smstxt + "";

              //HTTP connection
              HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(url);
              //Get response from Ozeki NG SMS Gateway Server and read the answer
              HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
              System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
              string responseString = respStreamReader.ReadToEnd();
              respStreamReader.Close();
              myResp.Close();
          }
          catch { }

      }


    }
}
