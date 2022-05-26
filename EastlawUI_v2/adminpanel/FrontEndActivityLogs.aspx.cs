using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace EastlawUI_v2.adminpanel
{
    public partial class FrontEndActivityLogs : System.Web.UI.Page
    {
        EastLawBL.Users objUsr = new EastLawBL.Users();
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
                GetAuditLogTypes();
                GetAuditLogs(0);
            }
        }
        void GetAuditLogs(int ID)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objUsr.GetAuditLog(ID);
                if (ID == 0)
                {
                    dt.AcceptChanges();
                    gv.DataSource = dt;
                    gv.DataBind();
                }
                else
                {


                }
            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("FrontEndActivityLogs.aspx", "GetAuditLogs", e.Message);
            }
        }
        void GetAuditLogsByType(string ActivityType)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objUsr.GetAuditLogByActivityTypes(ActivityType,DateTime.Now.Date.ToString("MM/dd/yyy"), DateTime.Now.Date.ToString("MM/dd/yyy"));
                
                    dt.AcceptChanges();
                    gv.DataSource = dt;
                    gv.DataBind();
               
            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("FrontEndActivityLogs.aspx", "GetAuditLogsByType", e.Message);
            }
        }
        void GetAuditLogTypes()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objUsr.GetAuditLogTypes();
                ddlLogType.DataValueField = "ActivityType";
                ddlLogType.DataTextField = "ActivityType";
                ddlLogType.DataSource = dt;
                ddlLogType.DataBind();
                ddlLogType.Items.Insert(0, new ListItem("Select", "0"));
            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("FrontEndActivityLogs.aspx", "GetAuditLogs", e.Message);
            }
        }
        protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gv.PageIndex = e.NewPageIndex;
                if (ddlLogType.SelectedValue != "0")
                {
                    GetAuditLogsByType(ddlLogType.SelectedValue);
                }
                else
                {
                    GetAuditLogs(0);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("FrontEndActivityLogs.aspx", "gv_PageIndexChanging", ex.Message);
            }
        }

        protected void ddlLogType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlLogType.SelectedValue != "0")
            {
                GetAuditLogsByType(ddlLogType.SelectedValue);
            }
            else
            {
                GetAuditLogs(0);
            }
        }
    }
}