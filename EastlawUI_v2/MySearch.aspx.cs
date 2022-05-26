using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
namespace EastlawUI_v2
{
    public partial class MySearch : System.Web.UI.Page
    {
        EastLawBL.Search objsrc = new EastLawBL.Search();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GetResults(int.Parse(Session["MemberID"].ToString()));
            }
        }
        void GetResults(int UserID)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objsrc.GetSearchTxt(UserID);
                dt.Columns.Add("strFoundResult");
                for (int a = 0; a < dt.Rows.Count; a++)
                {
                    if (dt.Rows[a]["FoundResult"].ToString() == "1")
                        dt.Rows[a]["strFoundResult"] = "Yes";
                    else
                        dt.Rows[a]["strFoundResult"] = "No";
                }
                gvLst.DataSource = dt;
                gvLst.DataBind();

            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("MySearch.aspx", "GetResults", e.Message);
            }
        }

        protected void gvLst_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvLst.PageIndex = e.NewPageIndex;
                GetResults(int.Parse(Session["MemberID"].ToString()));

            }
            catch { }
        }
    }
}