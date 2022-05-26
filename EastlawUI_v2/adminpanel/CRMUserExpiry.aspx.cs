using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace EastlawUI_v2.adminpanel
{
    public partial class CRMUserExpiry : System.Web.UI.Page
    {
        EastLawBL.Users objusr = new EastLawBL.Users();
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

                GetNearUserExpiryList();
            }

        }
        void GetNearUserExpiryList()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objusr.GetNearUserExpiryList();
                gv.DataSource = dt;
                gv.DataBind();

            }
            catch (Exception ex)
            {

            }
        }
        protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gv.PageIndex = e.NewPageIndex;
                GetNearUserExpiryList();
            }
            catch (Exception ex)
            {

            }
        }
    }
}