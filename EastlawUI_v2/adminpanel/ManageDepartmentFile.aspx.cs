using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
namespace EastlawUI_v2.adminpanel
{
    public partial class ManageDepartmentFile : System.Web.UI.Page
    {
        EastLawBL.Departments objdept = new EastLawBL.Departments();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["UserID"] == null)
                {
                    Response.Redirect("default.aspx");
                }
                if (!ValidateUserGroup.ValidateGroup(int.Parse(Session["UserTypeID"].ToString()), ValidateUserGroup.getPageName(Request.Url.AbsolutePath)))
                    Response.Redirect("NotAuthorize.aspx");
                GetDeptFileDetails(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())));
            }
        }
        void GetDeptFileDetails(int ID)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objdept.GetDepartmentFileDetailsByID(ID);
                if (dt.Rows.Count > 0)
                {
                    lblID.Text = dt.Rows[0]["ID"].ToString();
                    txtDeptID.Text = dt.Rows[0]["DeptID"].ToString();
                    txtTitle.Text = dt.Rows[0]["Title"].ToString();
                    txtYear.Text = dt.Rows[0]["Year"].ToString();
                    txtNo.Text = dt.Rows[0]["No"].ToString();
                    txtDate.Text = dt.Rows[0]["DDate"].ToString();
                    txtType.Text = dt.Rows[0]["DType"].ToString();

                    editorContent.Content = dt.Rows[0]["FileContent"].ToString();
                    if (dt.Rows[0]["Active"].ToString() == "1")
                        chkActive.Checked = true;
                    else
                        chkActive.Checked = false;

                    if (dt.Rows[0]["dateformated"].ToString() == "1")
                        chkDateFormated.Checked = true;
                    else
                        chkDateFormated.Checked = false;
                }
            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("ManageDepartmentFile.aspx", "GetDeptFileDetails", e.Message);
            }
        }
        void EditRecord(int ID)
        {
            try
            {
                if (chkDateFormated.Checked == true)
                {
                    if (!ValidateDate(txtDate.Text))
                    {
                        lblDeptFileAdd.Text = "Invalid Date format.";
                        lblDeptFileAdd.ForeColor = System.Drawing.Color.Red;
                        return;
                    }
                }
                objdept.FileContent = editorContent.Content;
                
                if (chkActive.Checked == true)
                    objdept.Active = 1;
                else
                    objdept.Active = 0;

                objdept.ModifiedBy = int.Parse(Session["UserID"].ToString());
                objdept.Title = txtTitle.Text;
                objdept.Year = txtYear.Text;
                objdept.No = txtNo.Text;
                objdept.DType = txtType.Text;
                objdept.DDate = txtDate.Text;
                if (chkDateFormated.Checked == true)
                    objdept.DateFormated = 1;
                else
                    objdept.DateFormated = 0;
                int chk = objdept.EditDepartmentFile(ID);
                if (chk > 0)
                {
                    divSuccess.Style["Display"] = "";
                    divError.Style["Display"] = "none";
                  
                }
                else
                {
                    divSuccess.Style["Display"] = "none";
                    divError.Style["Display"] = "";
                }
            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("ManageJudges.aspx", "EditRecord", e.Message);
            }
        }
        bool ValidateDate(string Date)
        {
            DateTime dateValue;
            if (DateTime.TryParseExact(Date, "dd/MM/yyyy", new CultureInfo("en-US"), DateTimeStyles.None, out dateValue))
                return true;
            else
                return false;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //txtIndexTitle.Text = "";
            //editorContent.Content = "";
            //lblID.Text = "0";
            //chkActive.Checked = false;
           
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblID.Text != "0")
                   EditRecord(int.Parse(lblID.Text));
               
                   
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("ManageStatutesIndex.aspx", "btnSave_Click", ex.Message);
            }
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int chk = objdept.DeleteDepartmentFile(int.Parse(lblID.Text), int.Parse(Session["UserID"].ToString()));
                if (chk > 0)
                {
                    divSuccess.Style["Display"] = "";
                    divError.Style["Display"] = "none";
                    // ClearFields();

                }
                else
                {
                    divSuccess.Style["Display"] = "none";
                    divError.Style["Display"] = "";
                }
            }
            catch { }
        }
    }
}