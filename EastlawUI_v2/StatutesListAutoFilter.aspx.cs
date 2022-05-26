    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Data;

namespace EastlawUI_v2
{
    public partial class StatutesListAutoFilter : System.Web.UI.Page
    {
        EastLawBL.Users objusr = new EastLawBL.Users();
        EastLawBL.Statutes objstate = new EastLawBL.Statutes();
        protected void Page_Load(object sender, EventArgs e)
        {
           // BindStatuteTitle();
            if (!Page.IsPostBack)
            {
                InsertAuditLog("Statutes", "Statutes List Auto Filter", "");
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
                //else
                //{
                //    Response.Redirect("/member/member-login?req=" + HttpContext.Current.Request.Url.AbsolutePath);
                //}
                Session["Sorting"] = null;

            }
        }
        //void BindStatuteTitle()
        //{
        //    try
        //    {
        //        DataTable dt = new DataTable();
        //        dt = objstate.GetActiveStatutesLessInfo();
        //        radTitle.DataTextField = "Title";
        //        radTitle.DataSource = dt;
        //        radTitle.DataBind();
        //    }
        //    catch { }
        //}
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
                string StatutesFilter = "";

                if(HttpContext.Current.Items["StatutesFilter"] != null)
                {
                    StatutesFilter=HttpContext.Current.Items["StatutesFilter"].ToString();
                }
                lblCurCrumb.InnerText = CommonClass.MakeFirstCap(StatutesFilter.Replace("-", " "));

               // Page.Title = StatutesFilter + " - Eastlaw.pk - Pakistan's Largest Online Law Library";

                if (StatutesFilter == "federal-legislation")
                {
                    InsertAuditLog("Statutes", "Statutes List Auto Filter", "Fedral Legislation");

                    string cri = "Where A.IsDeleted=0";
                    cri = cri + " AND A.Pri_Sec='PRIMARY' and A.GroupID=1";

                    dt = objstate.GetStatutesSearch(cri);
                    Session["StatutesSearch"] = dt;
                }
                else if (StatutesFilter == "provincial-legislation")
                {
                    InsertAuditLog("Statutes", "Statutes List Auto Filter", "Provincial Legislation");

                    string cri = "Where A.IsDeleted=0";

                    cri = cri + " AND A.Pri_Sec='PRIMARY' and A.GroupID=2";

                    dt = objstate.GetStatutesSearch(cri);
                    Session["StatutesSearch"] = dt;
                }
                else if (StatutesFilter == "federal-amendment-acts")
                {
                    InsertAuditLog("Statutes", "Statutes List Auto Filter", "Federal Amendment Acts");

                    string cri = "Where A.IsDeleted=0";

                    cri = cri + " AND A.Pri_Sec='Amendment Act' and A.GroupID=1";
                    dt = objstate.GetStatutesSearch(cri);
                    Session["StatutesSearch"] = dt;
                }
                else if (StatutesFilter == "provincial-amendment-acts")
                {
                    InsertAuditLog("Statutes", "Statutes List Auto Filter", "Provincial Amendment Acts");

                    string cri = "Where A.IsDeleted=0";

                    cri = cri + " AND A.Pri_Sec='Amendment Act' and A.GroupID=2";
                    dt = objstate.GetStatutesSearch(cri);
                    Session["StatutesSearch"] = dt;
                }
                else if (StatutesFilter == "federal-rules-and-regulations")
                {
                    InsertAuditLog("Statutes", "Statutes List Auto Filter", "Federal Rules and Regulations");

                    string cri = "Where A.IsDeleted=0";

                    cri = cri + " AND A.Pri_Sec='SECONDARY' and A.GroupID=1";
                    dt = objstate.GetStatutesSearch(cri);
                    Session["StatutesSearch"] = dt;
                }
                else if (StatutesFilter == "provincial-rules-and-regulations")
                {
                    InsertAuditLog("Statutes", "Statutes List Auto Filter", "Provincial Rules and Regulations");

                    string cri = "Where A.IsDeleted=0";

                    cri = cri + " AND A.Pri_Sec='SECONDARY' and A.GroupID=2";
                    dt = objstate.GetStatutesSearch(cri);
                    Session["StatutesSearch"] = dt;
                }
                else if (StatutesFilter == "bill-by-national-assembly")
                {
                    InsertAuditLog("Statutes", "Statutes List Auto Filter", "Bill By National Assembly");

                    string cri = "Where A.IsDeleted=0";

                    cri = cri + " AND A.Pri_Sec='Bills' and A.GroupID=1";
                    dt = objstate.GetStatutesSearch(cri);
                    Session["StatutesSearch"] = dt;
                }
                else if (StatutesFilter == "bill-by-provincial-assembly")
                {
                    InsertAuditLog("Statutes", "Statutes List Auto Filter", "Bill By Provincial Assembly");

                    string cri = "Where A.IsDeleted=0";

                    cri = cri + " AND A.Pri_Sec='Bills' and A.GroupID=2";
                    dt = objstate.GetStatutesSearch(cri);
                    Session["StatutesSearch"] = dt;
                }
                else if (StatutesFilter == "latest-legislations")
                {
                    InsertAuditLog("Statutes", "Statutes List Auto Filter", "latest-legislations");

                    dt = objstate.GetLatestStatutes();
                    Session["StatutesSearch"] = dt;
                }
                else if (StatutesFilter == "practice-area-legislations")
                {
                    lblCurCrumb.InnerText = CommonClass.MakeFirstCap(StatutesFilter.Replace("-", " ") + " - " + Request.QueryString["trans"].ToString().Replace("-", " "));
                    int PracticeAreaID = int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString()).ToString());
                    InsertAuditLog("Statutes", "Statutes List Auto Filter", "practice-area-legislations " + Request.QueryString["trans"].ToString());

