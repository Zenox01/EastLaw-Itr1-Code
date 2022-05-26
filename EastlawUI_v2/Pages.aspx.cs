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
    public partial class Pages : System.Web.UI.Page
    {
        EastLawBL.Users objUsr = new EastLawBL.Users();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                InsertAuditLog("Hit", "General", "");
                EnableValidators();
            }

        }
        void EnableValidators()
        {
            if (HttpContext.Current.Items["pageid"] != null)
            {
                if (HttpContext.Current.Items["pageid"].ToString() == "7")
                {
                    rfvContactName.Enabled = true;
                    rfvContactEmail.Enabled = true;
                    rfvContactMobile.Enabled = true;
                    rfvContactMsg.Enabled = true;
                    reContactEmail.Enabled = true;
                    reContactPhone.Enabled = true;
                }
                else if (HttpContext.Current.Items["pageid"].ToString() == "2")
                {
                    rfvFeedbackName.Enabled = true;
                    rfvFeedbackMobile.Enabled = true;
                    rfvFeedbackEmail.Enabled = true;
                    reFeedbackEmailID.Enabled = true;
                    reFeedbackMobile.Enabled = true;

                    rfvDescribeU.Enabled = true;
                    rfvPreviewNewSearch.Enabled = true;
                    rfvCurrentlyPay.Enabled = true;
                    rfvSiteFeedbackText.Enabled = true;




                }
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

        protected void btnSend_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            string emailcnt = EmailContentSiteFeedback();
            Email.SendMail("info@eastlaw.pk", emailcnt, "Site Feedback - EastLaw", "EastLaw", "");
            Email.SendMail("support@eastlaw.pk", emailcnt, "Site Feedback - EastLaw", "EastLaw", "");
            Email.SendMail("muhammad.abubakar@live.com", emailcnt, "Site Feedback - EastLaw", "EastLaw", "");
            divForm.Style["Display"] = "none";
            divThank.Style["Display"] = "";
        }
        string EmailContentSiteFeedback()
        {
            try
            {
                DataTable dt = new DataTable();
                EastLawBL.Pages objp = new EastLawBL.Pages();
                dt = objp.GetPages(int.Parse(HttpContext.Current.Items["pageid"].ToString()));


                string file = "";


                string html = "";

                file = System.Web.HttpContext.Current.Server.MapPath("/EmailTemplates/InquiryEmail.html");
                StreamReader sr = new StreamReader(file);
                FileInfo fi = new FileInfo(file);

                if (System.IO.File.Exists(file))
                {
                    html += sr.ReadToEnd();
                    sr.Close();
                }


                html = html.Replace("##INQTY##", dt.Rows[0]["Title"].ToString());
                html = html.Replace("##NM##", txtName.Text.Trim());
                html = html.Replace("##EMLID##", txtEmailID.Text.Trim());
                html = html.Replace("##MBL##", txtMobile.Text.Trim());
                string DesU = "";
                for (int a = 0; a < radioBestDescribe.Items.Count; a++)
                {
                    if (radioBestDescribe.Items[a].Selected == true)
                    {
                        DesU = DesU + radioBestDescribe.Items[a].Value + ",";
                    }
                }
                html = html.Replace("##DesU##", DesU);

                string OffLau = "";
                for (int a = 0; a < radioIntrestedOfficialyLaunched.Items.Count; a++)
                {
                    if (radioIntrestedOfficialyLaunched.Items[a].Selected == true)
                    {
                        OffLau = OffLau + radioIntrestedOfficialyLaunched.Items[a].Value;
                    }
                }
                html = html.Replace("##Offlau##", OffLau);


                html = html.Replace("##CSETXT##", txtExpirence.Text.Trim());

                string CSETXTEXP = "";
                for (int a = 0; a < radioCasetext.Items.Count; a++)
                {
                    if (radioCasetext.Items[a].Selected == true)
                    {
                        CSETXTEXP = CSETXTEXP + radioCasetext.Items[a].Value;
                    }
                }
                html = html.Replace("##CSETXTEXP##", CSETXTEXP);
                string CurPay = "";
                for (int a = 0; a < chkLstCurrentlypay.Items.Count; a++)
                {
                    if (chkLstCurrentlypay.Items[a].Selected == true)
                    {
                        CurPay = CurPay + chkLstCurrentlypay.Items[a].Value + ", ";
                    }
                }
                html = html.Replace("##CURPAY##", CurPay);
                html = html.Replace("##MSG##", txtMsg.Text.Trim());
                // html = html.Replace("##Pwd##", Session["Pwd"].ToString());


                return html;
            }
            catch
            {
                return "";
            }



        }
        string EmailContent()
        {
            try
            {
                DataTable dt = new DataTable();
                EastLawBL.Pages objp = new EastLawBL.Pages();
                dt = objp.GetPages(int.Parse(HttpContext.Current.Items["pageid"].ToString()));


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
            string emailcnt = EmailContent();
            Email.SendMail("info@eastlaw.pk", emailcnt, "Contact Us - Eastlaw", "Eastlaw", "");
            Email.SendMail("muhammad.abubakar@live.com", emailcnt, "Contact Us  - Eastlaw", "Eastlaw", "");
            divFormC.Style["Display"] = "none";
            divThankC.Style["Display"] = "";
        }
    }
}