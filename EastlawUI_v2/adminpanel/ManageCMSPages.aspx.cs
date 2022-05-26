using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace EastlawUI_v2.adminpanel
{
    public partial class ManageCMSPages : System.Web.UI.Page
    {
        EastLawBL.Pages objpages = new EastLawBL.Pages();
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
                GetPages(0);
            }
        }
        void GetPages(int ID)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objpages.GetPages(ID);
                if (ID == 0)
                {
                    ddlPages.DataValueField = "ID";
                    ddlPages.DataTextField = "PageName";
                    ddlPages.DataSource = dt;
                    ddlPages.DataBind();
                    ddlPages.Items.Insert(0, new ListItem("Select", "0"));
                }
                else
                {
                    txtTitle.Text = dt.Rows[0]["Title"].ToString();
                    txtKeywords.Text = dt.Rows[0]["Keywords"].ToString();
                    editorShortContent.Content = dt.Rows[0]["ShortContent"].ToString();
                    editorFullContent.Content = dt.Rows[0]["FullContent"].ToString();
                }

            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("ManageCMSPages.aspx", "GetAdvocates", e.Message);
            }
        }
        void ClearFields()
        {
            ddlPages.SelectedIndex = 0;
            txtTitle.Text = "";
            txtKeywords.Text = "";
            editorShortContent.Content = "";
            editorFullContent.Content = "";
            
        }
        void EditRecord(int ID)
        {
            try
            {
                objpages.Title = txtTitle.Text.Trim();
                objpages.Keywords = txtKeywords.Text.Trim();
                objpages.ShortContent = editorShortContent.Content;
                objpages.FullContent = editorFullContent.Content;
                objpages.ModifiedBy = int.Parse(Session["UserID"].ToString());
                int chk = objpages.EditPage(ID);
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
                ExceptionHandling.SendErrorReport("ManageCMSPages.aspx", "EditRecord", e.Message);
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                    EditRecord(int.Parse(ddlPages.SelectedValue));
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("ManageCMSPages.aspx", "btnSave_Click", ex.Message);
            }
        }

        protected void ddlPages_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetPages(int.Parse(ddlPages.SelectedValue));
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("ManageCMSPages.aspx", "ddlPages_SelectedIndexChanged", ex.Message);
            }
        }
    }
}