                    EastLawBL.PracticeAreas objPA = new EastLawBL.PracticeAreas();
                    dt = objPA.GetTaggesStatuesWithPracticeArea(PracticeAreaID);

                  
                    Session["StatutesSearch"] = dt;
                }
                if (Session["StatutesSearch"] != null)
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
                                string cri = "Where A.IsDeleted=0";

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

                #region Filter Group
                var query1 = from row in dt.AsEnumerable()
                            group row by row.Field<string>("Statutes_Category_SubGroup") into Cat
                            orderby Cat.Key
                            select new
                            {
                                Name = Cat.Key.ToString() + "(" + Cat.Count() + ")",
                                count = Cat.Count(),
                                valfiel = Cat.Key.ToString()
                            };
                chkFilter.DataSource = query1;
                chkFilter.DataValueField = "valfiel";
                chkFilter.DataTextField = "Name";
                chkFilter.DataBind();
                #endregion


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

                string Statutes_Category_SubGroup = "";

                DataTable dtFilter = new DataTable();
                dtFilter = (DataTable)Session["StatutesSearch"];

                for (int a = 0; a < chkFilter.Items.Count; a++)
                {
                    if (chkFilter.Items[a].Selected == true)
                    {
                        Statutes_Category_SubGroup = Statutes_Category_SubGroup + "'" + chkFilter.Items[a].Value + "'" + ",";
                    }
                }


                for (int a = 0; a < chkCat.Items.Count; a++)
                {
                    if (chkCat.Items[a].Selected == true)
                    {
                        cat = cat + "'" + chkCat.Items[a].Value + "'" + ",";
                    }
                }

                
                DataView dv = new DataView(dtFilter);
             //   string txt = e.Text;
             //   dv.RowFilter = string.Format("AccountDescription LIKE '%{0}%'", txt);
                //int a = dv.Count;
               

                if (!string.IsNullOrEmpty(Statutes_Category_SubGroup))
                {
                    Statutes_Category_SubGroup = Statutes_Category_SubGroup.Remove(Statutes_Category_SubGroup.Length - 1);
                    //DataRow[] filter = dtFilter.Select("Court in ("+courts+")");
                    //dtFilter.DefaultView.RowFilter = "Statutes_Category_SubGroup in (" + Statutes_Category_SubGroup + ")";
                    dv.RowFilter = "Statutes_Category_SubGroup in (" + Statutes_Category_SubGroup + ")";

                    if (dv.Count > 0)
                    {
                        dtFilter = dv.ToTable();
                    }
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

                if (!string.IsNullOrEmpty(cat))
                {
                    cat = cat.Remove(cat.Length - 1);
                    //DataRow[] filter = dtFilter.Select("Court in ("+courts+")");
                    dv.RowFilter = "CatName in (" + cat + ")";

                    if (dv.Count > 0)
                    {
                        dtFilter = dv.ToTable();
                    }
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
                
                //BindSearchResult();

            }
            catch { }
        }
        protected void chkCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterResults();
        }
        protected void chkFilter_SelectedIndexChanged(object sender, EventArgs e)
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
        protected void btnSearchTitle_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtTitle.Text.Trim()) || !string.IsNullOrEmpty(txtYear.Text.Trim()) || ddlStatutesCat.SelectedValue != "0")
                {
                    string cri = "Where A.IsDeleted=0 and A.Active=1";
                    string keywordtxt = "";
                    if (!string.IsNullOrEmpty(txtTitle.Text.Trim()))
                        cri = cri + " AND CONTAINS(A.Title,'\"" + txtTitle.Text + "\"')";

                    //if (!string.IsNullOrEmpty(radTitle.Text.Trim()))
                    //{
                    //    // string keywordtxt = "";
                    //    //string[] Keywords = txtKeyword.Text.Trim().Split(',');
                    //    string[] Keywords = radTitle.Text.Trim().Split(';');
                    //    for (int a = 0; a < Keywords.Length - 1; a++)
                    //    {

                    //        if (!string.IsNullOrEmpty(Keywords[a].ToString().Trim()))
                    //            keywordtxt = keywordtxt + " \"" + Keywords[a].ToString().Trim() + "\" or";

                    //    }
                    //    if (Keywords.Length < 1)
                    //    {
                    //        keywordtxt = "\"" + radTitle.Text.Trim() + "\"";
                    //    }
                    //    else
                    //    {
                    //        keywordtxt = keywordtxt.Remove(keywordtxt.Length - 3);
                    //    }

                    //    cri = cri + " AND  CONTAINS(A.Title,'" + keywordtxt + "')";

                    //}

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
    }
}