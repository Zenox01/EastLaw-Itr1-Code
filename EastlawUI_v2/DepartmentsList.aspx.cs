using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;

namespace EastlawUI_v2
{
    public partial class DepartmentsList : System.Web.UI.Page
    {
        EastLawBL.Departments objdept = new EastLawBL.Departments();
        EastLawBL.Users objusr = new EastLawBL.Users();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (HttpContext.Current.Items["DeptGroupID"] != null)
                    GetDeptTypeGroup(int.Parse(EncryptDecryptHelper.Decrypt(HttpContext.Current.Items["DeptGroupID"].ToString())));
                LoadYears();
                // BindTelerikTree(0);
                InsertAuditLog("Departments", "Deparments List", "");
                if (Request.QueryString["lstN"] != null)
                {
                    GetSelecteddeptFiles(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["lstN"].ToString())));
                    divDepartmentsSearch.Style["Display"] = "";
                    divRelatedDepartments.Style["Display"] = "";
                }
                else if (HttpContext.Current.Items["DeptGroupID"] != null)
                {
                    GetSelecteddeptParentFiles(int.Parse(EncryptDecryptHelper.Decrypt(HttpContext.Current.Items["DeptGroupID"].ToString())));
                    divDepartmentsSearch.Style["Display"] = "";
                    divRelatedDepartments.Style["Display"] = "";
                }

                else if (Session["DeptSearchKeyWord"] != null)
                {
                    GetDeptSearch();
                }
                if (Session["MemberID"] != null)
                {
                    GetUserFolder(int.Parse(Session["MemberID"].ToString()));
                    //spanMyFolder.Style["Display"] = "";
                }
                // lblCurCrumb.Text = HttpContext.Current.Items["Title"].ToString();
            }
        }
        //void GetBreadCrumb()
        //{
        //    try
        //    {
        //        System.Data.DataTable dtlevel3 = objdept.GetActiveDeptByParent(int.Parse(EncryptDecryptHelper.Decrypt(HttpContext.Current.Items["DeptGroupID"].ToString())));
        //        if (dtlevel3 != null)
        //        {
        //            if (dtlevel3.Rows.Count > 0)
        //            {
        //                for (int l3 = 0; l3 < dtlevel3.Rows.Count; l3++)
        //                {
        //                    lblCurCrumb.Text=
        //                }
        //            }
        //            else
        //            {
        //                //  objdept.getac
        //                System.Data.DataTable dtlevel2 = objdept.GetActiveDeptByParent(objdept.GetGroupParent(int.Parse(EncryptDecryptHelper.Decrypt(HttpContext.Current.Items["DeptGroupID"].ToString()))));
        //                if (dtlevel2 != null)
        //                {
        //                    if (dtlevel2.Rows.Count > 0)
        //                    {
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch { }
        //}
        void GetUserFolder(int UserID)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objusr.GetUserFolderByUser(UserID);

                //ddlFolders.DataValueField = "ID";
                //ddlFolders.DataTextField = "FolderName";
                //ddlFolders.DataSource = dt;
                //ddlFolders.DataBind();
                //ddlFolders.Items.Insert(0, new ListItem("Select", "0"));

            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("Statuteslist.aspx", "GetUserFolder", e.Message);
            }
        }
        protected void ddlFolders_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddlFolders.SelectedValue != "0")
            //    GetSelectedItems(int.Parse(ddlFolders.SelectedValue));
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
        private void BindTelerikTree(int ID)
        {
            try
            {
                //DataTable dtlevel1 = objdept.GetActiveDeptByParent(0);
                //if (dtlevel1 != null)
                //{
                //    for (int l1 = 0; l1 < dtlevel1.Rows.Count; l1++)
                //    {
                //        RadTreeNode level1 = new RadTreeNode(dtlevel1.Rows[l1]["DeptName"].ToString(), dtlevel1.Rows[l1]["ID"].ToString());
                //        tvDept.Nodes.Add(level1);


                //        DataTable dtlevel2 = objdept.GetActiveDeptByParent(int.Parse(dtlevel1.Rows[l1]["ID"].ToString()));
                //        if (dtlevel2 != null)
                //        {
                //            for (int l2 = 0; l2 < dtlevel2.Rows.Count; l2++)
                //            {
                //                RadTreeNode level2 = new RadTreeNode(dtlevel2.Rows[l2]["DeptName"].ToString(), dtlevel2.Rows[l2]["ID"].ToString());
                //                level1.Nodes.Add(level2);

                //                DataTable dtlevel3 = objdept.GetActiveDeptByParent(int.Parse(dtlevel2.Rows[l2]["ID"].ToString()));
                //                if (dtlevel3 != null)
                //                {
                //                    for (int l3 = 0; l3 < dtlevel3.Rows.Count; l3++)
                //                    {
                //                        RadTreeNode level3 = new RadTreeNode(dtlevel3.Rows[l3]["DeptName"].ToString(), dtlevel3.Rows[l3]["ID"].ToString());
                //                        level2.Nodes.Add(level3);
                //                    }
                //                }
                //            }
                //        }

                //    }
                //}
                //tvDept.ExpandAllNodes();
            }
            catch (Exception Ex) { throw Ex; }
        }
        protected void tvDept_NodeClick(object sender, RadTreeNodeEventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                Session["SelectedNode"] = e.Node.Value.ToString();
                InsertAuditLog("Departments", e.Node.Value.ToString(), e.Node.Text.ToString());
                GetSelecteddeptFiles(int.Parse(e.Node.Value.ToString()));
                //dt = objdept.GetDepartmentFilesByDepartments(int.Parse(e.Node.Value.ToString()));
                //if(dt.Rows.Count > 0)
                //{
                //    gvFile.DataSource = dt;
                //    gvFile.DataBind();
                //}
                //else
                //{
                //    gvFile.DataSource = null;
                //    gvFile.DataBind();
                //}
            }
            catch
            { }

        }
        void GetSelecteddeptFiles(int DeptID)
        {
            try
            {
                DataTable dt = new DataTable();

                dt = objdept.GetDepartmentFilesByDepartments(DeptID);
                if (dt.Rows.Count > 0)
                {
                    lblCurCrumb.Text = dt.Rows[0]["ParentDept"].ToString();


                    if (dt.Columns.Contains("ShortDesc") == false)
                    {
                        dt.Columns.Add("ShortDesc");
                    }


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
                    dt = (DataTable)Session["DeptSearch"];
                else
                    dt = objdept.DepartmentSearchWithPagging("\"" + EncryptDecryptHelper.Decrypt(Request.QueryString["lng"].ToString()) + "\"", 0, 20);


                if (dt.Rows.Count > 0)
                {
                    if (dt.Columns.Contains("ShortDesc") == false)
                    {
                        dt.Columns.Add("ShortDesc");
                    }
                    for (int a = 0; a < dt.Rows.Count; a++)
                    {
                        dt.Rows[a]["DType"] = EastlawUI_v2.CommonClass.MakeFirstCap(dt.Rows[a]["DType"].ToString());
                        dt.Rows[a]["Title"] = EastlawUI_v2.CommonClass.MakeFirstCap(dt.Rows[a]["Title"].ToString());
                        if (Session["DeptSearch"] == null)
                            dt.Rows[a]["ShortDesc"] = EastlawUI_v2.CommonClass.GetShortDesc(dt.Rows[a]["FileContent"].ToString(), Session["DeptSearchKeyWord"].ToString());
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
                    lblCurCrumb.Text = dt.Rows[0]["ParentDept"].ToString();
                    //lblCurCrumb1Link.HRef = "/Departments/" + dt.Rows[0]["ParentDept"].ToString() + "." + EncryptDecryptHelper.Encrypt(dt.Rows[0]["ParentDeptID"].ToString());

                    if (dt.Columns.Contains("ShortDesc") == false)
                    {
                        dt.Columns.Add("ShortDesc");
                    }
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
                    chk = objusr.InsertAuditLog(ActType, Action, Request.Url.AbsoluteUri.ToString(), visitorIPAddress, int.Parse(Session["MemberID"].ToString()), location.CountryName, location.RegionName, location.CityName, txt, BrowserName, SourcePlatform, "Desktop Website");
                else
                    chk = objusr.InsertAuditLog(ActType, Action, Request.Url.AbsoluteUri.ToString(), visitorIPAddress, 0, location.CountryName, location.RegionName, location.CityName, txt, BrowserName, SourcePlatform, "Desktop Website");
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
        void LoadYears()
        {
            // Clear items:    
            ddlYear.Items.Clear();
            // Add default item to the list
            ddlYear.Items.Insert(0, new ListItem("Year", "0"));
            // Start loop
            for (int i = 0; i < 69; i++)
            {
                // For each pass add an item
                // Add a number of years (negative, which will subtract) to current year
                ddlYear.Items.Add(DateTime.Now.AddYears(-i).Year.ToString());
            }
        }
        void GetDeptTypeGroup(int DeptID)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objdept.GetDepartmentFilesTypesGroupByDeptID(DeptID);
                ddlDeptTypeGroups.DataValueField = "DType";
                ddlDeptTypeGroups.DataTextField = "DType";
                ddlDeptTypeGroups.DataSource = dt;
                ddlDeptTypeGroups.DataBind();
                ddlDeptTypeGroups.Items.Insert(0, new ListItem("Select", "0"));
            }
            catch { }
        }
        void DepartmentSearch()
        {
            try
            {
                string cri = "Where A.IsDeleted=0";


                if (!string.IsNullOrEmpty(txtSearch.Text.Trim()))
                {
                    cri = cri + " AND A.Deptid=" + EncryptDecryptHelper.Decrypt(HttpContext.Current.Items["DeptGroupID"].ToString());
                    cri = cri + " AND  (CONTAINS(A.Title,'\"" + txtSearch.Text + "\"')";
                    cri = cri + " OR  CONTAINS(A.Year,'\"" + txtSearch.Text + "\"')";
                    cri = cri + " OR  CONTAINS(A.No,'\"" + txtSearch.Text + "\"')";
                    cri = cri + " OR  CONTAINS(A.DDate,'\"" + txtSearch.Text + "\"')";
                    cri = cri + " OR  CONTAINS(A.DType,'\"" + txtSearch.Text + "\"'))";


                    DataTable dt = new DataTable();
                    dt = objdept.DepartmentSearch(cri);
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            lblMsg.Text = "";
                            lblMsg.ForeColor = System.Drawing.Color.Red;
                            lblMsg.Visible = false;

                            Session["DeptSearchKeyWord"] = txtSearch.Text;
                            Session["DeptSearch"] = dt;
                            Response.Redirect("/departments/search-result");
                        }
                        else
                        {
                            lblMsg.Text = "No Records Found !";
                            lblMsg.ForeColor = System.Drawing.Color.Red;
                            lblMsg.Visible = true;
                        }
                    }

                }





            }
            catch (Exception ex)
            {

            }
        }
        void DepartmentFreeTextSearch()
        {
            try
            {
                string cri = "Where A.IsDeleted=0";


                if (!string.IsNullOrEmpty(txtFreeTextSearch.Text.Trim()))
                {
                    cri = cri + " AND A.Deptid=" + EncryptDecryptHelper.Decrypt(HttpContext.Current.Items["DeptGroupID"].ToString());
                    cri = cri + " AND  (CONTAINS(A.FileContent,'\"" + txtFreeTextSearch.Text + "\"'))";

                    Session["DeptSearchKeyWord"] = txtFreeTextSearch.Text;
                    Response.Redirect("/departments/search-result?lng=" + EncryptDecryptHelper.Encrypt(txtFreeTextSearch.Text).ToString());

                    DataTable dt = new DataTable();
                    dt = objdept.DepartmentSearch(cri);
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {

                            lblMsg.Text = "";
                            lblMsg.ForeColor = System.Drawing.Color.Red;
                            lblMsg.Visible = false;


                            Session["DeptSearch"] = dt;
                            Session["DeptSearchKeyWord"] = txtFreeTextSearch.Text;
                            Response.Redirect("/departments/search-result?lng=" + EncryptDecryptHelper.Encrypt(txtFreeTextSearch.Text));
                        }
                        else
                        {
                            lblMsg.Text = "No Records Found !";
                            lblMsg.ForeColor = System.Drawing.Color.Red;
                            lblMsg.Visible = true;
                        }
                    }

                }





            }
            catch (Exception ex)
            {

            }
        }
        void DepartmentSearchByDate()
        {
            try
            {
                string cri = "Where A.IsDeleted=0";


                if (!string.IsNullOrEmpty(txtSearchByDate.Text.Trim()))
                {
                    cri = cri + " AND A.Deptid=" + EncryptDecryptHelper.Decrypt(HttpContext.Current.Items["DeptGroupID"].ToString());
                    cri = cri + " AND  A.DateFormated=1 and convert(varchar(20),convert(datetime, A.DDate, 103),103)='" + txtSearchByDate.Text + "'";

                    Session["DeptSearchKeyWord"] = txtSearchByDate.Text;
                    //  Response.Redirect("/Departments/Search-Result?lng=" + EncryptDecryptHelper.Encrypt(txtFreeTextSearch.Text).ToString());

                    DataTable dt = new DataTable();
                    dt = objdept.DepartmentSearch(cri);
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {

                            lblMsg.Text = "";
                            lblMsg.ForeColor = System.Drawing.Color.Red;
                            lblMsg.Visible = false;


                            Session["DeptSearch"] = dt;
                            Session["DeptSearchKeyWord"] = txtSearchByDate.Text;
                            Response.Redirect("/departments/search-result?lng=" + EncryptDecryptHelper.Encrypt(txtSearchByDate.Text));
                        }
                        else
                        {
                            lblMsg.Text = "No Records Found !";
                            lblMsg.ForeColor = System.Drawing.Color.Red;
                            lblMsg.Visible = true;
                        }
                    }

                }





            }
            catch (Exception ex)
            {

            }
        }
        protected void btnFreeTextSearch_Click(object sender, EventArgs e)
        {
            DepartmentSearch();
        }

        protected void btnTypeSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string cri = "Where A.IsDeleted=0";


                if (ddlDeptTypeGroups.SelectedValue != "0")
                {
                    cri = cri + " AND A.Deptid=" + HttpContext.Current.Items["DeptGroupID"].ToString();
                    cri = cri + " AND A.DType='" + ddlDeptTypeGroups.SelectedValue + "'";

                    if (!string.IsNullOrEmpty(txtTypesNo.Text.Trim()))
                        cri = cri + " AND  CONTAINS(A.No,'\"" + txtTypesNo.Text + "\"')";

                    if (ddlYear.SelectedValue != "0")
                    {
                        cri = cri + " AND A.Year='" + ddlYear.SelectedValue + "'";
                    }

                    DataTable dt = new DataTable();
                    dt = objdept.DepartmentSearch(cri);
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            Session["DeptSearch"] = dt;
                            Session["DeptSearchKeyWord"] = ddlDeptTypeGroups.SelectedItem.Text + " " + ddlYear.SelectedValue + " " + txtTypesNo.Text;
                            Response.Redirect("/Departments/Search-Result");
                        }
                    }

                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            DepartmentSearch();
        }

        protected void txtFreeTextSearch_TextChanged(object sender, EventArgs e)
        {
            DepartmentFreeTextSearch();
        }

        protected void btnTextSearch_Click(object sender, EventArgs e)
        {
            DepartmentFreeTextSearch();
        }

        protected void btnSearchByDate_Click(object sender, EventArgs e)
        {
            DepartmentSearchByDate();
        }
    }
}