using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading;

namespace EastlawUI_v2
{
    public partial class Default : System.Web.UI.Page
    {
        EastLawBL.Users objUsr = new EastLawBL.Users();
        EastLawBL.Common objcom = new EastLawBL.Common();
        Email objemail = new Email();
        IPIntegrationValidation objip = new IPIntegrationValidation();
        string strMessage = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            
           // SendGridAPI.Execute("umar.mughal83@gmail.com", "", "Test email", "").ConfigureAwait(false);
            //SendMail("umar.mughal83@gmail.com");

            //AWSSimpleEmail();

            // bool chk = objip.IsInRange("192.168.2.8", "192.168.0.0/16");
            //            string htm = "";
            //            htm = htm + "<table border='1' cellspacing='0' cellpadding='0'>"
            //   +"<tbody>"
            //        +"<tr style='height: 21.35pt;'>"
            //            +"<td style='height: 21.35pt; width: 125.75pt; padding: 0in; border: 1pt solid black; text-align: left;'>"
            //            +"<p><strong>From</strong></p>"
            //            +"</td>"
            //            +"<td style='height: 21.35pt; width: 167.05pt; padding: 0in; border-top: 1pt solid black; border-right: 1pt solid black; border-bottom: 1pt solid black; border-left: none; text-align: left;'>"
            //            +"<p><strong><span style='letter-spacing: -0.2pt;'>To</span></strong></p>"
            //            +"</td>"
            //        +"</tr>"
            //        +"<tr style='height: 21.15pt;'>"
            //            +"<td style='height: 21.15pt; width: 125.75pt; padding: 0in; border-top: none; border-right: 1pt solid black; border-bottom: 1pt solid black; border-left: 1pt solid black; text-align: left;'>"
            //            +"<p><span style='letter-spacing: -0.6pt;'>1-8</span></p>"
            //            +"</td>"
            //            +"<td style='height: 21.15pt; width: 167.05pt; padding: 0in; border-top: none; border-right: 1pt solid black; border-bottom: 1pt solid black; border-left: none; text-align: left;'>"
            //            +"<p><span style='letter-spacing: -0.35pt;'>1-17</span></p>"
            //            +"</td>"
            //        +"</tr>"
            //    +"</tbody>"
            //+"</table>";
            //            PDFGenerator.GeneratePDF(htm, "test");
            try
            {
                if (!Page.IsPostBack)
                {

                     ValidateRegion();
                    if (Request.QueryString["bG9nb3V0"] != null)
                    {
                        if (Request.QueryString["bG9nb3V0"].ToString() == "do")
                        {
                            if (Session["MemberID"] != null)
                                InsertAuditLog("Login/Logout", "Logout - " + " Member ID:" + Session["MemberID"].ToString(), "");

                            Logout();
                            //Session.RemoveAll();
                        }
                    }
                    #region IPIntegration Check
                    string visitorIPAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    if (visitorIPAddress == "" || visitorIPAddress == null)
                        visitorIPAddress = Request.ServerVariables["REMOTE_ADDR"];

                    string[] iplst = visitorIPAddress.Split(',');
                    if (iplst.Length > 0)
                    {
                        foreach (string ip in iplst)
                        {
                            int checkipuser = objip.CheckIPPool(ip.Trim());
                            if (checkipuser != 0)
                            {
                                UserLoginIntegration(checkipuser);
                            }
                        }
                    }
                    else
                    {
                        if (String.IsNullOrEmpty(visitorIPAddress))
                            visitorIPAddress = Request.ServerVariables["REMOTE_ADDR"];
                        if (string.IsNullOrEmpty(visitorIPAddress))
                            visitorIPAddress = Request.UserHostAddress;

                        int checkipuser = objip.CheckIPPool(visitorIPAddress);
                        if (checkipuser != 0)
                        {
                            UserLoginIntegration(checkipuser);
                        }
                    }


                    #endregion

                    InsertAuditLog("Hit", "General", "");
                    CheckUserLogin();
                    GetOrgTypes();
                    GetCitiesByCoutry("PAK");

                    //string visitorIPAddress1 = Request.ServerVariables["HTTPS_X_FORWARDED_FOR"];
                    //int chk1 = objUsr.InsertAuditLog("Hit", "Traffic Source", Request.Url.AbsoluteUri.ToString(), visitorIPAddress1, 0, "", "", "", "", "", "", "Desktop Website");

                }
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("Home/Default.aspx", "Page_Load", ex.Message);
            }
        }
        public static void SendMail(string ToEmail)
        {
            try
            {
                SmtpClient smtpClinet = new SmtpClient();
                MailMessage msg = new MailMessage();
                msg.Body = "This is umar";
                msg.To.Add(new MailAddress(ToEmail));
                msg.From = new MailAddress("notification@eastlaw.pk", "Eastlaw.pk");
                msg.Subject = "THis is test";
                msg.IsBodyHtml = true;
                //if (AttachmentFilePath != "")
                //    msg.Attachments.Add(new System.Net.Mail.Attachment(AttachmentFilePath));

                SmtpClient client = new SmtpClient();
                client.Host = "eastlaw.awsapps.com";// ConfigurationSettings.AppSettings["smtp"].ToString();
                client.Credentials = new System.Net.NetworkCredential("notification@eastlaw.pk", "Gd234^34&2");


                client.Send(msg);
                msg.Dispose();


            }
            catch (Exception ex)
            {
               // Email.WriteLogs("Email Sending Failed Error Message: " + ex.Message, "Email Sending Failed");
            }
        }
        public int AWSSimpleEmail()
        {
            // Replace sender@example.com with your "From" address. 
            // This address must be verified with Amazon SES.
            String FROM = "notification@eastlaw.pk";
            String FROMNAME = "Eastlaw.pk - Notification";

            // Replace recipient@example.com with a "To" address. If your account 
            // is still in the sandbox, this address must be verified.
            String TO = "muhammad.abubakar@live.com";

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
            String SUBJECT =
                "Amazon SES test (SMTP interface accessed using C#)";

            // The body of the email
            String BODY =
                "<h1>Amazon SES Test</h1>" +
                "<p>This email was sent through the " +
                "<a href='https://aws.amazon.com/ses'>Amazon SES</a> SMTP interface " +
                "using the .NET System.Net.Mail library.</p>";

            // Create and build a new MailMessage object
            MailMessage message = new MailMessage();
            message.IsBodyHtml = true;
            message.From = new MailAddress(FROM, FROMNAME);
            message.To.Add(new MailAddress(TO));
            message.Subject = SUBJECT;
            message.Body = BODY;
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
                    // Console.WriteLine("The email was not sent.");
                    //  Console.WriteLine("Error message: " + ex.Message);
                }
            }
            return 1;
        }
        void ValidateRegion()
        {
            try
            {

                string Country = "";
                string Region = "";
                string City = "";

                string visitorIPAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (String.IsNullOrEmpty(visitorIPAddress))
                    visitorIPAddress = Request.ServerVariables["REMOTE_ADDR"];
                if (string.IsNullOrEmpty(visitorIPAddress))
                    visitorIPAddress = Request.UserHostAddress;

                int chk = 0;
                EastlawUI_v2.CommonClass.GetIPLocation(visitorIPAddress, ref Country, ref Region, ref City);
                //string APIKey = "76511e33ff8498c62f458bea0a641b144b031bdb1e3eade661df53a39815cb27";
                //string url = string.Format("http://api.ipinfodb.com/v3/ip-city/?key={0}&ip={1}&format=json", APIKey, visitorIPAddress);
                //Location location = new Location();
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
                if (!String.IsNullOrEmpty(Country))
                {
                    //if (Country == "United States" || Country == "Germany" || Country == "Netherlands" || Country == "Sweden" || Country == "Italy" || Country == "Switzerland" || Country == "Canada" || Country == "Turkey")
                    if (Country != "Pakistan" & Country != "United Arab Emirates" & Country != "Canada")
                    {
                        InsertAuditLog("Restricted Region", "Restricted Region Request", "Requested Country:" + Country);
                        Response.Redirect("/restricted/restricted-region");
                    }
                }
                //else
                //{
                //    InsertAuditLog("Restricted Region", "Restricted Region Request", "Requested Country:" + location.CountryName);
                //    Response.Redirect("/Restricted/Restricted-Region");
                //}
            }
            catch { }
        }
        protected void Logout()
        {
            //Session.Remove("MemberID");
            // Session.Abandon();
            Session.RemoveAll();
        }
        void GetNews()
        {
            try
            {
                DataTable dtnew = new DataTable();
                System.Data.DataTable dt = new System.Data.DataTable();
                EastLawBL.News objn = new EastLawBL.News();
                dt = objn.GetActiveNews();
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        dt.Columns.Add("imgURL");
                        for (int a = 0; a < dt.Rows.Count; a++)
                        {
                            string imgfilename = "";
                            if (dt.Rows[a]["ImageType"].ToString() == "Local")
                                dt.Rows[a]["imgURL"] = "/store/news/imgs/" + dt.Rows[a]["imgfile"].ToString();
                            else if (dt.Rows[a]["ImageType"].ToString() == "URL")
                                dt.Rows[a]["imgURL"] = "";
                            else
                                dt.Rows[a]["imgURL"] = dt.Rows[a]["imgfile"].ToString();
                        }
                        dt.AcceptChanges();

                        dtnew = dt.AsEnumerable().Take(10).CopyToDataTable();
                        RadRotator1.DataSource = dtnew;
                        RadRotator1.DataBind();

                    }
                }
               

                
            }
            catch { }
        }
        protected void TimerTick(object sender, EventArgs e)
        {
            //this.GetNews();
         
            //Timer1.Enabled = false;
            //imgLoader.Visible = false;
         
        }
        void CheckUserLogin()
        {
            try
            {
                if (Session["MemberID"] != null)
                {
                    loginWelcome.Style["Display"] = "";
                    loginContainer.Style["Display"] = "none";
                    lblUserName.Text = Session["MemberName"].ToString();
                    Response.Redirect("/member/member-dashboard");
                    //spanLoginName.Style["Display"] = "";
                }
                else
                {
                    loginWelcome.Style["Display"] = "none";
                    loginContainer.Style["Display"] = "";
                    //spanLoginName.Style["Display"] = "none";

                }
            }
            catch (Exception ex)
            { }
        }
        void GetOrgTypes()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objUsr.GetActiveOrgTypes();
                //ddlOrgType.DataValueField = "ID";
                //ddlOrgType.DataTextField = "OrgTypes";
                //ddlOrgType.DataSource = dt;
                //ddlOrgType.DataBind();

                //ddlOrgType.Items.Insert(0, new ListItem("Select", "0"));
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("Home/Default.aspx", "GetOrgTypes", ex.Message);
            }
        }
        void GetCountries()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objcom.GetCountries();
                //ddlCountry.DataValueField = "Code";
                //ddlCountry.DataTextField = "Name";
                //ddlCountry.DataSource = dt;
                //ddlCountry.DataBind();

                //ddlCountry.Items.Insert(0, new ListItem("Select", "0"));
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("Home/Default.aspx", "GetCountries", ex.Message);
            }
        }
        void GetCitiesByCoutry(string CountryCode)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objcom.GetCitiesByCountry(CountryCode);
                //ddlCity.DataValueField = "ID";
                //ddlCity.DataTextField = "Name";
                //ddlCity.DataSource = dt;
                //ddlCity.DataBind();

                //ddlCity.Items.Insert(0, new ListItem("Select", "0"));
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("Home/Registration.aspx", "GetCountries", ex.Message);
            }
        }
        void InsertAuditLog(string ActType, string Action, string txt)
        {
            try
            {
                string Country = "";
                string Region = "";
                string City = "";
                string visitorIPAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                string visitorIPAddress1 = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (String.IsNullOrEmpty(visitorIPAddress))
                    visitorIPAddress = Request.ServerVariables["REMOTE_ADDR"];
                if (string.IsNullOrEmpty(visitorIPAddress))
                    visitorIPAddress = Request.UserHostAddress;

                EastlawUI_v2.CommonClass.GetIPLocation(visitorIPAddress, ref Country, ref Region, ref City);
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
                    chk = objUsr.InsertAuditLog(ActType, Action, Request.Url.AbsoluteUri.ToString(), visitorIPAddress, int.Parse(Session["MemberID"].ToString()), Country, Region, City, txt, BrowserName, SourcePlatform, "Desktop Website");
                else
                    chk = objUsr.InsertAuditLog(ActType, Action, Request.Url.AbsoluteUri.ToString(), visitorIPAddress, 0, Country, Region, City, txt, BrowserName, SourcePlatform, "Desktop Website");
            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("home/Default.aspx", "InsertAuditLog", e.Message);
            }
        }
        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearch.Text))
            {
                lblMsg.Visible = true;
            }
            else
            {
                lblMsg.Visible = false;
                Response.Redirect("/search/" + CommonClass.RemoveSomeCharacters(txtSearch.Text.Trim()));
            }
        }
        #region Login
        void UserLogin(string EmailID, string Pwd)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objUsr.CheckLogin(EmailID, EncryptDecryptHelper.Encrypt(Pwd));
               
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {


                        if (chkRem.Checked)
                        {
                            Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(30);
                            Response.Cookies["Password"].Expires = DateTime.Now.AddDays(30);
                        }
                        else
                        {
                            Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(-1);
                            Response.Cookies["Password"].Expires = DateTime.Now.AddDays(-1);

                        }
                        Response.Cookies["UserName"].Value = EmailID;
                        Response.Cookies["Password"].Value = Pwd;

                        if (dt.Rows[0]["verify"].ToString() == "0")
                        {
                            InsertAuditLog("Login/Logout", "Login Failed, Account is not verified." + EmailID, "");
                            lblMsg.Text = "EmailID/Account is not verified, kindly check your email.";
                            lblMsg.Visible = true;

                            strMessage = "Account is not Activated, Please check email for activation or Call Helpline# 03-111-116-670";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup('" + strMessage + "');", true);
                            return;
                        }
                        else if (dt.Rows[0]["Status"].ToString() == "Expired")
                        {
                            InsertAuditLog("Login/Logout", "Login Failed, Account Locked with status of " + dt.Rows[0]["Status"].ToString() + " Email ID:" + EmailID, "");
                            lblMsg.Text = "EmailID/Account is locked, kindly contact administrator";
                            lblMsg.Visible = true;

                            strMessage = "Account is Expired, Please check email for activation or Call Helpline# 03-111-116-670";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup('" + strMessage + "');", true);

                            Session["ExpiredMemberID"] = dt.Rows[0]["ID"].ToString();
                            Response.Redirect("/member/subscription");
                            return;
                        }
                        else if (dt.Rows[0]["Status"].ToString() != "Approved")
                        {
                            InsertAuditLog("Login/Logout", "Login Failed, Account Locked with status of " + dt.Rows[0]["Status"].ToString() + " Email ID:" + EmailID, "");
                            lblMsg.Text = "EmailID/Account is locked, kindly contact administrator";
                            lblMsg.Visible = true;
                            strMessage = "Account is not Activated, Please check email for activation or Call Helpline# 03-111-116-670";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup('" + strMessage + "');", true);
                            return;
                        }
                        //else if (DateTime.Parse(dt.Rows[0]["AccessExpireOn"].ToString("dd/mm/yyyy")) < DateTime.Parse(DateTime.Now.Date.ToString("dd/mm/yyyy")))
                        else if (DateTime.Parse(dt.Rows[0]["FormatedExpire"].ToString()) < DateTime.Now.Date)
                        {
                            InsertAuditLog("Login/Logout", "Login Failed, Account Expired on  " + dt.Rows[0]["AccessExpireOn"].ToString() + " Email ID:" + EmailID, "");
                            lblMsg.Text = "Account Expired, kindly contact administrator";
                            lblMsg.Visible = true;

                            strMessage = "Account Expired, Please email us or Call Helpline# 03-111-116-670";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup('" + strMessage + "');", true);

                            Session["ExpiredMemberID"] = dt.Rows[0]["ID"].ToString();
                            Response.Redirect("/member/subscription");
                            //return;
                        }
                        else
                        {
                            //if (Login(txtEmailIDLogin.Text.Trim()))
                            //{
                            CheckUserAccessValidation(int.Parse(dt.Rows[0]["ID"].ToString()), dt.Rows[0]["EmailID"].ToString());

                            if (CheckUserAccessTimeValidation(int.Parse(dt.Rows[0]["PlanID"].ToString()), dt.Rows[0]["EmailID"].ToString()))
                            {

                                if (dt.Rows[0]["UserTypeID"].ToString() == "4")
                                {
                                    Session["CompanyAdminID"] = dt.Rows[0]["ID"].ToString();
                                    Session["CompanyAdminUserType"] = dt.Rows[0]["UserTypeID"].ToString();
                                    Session["CompanyAdminName"] = dt.Rows[0]["FullName"].ToString();
                                    Session["CompanyID"] = dt.Rows[0]["CompanyID"].ToString();

                                    lblMsg.Text = "";
                                    lblMsg.Visible = false;
                                    InsertAuditLog("Login/Logout", "Login Success " + EmailID, "");

                                    Response.Redirect("/corporate/dashboard");
                                }
                                else
                                {
                                    Session["MemberID"] = dt.Rows[0]["ID"].ToString();
                                    Session["MemberUserType"] = dt.Rows[0]["UserTypeID"].ToString();
                                    Session["MemberPlanID"] = dt.Rows[0]["PlanID"].ToString();
                                    Session["MemberName"] = dt.Rows[0]["FullName"].ToString();
                                    lblMsg.Text = "";
                                    lblMsg.Visible = false;
                                    InsertAuditLog("Login/Logout", "Login Success " + EmailID, "");

                                    HttpContext.Current.Application["usr_" + Session["MemberID"].ToString()] = HttpContext.Current.Session.SessionID;
                                    Session["UserLogged"] = "usr_" + Session["MemberID"].ToString();

                                    if (Request.QueryString["req"] != null)
                                    {

                                        Response.Redirect(Request.QueryString["req"].ToString());
                                    }
                                    else
                                    {

                                        Response.Redirect("/member/member-dashboard");
                                    }
                                }

                            }
                            else
                            {
                                strMessage = "Sign-in limited for complimentary package users. Please Sign-in after 7:00 am";
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup('" + strMessage + "');", true);

                                lblMsg.Text = "Sign-in limited for complimentary package users. Please Sign-in after 7:00 am";
                                lblMsg.Visible = true;
                         

                            }
                            //if (Request.UrlReferrer != null)
                            //{

                            //    if (ViewState["LastPage"] != null)
                            //    {
                            //        Uri uri = new Uri(ViewState["LastPage"].ToString());
                            //        string filename = System.IO.Path.GetFileName(uri.AbsolutePath);
                            //        if (filename == "Member-Activation")
                            //            Response.Redirect("/Member/Member-Dashboard");
                            //        else
                            //            Response.Redirect(ViewState["LastPage"].ToString());
                            //    }
                            //    else
                            //       Response.Redirect("/Member/Member-Dashboard");

                            //}
                            //else
                            //{
                            //    Response.Redirect("/Member/Member-Dashboard");
                            //}
                            //        }
                            //        else
                            //{
                            //    InsertAuditLog("Login/Logout", "Multi Access Try " + txtEmailIDLogin.Text, "This User is already login, kindly logout first");
                            //    lblMsg.Text = "This User is already login, kindly logout first";
                            //    lblMsg.Visible = true;
                            //    return;
                            //}
                        }


                    }
                    else
                    {
                        InsertAuditLog("Login/Logout", "Login Failed " + EmailID, "");
                        lblMsg.Text = "Email ID or Password is incorrect.";
                        lblMsg.Visible = true;
                        strMessage = "Email ID or Password is incorrect.";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup('" + strMessage + "');", true);
                    }
                }
                else
                {
                    InsertAuditLog("Login/Logout", "Login Failed" + EmailID, "");
                    lblMsg.Text = "Email ID or Password is incorrect.";
                    lblMsg.Visible = true;
                    strMessage = "Email ID or Password is incorrect.";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup('" + strMessage + "');", true);
                }
                uppnl.Update();
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("Home/Register.aspx", "UserLogin", ex.Message);
            }
        }
        void UserLoginIntegration(int UserID)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objUsr.CheckLogin_IPIntegration(UserID);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {

                        if (dt.Rows[0]["verify"].ToString() == "0")
                        {
                            InsertAuditLog("Login/Logout", "Login Failed, Account is not verified." + dt.Rows[0]["EmailID"].ToString(), "");
                            lblMsg.Text = "EmailID/Account is not verified, kindly check your email.";
                            lblMsg.Visible = true;

                            strMessage = "Account is not Activated, Please check email for activation or Call Helpline# 03-111-116-670";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup('" + strMessage + "');", true);
                            return;
                        }
                        else if (dt.Rows[0]["Status"].ToString() == "Expired")
                        {
                            InsertAuditLog("Login/Logout", "Login Failed, Account Locked with status of " + dt.Rows[0]["Status"].ToString() + " Email ID:" + dt.Rows[0]["EmailID"].ToString(), "");
                            lblMsg.Text = "EmailID/Account is locked, kindly contact administrator";
                            lblMsg.Visible = true;

                            strMessage = "Account is Expired, Please check email for activation or Call Helpline# 03-111-116-670";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup('" + strMessage + "');", true);

                            Session["ExpiredMemberID"] = dt.Rows[0]["ID"].ToString();
                            Response.Redirect("/member/subscription");
                            return;
                        }
                        else if (dt.Rows[0]["Status"].ToString() != "Approved")
                        {
                            InsertAuditLog("Login/Logout", "Login Failed, Account Locked with status of " + dt.Rows[0]["Status"].ToString() + " Email ID:" + dt.Rows[0]["EmailID"].ToString(), "");
                            lblMsg.Text = "EmailID/Account is locked, kindly contact administrator";
                            lblMsg.Visible = true;
                            strMessage = "Account is not Activated, Please check email for activation or Call Helpline# 03-111-116-670";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup('" + strMessage + "');", true);
                            return;
                        }
                        //else if (DateTime.Parse(dt.Rows[0]["AccessExpireOn"].ToString("dd/mm/yyyy")) < DateTime.Parse(DateTime.Now.Date.ToString("dd/mm/yyyy")))
                        else if (DateTime.Parse(dt.Rows[0]["FormatedExpire"].ToString()) < DateTime.Now.Date)
                        {
                            InsertAuditLog("Login/Logout", "Login Failed, Account Expired on  " + dt.Rows[0]["AccessExpireOn"].ToString() + " Email ID:" + dt.Rows[0]["EmailID"].ToString(), "");
                            lblMsg.Text = "Account Expired, kindly contact administrator";
                            lblMsg.Visible = true;

                            strMessage = "Account Expired, Please email us or Call Helpline# 03-111-116-670";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup('" + strMessage + "');", true);

                            Session["ExpiredMemberID"] = dt.Rows[0]["ID"].ToString();
                            Response.Redirect("/member/subscription");
                            //return;
                        }
                        else
                        {
                            //if (Login(txtEmailIDLogin.Text.Trim()))
                            //{
                            CheckUserAccessValidation(int.Parse(dt.Rows[0]["ID"].ToString()), dt.Rows[0]["EmailID"].ToString());

                            if (CheckUserAccessTimeValidation(int.Parse(dt.Rows[0]["PlanID"].ToString()), dt.Rows[0]["EmailID"].ToString()))
                            {
                                
                                    Session["MemberID"] = dt.Rows[0]["ID"].ToString();
                                    Session["MemberUserType"] = dt.Rows[0]["UserTypeID"].ToString();
                                Session["MemberPlanID"] = dt.Rows[0]["PlanID"].ToString();
                                Session["MemberName"] = dt.Rows[0]["FullName"].ToString();
                                    lblMsg.Text = "";
                                    lblMsg.Visible = false;
                                    InsertAuditLog("Login/Logout", "Login Success " + dt.Rows[0]["EmailID"].ToString(), "");

                                    HttpContext.Current.Application["usr_" + Session["MemberID"].ToString()] = HttpContext.Current.Session.SessionID;
                                    Session["UserLogged"] = "usr_" + Session["MemberID"].ToString();

                                    if (Request.QueryString["req"] != null)
                                    {

                                        Response.Redirect(Request.QueryString["req"].ToString());
                                    }
                                    else
                                    {

                                        Response.Redirect("/member/member-dashboard");
                                    }
                                

                            }
                            else
                            {
                                strMessage = "Sign-in limited for complimentary package users. Please Sign-in after 7:00 am";
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup('" + strMessage + "');", true);

                                lblMsg.Text = "Sign-in limited for complimentary package users. Please Sign-in after 7:00 am";
                                lblMsg.Visible = true;


                            }
                            //if (Request.UrlReferrer != null)
                            //{

                            //    if (ViewState["LastPage"] != null)
                            //    {
                            //        Uri uri = new Uri(ViewState["LastPage"].ToString());
                            //        string filename = System.IO.Path.GetFileName(uri.AbsolutePath);
                            //        if (filename == "Member-Activation")
                            //            Response.Redirect("/Member/Member-Dashboard");
                            //        else
                            //            Response.Redirect(ViewState["LastPage"].ToString());
                            //    }
                            //    else
                            //       Response.Redirect("/Member/Member-Dashboard");

                            //}
                            //else
                            //{
                            //    Response.Redirect("/Member/Member-Dashboard");
                            //}
                            //        }
                            //        else
                            //{
                            //    InsertAuditLog("Login/Logout", "Multi Access Try " + txtEmailIDLogin.Text, "This User is already login, kindly logout first");
                            //    lblMsg.Text = "This User is already login, kindly logout first";
                            //    lblMsg.Visible = true;
                            //    return;
                            //}
                        }


                    }
                    else
                    {
                        InsertAuditLog("Login/Logout", "Login Failed " + dt.Rows[0]["EmailID"].ToString(), "");
                        lblMsg.Text = "Email ID or Password is incorrect.";
                        lblMsg.Visible = true;

                        strMessage = "Email ID or Password is incorrect.";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup('" + strMessage + "');", true);
                    }

                }
                else
                {
                    InsertAuditLog("Login/Logout", "Login Failed" + dt.Rows[0]["EmailID"].ToString(), "");
                    lblMsg.Text = "Email ID or Password is incorrect.";
                    lblMsg.Visible = true;
                    strMessage = "Email ID or Password is incorrect.";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup('" + strMessage + "');", true);
                }
                uppnl.Update();
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("Home/Register.aspx", "UserLogin", ex.Message);
            }
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkTNC.Checked == false)
                {
                    lblMsg.Text = "Please Accept Terms & Condition";
                    lblMsg.Visible = true;
                    return;
                }
                else
                {
                    // if (Login(txtEmailIDLogin.Text.Trim()))
                    UserLogin(txtEmailIDLogin.Text.Trim(), txtPasswordLogin.Text.Trim());
                    //else
                    //{
                    //    InsertAuditLog("Login/Logout", "Multi Access Try " + txtEmailIDLogin.Text, "This User is already login, kindly logout first");
                    //    lblMsg.Text = "This User is already login, kindly logout first";
                    //    lblMsg.Visible = true;
                    //    return;
                    //}
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("Home/Register.aspx", "btnLogin_Click", ex.Message);
            }
        }
        protected void txtPasswordLogin_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkTNC.Checked == false)
                {
                    lblMsg.Text = "Please Accept Terms & Condition";
                    lblMsg.Visible = true;
                    return;
                }
                else
                {
                    // if (Login(txtEmailIDLogin.Text.Trim()))
                    UserLogin(txtEmailIDLogin.Text.Trim(), txtPasswordLogin.Text.Trim());
                    //else
                    //{
                    //    InsertAuditLog("Login/Logout", "Multi Access Try " + txtEmailIDLogin.Text, "This User is already login, kindly logout first");
                    //    lblMsg.Text = "This User is already login, kindly logout first";
                    //    lblMsg.Visible = true;
                    //    return;
                    //}
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("Home/Register.aspx", "txtPasswordLogin_TextChanged", ex.Message);
            }
        }
        void CheckUserAccessValidation(int UserID, string EmailID)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objUsr.UserElementViewReport((DateTime.Now.Date.ToString("MM/dd/yyy")), DateTime.Now.Date.ToString("MM/dd/yyy"), UserID);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        if (!String.IsNullOrEmpty(dt.Rows[0]["nooflogin_perday"].ToString()))
                        {
                            if (dt.Rows[0]["nooflogin_perday"].ToString() != "0")
                            {
                                if (int.Parse(dt.Rows[0]["NoOfLogin"].ToString()) >= int.Parse(dt.Rows[0]["nooflogin_perday"].ToString()))
                                {
                                    InsertAuditLog("Limit Exceeded", "Login", "Email ID: " + EmailID);
                                    Response.Redirect("/restricted/limit-Exceeded");
                                    return;

                                }
                            }
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("Home/Registration.aspx", "CheckUserAccessValidation", ex.Message);
            }
        }
        bool CheckUserAccessTimeValidation(int PlanID, string EmailID)
        {
            try
            {
                if (PlanID == 1 || PlanID == 18 || PlanID == 8 || PlanID == 25 || PlanID == 27 || PlanID == 21)
                {
                    TimeSpan start = new TimeSpan(00, 30, 0); //10 o'clock
                    TimeSpan end = new TimeSpan(07, 0, 0); //12 o'clock
                    TimeSpan now = DateTime.Now.TimeOfDay;

                    if ((now > start) && (now < end))
                    {
                        InsertAuditLog("Non Working Hours", "Login", "Email ID: " + EmailID);
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }

            }
            catch (Exception ex)
            {
                return false;
                ExceptionHandling.SendErrorReport("Home/Registration.aspx", "CheckUserAccessValidation", ex.Message);
            }
        }
        #endregion

        protected void btnSubscribe_Click(object sender, EventArgs e)
        {
            try
            {
                EastLawBL.Users objusr = new EastLawBL.Users();
                int chk = objusr.AddNewsletterEmailID(txtNewslettersubscribeemail.Text.Trim());
                if (chk > 0)
                {
                    lblSubscribeMsg.Visible = true;
                }
            }
            catch { }
        }

        protected void btnLogin0_Click(object sender, EventArgs e)
        {
            strMessage = "Email ID or Password is incorrect.";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup('" + strMessage + "');", true);
        }
    }
}