using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Configuration;


namespace EastlawUI_v2.m
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        EastLawBL.Users objUsr = new EastLawBL.Users();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CheckResetPassword();
                //Response.Write(EncryptDecryptHelper.Encrypt("umar.mughal83@gmail.com"));
            }
        }
        void CheckResetPassword()
        {
            try
            {
                DataTable dt = new DataTable();
                if (Request.QueryString["ac"] != null)
                {
                    dt = objUsr.CheckUserExistByEmail(EncryptDecryptHelper.Decrypt(Request.QueryString["ac"].ToString()));
                    if (dt.Rows.Count > 0)
                    {
                        txtEmailID.Text = EncryptDecryptHelper.Decrypt(Request.QueryString["ac"].ToString());
                        txtEmailID.Enabled = false;
                        divNewPassword.Style["Display"] = "";
                        divConfirmNewPassword.Style["Display"] = "";
                        rfvNewPass.Enabled = true;
                        rfvConfirmNewPass.Enabled = true;
                        cvNewPass.Enabled = true;
                        btnUpdatePassword.Visible = true;
                        btnSendPassword.Visible = false;
                    }
                    else
                    {
                        txtEmailID.Text = "";
                        txtEmailID.Enabled = true;
                        divNewPassword.Style["Display"] = "none";
                        divConfirmNewPassword.Style["Display"] = "none";
                        rfvNewPass.Enabled = false;
                        rfvConfirmNewPass.Enabled = false;
                        cvNewPass.Enabled = false;
                        btnUpdatePassword.Visible = false;
                        btnSendPassword.Visible = true;


                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("Home/ForgetPassword.aspx", "CheckEmailExist", ex.Message);
            }
        }
        string EmailContent(string Name, string EmailID)
        {
            try
            {
                string file = "";


                string html = "";

                file = System.Web.HttpContext.Current.Server.MapPath("/EmailTemplates/ForgetPassword.html");
                StreamReader sr = new StreamReader(file);
                FileInfo fi = new FileInfo(file);

                if (System.IO.File.Exists(file))
                {
                    html += sr.ReadToEnd();
                    sr.Close();
                }


                html = html.Replace("##FullName##", Name);
                html = html.Replace("##ClickLink##", "<a href='" + ConfigurationSettings.AppSettings["websiteUrl"].ToString() + "Member/Forget-Password?ac=" + EncryptDecryptHelper.Encrypt(EmailID) + "' target='_blank'>Reset your password now </a>");
                html = html.Replace("##FullLink##", ConfigurationSettings.AppSettings["websiteUrl"].ToString() + "Member/Forget-Password?ac=" + EncryptDecryptHelper.Encrypt(EmailID));
                // html = html.Replace("##Pwd##", Session["Pwd"].ToString());


                return html;
            }
            catch
            {
                return "";
            }



        }
        void CheckEmailExist(string EmailID)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objUsr.CheckUserExistByEmail(EmailID);
                if (dt.Rows.Count > 0)
                {
                    string emailcnt = EmailContent(dt.Rows[0]["FullName"].ToString(), dt.Rows[0]["EmailID"].ToString());
                    objUsr.AddUserNotificationLog(int.Parse(dt.Rows[0]["ID"].ToString()), "Forget Password Email", emailcnt);
                    Email.SendMail(dt.Rows[0]["EmailID"].ToString(), emailcnt, "Password Reset - eastlaw", "Eastlaw", "");

                    lblMsg.Text = "Password Reset Link has been sent to the given Email ID. Please check your inbox now.";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    lblMsg.Visible = true;

                    divEmailAdd.Style["Display"] = "none";
                    btnSendPassword.Visible = false;

                }
                else
                {
                    lblMsg.Text = "Account doesn't exist, kindly register your account.";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    lblMsg.Visible = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("Home/ForgetPassword.aspx", "CheckEmailExist", ex.Message);
            }
        }

        protected void btnSendPassword_Click(object sender, EventArgs e)
        {
            try
            {
                CheckEmailExist(txtEmailID.Text);
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("Home/ForgetPassword.aspx", "btnSendPassword_Click", ex.Message);
            }
        }
        protected void btnUpdatePassword_Click(object sender, EventArgs e)
        {
            try
            {
                int chk = objUsr.PasswordReset(txtEmailID.Text, EncryptDecryptHelper.Encrypt(txtNewPassword.Text));
                if (chk > 0)
                {
                    // Email.SendMail(dt.Rows[0]["EmailID"].ToString(), EmailContent(dt.Rows[0]["FullName"].ToString(), dt.Rows[0]["EmailID"].ToString()), "Password Reset - eastlaw", "eastlaw.pk");
                    lblMsg.Text = "your password is changed, kindly login with new password <a href='/Member/Member-Login/'>Click Here</a>";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    lblMsg.Visible = true;

                    divNewPassword.Style["Display"] = "none";
                    divConfirmNewPassword.Style["Display"] = "none";
                    rfvNewPass.Enabled = false;
                    rfvConfirmNewPass.Enabled = false;
                    cvNewPass.Enabled = false;
                    btnUpdatePassword.Visible = false;
                    btnSendPassword.Visible = false;

                }
                else
                {
                    lblMsg.Text = "Account doesn't exist, kindly register your account.";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    lblMsg.Visible = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("Home/ForgetPassword.aspx", "btnSendPassword_Click", ex.Message);
            }
        }
    }
}