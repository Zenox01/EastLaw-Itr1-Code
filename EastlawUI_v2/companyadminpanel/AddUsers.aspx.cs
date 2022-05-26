using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace EastlawUI_v2.companyadminpanel
{
    public partial class AddUsers : System.Web.UI.Page
    {
        EastLawBL.Users objUsr = new EastLawBL.Users();
        EastLawBL.Common objcom = new EastLawBL.Common();
        EastLawBL.Plans objPlan = new EastLawBL.Plans();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //if (Request.QueryString["param"] != null)
                   // GetUser(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())));
            }
            catch { }
        }
        //void GetUser(int ID)
        //{
        //    try
        //    {
        //        DataTable dt = new DataTable();
        //        dt = objUsr.GetUsers(ID);
        //        if (ID == 0)
        //        {
                   
        //        }
        //        else
        //        {

        //            ddlOrgType.SelectedValue = dt.Rows[0]["OrgTypeID"].ToString();
        //            ddlUserType.SelectedValue = dt.Rows[0]["UserTypeID"].ToString();
        //            if (dt.Rows[0]["UserTypeID"].ToString() == "4")
        //            {
        //                GetActiveCompanies();
        //                divCompany.Style["Display"] = "";
        //                rfvddlCompany.Enabled = true;
        //                ddlCompany.SelectedValue = dt.Rows[0]["CompanyID"].ToString();
        //            }

        //            ddlPlan.SelectedValue = dt.Rows[0]["PlanID"].ToString();
        //            GetActivePlans(int.Parse(dt.Rows[0]["PlanID"].ToString()));
        //            ddlPlan.Enabled = false;

        //            txtEmailID.Text = dt.Rows[0]["EmailID"].ToString();
        //            txtEmailID.Enabled = false;

        //            divPassword.Style["Display"] = "none";
        //            rfvtxtPassword.Enabled = false;
        //            rfvtxtConfirmPassword.Enabled = false;
        //            cvPassword.Enabled = false;

        //            txtFullName.Text = dt.Rows[0]["FullName"].ToString(); ;
        //            txtPhone.Text = dt.Rows[0]["PhoneNo"].ToString(); ;
        //            txtAdd.Text = dt.Rows[0]["Address"].ToString(); ;
        //            ddlCountry.SelectedValue = dt.Rows[0]["Country"].ToString();
        //            GetCitiesByCoutry(ddlCountry.SelectedValue);
        //            ddlCity.SelectedValue = dt.Rows[0]["CityID"].ToString();
        //            txtNoOfPCAllowed.Text = dt.Rows[0]["NoOfPCAllowd"].ToString(); ;
        //            ddlStatus.SelectedValue = dt.Rows[0]["Status"].ToString(); ;
        //            ddlStatus.Enabled = false;
        //            if (dt.Rows[0]["Active"].ToString() == "1")
        //                chkActive.Checked = true;
        //            else
        //                chkActive.Checked = false;



        //            divSuccess.Style["Display"] = "none";
        //            divError.Style["Display"] = "none";



        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        ExceptionHandling.SendErrorReport("AddUsers.aspx", "GetUser", e.Message);
        //    }
        //}
        void SaveRecord()
        {
            try
            {
                DataTable dtCompanyUser = new DataTable();
                dtCompanyUser = objUsr.GetUsers(int.Parse(Session["CompanyAdminID"].ToString()));
                if (dtCompanyUser != null)
                {
                    if (dtCompanyUser.Rows.Count > 0)
                    {

                        DataTable dtCompanyValidation = new DataTable();
                        dtCompanyValidation = objUsr.GetUsersByCompany(int.Parse(Session["CompanyID"].ToString()));
                        if (dtCompanyValidation != null)
                        {
                            if (dtCompanyValidation.Rows.Count > 0)
                            {
                                int AlllowedNoOfUsers = int.Parse(dtCompanyValidation.Rows[0]["PlanNoOfUsers"].ToString()) + int.Parse(dtCompanyUser.Rows[0]["CompUser"].ToString());
                                if (dtCompanyValidation.Rows.Count > AlllowedNoOfUsers)
                                {
                                    string strMessage = "Sorry, You have reached your users limit, kindly contact administrator.";
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup('" + strMessage + "');", true);

                                    lblConfirmation.Text = strMessage;
                                    return;
                                }
 
                            }
                        }
                        objUsr.OrgTypeID = int.Parse(dtCompanyUser.Rows[0]["OrgTypeID"].ToString());
                        objUsr.UserTypeID = 17;
                        objUsr.EmailID = txtEmailID.Text.Trim();// +"@" + dtCompanyUser.Rows[0]["CompanyUserAbbr"].ToString();
                        objUsr.Pwd = EncryptDecryptHelper.Encrypt(txtPassword.Text.Trim());
                        objUsr.FullName = txtFullName.Text.Trim();
                        objUsr.PhoneNo = txtPhone.Text.Trim();
                        objUsr.Address = dtCompanyUser.Rows[0]["Address"].ToString();
                        objUsr.Country = dtCompanyUser.Rows[0]["Country"].ToString();// ddlCountry.SelectedValue;
                        objUsr.CityID = int.Parse(dtCompanyUser.Rows[0]["CityID"].ToString());
                        objUsr.PlanID = int.Parse(dtCompanyUser.Rows[0]["PlanID"].ToString());
                        objUsr.CompanyID = int.Parse(dtCompanyUser.Rows[0]["CompanyID"].ToString());
                        objUsr.CompUser = 0;
                        objUsr.CompanyUserAbbr = dtCompanyUser.Rows[0]["CompanyUserAbbr"].ToString();// txtUserAbbriviations.Text.Trim();
                        objUsr.Verify = 1;
                        objUsr.Status = "Approved";
                        if (chkActive.Checked == true)
                            objUsr.Active = 1;
                        else
                            objUsr.Active = 0;
                        objUsr.CreatedBy = int.Parse(Session["CompanyAdminID"].ToString());
                        objUsr.NoOfPCAllowd = 0;
                        objUsr.AccessExpireOn = DateTime.Now.AddDays(double.Parse(dtCompanyUser.Rows[0]["ExpireIn"].ToString())).ToString("MM/dd/yyyy HH:MM:ss");
                        int chk = objUsr.InsertUser();
                        if (chk > 0)
                        {
                            string strMessage = "New User Created";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup('" + strMessage + "');", true);
                            lblConfirmation.Text = strMessage;
                            //divSuccess.Style["Display"] = "";
                            //divError.Style["Display"] = "none";
                            //ClearFields();

                        }
                        else
                        {
                            //divSuccess.Style["Display"] = "none";
                            //divError.Style["Display"] = "";
                        }

                    }
                    else
                    {
                        string strMessage = "User Creation failed, please contact administrator";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup('" + strMessage + "');", true);
                        lblConfirmation.Text = strMessage;
                    }
                }
                else
                {
                    string strMessage = "User Creation failed, please contact administrator";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup('" + strMessage + "');", true);
                    lblConfirmation.Text = strMessage;
                }
               
            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("AddUsers.aspx", "SaveRecord", e.Message);
            
            }
        }
        //void EditRecord(int ID)
        //{
        //    try
        //    {
        //        objUsr.OrgTypeID = int.Parse(ddlOrgType.SelectedValue);
        //        objUsr.UserTypeID = int.Parse(ddlUserType.SelectedValue);
        //        objUsr.FullName = txtFullName.Text.Trim();
        //        objUsr.PhoneNo = txtPhone.Text.Trim();
        //        objUsr.Address = txtAdd.Text.Trim();
        //        objUsr.Country = ddlCountry.SelectedValue;
        //        objUsr.CityID = int.Parse(ddlCity.SelectedValue);
        //        objUsr.PlanID = int.Parse(ddlPlan.SelectedValue);
        //        if (ddlUserType.SelectedValue == "4")
        //            objUsr.CompanyID = int.Parse(ddlCompany.SelectedValue);
        //        else
        //            objUsr.CompanyID = 0;
        //        if (chkVerify.Checked == true)
        //            objUsr.Verify = 0;
        //        else
        //            objUsr.Verify = 1;
        //        objUsr.Status = ddlStatus.SelectedValue;
        //        if (chkActive.Checked == true)
        //            objUsr.Active = 1;
        //        else
        //            objUsr.Active = 0;
        //        objUsr.ModifiedBy = int.Parse(Session["UserID"].ToString());
        //        objUsr.NoOfPCAllowd = int.Parse(txtNoOfPCAllowed.Text.Trim());
        //        //objUsr.AccessExpireOn = DateTime.Now.AddDays(double.Parse(lblPlanNoOfDays.Text)).ToString();
        //        int chk = objUsr.EditUser(ID);
        //        if (chk > 0)
        //        {
        //            //DataTable dt = objUsr.GetUsers(ID);
        //            //if (chkVerify.Checked == true)
        //            //{
        //            //    Email.SendMail(txtEmailID.Text, EmailContent(ID), "Welcome to eastlaw.pk", "EastLaw", "");
        //            //    Email.SendMail("registration@eastlaw.pk", AdminEmailContent(), "Welcome to eastlaw.pk", "EastLaw", "");
        //            //}
        //            ////if (ddlStatus.SelectedValue == "Approved")
        //            ////{

        //            ////    Email.SendMail(dt.Rows[0]["EmailID"].ToString(), WelcomeEmailContent(dt.Rows[0]["FullName"].ToString(), dt.Rows[0]["EmailID"].ToString(), dt.Rows[0]["Pwd"].ToString()), "Account Verified - Welcome to eastlaw.pk", "EastLaw", "");
        //            ////    Email.SendMail("registration@eastlaw.pk", WelcomeEmailContentAdmin(dt.Rows[0]["FullName"].ToString(), dt.Rows[0]["EmailID"].ToString(), dt.Rows[0]["Pwd"].ToString()), "Account Verified  - Welcome to eastlaw.pk", "EastLaw", "");
        //            ////}
        //            //else
        //            //{
        //            //    Email.SendMail(dt.Rows[0]["EmailID"].ToString(), UserProfileUpdateEmail(dt.Rows[0]["FullName"].ToString(), ddlStatus.SelectedItem.Text), "Account Update", "EastLaw", "");
        //            //    Email.SendMail("registration@eastlaw.pk", UserProfileUpdateEmail(dt.Rows[0]["FullName"].ToString(), ddlStatus.SelectedItem.Text), "Account Update", "EastLaw", "");

        //            //}
        //            divSuccess.Style["Display"] = "";
        //            divError.Style["Display"] = "none";
        //            //ClearFields();

        //        }
        //        else
        //        {
        //            divSuccess.Style["Display"] = "none";
        //            divError.Style["Display"] = "";
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        ExceptionHandling.SendErrorReport("AddUsers.aspx", "SaveRecord", e.Message);
        //    }
        //}
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
        protected void txtEmailID_TextChanged(object sender, EventArgs e)
        {
            int chk = CheckEmailExist(txtEmailID.Text.Trim());
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
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["param"] == null)
                {

                    int chk = CheckEmailExist(txtEmailID.Text.Trim());
                    if (chk == 0)
                    {
                        SaveRecord();
                    }
                    else
                    {
                        lblExist.Text = "Email ID already exist";
                        lblExist.Visible = true;



                    }

                }
               // else
                 //   EditRecord(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())));
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("AddUsers.aspx", "btnSave_Click", ex.Message);
            }
        }

       
        
    }
}