using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.IO;
using System.Net;

namespace EastlawUI_v2.companyadminpanel
{
    public partial class resetpassword : System.Web.UI.Page
    {
        EastLawBL.Users objUsr = new EastLawBL.Users();
        EastLawBL.Common objcom = new EastLawBL.Common();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    if (Session["CompanyAdminID"] == null)
                    {
                        Response.Redirect("/");
                    }
                    

                    if (Request.QueryString["param"] != null)
                        GetUser(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())));
                }
            }
            catch (Exception ex)
            {

            }
        }
        void GetUser(int ID)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objUsr.GetUsers(ID);
                if (ID == 0)
                {
                    //dt.Columns.Add("strActive");
                    //for (int a = 0; a < dt.Rows.Count; a++)
                    //{
                    //    if (dt.Rows[a]["Active"].ToString() == "1")
                    //        dt.Rows[a]["strActive"] = "Yes";
                    //    else
                    //        dt.Rows[a]["strActive"] = "No";
                    //}
                    //dt.AcceptChanges();
                    //gv.DataSource = dt;
                    //gv.DataBind();
                }
                else
                {

                    txtEmailID.Text = dt.Rows[0]["EmailID"].ToString();
                    txtEmailID.Enabled = false;

                    txtFullName.Text = dt.Rows[0]["FullName"].ToString(); ;
                    txtPhone.Text = dt.Rows[0]["PhoneNo"].ToString(); ;

                    if (dt.Rows[0]["Active"].ToString() == "1")
                        chkActive.Checked = true;
                    else
                        chkActive.Checked = false;

                    divSuccess.Style["Display"] = "none";
                    divError.Style["Display"] = "none";

                }
            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("AddUsers.aspx", "GetUser", e.Message);
            }
        }
        void ChangePassword()
        {
            try
            {

                int chk = objUsr.PasswordReset(txtEmailID.Text, EncryptDecryptHelper.Encrypt(txtNewPassword.Text));
                if (chk > 0)
                {
                    //Email.SendMail(txtEmailID.Text, PasswordResetEmailContent(txtFullName.Text, txtEmailID.Text, txtNewPassword.Text, txtPhone.Text), "Password Reset - eastlaw.pk", "EastLaw", "");
                    SendResetPasswordConfirmationSMS();
                    txtNewPassword.Text = "";
                    divSuccess.Style["Display"] = "";
                    divError.Style["Display"] = "none";
                }

            }
            catch { }
        }
        public string CreatePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }
        protected void btnGenerateAutoPassword_Click(object sender, EventArgs e)
        {
            txtNewPassword.Text = CreatePassword(6);
        }
        string PasswordResetEmailContent(string Name, string EmailID, string Pwd, string MobileNo)
        {
            try
            {

                string file = "";


                string html = "";

                file = System.Web.HttpContext.Current.Server.MapPath("/EmailTemplates/ResetPassword.html");
                StreamReader sr = new StreamReader(file);
                FileInfo fi = new FileInfo(file);

                if (System.IO.File.Exists(file))
                {
                    html += sr.ReadToEnd();
                    sr.Close();
                }


                html = html.Replace("##FullName##", Name);
                html = html.Replace("##USRNME##", EmailID);
                html = html.Replace("##PWD##", Pwd);
                html = html.Replace("##MBLNO##", MobileNo);
                // html = html.Replace("##Pwd##", Session["Pwd"].ToString());


                return html;

            }
            catch
            {
                return "";
            }



        }
        void SendResetPasswordConfirmationSMS()
        {
            try
            {
                string smstxt = "Dear " + txtFullName.Text.Trim() + ", your password has been reset on eastlaw.pk account, kindly check your email for new credentials.";

                string mobilenumber = txtPhone.Text.Trim();
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            ChangePassword();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ManageUsers.aspx");
        }

    }
}