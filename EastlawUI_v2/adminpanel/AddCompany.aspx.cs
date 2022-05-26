using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace EastlawUI_v2.adminpanel
{
    public partial class AddCompany : System.Web.UI.Page
    {
        EastLawBL.Users objUsr = new EastLawBL.Users();
        EastLawBL.Common objcom = new EastLawBL.Common();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    if (Session["UserID"] == null)
                    {
                        Response.Redirect("default.aspx");
                    }
                    if (!ValidateUserGroup.ValidateGroup(int.Parse(Session["UserTypeID"].ToString()), ValidateUserGroup.getPageName(Request.Url.AbsolutePath)))
                        Response.Redirect("NotAuthorize.aspx");
                    GetCountries();
                    GetOrgTypes();
                    if (Request.QueryString["param"] != null)
                        GetCompanies(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())));
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("AddCompany.aspx", "Page_Load", ex.Message);
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
                ExceptionHandling.SendErrorReport("AddCompany.aspx", "GetOrgTypes", ex.Message);
            }
        }
        void GetCountries()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objcom.GetCountries();
                ddlCountry.DataValueField = "Code";
                ddlCountry.DataTextField = "Name";
                ddlCountry.DataSource = dt;
                ddlCountry.DataBind();

                ddlCountry.Items.Insert(0, new ListItem("Select", "0"));
            }
            catch(Exception ex)
            {
                ExceptionHandling.SendErrorReport("AddCompany.aspx", "GetCountries", ex.Message);
            }
        }
        void GetCompanies(int ID)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objUsr.GetCompanies(ID);
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
                    ddlOrgType.SelectedValue = dt.Rows[0]["OrgTypeID"].ToString();
                    txtCompanyName.Text = dt.Rows[0]["CompanyName"].ToString();
                    ddlCountry.SelectedValue = dt.Rows[0]["Country"].ToString();
                    txtAdd.Text = dt.Rows[0]["Address"].ToString();
                    txtPhone.Text = dt.Rows[0]["PhoneNo"].ToString();
                    txtContactPersonName.Text = dt.Rows[0]["ContactPersonName"].ToString();
                    txtContactPersonEmailID.Text = dt.Rows[0]["ContactPersonEmail"].ToString();
                    txtContactPersonPhoneNo.Text = dt.Rows[0]["ContactPersonPhone"].ToString();
                    txtCompanyEmailID.Text = dt.Rows[0]["CompanyEmail"].ToString();
                    txtWebsite.Text = dt.Rows[0]["WebURL"].ToString();
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
                ExceptionHandling.SendErrorReport("AddCompany.aspx", "GetCompanies", e.Message);
            }
        }
        void SaveRecord()
        {
            try
            {
                objUsr.OrgTypeID = int.Parse(ddlOrgType.SelectedValue);
                objUsr.CompanyName = txtCompanyName.Text.Trim();
                objUsr.Country = ddlCountry.SelectedValue;
                objUsr.Address = txtAdd.Text.Trim();
                objUsr.PhoneNo = txtPhone.Text.Trim();
                objUsr.ContactPersonName = txtContactPersonName.Text.Trim();
                objUsr.ContactPersonEmail = txtContactPersonEmailID.Text.Trim();
                objUsr.ContactPersonPhone = txtContactPersonPhoneNo.Text.Trim();
                objUsr.CompanyEmail = txtCompanyEmailID.Text.Trim();
                objUsr.WebURL = txtWebsite.Text.Trim();
                if (chkActive.Checked == true)
                    objUsr.Active = 1;
                else
                    objUsr.Active = 0;
                objUsr.CreatedBy = int.Parse(Session["UserID"].ToString());
                int chk = objUsr.InsertCompany();
                if (chk > 0)
                {
                    divSuccess.Style["Display"] = "";
                    divError.Style["Display"] = "none";
                    ClearFields();
                    
                }
                else
                {
                    divSuccess.Style["Display"] = "none";
                    divError.Style["Display"] = "";
                }
            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("AddCompany.aspx", "SaveRecord", e.Message);
            }
        }
        void ClearFields()
        {
            txtCompanyName.Text = "";
            ddlCountry.SelectedIndex = 0;
            txtAdd.Text = "";
            txtPhone.Text = "";
            txtContactPersonName.Text = "";
            txtContactPersonEmailID.Text = "";
            txtContactPersonPhoneNo.Text = "";
            txtCompanyEmailID.Text = "";
            txtWebsite.Text = "";
            chkActive.Checked = false;

        }
        void EditRecord(int ID)
        {
            try
            {
                objUsr.OrgTypeID = int.Parse(ddlOrgType.SelectedValue);
                objUsr.CompanyName = txtCompanyName.Text.Trim();
                objUsr.Country = ddlCountry.SelectedValue;
                objUsr.Address = txtAdd.Text.Trim();
                objUsr.PhoneNo = txtPhone.Text.Trim();
                objUsr.ContactPersonName = txtContactPersonName.Text.Trim();
                objUsr.ContactPersonEmail = txtContactPersonEmailID.Text.Trim();
                objUsr.ContactPersonPhone = txtContactPersonPhoneNo.Text.Trim();
                objUsr.CompanyEmail = txtCompanyEmailID.Text.Trim();
                objUsr.WebURL = txtWebsite.Text.Trim();
                if (chkActive.Checked == true)
                    objUsr.Active = 1;
                else
                    objUsr.Active = 0;
                objUsr.ModifiedBy = int.Parse(Session["UserID"].ToString());
                int chk = objUsr.EditCompany(ID);
                if (chk > 0)
                {
                    divSuccess.Style["Display"] = "";
                    divError.Style["Display"] = "none";
                    ClearFields();
                    
                }
                else
                {
                    divSuccess.Style["Display"] = "none";
                    divError.Style["Display"] = "";
                }
            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("AddCompany.aspx", "EditRecord", e.Message);
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["param"] == null)
                    SaveRecord();
                else
                    EditRecord(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())));
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("AddCompany.aspx", "btnSave_Click", ex.Message);
            }
        }
    }
}