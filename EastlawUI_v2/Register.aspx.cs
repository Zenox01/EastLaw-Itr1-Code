using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Configuration;
namespace EastlawUI_v2
{
    public partial class Register : System.Web.UI.Page
    {
        EastLawBL.Users objUsr = new EastLawBL.Users();
        EastLawBL.Common objcom = new EastLawBL.Common();
        string strMessage = "";
        IPIntegrationValidation objip = new IPIntegrationValidation();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    InsertAuditLog("Hit", "Login/Register", "");
                    // string urlName = Request.UrlReferrer.ToString();
                    ViewState["LastPage"] = Request.UrlReferrer.ToString();

                    if (Request.Cookies["UserName"] != null && Request.Cookies["Password"] != null)
                    {
                        txtEmailIDLogin.Text = Request.Cookies["UserName"].Value;
                        txtPasswordLogin.Attributes["value"] = Request.Cookies["Password"].Value;
                        chkRem.Checked = true;
                    }

                    #region IPIntegration Check
                    string visitorIPAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    if (String.IsNullOrEmpty(visitorIPAddress))
                        visitorIPAddress = Request.ServerVariables["REMOTE_ADDR"];
                    if (string.IsNullOrEmpty(visitorIPAddress))
                        visitorIPAddress = Request.UserHostAddress;

                    int checkipuser = objip.CheckIPPool(visitorIPAddress);
                    if (checkipuser != 0)
                    {
                        UserLoginIntegration(checkipuser);
                    }
                    #endregion
                    //GetOrgTypes();
                    //GetCountries();
                    //GetCitiesByCoutry(ddlCountry.SelectedValue);
                    //Response.Write(EncryptDecryptHelper.Encrypt("logout"));

                }
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("Home/Registration.aspx", "Page_Load", ex.Message);
            }
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
                ExceptionHandling.SendErrorReport("Home/Registration.aspx", "GetOrgTypes", ex.Message);
            }
        }
        void GetCountries()
        {
            try
            {
                DataTable dt = new DataTable();
               // dt = objcom.GetCountries();
                //ddlCountry.DataValueField = "Code";
                //ddlCountry.DataTextField = "Name";
                //ddlCountry.DataSource = dt;
                //ddlCountry.DataBind();

                //ddlCountry.Items.Insert(0, new ListItem("Select", "0"));

                //ddlCountry.SelectedValue = "PAK";
                //ddlCountry.Enabled = false;
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("Home/Registration.aspx", "GetCountries", ex.Message);
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

                                    Response.Redirect("/restricted/limit-exceeded");
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
        void GetCitiesByCoutry(string CountryCode)
        {
            try
            {
                DataTable dt = new DataTable();
                //dt = objcom.GetCitiesByCountry(CountryCode);
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
                                    lblMsg.Text = strMessage;
                                    lblMsg.Visible = true;
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup('" + strMessage + "');", true);
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
                            //}
                            //else
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
        protected bool Login(string userName)
        {
            System.Collections.Generic.List<string> d = Application["UsersLoggedIn"] as System.Collections.Generic.List<string>;
            if (d != null)
            {
                lock (d)
                {
                    if (d.Contains(userName))
                    {
                        // User is already logged in!!!
                        return false;
                    }
                    d.Add(userName);
                }
            }
            Session["UserLoggedIn"] = userName;
            return true;
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
        void InsertAuditLog(string ActType, string Action, string txt)
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
                //Location location = new Location();
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
    }
}