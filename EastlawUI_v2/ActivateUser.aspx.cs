using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net;
using System.IO;
using System.Configuration;

namespace EastlawUI_v2
{
    public partial class ActivateUser : System.Web.UI.Page
    {
        EastLawBL.Users objusr = new EastLawBL.Users();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SendSMSCode();
               // VerifyAccountMobileWithoutChecking();


            }
        }
        void SendSMSCode()
        {
            try
            {
                DataTable dt = new DataTable();
                if (Request.QueryString["uval"] != null)
                {
                    dt = objusr.GetUsers(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["uval"].ToString())));
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            divSMSveri.Style["Display"] = "none";
                            if (dt.Rows[0]["mobileverify"].ToString() != "1")
                            {
                                Random RandomPIN = new Random();
                                var RandomPINResult = RandomPIN.Next(0, 9999).ToString();
                                RandomPINResult = RandomPINResult.PadLeft(4, '0');
                                string smstxt = "Your one time verification code is: " + RandomPINResult.ToString();
                                string mobilenumber = dt.Rows[0]["PhoneNo"].ToString();
                                //string url = "http://bulksms.com.pk/api/sms.php?username=923214264174&password=5974&sender=eastlaw.pk&mobile=923214264174&message=" + smstxt + "";
                                //string url = "http://bulksms.com.pk/api/sms.php?username=923228451969&password=1813&sender=eastlaw.pk&mobile=" + mobilenumber + "&message=" + smstxt + "";
                                string url = "https://connect.jazzcmt.com/sendsms_url.html?Username=03082479671&Password=Eastlaw@123api&From=EastLaw&To=" + mobilenumber + "&Message=" + smstxt + "";
                                //HTTP connection
                                HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(url);
                                //Get response from Ozeki NG SMS Gateway Server and read the answer
                                HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
                                System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
                                string responseString = respStreamReader.ReadToEnd();
                                respStreamReader.Close();
                                myResp.Close();

                                int chk1 = objusr.MobileVerifyCode(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["uval"].ToString())), RandomPINResult.ToString());

                                divAlreadyVerified.Style["Display"] = "none";
                                divConfirm.Style["Display"] = "none";
                                divInvalid.Style["Display"] = "none";
                                divSMSveri.Style["Display"] = "";
                                return;
                            }
                            if (dt.Rows[0]["verify"].ToString() == "1")
                            {
                                divAlreadyVerified.Style["Display"] = "";
                                divConfirm.Style["Display"] = "none";
                                divInvalid.Style["Display"] = "none";
                                divSMSveri.Style["Display"] = "none";

                            }
                            else
                            {
                                VerifyAccount();

                            }
                        }
                    }

                }


            }
            catch { }
        }
        void VerifyAccount()
        {

            try
            {
                DataTable dtuser = new DataTable();
                if (Request.QueryString["uval"] != null)
                {
                    dtuser = objusr.GetUsers(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["uval"].ToString())));
                    if (dtuser != null)
                    {
                        if (dtuser.Rows.Count > 0)
                        {
                            if (dtuser.Rows[0]["verify"].ToString() == "1")
                            {
                                divAlreadyVerified.Style["Display"] = "";
                                divConfirm.Style["Display"] = "none";
                                divInvalid.Style["Display"] = "none";
                                divSMSveri.Style["Display"] = "none";

                            }
                            else
                            {
                                int chk = objusr.VerifyAccount(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["uval"].ToString())));
                                if (chk > 0)
                                {

                                    divConfirm.Style["Display"] = "";
                                    divInvalid.Style["Display"] = "none";
                                    DataTable dt = new DataTable();
                                    if (Request.QueryString["uval"] != null)
                                    {
                                        dt = objusr.GetActiveAndApprovedUser(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["uval"].ToString())));
                                        Email.SendMail(dt.Rows[0]["EmailID"].ToString(), WelcomeEmailContent(dt.Rows[0]["FullName"].ToString(), dt.Rows[0]["EmailID"].ToString(), dt.Rows[0]["Pwd"].ToString(), dt.Rows[0]["PhoneNo"].ToString()), "Account Verified - Welcome to eastlaw.pk", "EastLaw", "");
                                        //Email.SendMail("registration@eastlaw.pk", WelcomeEmailContentAdmin(dt.Rows[0]["FullName"].ToString(), dt.Rows[0]["EmailID"].ToString(), dt.Rows[0]["Pwd"].ToString()), "Account Verified  - Welcome to eastlaw.pk", "EastLaw", "");
                                        Email.SendMail(ConfigurationManager.AppSettings["regEmailTransferToAbubakar"].ToString(), WelcomeEmailContentAdmin(dt.Rows[0]["FullName"].ToString(), dt.Rows[0]["EmailID"].ToString(), dt.Rows[0]["Pwd"].ToString()), "Account Verified  - Welcome to eastlaw.pk", "EastLaw", "");
                                        Email.SendMail(ConfigurationManager.AppSettings["regEastLawTeam"].ToString(), WelcomeEmailContentAdmin(dt.Rows[0]["FullName"].ToString(), dt.Rows[0]["EmailID"].ToString(), dt.Rows[0]["Pwd"].ToString()), "Account Verified  - Welcome to eastlaw.pk", "EastLaw", "");

                                    }

                                    //ClearFields();

                                }
                                else
                                {
                                    divConfirm.Style["Display"] = "none";
                                    divInvalid.Style["Display"] = "";
                                }
                            }
                        }
                    }
                }
                else
                {
                    divConfirm.Style["Display"] = "none";
                    divInvalid.Style["Display"] = "";
                }
            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("home/ActivateUser.aspx", "VerifyAccount", e.Message);
            }


        }
        void VerifyAccountMobile()
        {

            try
            {
                DataTable dt = new DataTable();
                if (Request.QueryString["uval"] != null)
                {

                    dt = objusr.GetUsers(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["uval"].ToString())));
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            string mobilecode = dt.Rows[0]["mobilecode"].ToString();
                            if (!string.IsNullOrEmpty(mobilecode))
                            {
                                if (mobilecode == txtSMSCode.Text)
                                {
                                    int chk = objusr.MobileVerify(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["uval"].ToString())));
                                    if (chk > 0)
                                    {

                                        divConfirm.Style["Display"] = "";
                                        divInvalid.Style["Display"] = "none";
                                        divSMSveri.Style["Display"] = "none";
                                        //Email.SendMail(txtEmail.Text, EmailContent(chk), "Welcome to eastlaw.pk", "eastlaw.pk");
                                        //ClearFields();
                                        VerifyAccount();

                                    }
                                    else
                                    {
                                        divConfirm.Style["Display"] = "none";
                                        divInvalid.Style["Display"] = "";
                                    }
                                }

                            }
                        }
                    }

                }
                else
                {
                    divConfirm.Style["Display"] = "none";
                    divInvalid.Style["Display"] = "";
                }
            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("home/ActivateUser.aspx", "VerifyAccount", e.Message);
            }


        }
        void VerifyAccountMobileWithoutChecking()
        {

            try
            {
                DataTable dt = new DataTable();
                if (Request.QueryString["uval"] != null)
                {

                    dt = objusr.GetUsers(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["uval"].ToString())));
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {

                            int chk = objusr.MobileVerify(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["uval"].ToString())));
                            if (chk > 0)
                            {

                                divConfirm.Style["Display"] = "";
                                divInvalid.Style["Display"] = "none";
                                divSMSveri.Style["Display"] = "none";
                                //Email.SendMail(txtEmail.Text, EmailContent(chk), "Welcome to eastlaw.pk", "eastlaw.pk");
                                //ClearFields();
                                VerifyAccount();

                            }
                            else
                            {
                                divConfirm.Style["Display"] = "none";
                                divInvalid.Style["Display"] = "";
                            }
                            
                        }
                    }

                }
                else
                {
                    divConfirm.Style["Display"] = "none";
                    divInvalid.Style["Display"] = "";
                }
            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("home/ActivateUser.aspx", "VerifyAccount", e.Message);
            }


        }
        string WelcomeEmailContent(string Name, string EmailID, string Pwd, string MobileNo)
        {
            try
            {

                string file = "";


                string html = "";

                file = System.Web.HttpContext.Current.Server.MapPath("/EmailTemplates/Welcome.html");
                StreamReader sr = new StreamReader(file);
                FileInfo fi = new FileInfo(file);

                if (System.IO.File.Exists(file))
                {
                    html += sr.ReadToEnd();
                    sr.Close();
                }


                html = html.Replace("##FullName##", Name);
                html = html.Replace("##USRNME##", EmailID);
                html = html.Replace("##PWD##", EncryptDecryptHelper.Decrypt(Pwd));
                html = html.Replace("##MBLNO##", MobileNo);
                // html = html.Replace("##Pwd##", Session["Pwd"].ToString());


                return html;

            }
            catch
            {
                return "";
            }



        }
        string WelcomeEmailContentAdmin(string Name, string EmailID, string Pwd)
        {
            try
            {

                string file = "";


                string html = "";

                html = "User: " + EmailID + " Activated.";


                // html = html.Replace("##Pwd##", Session["Pwd"].ToString());


                return html;

            }
            catch
            {
                return "";
            }



        }
        protected void btnResend_Click(object sender, EventArgs e)
        {
            SendSMSCode();
        }

        protected void btnVerifyCode_Click(object sender, EventArgs e)
        {
            // VerifyAccount();
            VerifyAccountMobile();
        }
    }
}