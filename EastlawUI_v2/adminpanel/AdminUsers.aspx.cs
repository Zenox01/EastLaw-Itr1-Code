using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace EastlawUI_v2.adminpanel
{
    public partial class AdminUsers : System.Web.UI.Page
    {
        EastLawBL.Users objusr = new EastLawBL.Users();
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
                GetPendingInvoices();
                BindUserLoginChart();
                BindUserRegChart();
            }
        }
        void GetPendingInvoices()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objusr.GetPendingInvoices();
                dt.Columns.Add("OrdNo");
                for (int a = 0; a < dt.Rows.Count; a++)
                {
                    dt.Rows[a]["OrdNo"] = dt.Rows[a]["ID"].ToString().PadLeft(6, '0');
                }
                dt.AcceptChanges();
                gvPendingInvoices.DataSource = dt;
                gvPendingInvoices.DataBind();
            }
            catch { }
        }
        protected void gvPendingInvoices_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                GridViewRow row = gvPendingInvoices.Rows[e.NewEditIndex];
                HiddenField hd = default(HiddenField);
                HiddenField hdUserID = default(HiddenField);
                HiddenField hdPlanID = default(HiddenField);
                

                if ((row != null))
                {
                    hd = (HiddenField)row.FindControl("hdID");
                    int ID = Convert.ToInt32(hd.Value);

                    hdUserID = (HiddenField)row.FindControl("hdUserID");
                    hdPlanID = (HiddenField)row.FindControl("hdPlanID");

                    Response.Redirect("ManageUserPlanUpdate.aspx?param=" + EncryptDecryptHelper.Encrypt(hdUserID.Value.ToString())+"&inv="+EncryptDecryptHelper.Encrypt(hd.Value.ToString())+"&pl="+EncryptDecryptHelper.Encrypt(hdPlanID.Value.ToString()));



                }
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("AdminMain.aspx", "gvPendingReportError_RowEditing", ex.Message);
            }
        }

        protected void gvPendingInvoices_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPendingInvoices.PageIndex = e.NewPageIndex;
            GetPendingInvoices();
        }

        protected void gvPendingInvoices_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                GridViewRow row = gvPendingInvoices.Rows[e.RowIndex];
                HiddenField hd = default(HiddenField);

                if ((row != null))
                {
                    hd = (HiddenField)row.FindControl("hdID");
                    int ID = Convert.ToInt32(hd.Value);

                    int chk = objusr.UpdateInvoiceStatus(ID,"Reject", int.Parse(Session["UserID"].ToString()),"");
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
                gvPendingInvoices.EditIndex = -1;
                GetPendingInvoices();

            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("AdminUsers.aspx", "gv_RowDeleting", ex.Message);
            }
        }
        void BindUserLoginChart()
        {
            DataTable dt = new DataTable();
            dt = objusr.UserLoginChart();
            radChartUserLogin.DataSource = dt;
            radChartUserLogin.DataBind();
        }
        void BindUserRegChart()
        {
            DataTable dt = new DataTable();
            dt = objusr.UserRegistrationChart();
            radChartUserRegistration.DataSource = dt;
            radChartUserRegistration.DataBind();
        }
        private DataSet GetData1()
        {
            DataSet ds = new DataSet("Bookstore");
            DataTable dt = new DataTable("Products");
            dt.Columns.Add("Id", Type.GetType("System.Int32"));
            dt.Columns.Add("Name", Type.GetType("System.String"));
            dt.Columns.Add("Price", Type.GetType("System.Double"));

            dt.Rows.Add(1, "12/Dec/2016", 5.45);
            dt.Rows.Add(2, "12/Dec/2016", 9.95);
            dt.Rows.Add(3, "5/Dec/2016", 1.99);
            dt.Rows.Add(4, "12/Dec/2016", 15.95);
            dt.Rows.Add(5, "12/Dec/2016", 0.95);
            dt.Rows.Add(6, "12/Dec/2016", 3.95);
            ds.Tables.Add(dt);
            return ds;
        }
    }
}