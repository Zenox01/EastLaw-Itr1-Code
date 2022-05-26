using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


namespace EastlawUI_v2.m
{
    public partial class DepartmentsList : System.Web.UI.Page
    {
        EastLawBL.Departments objdept = new EastLawBL.Departments();
        EastLawBL.Users objusr = new EastLawBL.Users();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // BindTelerikTree(0);
                InsertAuditLog("Departments", "Deparments List", "");
                if (Request.QueryString["lstN"] != null)
                {
                    GetSelecteddeptFiles(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["lstN"].ToString())));
                }
                else if (HttpContext.Current.Items["DeptGroupID"] != null)
                {
                    GetSelecteddeptParentFiles(int.Parse(EncryptDecryptHelper.Decrypt(HttpContext.Current.Items["DeptGroupID"].ToString())));
                }

                else if (Session["DeptSearchKeyWord"] != null)
                {
                    GetDeptSearch();
                }
                if (Session["MemberID"] != null)
                {
                    GetUserFolder(int.Parse(Session["MemberID"].ToString()));
                    spanMyFolder.Style["Display"] = "";
                }
                // lblCurCrumb.Text = HttpContext.Current.Items["Title"].ToString();
            }
        }
        void GetUserFolder(int UserID)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objusr.GetUserFolderByUser(UserID);

                ddlFolders.DataValueField = "ID";
                ddlFolders.DataTextField = "FolderName";
                ddlFolders.DataSource = dt;
                ddlFolders.DataBind();
                ddlFolders.Items.Insert(0, new ListItem("Select", "0"));

            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("Statuteslist.aspx", "GetUserFolder", e.Message);
            }
        }
        protected void ddlFolders_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFolders.SelectedValue != "0")
                GetSelectedItems(int.Parse(ddlFolders.SelectedValue));
        }
        void GetSelectedItems(int FolderID)
        {
            try
            {

                //GridViewRow row = default(GridViewRow);
                CheckBox chkSelect = default(CheckBox);
                HiddenField hdID = default(HiddenField);

                foreach (GridViewRow row in gvLst.Rows)
                {
                    if ((row != null))
                    {

                        hdID = (HiddenField)row.FindControl("hdID");


                        chkSelect = (CheckBox)row.FindControl("chkSelect");
                        if (chkSelect.Checked == true)
                        {
                            objusr.UserID = int.Parse(Session["MemberID"].ToString());
                            objusr.FolderID = FolderID;
                            objusr.ItemType = "Department";
                            objusr.ItemID = int.Parse(hdID.Value);
                            objusr.CreatedBy = int.Parse(Session["MemberID"].ToString());
                            int chk = objusr.InsertUserFolderItem();
                        }




                    }
                }
            }
            catch (Exception ex)
            {
                //    string script = "alert('" + ex.Message() + "')";
                //    ScriptManager.RegisterStartupScript(this, typeof(Page), "RedirectTo", script, true);
            }
        }
      
      
        void GetSelecteddeptFiles(int DeptID)
        {
            try
            {
                DataTable dt = new DataTable();

                dt = objdept.GetDepartmentFilesByDepartments(DeptID);
                if (dt.Rows.Count > 0)
                {
                    dt.Columns.Add("ShortDesc");
                    gvLst.DataSource = dt;
                    gvLst.DataBind();
                }
                else
                {
                    gvLst.DataSource = null;
                    gvLst.DataBind();
                }
            }
            catch { }
        }
        void GetDeptSearch()
        {
            try
            {
                DataTable dt = new DataTable();

                if (Session["DeptSearch"] != null)
                {
                    dt = (DataTable)Session["DeptSearch"];
                }
                else
                    dt = objdept.DepartmentSearchWithPagging("\"" + EncryptDecryptHelper.Decrypt(Request.QueryString["lng"].ToString()) + "\"", 0, 20);


                if (dt.Rows.Count > 0)
                {
                    dt.Columns.Add("ShortDesc");
                    for (int a = 0; a < dt.Rows.Count; a++)
                    {
                        dt.Rows[a]["DType"] = CommonClass.MakeFirstCap(dt.Rows[a]["DType"].ToString());
                        dt.Rows[a]["Title"] = CommonClass.MakeFirstCap(dt.Rows[a]["Title"].ToString());
                        if (Session["DeptSearch"] == null)
                            dt.Rows[a]["ShortDesc"] = CommonClass.GetShortDesc(dt.Rows[a]["FileContent"].ToString(), Session["DeptSearchKeyWord"].ToString());
                    }
                    dt.AcceptChanges();
                    gvLst.DataSource = dt;
                    gvLst.DataBind();
                }
                else
                {
                    gvLst.DataSource = null;
                    gvLst.DataBind();
                }
            }
            catch { }
        }
        void GetSelecteddeptParentFiles(int DeptID)
        {
            try
            {
                DataTable dt = new DataTable();

                dt = objdept.GetDepartmentFilesByDepartmentsParent(DeptID);
                if (dt.Rows.Count > 0)
                {
                    dt.Columns.Add("ShortDesc");
                    gvLst.DataSource = dt;
                    gvLst.DataBind();
                }
                else
                {
                    gvLst.DataSource = null;
                    gvLst.DataBind();
                }
            }
            catch { }
        }
        //void GetSelecteddeptFiles(int DeptID)
        //{
        //    try
        //    {
        //        DataTable dt = new DataTable();

        //        dt = objdept.GetDepartmentFilesByDepartments(DeptID);
        //        if (dt.Rows.Count > 0)
        //        {
        //            gvLst.DataSource = dt;
        //            gvLst.DataBind();
        //        }
        //        else
        //        {
        //            gvLst.DataSource = null;
        //            gvLst.DataBind();
        //        }
        //    }
        //    catch { }
        //}

        protected void gvLst_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                if (HttpContext.Current.Items["DeptGroupID"] != null)
                {

                    gvLst.PageIndex = e.NewPageIndex;
                    if (Session["SelectedNode"] != null)
                        GetSelecteddeptFiles(int.Parse(Session["SelectedNode"].ToString()));

                }
                else if (Session["DeptSearch"] != null)
                {
                    gvLst.PageIndex = e.NewPageIndex;
                    GetDeptSearch();
                }

                ;
            }
            catch (Exception ex)
            {

            }
        }
        void InsertAuditLog(string ActType, string Action, string txt)
        {
            try
            {
                string visitorIPAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (String.IsNullOrEmpty(visitorIPAddress))
                    visitorIPAddress = Request.ServerVariables["REMOTE_ADDR"];
                if (string.IsNullOrEmpty(visitorIPAddress))
                    visitorIPAddress = Request.UserHostAddress;

                int chk = 0;
                Location location = new Location();
                //string APIKey = "76511e33ff8498c62f458bea0a641b144b031bdb1e3eade661df53a39815cb27";
                //string url = string.Format("http://api.ipinfodb.com/v3/ip-city/?key={0}&ip={1}&format=json", APIKey, visitorIPAddress);

                //try
                //{
                //    using (System.Net.WebClient client = new System.Net.WebClient())
                //    {
                //        string json = client.DownloadString(url);
                //        location = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<Location>(json);

                //    }
                //}
                //catch
                //{ }

                string BrowserName = "";
                string SourcePlatform = "";
                try
                {
                    System.Web.HttpBrowserCapabilities browser = Request.Browser;
                    BrowserName = browser.Browser.ToString();
                    SourcePlatform = browser.Platform.ToString();
                }
                catch { }


                if (Session["MemberID"] != null)
                    chk = objusr.InsertAuditLog(ActType, Action, Request.Url.AbsoluteUri.ToString(), visitorIPAddress, int.Parse(Session["MemberID"].ToString()), location.CountryName, location.RegionName, location.CityName, txt, BrowserName, SourcePlatform, "Mobile Website");
                else
                    chk = objusr.InsertAuditLog(ActType, Action, Request.Url.AbsoluteUri.ToString(), visitorIPAddress, 0, location.CountryName, location.RegionName, location.CityName, txt, BrowserName, SourcePlatform, "Mobile Website");
            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("home/Default.aspx", "InsertAuditLog", e.Message);
            }
        }
        //protected void OnDataBound(object sender, EventArgs e)
        //{
        //    GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
        //    for (int i = 0; i < gvLst.Columns.Count; i++)
        //    {
        //        TableHeaderCell cell = new TableHeaderCell();
        //        TextBox txtSearch = new TextBox();
        //        txtSearch.Attributes["placeholder"] = gvLst.Columns[i].HeaderText;
        //        txtSearch.CssClass = "search_textbox";
        //        cell.Controls.Add(txtSearch);
        //        row.Controls.Add(cell);
        //    }
        //    gvLst.HeaderRow.Parent.Controls.AddAt(1, row);
        //}
    }
}