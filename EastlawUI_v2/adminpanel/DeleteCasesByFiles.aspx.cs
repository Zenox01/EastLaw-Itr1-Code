using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace EastlawUI_v2.adminpanel
{
    public partial class DeleteCasesByFiles : System.Web.UI.Page
    {
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
                GetJournals();
               
            }
        }
        void GetJournals()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objJo.GetActiveJournals();
                ddlJournal.DataValueField = "ID";
                ddlJournal.DataTextField = "JournalName";
                ddlJournal.DataSource = dt;
                ddlJournal.DataBind();
                ddlJournal.Items.Insert(0, new ListItem("Select", "0"));

            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("ReviewCasesMigration.aspx", "GetAdvocates", e.Message);
            }
        }
        void GetYearsByJournals(int JournalID)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objcase.GetCasesYears(JournalID);
                dt.Columns.Add("strFile");
                for (int a = 0; a < dt.Rows.Count; a++)
                {

                    dt.Rows[a]["strFile"] = dt.Rows[a]["Year"].ToString() + " (" + dt.Rows[a]["count"].ToString() + ")";

                }
                ddlYear.DataValueField = "Year";
                ddlYear.DataTextField = "strFile";
                ddlYear.DataSource = dt;
                ddlYear.DataBind();
                ddlYear.Items.Insert(0, new ListItem("Select", "0"));

            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("ReviewCasesMigration.aspx", "GetAdvocates", e.Message);
            }
        }
        void GetFiles(int JournalID,int Year)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objcase.GetCasesFilesByJournalAndYear(JournalID,Year);
                dt.Columns.Add("strFile");
                for (int a = 0; a < dt.Rows.Count; a++)
                {

                    dt.Rows[a]["strFile"] = dt.Rows[a]["uploadfilename"].ToString() +" (" + dt.Rows[a]["CreatedOn"].ToString() + ")" +" (" + dt.Rows[a]["count"].ToString() + ")";
                    
                }
                dt.AcceptChanges();
                ddlFileName.DataValueField = "uploadfilename";
                ddlFileName.DataTextField = "strFile";
                ddlFileName.DataSource = dt;
                ddlFileName.DataBind();
                ddlFileName.Items.Insert(0, new ListItem("Select", "0"));

            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("DeleteCasesByFiles.aspx", "GetFiles", e.Message);
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int chk = objcase.DeleteCasesByFileName(ddlFileName.SelectedValue, int.Parse(Session["UserID"].ToString()),int.Parse(ddlJournal.SelectedValue),int.Parse(ddlYear.SelectedValue));
                if (chk > 0)
                {
                    divSuccess.Style["Display"] = "";
                    divSuccess.InnerHtml = "File: " + ddlFileName.SelectedValue + " Deleted Sucussfully.";
                    divError.Style["Display"] = "none";
                    GetFiles(int.Parse(ddlJournal.SelectedValue),int.Parse(ddlYear.SelectedValue));
                }
                else
                {
                    divSuccess.Style["Display"] = "none";
                    divError.Style["Display"] = "";
                }
            }
            catch(Exception ex)
            {
                ExceptionHandling.SendErrorReport("DeleteCasesByFiles.aspx", "btnDelete_Click", ex.Message);
            }
        }

        protected void ddlJournal_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetYearsByJournals(int.Parse(ddlJournal.SelectedValue));
            }
            catch { }
        }

        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetFiles(int.Parse(ddlJournal.SelectedValue),int.Parse(ddlYear.SelectedValue));
            }
            catch { }
        }

        protected void btnPublish_Click(object sender, EventArgs e)
        {
            try
            {
                int chk = objcase.Publish_UnpublishCasesByFileName(1, ddlFileName.SelectedValue, int.Parse(Session["UserID"].ToString()), int.Parse(ddlJournal.SelectedValue), int.Parse(ddlYear.SelectedValue));
                if (chk > 0)
                {
                    divSuccess.Style["Display"] = "";
                    divSuccess.InnerHtml = "File: " + ddlFileName.SelectedValue + " Published Sucussfully.";
                    divError.Style["Display"] = "none";
                    
                }
                else
                {
                    divSuccess.Style["Display"] = "none";
                    divError.Style["Display"] = "";
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        protected void btnUnPublish_Click(object sender, EventArgs e)
        {
            try
            {
                int chk = objcase.Publish_UnpublishCasesByFileName(0,ddlFileName.SelectedValue, int.Parse(Session["UserID"].ToString()), int.Parse(ddlJournal.SelectedValue), int.Parse(ddlYear.SelectedValue));
                if (chk > 0)
                {
                    divSuccess.Style["Display"] = "";
                    divSuccess.InnerHtml = "File: " + ddlFileName.SelectedValue + " Un-Published Sucussfully.";
                    divError.Style["Display"] = "none";
                    
                }
                else
                {
                    divSuccess.Style["Display"] = "none";
                    divError.Style["Display"] = "";
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("DeleteCasesByFiles.aspx", "btnDelete_Click", ex.Message);
            }
        }
    }
}