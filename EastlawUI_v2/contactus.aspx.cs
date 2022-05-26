using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

namespace EastlawUI_v2
{
    public partial class contactus : System.Web.UI.Page
    {
        EastLawBL.Users objUsr = new EastLawBL.Users();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                InsertAuditLog("Hit", "General", "Contact Us");
                
            }
        }
        void InsertAuditLog(string ActType, string Action, string txt)
        {
            try
            {
                string visitorIPAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (String.IsNullOrEmpty(visitorIPAddress))
                    visitorIPAddress = Request.ServerVariables["REMOTE_ADDR"];
                if (string.IsNullOrEmpty(visitorIPAddress))
                    visitorIPAddress = Request.UserHostAddress;

                int chk = 0;
                Location location = new Location();
                //string APIKey = "76511e33ff8498c62f458bea0a641b144b031bdb1e3eade661df53a39815cb27";
                //string url = string.Format("http://api.ipinfodb.com/v3/ip-city/?key={0}&ip={1}&format=json", APIKey, visitorIPAddress);

                //try
                //{
                //    using (System.Net.WebClient client = new System.Net.WebClient())
                //    {
                //        string json = client.DownloadString(url);
                //        location = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<Location>(json);

                //    }
                //}
                //catch
                //{ }

                string BrowserName = "";
                string SourcePlatform = "";
                try
                {
                    System.Web.HttpBrowserCapabilities browser = Request.Browser;
                    BrowserName = browser.Browser.ToString();
                    SourcePlatform = browser.Platform.ToString();
                }
                catch { }

                if (Session["MemberID"] != null)
                    chk = objUsr.InsertAuditLog(ActType, Action, Request.Url.AbsoluteUri.ToString(), visitorIPAddress, int.Parse(Session["MemberID"].ToString()), location.CountryName, location.RegionName, location.CityName, txt, BrowserName, SourcePlatform, "Desktop Website");
                else
                    chk = objUsr.InsertAuditLog(ActType, Action, Request.Url.AbsoluteUri.ToString(), visitorIPAddress, 0, location.CountryName, location.RegionName, location.CityName, txt, BrowserName, SourcePlatform, "Desktop Website");
            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("home/Default.aspx", "InsertAuditLog", e.Message);
            }
        }

        
   
        string EmailContent()
        {
            try
            {
                DataTable dt = new DataTable();
                //EastLawBL.Pages objp = new EastLawBL.Pages();
                //dt = objp.GetPages(int.Parse(HttpContext.Current.Items["pageid"].ToString()));


                string file = "";


                string html = "";

                file = System.Web.HttpContext.Current.Server.MapPath("/EmailTemplates/ContactEmail.html");
                StreamReader sr = new StreamReader(file);
                FileInfo fi = new FileInfo(file);

                if (System.IO.File.Exists(file))
                {
                    html += sr.ReadToEnd();
                    sr.Close();
                }


                html = html.Replace("##INQTY##", "Contact Us");
                html = html.Replace("##NM##", txtNameC.Text.Trim());
                html = html.Replace("##EMLID##", txtEmailIDC.Text.Trim());
                html = html.Replace("##MBL##", txtMobileNoC.Text.Trim());


                html = html.Replace("##MSG##", txtMsgC.Text.Trim());
                // html = html.Replace("##Pwd##", Session["Pwd"].ToString());


                return html;
            }
            catch
            {
                return "";
            }



        }

        protected void btnSendC_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;
            string emailcnt = EmailContent();
            Email.SendMail("info@eastlaw.pk", emailcnt, "Contact Us - Eastlaw", "Eastlaw", "");
            Email.SendMail("muhammad.abubakar@live.com", emailcnt, "Contact Us  - Eastlaw", "Eastlaw", "");
            divFormC.Style["Display"] = "none";
            divThankC.Style["Display"] = "";
        }
    }
}