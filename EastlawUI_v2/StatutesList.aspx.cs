using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace EastlawUI_v2
{
    public partial class StatutesList : System.Web.UI.Page
    {
        EastLawBL.Users objusr = new EastLawBL.Users();
        EastLawBL.Statutes objstate = new EastLawBL.Statutes();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                InsertAuditLog("Statutes", "Statutes List", "");
                //if (Session["MemberID"] != null)
                //{
                //    GetStatutesCat();
                //    GetUserFolder(int.Parse(Session["MemberID"].ToString()));
                //    //spanMyFolder.Style["Display"] = "";
                //    BindSearchResult();
                //}
                //else
                //{
                //    Response.Redirect("/member/member-login?req=" + HttpContext.Current.Request.Url.AbsolutePath);
                //}
                GetStatutesCat();
                BindSearchResult();
                if (Session["MemberID"] != null)
                {
                    
                    GetUserFolder(int.Parse(Session["MemberID"].ToString()));
                    //spanMyFolder.Style["Display"] = "";
                    
                }
                
                Session["Sorting"] = null;

            }
        }
        void GetStatutesCat()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objstate.GetActiveStatutesCategories();

                ddlStatutesCat.DataValueField = "ID";
                ddlStatutesCat.DataTextField = "CatName";
                ddlStatutesCat.DataSource = dt;
                ddlStatutesCat.DataBind();
                ddlStatutesCat.Items.Insert(0, new ListItem("All", "0"));

            }
            catch (Exception e)
            {
            }
        }
        void BindSearchResult()
        {
            try
            {
                DataTable dt = new DataTable();
                if (Request.QueryString["sc"] != null)
                {
                    InsertAuditLog("Statutes", "Statutes List By Alpha", Request.QueryString["alp"].ToString());
                    string cri = "Where A.Active=1 and A.IsDeleted=0";

                    cri = cri + " AND A.Title like  '" + Request.QueryString["alp"].ToString() + "%'";
                    dt = objstate.GetStatutesSearch(cri);
                    Session["StatutesSearch"] = dt;
                }
                else if (Request.QueryString["cat"] != null)
                {
                    InsertAuditLog("Statutes", "Statutes List By Category", EncryptDecryptHelper.Decrypt(Request.QueryString["cat"].ToString()));
                    string cri = "Where A.Active=1 and A.IsDeleted=0";

                    cri = cri + " AND A.CatID='" + EncryptDecryptHelper.Decrypt(Request.QueryString["cat"].ToString()) + "'";
                    dt = objstate.GetStatutesSearch(cri);
                    Session["StatutesSearch"] = dt;
                }
                else if (Request.QueryString["year"] != null)
                {
                    InsertAuditLog("Statutes", "Statutes List By Year", EncryptDecryptHelper.Decrypt(Request.QueryString["year"].ToString()));
                    string cri = "Where A.Active=1 and A.IsDeleted=0";

                    cri = cri + " AND contains( A.Date,'" + EncryptDecryptHelper.Decrypt(Request.QueryString["year"].ToString()) + "')";
                    dt = objstate.GetStatutesSearch(cri);
                    Session["StatutesSearch"] = dt;
                }
                else if (Session["StatutesSearch"] != null)
                {
                    dt = (DataTable)Session["StatutesSearch"];
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            dt = (DataTable)Session["StatutesSearch"];
                        }
                        else
                        {
                            if (Request.QueryString["alp"] != null)
                            {
                                string cri = "Where  A.Active=1 and A.IsDeleted=0";

                                cri = cri + " AND A.Title like  '" + Request.QueryString["alp"].ToString() + "%'";
                                dt = objstate.GetStatutesSearch(cri);
                            }
                        }
                    }
                    //if( Request.QueryString["alp"] != null)
                    //{ 
                    //string cri = "Where A.IsDeleted=0";

                    //    cri = cri + " AND A.Title like  '" + Request.QueryString["alp"].ToString() + "%'";
                    //    dt = objstate.GetStatutesSearch(cri);
                    //}

                }
                else
                {
                    //string cri = "Where A.IsDeleted=0";

                    //dt = objstate.GetStatutesSearch(cri);
                    //Session["StatutesSearch"] = dt;

                    //dt = (DataTable)Session["StatutesSearch"];
                    //if (dt != null)
                    //{
                    //    if (dt.Rows.Count > 0)
                    //    {
                    //        dt = (DataTable)Session["StatutesSearch"];
                    //    }
                    //    else
                    //    {
                    //if (Request.QueryString["alp"] != null)
                    //{
                    string cri = "Where A.Active=1 and A.IsDeleted=0";

                    // cri = cri + " AND A.Title like  '" + Request.QueryString["alp"].ToString() + "%'";
                    dt = objstate.GetStatutesSearch(cri);
                    // }
                    //    }
                    //}
                }
                if (Request.QueryString["alp"] != null)
                {
                    if (Request.QueryString["alp"].ToString() != "all")
                    {
                        DataRow[] rows = dt.Select("Title like  '" + Request.QueryString["alp"].ToString() + "%'");
                        DataTable dtfilter = dt.Clone();
                        foreach (DataRow drow in rows)
                        {
                            dtfilter.ImportRow(drow);
                        }
                        dtfilter.AcceptChanges();
                        dt = dtfilter;
                    }
                }
                #region Cat Group
                var query = from row in dt.AsEnumerable()
                            group row by row.Field<string>("CatName") into Cat
                            orderby Cat.Key
                            select new
                            {
                                Name = Cat.Key.ToString() + "(" + Cat.Count() + ")",
                                count = Cat.Count(),
                                valfiel = Cat.Key.ToString()
                            };
                chkCat.DataSource = query;
                chkCat.DataValueField = "valfiel";
                chkCat.DataTextField = "Name";
                chkCat.DataBind();
                #endregion
               // lblCount.Text = dt.Rows.Count.ToString();
                DataColumnCollection columns = dt.Columns;
                if (!columns.Contains("ChildStatutestxt"))
                {
                    dt.Columns.Add("ChildStatutestxt");
                }
                //if (dt.Columns["ChildStatutestxt"].con)
                //dt.Columns.Add("ChildStatutestxt");
                for (int a = 0; a < dt.Rows.Count; a++)
                {
                    if (!string.IsNullOrEmpty(dt.Rows[a]["Date"].ToString()) && !string.IsNullOrEmpty(dt.Rows[a]["Act"].ToString()))
                        dt.Rows[a]["FormatedDateAct"] = "<strong>" + dt.Rows[a]["Date"].ToString() + " | " + dt.Rows[a]["Act"].ToString() + " | " + dt.Rows[a]["Certified"].ToString() + "</strong>";
                    else if (!string.IsNullOrEmpty(dt.Rows[a]["Date"].ToString()))
                        dt.Rows[a]["FormatedDateAct"] = "<strong>" + dt.Rows[a]["Date"].ToString() + " | " + dt.Rows[a]["Certified"].ToString() + "</strong>";
                    else if (!string.IsNullOrEmpty(dt.Rows[a]["Act"].ToString()))
                        dt.Rows[a]["FormatedDateAct"] = "<strong>" + dt.Rows[a]["Act"].ToString() + " | " + dt.Rows[a]["Certified"].ToString() + "</strong>";

                    DataTable dtStatutesChild = new DataTable();
                    dtStatutesChild = objstate.GetStatutesByPrimaryStatutes(int.Parse(dt.Rows[a]["ID"].ToString()));
                    if (dtStatutesChild != null)
                    {
                        string ChildStatutes = "";
                        for (int b = 0; b < dtStatutesChild.Rows.Count; b++)
                        {
                            ChildStatutes = ChildStatutes + "<a href='/statutes/" + clsUtilities.RemoveSpecialChars(dtStatutesChild.Rows[b]["Title"].ToString()).Replace(" ", "-") + "." + EncryptDecryptHelper.Encrypt(dtStatutesChild.Rows[b]["ID"].ToString()) + "'>" + dtStatutesChild.Rows[b]["Title"].ToString() + "</a>" + " | ";
                        }
                        dt.Rows[a]["ChildStatutestxt"] = ChildStatutes;
                    }


                }
                dt.AcceptChanges();



                gvLst.DataSource = dt;
                gvLst.DataBind();
            }
            catch (Exception ex)
            {

            }
        }
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
        protected void btnSearchTitle_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtTitle.Text.Trim()) || !string.IsNullOrEmpty(txtYear.Text.Trim()) || ddlStatutesCat.SelectedValue != "0")
                {
                    string cri = "Where A.IsDeleted=0 and A.Active=1";

                    if (!string.IsNullOrEmpty(txtTitle.Text.Trim()))
                        cri = cri + " AND CONTAINS(A.Title,'\"" + txtTitle.Text + "\"')";

                    if (!string.IsNullOrEmpty(txtYear.Text.Trim()))
                        cri = cri + " AND CONTAINS(A.Title,'\"" + txtYear.Text + "\"')";


                    //if (!string.IsNullOrEmpty(txtYear.Text.Trim()))
                    //    cri = cri + " AND  A.Year='" + txtYear.Text.Trim() + "'";

                    if (ddlStatutesCat.SelectedValue != "0")
                        cri = cri + " AND  A.CatID=" + ddlStatutesCat.SelectedValue + "";

                    //if (chkPrimTitle.Checked == true && chkSecTitle.Checked == true)
                    //    cri = cri + " AND (A.Pri_Sec='PRIMARY' OR A.Pri_Sec='SECONDARY')";

                    //if (chkPrimTitle.Checked == true)
                    //    cri = cri + " AND A.Pri_Sec='PRIMARY'";

                    //if (chkSecTitle.Checked == true)
                    //    cri = cri + " AND A.Pri_Sec='SECONDARY'";



                    DataTable dt = new DataTable();
                    dt = objstate.GetStatutesSearch(cri);
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            Session["StatutesSearch"] = dt;
                            Response.Redirect("/statutes/search-result");
                        }
                    }
                }

            }
            catch { }
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
                            objusr.ItemType = "Statutes";
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
        protected void gvLst_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                if (Session["Sorting"] != null)
                {
                    if (Session["Sorting"].ToString() == "Yes")
                    {
                        gvLst.PageIndex = e.NewPageIndex;
                        FilterResults();

                    }
                }
                else
                {
                    gvLst.PageIndex = e.NewPageIndex;
                    BindSearchResult();
                }
            }
            catch (Exception ex)
            {

            }
        }
        protected void ddlFolders_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddlFolders.SelectedValue != "0")
            //    GetSelectedItems(int.Parse(ddlFolders.SelectedValue));
        }
        void FilterResults()
        {
            try
            {
                string cat = "";

                DataTable dtFilter = new DataTable();
                dtFilter = (DataTable)Session["StatutesSearch"];
                for (int a = 0; a < chkCat.Items.Count; a++)
                {
                    if (chkCat.Items[a].Selected == true)
                    {
                        cat = cat + "'" + chkCat.Items[a].Value + "'" + ",";
                    }
                }

                if (!string.IsNullOrEmpty(cat))
                {
                    cat = cat.Remove(cat.Length - 1);
                    //DataRow[] filter = dtFilter.Select("Court in ("+courts+")");
                    dtFilter.DefaultView.RowFilter = "CatName in (" + cat + ")";
                    for (int a = 0; a < dtFilter.Rows.Count; a++)
                    {
                        if (!string.IsNullOrEmpty(dtFilter.Rows[a]["Date"].ToString()) && !string.IsNullOrEmpty(dtFilter.Rows[a]["Act"].ToString()))
                            dtFilter.Rows[a]["FormatedDateAct"] = "<strong>" + dtFilter.Rows[a]["Date"].ToString() + " | " + dtFilter.Rows[a]["Act"].ToString() + " | " + dtFilter.Rows[a]["Certified"].ToString() + "</strong>";
                    }
                    dtFilter.AcceptChanges();
                    gvLst.DataSource = dtFilter;
                    gvLst.DataBind();
                    Session["Sorting"] = "Yes";
                    return;
                }
                BindSearchResult();

            }
            catch { }
        }
        protected void chkCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterResults();
        }
        void InsertAuditLog(string ActType, string Action, string txt)
        {
            try
            {

                string Country = "";
                string Region = "";
                string City = "";

                string visitorIPAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (String.IsNullOrEmpty(visitorIPAddress))
                    visitorIPAddress = Request.ServerVariables["REMOTE_ADDR"];
                if (string.IsNullOrEmpty(visitorIPAddress))
                    visitorIPAddress = Request.UserHostAddress;

                int chk = 0;
                EastlawUI_v2.CommonClass.GetIPLocation(visitorIPAddress, ref Country, ref Region, ref City);
                //Location location = new Location();
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
                    chk = objusr.InsertAuditLog(ActType, Action, Request.Url.AbsoluteUri.ToString(), visitorIPAddress, int.Parse(Session["MemberID"].ToString()), Country, Region, City, txt, BrowserName, SourcePlatform, "Desktop Website");
                else
                    chk = objusr.InsertAuditLog(ActType, Action, Request.Url.AbsoluteUri.ToString(), visitorIPAddress, 0, Country, Region, City, txt, BrowserName, SourcePlatform, "Desktop Website");
            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("home/Default.aspx", "InsertAuditLog", e.Message);
            }
        }
    }
}