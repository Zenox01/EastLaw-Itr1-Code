using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ClosedXML.Excel;
using System.IO;

namespace EastLawUI.adminpanel.usercontrol
{
    public partial class usersearch_actionpanel : System.Web.UI.UserControl
    {
        EastLawBL.Users objUsr = new EastLawBL.Users();
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        void UsersSearch()
        {
            try
            {
                string cri = "Select A.*,DATEDIFF(DAY, getdate(),CONVERT(datetime,A.AccessExpireOn)) as ExpireIn,CONVERT(datetime,A.AccessExpireOn) as FormatedExpire,E.UserType,B.Name as CountryName,C.PlanName,C.NoofDays,D.CompanyName ,(select top 1 convert(varchar(101), Createdon)  from tbl_auditlog where activitytype='Login/Logout' and [Action] like '%Success%' and userid=A.ID) as FirstLogin from dbo.tbl_Users A"
            + " inner join Country B on A.Country=B.Code"
            + " inner join tbl_Plans C on A.PlanID=C.ID"
            + " inner join tbl_Companies D on A.CompanyID=D.ID"
            + " inner join dbo.tbl_UserType E on A.UserTypeID=E.ID Where A.CreatedOn is not null and A.isdeleted=0";



                if (!string.IsNullOrEmpty(txtSearchEmailID.Text.Trim()))
                    cri = cri + " AND  A.EmailID='" + txtSearchEmailID.Text.Trim() + "' ";

                if (!string.IsNullOrEmpty(txSearchtMobileNo.Text.Trim()))
                    cri = cri + " AND  A.PhoneNo='" + txSearchtMobileNo.Text.Trim() + "' ";

                cri = cri + " union all "
            + " Select A.*,DATEDIFF(DAY, getdate(),CONVERT(datetime,A.AccessExpireOn)) as ExpireIn,CONVERT(datetime,A.AccessExpireOn),D.UserType,B.Name as CountryName,C.PlanName,C.NoofDays,'' as CompanyName,(select top 1 convert(varchar(101), Createdon)  from tbl_auditlog where activitytype='Login/Logout' and [Action] like '%Success%' and userid=A.ID) as FirstLogin  from dbo.tbl_Users A"
            + " inner join Country B on A.Country=B.Code"
            + " inner join tbl_Plans C on A.PlanID=C.ID"
            + " inner join dbo.tbl_UserType D on A.UserTypeID=D.ID  Where A.CreatedOn is not null and A.isdeleted=0";

                //if (ddlCiJournal.SelectedValue != "0" && ddlCYear.SelectedValue != "0" && txtCNumber.Text != "")
                //    cri = cri + " AND E.JournalName='" + ddlCiJournal.SelectedValue + "' AND A.Year=" + ddlCYear.SelectedValue + " AND A.Citation like '%" + txtCNumber.Text.Trim() + "%'";

                if (!string.IsNullOrEmpty(txtSearchEmailID.Text.Trim()))
                    cri = cri + " AND  A.EmailID='" + txtSearchEmailID.Text.Trim() + "' ";

                if (!string.IsNullOrEmpty(txSearchtMobileNo.Text.Trim()))
                    cri = cri + " AND  A.PhoneNo='" + txSearchtMobileNo.Text.Trim() + "' ";

                DataTable dt = new DataTable();
                dt = objUsr.GetUsersSearchBackendOpenQuery(cri);
                if (dt != null)
                {
                    if (dt.Rows.Count == 1)
                    {

                        trActions.Style["Display"] = "";
                       
                        lblID.Text = dt.Rows[0]["ID"].ToString();
                        lblUserName.Text = dt.Rows[0]["FullName"].ToString();
                        lblEmailID.Text = dt.Rows[0]["EmailID"].ToString();
                        lblMobNo.Text = dt.Rows[0]["PhoneNo"].ToString();
                        //Response.Redirect("UpdateUserStatus.aspx?param=" + EncryptDecryptHelper.Encrypt(dt.Rows[0]["ID"].ToString()));
                        lblMsg.Text = "";
                    }
                    else
                    {

                        trActions.Style["Display"] = "none";
                        lblID.Text = "0";
                        lblUserName.Text = "";
                        lblEmailID.Text = "";
                        lblMobNo.Text = "";
                        lblMsg.Text = "User not found.";
                        lblMsg.ForeColor = System.Drawing.Color.Red;

                    }
                }

            }
            catch (Exception ex)
            {

            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            UsersSearch();
            
        }

        protected void btnUpdateStatus_Click(object sender, EventArgs e)
        {
            if (lblID.Text !="0")
                Response.Redirect("UpdateUserStatus.aspx?param=" + EncryptDecryptHelper.Encrypt(lblID.Text.ToString()));
        }

        protected void btnUpdatePlan_Click(object sender, EventArgs e)
        {
            if (lblID.Text != "0")
                Response.Redirect("UpdatePlanBackend.aspx?param=" + EncryptDecryptHelper.Encrypt(lblID.Text.ToString()));
        }

        protected void btnResetPassword_Click(object sender, EventArgs e)
        {
            if (lblID.Text != "0")
                Response.Redirect("ResetUserPassword.aspx?param=" + EncryptDecryptHelper.Encrypt(lblID.Text.ToString()));
        }

        protected void btnHistory_Click(object sender, EventArgs e)
        {
            if (lblID.Text != "0")
            {
                DataTable dt = new DataTable();
                dt = objUsr.GetUserHistory(int.Parse(lblID.Text.ToString()));
                if (dt != null)
                {
                    ExportExcel_XML(dt, "Users_History_Report_" + DateTime.Now.Date.ToString("dd/MM/yyy"));
                }
            }
        }
        public void ExportExcel_XML(DataTable dt, String FileName)
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=" + FileName + ".xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    //Response.End();
                    HttpContext.Current.ApplicationInstance.CompleteRequest(); //This would bypass the Application_EndRequest
                }
            }
        }
    }
}