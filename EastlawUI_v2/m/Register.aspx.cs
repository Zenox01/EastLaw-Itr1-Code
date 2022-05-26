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

namespace EastlawUI_v2.m
{
    public partial class Register : System.Web.UI.Page
    {
        EastLawBL.Users objUsr = new EastLawBL.Users();
        EastLawBL.Common objcom = new EastLawBL.Common();
        EastLawBL.PracticeAreas objPA = new EastLawBL.PracticeAreas();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    InsertAuditLog("Hit", "Login/Register", "New Registration");
                    GetOrgTypes();
                    GetPracticeArea();
                    GetCitiesByCoutry("PAK");
                    // string urlName = Request.UrlReferrer.ToString();
                    ViewState["LastPage"] = Request.UrlReferrer.ToString();


                    //Response.Write(EncryptDecryptHelper.Encrypt("logout"));

                }
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("Home/Registration.aspx", "Page_Load", ex.Message);
            }
        }
        void GetPracticeArea()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objPA.GetActivePracticeAreaSubCategoriesByCategory(3);
                chkPracticeArea.DataValueField = "ID";
                chkPracticeArea.DataTextField = "PracticeAreaSubCatName";
                chkPracticeArea.DataSource = dt;
                chkPracticeArea.DataBind();


            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("Home/Registration.aspx", "GetOrgTypes", ex.Message);
            }
        }
        void GetOrgTypes()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objUsr.GetActiveOrgTypes();
                ddlOrgType.DataValueField = "ID";
                ddlOrgType.DataTextField = "OrgTypes";
                ddlOrgType.DataSource = dt;
                ddlOrgType.DataBind();

                ddlOrgType.Items.Insert(0, new ListItem("Select", "0"));
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("Home/Registration.aspx", "GetOrgTypes", ex.Message);
            }
        }

        //void GetCountries()
        //{
        //    try
        //    {
        //        DataTable dt = new DataTable();
        //        dt = objcom.GetCountries();
        //        ddlCountry.DataValueField = "Code";
        //        ddlCountry.DataTextField = "Name";
        //        ddlCountry.DataSource = dt;
        //        ddlCountry.DataBind();

        //        ddlCountry.Items.Insert(0, new ListItem("Select", "0"));

        //        ddlCountry.SelectedValue = "PAK";
        //        ddlCountry.Enabled = false;
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionHandling.SendErrorReport("Home/Registration.aspx", "GetCountries", ex.Message);
        //    }
        //}
        void GetCitiesByCoutry(string CountryCode)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objcom.GetCitiesByCountry(CountryCode);
                ddlCity.DataValueField = "ID";
                ddlCity.DataTextField = "Name";
                ddlCity.DataSource = dt;
                ddlCity.DataBind();

                ddlCity.Items.Insert(0, new ListItem("Select", "0"));
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("Home/Registration.aspx", "GetCountries", ex.Message);
            }
        }
        void SaveRecord()
        {
            try
            {
                objUsr.OrgTypeID = int.Parse(ddlOrgType.SelectedValue);
                objUsr.UserTypeID = 5;
                objUsr.EmailID = txtEmail.Text.Trim();
                objUsr.Pwd = EncryptDecryptHelper.Encrypt(txtPassword.Text.Trim());
                objUsr.FullName = txtFName.Text.Trim() +" "+txtLName.Text.Trim();
                objUsr.PhoneNo = txtMobCountryCode.Text.Trim() + txtMobCode.Text.Trim() + txtMobNo.Text.Trim();  //txtPhoneNo.Text.Trim();
                //objUsr.Address = txtAdd.Text.Trim();
                objUsr.Country = "PAK";
                objUsr.CityID = int.Parse(ddlCity.SelectedValue);
                objUsr.PlanID = 27;
                objUsr.CompanyID = 0;
                objUsr.CompanyName = txtOrgName.Text.Trim();
                objUsr.Verify = 0;
                objUsr.Status = "Pending - Activation";
                objUsr.Active = 0;
                objUsr.CreatedBy = 0;
                objUsr.NoOfPCAllowd = 0;// int.Parse(txtNoOfPCAllowed.Text.Trim());
                objUsr.AccessExpireOn = DateTime.Now.AddDays(5).ToString("MM/dd/yyyy HH:MM:ss");
                objUsr.PostalAddress = txtPostalAddress.Text.Trim();
                int chk = objUsr.InsertUser();
                if (chk > 0)
                {
                    try
                    {
                        for (int a = 0; a < chkPracticeArea.Items.Count; a++)
                        {
                            if (chkPracticeArea.Items[a].Selected == true)
                                objUsr.AddUserPracticeArea(chk, int.Parse(chkPracticeArea.Items[a].Value));
                        }
                    }
                    catch { }
                    divThank.Style["Display"] = "";
                    divReg.Style["Display"] = "none";
                    string emailcnt = EmailContent(chk);
                    objUsr.AddUserNotificationLog(chk, "New Registration Activation", emailcnt);
                    Email.SendMail(txtEmail.Text, emailcnt, "Welcome to eastlaw.pk", "EastLaw", "");
                    //Email.SendMail("registration@eastlaw.pk", AdminEmailContent(), "Welcome to eastlaw.pk", "EastLaw", "");
                    Email.SendMail("muhammad.abubakar@live.com", AdminEmailContent(), "Welcome to eastlaw.pk", "EastLaw", "");
                    Email.SendMail("staff1@eastlaw.pk", AdminEmailContent(), "Welcome to eastlaw.pk", "EastLaw", "");
                    SendWelcomeSMS(chk);
                    //ClearFields();

                }
                else
                {
                    divThank.Style["Display"] = "none";
                    divReg.Style["Display"] = "";
                }
            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("home/Registration.aspx", "SaveRecord", e.Message);
            }
        }
        string EmailContent(int ID)
        {
            try
            {
                string file = "";


                string html = "";

                file = System.Web.HttpContext.Current.Server.MapPath("/EmailTemplates/NewRegistration.html");
                StreamReader sr = new StreamReader(file);
                FileInfo fi = new FileInfo(file);

                if (System.IO.File.Exists(file))
                {
                    html += sr.ReadToEnd();
                    sr.Close();
                }


                html = html.Replace("##FullName##", txtFName.Text.Trim());
                html = html.Replace("##ClickHere##", "<a href='" + ConfigurationSettings.AppSettings["websiteUrl"].ToString() + "member/member-activation?uval=" + EncryptDecryptHelper.Encrypt(ID.ToString()) + "' target='_blank'>Click Here</a>");
                html = html.Replace("##FullLink##", ConfigurationSettings.AppSettings["websiteUrl"].ToString() + "member/member-activation?uval=" + EncryptDecryptHelper.Encrypt(ID.ToString()));
                // html = html.Replace("##Pwd##", Session["Pwd"].ToString());


                return html;
            }
            catch
            {
                return "";
            }



        }
        string AdminEmailContent()
        {
            try
            {
                string file = "";


                string html = "";

                file = System.Web.HttpContext.Current.Server.MapPath("/EmailTemplates/NewRegistrationAdmin.html");
                StreamReader sr = new StreamReader(file);
                FileInfo fi = new FileInfo(file);

                if (System.IO.File.Exists(file))
                {
                    html += sr.ReadToEnd();
                    sr.Close();
                }



                html = html.Replace("##FullName##", txtFName.Text.Trim());
                html = html.Replace("##USRNME##", txtEmail.Text);
                html = html.Replace("##MBLNM##", txtMobCountryCode.Text + txtMobCode.Text + txtMobNo.Text);
                // html = html.Replace("##Pwd##", Session["Pwd"].ToString());


                return html;
            }
            catch
            {
                return "";
            }



        }
        void SendWelcomeSMS(int ID)
        {
            try
            {
                string smstxt = "Dear " + txtFName.Text.Trim() + ", Thank you for Registering at EastLaw.pk."
                + "Please check your email to activate your account or Click " + ConfigurationSettings.AppSettings["websiteUrl"].ToString() + "member/member-activation?uval=" + EncryptDecryptHelper.Encrypt(ID.ToString()) + " to activate it now.  Helpline#. 04237311670";

                string mobilenumber = txtMobCountryCode.Text.Trim() + txtMobCode.Text.Trim() + txtMobNo.Text.Trim();
                //string url = "http://bulksms.com.pk/api/sms.php?username=923214264174&password=5974&sender=eastlaw.pk&mobile=923214264174&message=" + smstxt + "";
                string url = "http://bulksms.com.pk/api/sms.php?username=923228451969&password=1943&sender=eastlaw.pk&mobile=" + mobilenumber + "&message=" + smstxt + "";

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
        int CheckEmailExist(string EmailID)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objUsr.CheckUserExistByEmail(EmailID);
                if (dt.Rows.Count > 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch { return 0; }
        }
        int CheckPhoneNoExist(string PhoneNo)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objUsr.CheckUserExistByPhone(PhoneNo);
                if (dt.Rows.Count > 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch { return 0; }
        }
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                // SaveRecord();
                int chk = CheckEmailExist(txtEmail.Text.Trim());
                if (chk == 0)
                {
                    if (chkTNC.Checked == false)
                    {
                        lblMsgReg.Text = "Please Accept Terms & Condition";
                        lblMsgReg.Visible = true;
                        return;
                    }
                    else
                    {

                        chk = CheckPhoneNoExist(txtMobCountryCode.Text.Trim() + txtMobCode.Text.Trim() + txtMobNo.Text.Trim());
                        if (chk == 1)
                        {
                            lblPhoneNoExist.Text = "Mobile No. exist";
                            lblPhoneNoExist.Visible = true;

                            lblMsgReg.Text = "Mobile No. exist";
                            lblMsgReg.ForeColor = System.Drawing.Color.Red;

                            lblMsgReg.Visible = true;
                        }
                        else
                        {
                            SaveRecord();
                        }
                    }
                }
                else
                {
                    lblExist.Text = "Email ID already exist";
                    lblExist.Visible = true;

                    lblMsgReg.Text = "Email ID already exist";
                    lblMsgReg.ForeColor = System.Drawing.Color.Red;

                    lblMsgReg.Visible = true;

                }
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("Home/Registration.aspx", "btnRegister_Click", ex.Message);
            }
        }

        protected void txtEmail_TextChanged(object sender, EventArgs e)
        {
            int chk = CheckEmailExist(txtEmail.Text.Trim());
            if (chk == 1)
            {
                lblExist.Text = "Email ID already exist";
                lblExist.Visible = true;
            }
            else
            {
                lblExist.Text = "";
                lblExist.Visible = false;
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
                CommonClass.GetIPLocation(visitorIPAddress, ref Country, ref Region, ref City);
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

        protected void txtMobNo_TextChanged(object sender, EventArgs e)
        {
            int chk = CheckPhoneNoExist(txtMobCountryCode.Text.Trim() + txtMobCode.Text.Trim() + txtMobNo.Text.Trim());
            if (chk == 1)
            {
                lblPhoneNoExist.Text = "Mobile No. exist";
                lblPhoneNoExist.Visible = true;
            }
            else
            {
                lblPhoneNoExist.Text = "";
                lblPhoneNoExist.Visible = false;
            }
        }
    }
}