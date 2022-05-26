using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ClosedXML.Excel;
using System.IO;

namespace EastlawUI_v2.companyadminpanel
{
    public partial class ManageUsers : System.Web.UI.Page
    {
        public string SortDireaction
        {
            get
            {
                if (ViewState["SortDireaction"] == null)
                    return string.Empty;
                else
                    return ViewState["SortDireaction"].ToString();
            }
            set
            {
                ViewState["SortDireaction"] = value;
            }
        }
        private string _sortDirection;
        EastLawBL.Users objUsr = new EastLawBL.Users();
        EastLawBL.Plans objPlan = new EastLawBL.Plans();
        DataTable dt = new DataTable();
        Image sortImage = new Image();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GetCompanyUsers(int.Parse(Session["CompanyID"].ToString()));
            }
        }
        void GetCompanyUsers(int CompanyID)
        {
            try
            {

                dt = objUsr.GetUsersByCompany(CompanyID);
                
                    dt.Columns.Add("strActive");
                    dt.Columns.Add("CustNo");
                    for (int a = 0; a < dt.Rows.Count; a++)
                    {
                        if (dt.Rows[a]["Active"].ToString() == "1")
                            dt.Rows[a]["strActive"] = "Yes";
                        else
                            dt.Rows[a]["strActive"] = "No";

                        dt.Rows[a]["CustNo"] = dt.Rows[a]["ID"].ToString().PadLeft(6, '0');
                    }
                    dt.AcceptChanges();
                    DataView dv = dt.DefaultView;
                    if (this.ViewState["SortExpression"] != null)
                    {
                        dv.Sort = string.Format("{0} {1}", ViewState["SortExpression"].ToString(), this.ViewState["SortOrder"].ToString());
                    }
                    gv.DataSource = dt;
                    gv.DataBind();
               
            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("ManageUsers.aspx", "GetUser", e.Message);
            }
        }
    

        protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gv.PageIndex = e.NewPageIndex;
                GetCompanyUsers(int.Parse(Session["CompanyID"].ToString()));
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("ManageUsers.aspx", "gv_PageIndexChanging", ex.Message);
            }
        }

        protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                GridViewRow row = gv.Rows[e.RowIndex];
                HiddenField hd = default(HiddenField);

                if ((row != null))
                {
                    hd = (HiddenField)row.FindControl("hdID");
                    int ID = Convert.ToInt32(hd.Value);

                    int chk = objUsr.DeleteUser(ID, int.Parse(Session["CompanyAdminID"].ToString()));
                    if (chk > 0)
                    {
                       
                        string strMessage = "Deleted Successfully";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup('" + strMessage + "');", true);
                    }
                    else
                    {
                       

                        string strMessage = "Deletion Failed";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup('" + strMessage + "');", true);
                    }
                }
                gv.EditIndex = -1;
                GetCompanyUsers(int.Parse(Session["CompanyID"].ToString()));

            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("ManageUsers.aspx", "gv_RowDeleting", ex.Message);
            }
        }

        protected void gv_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                GridViewRow row = gv.Rows[e.NewEditIndex];
                HiddenField hd = default(HiddenField);

                if ((row != null))
                {
                    hd = (HiddenField)row.FindControl("hdID");
                    int ID = Convert.ToInt32(hd.Value);

                    Response.Redirect("add-user/?param=" + EncryptDecryptHelper.Encrypt(ID.ToString()));



                }
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("ManageUsers.aspx", "gv_RowEditing", ex.Message);
            }
        }

        protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                ImageButton imgBtn = default(ImageButton);
                string script = null;
                script = "";

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    imgBtn = (ImageButton)e.Row.Controls[0].FindControl("ibtnDelete");
                    script = "javascript:return(confirm_delete())";
                    imgBtn.Attributes.Add("onclick", script);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("ManageUsers.aspx", "gv_RowDataBound", ex.Message);
            }
        }
        protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "UpdatePlan")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow row = gv.Rows[index];
                    //HiddenField hdnField = (HiddenField)row.FindControl("hdID");
                    string val = (string)this.gv.DataKeys[index]["Id"].ToString();
                 //   Response.Redirect("ManageUserPlanUpdate.aspx?param=" + EncryptDecryptHelper.Encrypt(val.ToString()));
                }
                else if (e.CommandName == "ResetPassword")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow row = gv.Rows[index];
                    //HiddenField hdnField = (HiddenField)row.FindControl("hdID");
                    string val = (string)this.gv.DataKeys[index]["Id"].ToString();
                    Response.Redirect("/corporate/password-reset?param=" + EncryptDecryptHelper.Encrypt(val.ToString()));
                }

            }
            catch (Exception ex)
            {

            }
        }

        protected void gv_RowCommand1(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "UpdatePlan")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow row = gv.Rows[index];
                    //HiddenField hdnField = (HiddenField)row.FindControl("hdID");
                    string val = (string)this.gv.DataKeys[index]["Id"].ToString();
                    Response.Redirect("UpdatePlanBackend.aspx?param=" + EncryptDecryptHelper.Encrypt(val.ToString()));
                }
                if (e.CommandName == "UpdateStatus")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow row = gv.Rows[index];
                    //HiddenField hdnField = (HiddenField)row.FindControl("hdID");
                    string val = (string)this.gv.DataKeys[index]["Id"].ToString();
                    Response.Redirect("UpdateUserStatus.aspx?param=" + EncryptDecryptHelper.Encrypt(val.ToString()));
                }
                if (e.CommandName == "ResetPassword")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow row = gv.Rows[index];
                    //HiddenField hdnField = (HiddenField)row.FindControl("hdID");
                    string val = (string)this.gv.DataKeys[index]["Id"].ToString();
                    
                    Response.Redirect("/corporate/password-reset?param=" + EncryptDecryptHelper.Encrypt(val.ToString()));
                }
                if (e.CommandName == "History")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    //GridViewRow row = gv.Rows[index];
                    ////HiddenField hdnField = (HiddenField)row.FindControl("hdID");
                    //string val = (string)this.gv.DataKeys[index]["Id"].ToString();

                    //DataTable dt = new DataTable();
                    //dt = objUsr.GetUserHistory(int.Parse(val.ToString()));
                    //if (dt != null)
                    //{
                    //    ExportExcel_XML(dt, "Users_History_Report_" + DateTime.Now.Date.ToString("dd/MM/yyy"));
                    //}
                }
                if (e.CommandName.Equals("Sort"))
                {
                    if (ViewState["SortExpression"] != null)
                    {
                        if (this.ViewState["SortExpression"].ToString() == e.CommandArgument.ToString())
                        {
                            if (ViewState["SortOrder"].ToString() == "ASC")
                                ViewState["SortOrder"] = "DESC";
                            else
                                ViewState["SortOrder"] = "ASC";
                        }
                        else
                        {
                            ViewState["SortOrder"] = "ASC";
                            ViewState["SortExpression"] = e.CommandArgument.ToString();
                        }

                    }
                    else
                    {
                        ViewState["SortExpression"] = e.CommandArgument.ToString();
                        ViewState["SortOrder"] = "ASC";
                    }
                    GetCompanyUsers(int.Parse(Session["CompanyID"].ToString()));
                }
                

            }
            catch (Exception ex)
            {

            }
        }

        protected void gv_Sorting(object sender, GridViewSortEventArgs e)
        {
            SetSortDirection(SortDireaction);
            if (dt != null)
            {
                //Sort the data.
                dt.DefaultView.Sort = e.SortExpression + " " + _sortDirection;
                gv.DataSource = dt;
                gv.DataBind();
                SortDireaction = _sortDirection;
                int columnIndex = 0;
                foreach (DataControlFieldHeaderCell headerCell in gv.HeaderRow.Cells)
                {
                    if (headerCell.ContainingField.SortExpression == e.SortExpression)
                    {
                        columnIndex = gv.HeaderRow.Cells.GetCellIndex(headerCell);
                    }
                }

                gv.HeaderRow.Cells[columnIndex].Controls.Add(sortImage);
            }
        }
        protected void SetSortDirection(string sortDirection)
        {
            if (sortDirection == "ASC")
            {
                _sortDirection = "DESC";
                sortImage.ImageUrl = "media/img/view_sort_ascending.png";

            }
            else
            {
                _sortDirection = "ASC";
                sortImage.ImageUrl = "media/img/view_sort_descending.png";
            }
        }
        //void UsersSearch()
        //{
        //    try
        //    {
        //        string cri = "Select A.*,DATEDIFF(DAY, getdate(),CONVERT(datetime,A.AccessExpireOn)) as ExpireIn,CONVERT(datetime,A.AccessExpireOn) as FormatedExpire,E.UserType,B.Name as CountryName,C.PlanName,C.NoofDays,D.CompanyName ,(select top 1 convert(varchar(101), Createdon)  from tbl_auditlog where activitytype='Login/Logout' and [Action] like '%Success%' and userid=A.ID) as FirstLogin from dbo.tbl_Users A"
        //    + " inner join Country B on A.Country=B.Code"
        //    + " inner join tbl_Plans C on A.PlanID=C.ID"
        //    + " inner join tbl_Companies D on A.CompanyID=D.ID"
        //    + " inner join dbo.tbl_UserType E on A.UserTypeID=E.ID Where A.CreatedOn is not null and A.isdeleted=0";

        //        if (ddlUserType.SelectedValue != "0")
        //            cri = cri + " AND A.UserTypeID=" + ddlUserType.SelectedValue + "";

        //        if (ddlStatus.SelectedValue != "0")
        //            cri = cri + " AND A.Status Like '" + ddlStatus.SelectedValue + "' ";
        //        //cri = cri + " AND A.Status=''" + ddlStatus.SelectedValue + "''";

        //        if (ddlPlan.SelectedValue != "0")
        //            cri = cri + " AND A.PlanID=" + ddlPlan.SelectedValue + "";


        //        if (!string.IsNullOrEmpty(txtName.Text.Trim()))
        //            cri = cri + " AND A.FullName Like '%" + txtName.Text.Trim() + "%' ";

        //        if (!string.IsNullOrEmpty(txtEmailID.Text.Trim()))
        //            cri = cri + " AND  A.EmailID='" + txtEmailID.Text + "' ";

        //        if (!string.IsNullOrEmpty(txtMobileNo.Text.Trim()))
        //            cri = cri + " AND  A.PhoneNo='" + txtMobileNo.Text + "' ";

        //        cri = cri + " union all "
        //    + " Select A.*,DATEDIFF(DAY, getdate(),CONVERT(datetime,A.AccessExpireOn)) as ExpireIn,CONVERT(datetime,A.AccessExpireOn),D.UserType,B.Name as CountryName,C.PlanName,C.NoofDays,'' as CompanyName,(select top 1 convert(varchar(101), Createdon)  from tbl_auditlog where activitytype='Login/Logout' and [Action] like '%Success%' and userid=A.ID) as FirstLogin  from dbo.tbl_Users A"
        //    + " inner join Country B on A.Country=B.Code"
        //    + " inner join tbl_Plans C on A.PlanID=C.ID"
        //    + " inner join dbo.tbl_UserType D on A.UserTypeID=D.ID  Where A.CreatedOn is not null and A.isdeleted=0";

        //        //if (ddlCiJournal.SelectedValue != "0" && ddlCYear.SelectedValue != "0" && txtCNumber.Text != "")
        //        //    cri = cri + " AND E.JournalName='" + ddlCiJournal.SelectedValue + "' AND A.Year=" + ddlCYear.SelectedValue + " AND A.Citation like '%" + txtCNumber.Text.Trim() + "%'";

        //        if (ddlUserType.SelectedValue != "0")
        //            cri = cri + " AND A.UserTypeID=" + ddlUserType.SelectedValue + "";

        //        if (ddlStatus.SelectedValue != "0")
        //            cri = cri + " AND A.Status Like '" + ddlStatus.SelectedValue + "' ";
        //        //cri = cri + " AND A.Status=''" + ddlStatus.SelectedValue + "''";

        //        if (ddlPlan.SelectedValue != "0")
        //            cri = cri + " AND A.PlanID=" + ddlPlan.SelectedValue + "";


        //        if (!string.IsNullOrEmpty(txtName.Text.Trim()))
        //            cri = cri + " AND A.FullName Like '%" + txtName.Text.Trim() + "%' ";

        //        if (!string.IsNullOrEmpty(txtEmailID.Text.Trim()))
        //            cri = cri + " AND  A.EmailID='" + txtEmailID.Text + "' ";

        //        if (!string.IsNullOrEmpty(txtMobileNo.Text.Trim()))
        //            cri = cri + " AND  A.PhoneNo='" + txtMobileNo.Text + "' ";

        //        DataTable dt = new DataTable();
        //        dt = objUsr.GetUsersSearchBackendOpenQuery(cri);
        //        if (dt != null)
        //        {
        //            if (dt.Rows.Count > 0)
        //            {
        //                dt.Columns.Add("strActive");
        //                dt.Columns.Add("CustNo");
        //                for (int a = 0; a < dt.Rows.Count; a++)
        //                {
        //                    if (dt.Rows[a]["Active"].ToString() == "1")
        //                        dt.Rows[a]["strActive"] = "Yes";
        //                    else
        //                        dt.Rows[a]["strActive"] = "No";

        //                    dt.Rows[a]["CustNo"] = dt.Rows[a]["ID"].ToString().PadLeft(6, '0');
        //                }
        //                dt.AcceptChanges();
        //                DataView dv = dt.DefaultView;
        //                if (this.ViewState["SortExpression"] != null)
        //                {
        //                    dv.Sort = string.Format("{0} {1}", ViewState["SortExpression"].ToString(), this.ViewState["SortOrder"].ToString());
        //                }
        //                gv.DataSource = dt;
        //                gv.DataBind();
        //            }
        //            else
        //            {
        //                gv.DataSource = null;
        //                gv.DataBind();
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
        //public void ExportExcel_XML(DataTable dt, String FileName)
        //{
        //    using (XLWorkbook wb = new XLWorkbook())
        //    {
        //        wb.Worksheets.Add(dt);

        //        Response.Clear();
        //        Response.Buffer = true;
        //        Response.Charset = "";
        //        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //        Response.AddHeader("content-disposition", "attachment;filename=" + FileName + ".xlsx");
        //        using (MemoryStream MyMemoryStream = new MemoryStream())
        //        {
        //            wb.SaveAs(MyMemoryStream);
        //            MyMemoryStream.WriteTo(Response.OutputStream);
        //            Response.Flush();
        //            //Response.End();
        //            HttpContext.Current.ApplicationInstance.CompleteRequest(); //This would bypass the Application_EndRequest
        //        }
        //    }
        //}
        //protected void btnAll_Click(object sender, EventArgs e)
        //{
        //    GetUser(0);
        //}

        //protected void btnSearch_Click(object sender, EventArgs e)
        //{
        //    UsersSearch();

        //}

        //protected void btnAllExportExcel_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        DataTable dt = new DataTable();
        //        dt = objUsr.GetUsers(0);

        //        //    dt.Columns.Add("strActive");
        //        //    for (int a = 0; a < dt.Rows.Count; a++)
        //        //    {
        //        //        if (dt.Rows[a]["Active"].ToString() == "1")
        //        //            dt.Rows[a]["strActive"] = "Yes";
        //        //        else
        //        //            dt.Rows[a]["strActive"] = "No";
        //        //    }
        //        //dt.AcceptChanges();

        //        dt.Columns.Add("strActive");
        //        dt.Columns.Add("CustNo");
        //        for (int a = 0; a < dt.Rows.Count; a++)
        //        {
        //            if (dt.Rows[a]["Active"].ToString() == "1")
        //                dt.Rows[a]["strActive"] = "Yes";
        //            else
        //                dt.Rows[a]["strActive"] = "No";

        //            dt.Rows[a]["CustNo"] = dt.Rows[a]["ID"].ToString().PadLeft(6, '0');
        //        }
        //        dt.AcceptChanges();

        //        dt.Columns.Remove("ID");
        //        dt.Columns.Remove("UserTypeID");
        //        dt.Columns.Remove("OrgTypeID");
        //        dt.Columns.Remove("Pwd");
        //        dt.Columns.Remove("PlanID");
        //        dt.Columns.Remove("CompanyID");
        //        dt.Columns.Remove("CreatedBy");
        //        dt.Columns.Remove("ModifiedBy");
        //        dt.AcceptChanges();
        //        ExportExcel_XML(dt, "eastlaw_users");

        //    }
        //    catch { }
        //}

        //protected void btnExportSearchExcel_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string cri = "Select A.*,E.UserType,B.Name as CountryName,C.PlanName,C.NoofDays,D.CompanyName from dbo.tbl_Users A"
        //    + " inner join Country B on A.Country=B.Code"
        //    + " inner join tbl_Plans C on A.PlanID=C.ID"
        //    + " inner join tbl_Companies D on A.CompanyID=D.ID"
        //    + " inner join dbo.tbl_UserType E on A.UserTypeID=E.ID"
        //    + " union all "
        //    + " Select A.*,D.UserType,B.Name as CountryName,C.PlanName,C.NoofDays,'' as CompanyName from dbo.tbl_Users A"
        //    + " inner join Country B on A.Country=B.Code"
        //    + " inner join tbl_Plans C on A.PlanID=C.ID"
        //    + " inner join dbo.tbl_UserType D on A.UserTypeID=D.ID  Where A.CreatedOn is not null";

        //        //if (ddlCiJournal.SelectedValue != "0" && ddlCYear.SelectedValue != "0" && txtCNumber.Text != "")
        //        //    cri = cri + " AND E.JournalName='" + ddlCiJournal.SelectedValue + "' AND A.Year=" + ddlCYear.SelectedValue + " AND A.Citation like '%" + txtCNumber.Text.Trim() + "%'";

        //        if (ddlUserType.SelectedValue != "0")
        //            cri = cri + " AND A.UserTypeID=" + ddlUserType.SelectedValue + "";

        //        if (ddlStatus.SelectedValue != "0")
        //            cri = cri + " AND A.Status Like '" + ddlStatus.SelectedValue + "' ";
        //        //cri = cri + " AND A.Status=''" + ddlStatus.SelectedValue + "''";

        //        if (ddlPlan.SelectedValue != "0")
        //            cri = cri + " AND A.PlanID=" + ddlPlan.SelectedValue + "";


        //        if (!string.IsNullOrEmpty(txtName.Text.Trim()))
        //            cri = cri + " AND A.FullName Like '%" + txtName.Text.Trim() + "%' ";

        //        if (!string.IsNullOrEmpty(txtEmailID.Text.Trim()))
        //            cri = cri + " AND  A.EmailID='" + txtEmailID.Text + "' ";

        //        if (!string.IsNullOrEmpty(txtMobileNo.Text.Trim()))
        //            cri = cri + " AND  A.PhoneNo='" + txtMobileNo.Text + "' ";

        //        DataTable dt = new DataTable();
        //        dt = objUsr.GetUsersSearchBackendOpenQuery(cri);
        //        if (dt != null)
        //        {
        //            if (dt.Rows.Count > 0)
        //            {
        //                dt.Columns.Add("strActive");
        //                dt.Columns.Add("CustNo");
        //                for (int a = 0; a < dt.Rows.Count; a++)
        //                {
        //                    if (dt.Rows[a]["Active"].ToString() == "1")
        //                        dt.Rows[a]["strActive"] = "Yes";
        //                    else
        //                        dt.Rows[a]["strActive"] = "No";

        //                    dt.Rows[a]["CustNo"] = dt.Rows[a]["ID"].ToString().PadLeft(6, '0');
        //                }
        //                dt.AcceptChanges();
        //                dt.Columns.Remove("ID");
        //                dt.Columns.Remove("UserTypeID");
        //                dt.Columns.Remove("OrgTypeID");
        //                dt.Columns.Remove("Pwd");
        //                dt.Columns.Remove("PlanID");
        //                dt.Columns.Remove("CompanyID");
        //                dt.Columns.Remove("CreatedBy");
        //                dt.Columns.Remove("ModifiedBy");
        //                dt.AcceptChanges();
        //                ExportExcel_XML(dt, "eastlaw_users");
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //}
    }
}