using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace EastlawUI_v2.companyadminpanel
{
    public partial class changepassword : System.Web.UI.Page
    {
        EastLawBL.Users objusr = new EastLawBL.Users();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CompanyID"] == null)
            {
                Response.Redirect("/");
            }
        }
        void ChangePasswordMethod()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objusr.GetUsers(int.Parse(Session["CompanyAdminID"].ToString()));
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        if (EncryptDecryptHelper.Encrypt(txtOldPassword.Text) != dt.Rows[0]["Pwd"].ToString())
                        {
                            lblMsg.Text = "Old Password doesn't match";
                            lblMsg.ForeColor = System.Drawing.Color.Red;
                            lblMsg.Visible = true;
                            return;
                        }
                        else
                        {
                            int chk = objusr.PasswordReset(dt.Rows[0]["EmailID"].ToString(), EncryptDecryptHelper.Encrypt(txtNewPassword.Text));
                            if (chk > 0)
                            {
                                // Email.SendMail(dt.Rows[0]["EmailID"].ToString(), EmailContent(dt.Rows[0]["FullName"].ToString(), dt.Rows[0]["EmailID"].ToString()), "Password Reset - eastlaw", "eastlaw.pk");
                                lblMsg.Text = "your password is changed, kindly login with new password";
                                lblMsg.ForeColor = System.Drawing.Color.Green;
                                lblMsg.Visible = true;
                            }
                        }
                    }
                }
            }
            catch { }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            ChangePasswordMethod();
        }
    }
}