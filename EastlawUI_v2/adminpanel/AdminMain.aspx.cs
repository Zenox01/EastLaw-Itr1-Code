using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace EastlawUI_v2.adminpanel
{
    public partial class AdminMain : System.Web.UI.Page
    {
        EastLawBL.ErrorReporting objer = new EastLawBL.ErrorReporting();
        EastLawBL.Cases objcase = new EastLawBL.Cases();
        EastLawBL.Journals objJo = new EastLawBL.Journals();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                if (Session["UserID"] == null)
                {
                    Response.Redirect("default.aspx");
                }

                if (!ValidateUserGroup.ValidateGroup(int.Parse(Session["UserTypeID"].ToString()), ValidateUserGroup.getPageName(Request.Url.AbsolutePath)))
                    Response.Redirect("NotAuthorize.aspx");
              //  GetPendingUsersComments();
                GetPendingReportedErrors();
                GetPendingReportedErrorsGeneral();
                GetJournals();


            }
        }
        void GetJournals()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objJo.GetActiveJournals();
                ddlrepotedErroSearchJournal.DataValueField = "ID";
                ddlrepotedErroSearchJournal.DataTextField = "JournalName";
                ddlrepotedErroSearchJournal.DataSource = dt;
                ddlrepotedErroSearchJournal.DataBind();
                ddlrepotedErroSearchJournal.Items.Insert(0, new ListItem("Select", "0"));

               

            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("ReviewCasesMigration.aspx", "GetAdvocates", e.Message);
            }
        }
        void GetPendingUsersComments()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objcase.GetPendingUsersComments();
                //dt.Columns.Add("CustomLink");
                //for (int a = 0; a < dt.Rows.Count; a++)
                //{
                //    if (dt.Rows[a]["ItemType"].ToString() == "Cases")
                //        dt.Rows[a]["CustomLink"] = "ReviewCasesMigration.aspx?param=" + EncryptDecryptHelper.Encrypt(dt.Rows[a]["ItemID"].ToString());
                //}
                dt.AcceptChanges();
                gvPendingUsersComments.DataSource = dt;
                gvPendingUsersComments.DataBind();

            }
            catch (Exception ex)
            { }
        }
        void GetPendingReportedErrors()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objer.GetPendingErrors();
                dt.Columns.Add("CustomLink");
                for (int a = 0; a < dt.Rows.Count; a++)
                {
                    if (dt.Rows[a]["ItemType"].ToString() == "Cases")
                        dt.Rows[a]["CustomLink"] = "AddCaseUpdateHistory.aspx?param=" + EncryptDecryptHelper.Encrypt(dt.Rows[a]["ItemID"].ToString());
                    if (dt.Rows[a]["ItemType"].ToString() == "Department")
                        dt.Rows[a]["CustomLink"] = "ManageDepartmentFile.aspx?param=" + EncryptDecryptHelper.Encrypt(dt.Rows[a]["ItemID"].ToString());

                }
                dt.AcceptChanges();
                gvPendingReportError.DataSource = dt;
                gvPendingReportError.DataBind();

            }
            catch(Exception ex)
            { }
        }
        void GetPendingReportedErrorsGeneral()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objer.GetPendingErrorsGeneral();
                gvMissingDataGeneral.DataSource = dt;
                gvMissingDataGeneral.DataBind();

            }
            catch (Exception ex)
            { }
        }

        protected void gvPendingReportError_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                GridViewRow row = gvPendingReportError.Rows[e.NewEditIndex];
                HiddenField hd = default(HiddenField);
                DropDownList ddlWorkFlow = default(DropDownList);
                TextBox txtAdminComment = default(TextBox);

                if ((row != null))
                {
                    hd = (HiddenField)row.FindControl("hdID");
                    int ID = Convert.ToInt32(hd.Value);

                    ddlWorkFlow = (DropDownList)row.FindControl("ddlWorkflow");
                    txtAdminComment = (TextBox)row.FindControl("txtAdminComments");

                    objer.AdminComment = txtAdminComment.Text.Trim();
                    objer.WorkflowID = int.Parse(ddlWorkFlow.SelectedValue);
                    objer.ModifiedBy = int.Parse(Session["UserID"].ToString());
                    int chk = objer.EditUpdateErrorStatus(ID);
                    if (chk > 0)
                    {
                        GetPendingReportedErrors();
                        divSuccess.Style["Display"] = "";
                        divError.Style["Display"] = "none";

                    }
                    else
                    {
                        divSuccess.Style["Display"] = "none";
                        divError.Style["Display"] = "";
                    }
                    


                }
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("AdminMain.aspx", "gvPendingReportError_RowEditing", ex.Message);
            }
        }
        protected void gvPendingUsersComments_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                GridViewRow row = gvPendingUsersComments.Rows[e.NewEditIndex];
                HiddenField hd = default(HiddenField);
                DropDownList ddlWorkFlow = default(DropDownList);
              

                if ((row != null))
                {
                    hd = (HiddenField)row.FindControl("hdID");
                    int ID = Convert.ToInt32(hd.Value);

                    ddlWorkFlow = (DropDownList)row.FindControl("ddlWorkflow");
                   

                    

                    int chk = objcase.UpdateUserComment(ID,ddlWorkFlow.SelectedValue,int.Parse(Session["UserID"].ToString()));
                    if (chk > 0)
                    {
                        GetPendingUsersComments();
                        divSuccess.Style["Display"] = "";
                        divError.Style["Display"] = "none";

                    }
                    else
                    {
                        divSuccess.Style["Display"] = "none";
                        divError.Style["Display"] = "";
                    }



                }
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("AdminMain.aspx", "gvPendingUsersComments_RowEditing", ex.Message);
            }
        }
        protected void gvMissingDataGeneral_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                GridViewRow row = gvMissingDataGeneral.Rows[e.NewEditIndex];
                HiddenField hd = default(HiddenField);
                DropDownList ddlWorkFlow = default(DropDownList);
                TextBox txtAdminComment = default(TextBox);

                if ((row != null))
                {
                    hd = (HiddenField)row.FindControl("hdID");
                    int ID = Convert.ToInt32(hd.Value);

                    ddlWorkFlow = (DropDownList)row.FindControl("ddlWorkflow");
                    txtAdminComment = (TextBox)row.FindControl("txtAdminComments");

                    objer.AdminComment = txtAdminComment.Text.Trim();
                    objer.WorkflowID = int.Parse(ddlWorkFlow.SelectedValue);
                    objer.ModifiedBy = int.Parse(Session["UserID"].ToString());
                    int chk = objer.EditUpdateErrorStatus(ID);
                    if (chk > 0)
                    {
                        GetPendingReportedErrorsGeneral();
                        divSuccess.Style["Display"] = "";
                        divError.Style["Display"] = "none";

                    }
                    else
                    {
                        divSuccess.Style["Display"] = "none";
                        divError.Style["Display"] = "";
                    }



                }
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("AdminMain.aspx", "gvPendingReportError_RowEditing", ex.Message);
            }
        }
        protected void gvPendingUsersComments_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPendingUsersComments.PageIndex = e.NewPageIndex;
            GetPendingUsersComments();
        }
        protected void gvPendingReportError_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPendingReportError.PageIndex = e.NewPageIndex;
            GetPendingReportedErrors();
        }
        protected void gvMissingDataGeneral_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMissingDataGeneral.PageIndex = e.NewPageIndex;
            GetPendingReportedErrorsGeneral();
        }
        protected void btnRepotedErroSearch_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = objer.GetPendingErrorsSearch(int.Parse(txtrepotedErroSearchYear.Text.Trim()), int.Parse(ddlrepotedErroSearchJournal.SelectedValue), txtrepotedErroSearchPageNo.Text.Trim());
            if (dt != null)
            {

                if (dt.Rows.Count > 0)
                {
                    dt.Columns.Add("CustomLink");
                    for (int a = 0; a < dt.Rows.Count; a++)
                    {
                        if (dt.Rows[a]["ItemType"].ToString() == "Cases")
                            dt.Rows[a]["CustomLink"] = "AddCaseUpdateHistory.aspx?param=" + EncryptDecryptHelper.Encrypt(dt.Rows[a]["ItemID"].ToString());
                        if (dt.Rows[a]["ItemType"].ToString() == "Department")
                            dt.Rows[a]["CustomLink"] = "ManageDepartmentFile.aspx?param=" + EncryptDecryptHelper.Encrypt(dt.Rows[a]["ItemID"].ToString());

                    }
                    dt.AcceptChanges();
                    gvPendingReportError.DataSource = dt;
                    gvPendingReportError.DataBind();
                }
            }
        }
    }
}