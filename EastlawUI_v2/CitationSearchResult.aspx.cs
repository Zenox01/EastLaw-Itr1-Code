using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;

namespace EastlawUI_v2
{
    public partial class CitationSearchResult : System.Web.UI.Page
    {
        EastLawBL.Users objusr = new EastLawBL.Users();
        EastLawBL.Keywords objkeyword = new EastLawBL.Keywords();
        
        EastLawBL.Search objsrc = new EastLawBL.Search();
        EastLawBL.Cases objcase = new EastLawBL.Cases();
        EastLawBL.Users objuser = new EastLawBL.Users();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                InsertAuditLog("Search", "Case Advance/Citation Search", "Result");
                BindSearchResult();
                GetUserFolder(int.Parse(Session["MemberID"].ToString()));
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
                ExceptionHandling.SendErrorReport("MyFolder.aspx", "GetUserFolder", e.Message);
            }
        }
        void BindSearchResult()
        {
            try
            {
                if (Session["CasesSearch"] != null)
                {


                    DataTable dt = new DataTable();
                    dt = (DataTable)Session["CasesSearch"];

                    lblCount.Text = dt.Rows.Count.ToString();
                    #region Court Group
                    var query = from row in dt.AsEnumerable()
                                where !(row.Field<string>("Court") == null || row.Field<string>("Court") == "")
                                group row by row.Field<string>("Court") into courts
                                orderby courts.Key
                                select new
                                {
                                    Name = courts.Key.ToString() + " (" + courts.Count() + ")",
                                    count = courts.Count(),
                                    valfiel = courts.Key.ToString()
                                };

                    // print result
                    //foreach (var courts in query)
                    //{
                    //    Response.Write(courts.Name + " " + courts.count);
                    //    //Console.WriteLine("{0}\t{1}", salesman.Name, salesman.CountOfClients);
                    //}
                    chkCourtLst.DataSource = query;
                    chkCourtLst.DataValueField = "valfiel";
                    chkCourtLst.DataTextField = "Name";
                    chkCourtLst.DataBind();
                    #endregion

                    #region Year Group
                    var queryYear = from row in dt.AsEnumerable()
                                        // where row.Field<string>("Year") > 200
                                    group row by row.Field<string>("Year") into YEARS
                                    orderby YEARS.Key

                                    select new
                                    {
                                        Name = YEARS.Key + " (" + YEARS.Count() + ")",
                                        count = YEARS.Count(),
                                        valfiel = YEARS.Key
                                    };




                    chkLstYear.DataSource = CreateYearRange(dt);
                    chkLstYear.DataValueField = "ValData";
                    chkLstYear.DataTextField = "Title";
                    chkLstYear.DataBind();
                    #endregion
                    gvLst.DataSource = dt;
                    gvLst.DataBind();
                }
                else
                {
                    Response.Redirect("/cases/advance-search");
                }


            }
            catch (Exception ex)
            {

            }
        }
        void SearchWithin(string SearchWithin)
        {
            try
            {
                DataTable dt1 = new DataTable();
                DataTable dt = (DataTable)Session["CasesSearch"];
                DataRow[] filteredRows = dt.Select("Judgment LIKE '%" + SearchWithin + "%'");
                if (filteredRows.Length > 0)
                    dt1 = filteredRows.CopyToDataTable();

                gvLst.DataSource = dt1;
                gvLst.DataBind();
            }
            catch { }
        }
        public string HighlightText(string InputTxt)
        {
            string Search_Str = HttpContext.Current.Items["keywordtxt"].ToString();


            Session["SearchMain"] = Search_Str.ToString();
            Search_Str = CommonClass.RemoveExtraWordsForHiglight(Search_Str);
            Regex RegExp;//= new Regex();

            if (Search_Str.Contains("\""))
                RegExp = new Regex(Search_Str.Replace("\"", "").Trim(), RegexOptions.IgnoreCase);
            //if (Search_Str.Contains("and"))
            //    RegExp = new Regex(Search_Str.Replace("and ", "").Trim(), RegexOptions.IgnoreCase);
            else
                RegExp = new Regex(Search_Str.Replace(" ", "|").Trim(), RegexOptions.IgnoreCase);


            return RegExp.Replace(InputTxt, new MatchEvaluator(ReplaceKeyWords));

        }

        public string HighlightTextWithin(string InputTxt)
        {

            string Search_Str_WithIn = "";
            if (Session["SearchWithIn"] != null)
            {
                Search_Str_WithIn = Session["SearchWithIn"].ToString();
                Search_Str_WithIn = CommonClass.RemoveExtraWordsForHiglight(Search_Str_WithIn);


                Regex RegExp;

                if (Search_Str_WithIn.Contains("\""))
                    RegExp = new Regex(Search_Str_WithIn.Replace("\"", "").Trim(), RegexOptions.IgnoreCase);

                Session["SearchWithIn"] = Search_Str_WithIn.ToString();
                RegExp = new Regex(Search_Str_WithIn.Replace(" ", "|").Trim(), RegexOptions.IgnoreCase);


                return RegExp.Replace(InputTxt, new MatchEvaluator(ReplaceKeyWordsWithin));
            }
            return InputTxt;

            //   return ("<span class=highlight>" + Search_Str + "</span>");
        }
        public string ReplaceKeyWords(Match m)
        {
            return ("<span class=highlight>" + m.Value + "</span>");
        }
        public string ReplaceKeyWordsWithin(Match m)
        {
            //string content = "";
            //if (Session["SearchMain"] != null)
            //    content = "<span class=highlight>" + m.Value + "</span>";
            //if (Session["SearchWithIn"] != null)
            //    content = "<span class=highlightBlue>" + m.Value + "</span>";
            //return content;
            return ("<span class=highlightBlue>" + m.Value + "</span>");
        }
        string GetClientIP()
        {
            string visitorIPAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (String.IsNullOrEmpty(visitorIPAddress))
                visitorIPAddress = Request.ServerVariables["REMOTE_ADDR"];
            if (string.IsNullOrEmpty(visitorIPAddress))
                visitorIPAddress = Request.UserHostAddress;
            return visitorIPAddress;
        }
        DataTable ReturnRemoveUserSearchPara(DataTable dt)
        {
            try
            {
                if (Session["MemberID"] != null)
                {
                    if (Session["MemberSearchPara"] != null)
                    {
                        string[] membersearch = (string[])Session["MemberSearchPara"];

                        if (string.IsNullOrEmpty(membersearch[0]))
                        {
                            for (int a = 0; a < dt.Rows.Count; a++)
                            {
                                if (dt.Rows[a]["ResultType"].ToString() == "Cases")
                                {
                                    dt.Rows[a].Delete();
                                }
                            }
                            dt.AcceptChanges();
                        }
                        if (string.IsNullOrEmpty(membersearch[1]))
                        {
                            for (int a = 0; a < dt.Rows.Count; a++)
                            {
                                if (dt.Rows[a]["ResultType"].ToString() == "Statutes")
                                {
                                    dt.Rows[a].Delete();
                                }

                                else if (dt.Rows[a]["ResultType"].ToString() == "Primary Legislation")
                                {
                                    dt.Rows[a].Delete();
                                }
                                else if (dt.Rows[a]["ResultType"].ToString() == "Secondary Legislation")
                                {
                                    dt.Rows[a].Delete();
                                }
                            }
                            dt.AcceptChanges();
                        }
                        if (string.IsNullOrEmpty(membersearch[2]))
                        {
                            for (int a = 0; a < dt.Rows.Count; a++)
                            {
                                if (dt.Rows[a]["ResultType"].ToString() == "Dictionary")
                                {
                                    dt.Rows[a].Delete();
                                }
                            }
                            dt.AcceptChanges();
                        }
                    }

                }
            }
            catch { }
            return dt;
        }
        
        DataTable CreateYearRange(DataTable dt)
        {
            try
            {
                DataTable dtNewRange = new DataTable();
                dtNewRange.Columns.Add("Title");
                dtNewRange.Columns.Add("ValData");

                for (int a = 0; a < dt.Rows.Count; a++)
                {
                    string Refval = "";
                    string chkval = ReturnYearRange(int.Parse(dt.Rows[a]["Year"].ToString()), ref Refval);
                    if (!string.IsNullOrEmpty(chkval))
                    {
                        DataRow dr = dtNewRange.NewRow();

                        dr["Title"] = chkval;
                        dr["ValData"] = Refval;// "Year >= 5000 and Year <= 5999";// ReturnYearRange(int.Parse(dt.Rows[a]["Year"].ToString()));
                        dtNewRange.Rows.Add(dr);
                    }
                }
                dtNewRange.AcceptChanges();



                dtNewRange = dtNewRange.AsEnumerable().GroupBy(r => r.Field<string>("Title")).Select(g => g.First()).CopyToDataTable();


                return dtNewRange;
            }
            catch
            {
                return null;
            }
        }
        string ReturnYearRange(int n, ref string Years)
        {
            DataTable dtNewRange = new DataTable();

            if (n >= 1950 && n <= 1955)
            {
                Years = "1950,1951,1952,1953,1954,1955";
                return "1950 - 1955";

            }
            if (n >= 1955 && n <= 1960)
            {
                Years = "1955,1956,1957,1958,1959,1960";
                return "1955 - 1960";
            }

            if (n >= 1960 && n <= 1965)
            {
                Years = "1960,1961,1962,1963,1964,1965";
                return "1960 - 1965";
            }
            if (n >= 1965 && n <= 1970)
            {
                Years = "1965,1966,1967,1968,1969,1970";
                return "1965 - 1970";
            }
            if (n >= 1970 && n <= 1975)
            {
                Years = "1970,1971,1972,1973,1974,1975";
                return "1970 - 1975";
            }
            if (n >= 1975 && n <= 1980)
            {
                Years = "1975,1976,1977,1978,1979,1980";
                return "1975 - 1980";
            }
            if (n >= 1980 && n <= 1985)
            {
                Years = "1980,1981,1982,1983,1984,1985";
                return "1980 - 1985";
            }
            if (n >= 1985 && n <= 1990)
            {
                Years = "1985,1986,1987,1988,1989,1990";
                return "1985 - 1990";
            }
            if (n >= 1990 && n <= 1995)
            {
                Years = "1990,1991,1992,1993,1994,1995";
                return "1990 - 1995";
            }
            if (n >= 1995 && n <= 2000)
            {
                Years = "1995,1996,1997,1998,1999,2000";
                return "1995 - 2000";
            }
            if (n >= 2000 && n <= 2005)
            {
                Years = "2000,2001,2002,2003,2004,2005";
                return "2000 - 2005";
            }
            if (n >= 2005 && n <= 2010)
            {
                Years = "2005,2006,2007,2008,2009,2010";
                return "2005 - 2010";
            }
            if (n >= 2010 && n <= 2015)
            {
                Years = "2010,2011,2012,2013,2014,2015";
                return "2010 - 2015";
            }

            if (n >= 2015 && n <= 2016)
            {
                Years = "2015,2016";
                return "2015 - 2016";
            }

             if (n >= 2016 && n <= 2016)
            {
                Years = "2016,2016";
                return "2016";
            }
            if (n >= 2017 && n <= 2017)
            {
                Years = "2017,2017";
                return "2017";
            }
            if (n >= 2018 && n <= 2018)
            {
                Years = "2018,2018";
                return "2018";
            }
            if (n >= 2019 && n <= 2019)
            {
                Years = "2019,2019";
                return "2019";
            }
            if (n >= 2020 && n <= 2020)
            {
                Years = "2020,2020";
                return "2020";
            }


            return "";
        }

        protected void gvLst_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {

                if (!string.IsNullOrEmpty(txtSearchWithinResult.Text))
                {
                    gvLst.PageIndex = e.NewPageIndex;
                    SearchWithin(txtSearchWithinResult.Text.Trim());
                }
                else
                {
                    gvLst.PageIndex = e.NewPageIndex;
                    BindSearchResult();
                }


                //if (Session["Sorting"] != null)
                //{
                //    if (Session["Sorting"].ToString() == "Yes")
                //    {
                //        gvLst.PageIndex = e.NewPageIndex;
                //        FilterResults();

                //    }
                //    else
                //    {
                //        gvLst.PageIndex = e.NewPageIndex;
                //        BindSearchResult();
                //    }
                //}
                //else
                //{
                //    gvLst.PageIndex = e.NewPageIndex;
                //    BindSearchResult();
                //}
            }
            catch (Exception ex)
            {

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
                            objusr.ItemType = "Cases";
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
        protected void ddlFolders_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddlFolders.SelectedValue != "0")
            //    GetSelectedItems(int.Parse(ddlFolders.SelectedValue));
        }
        void FilterResults()
        {
            try
            {
                string contenttype = "";
                string courts = "";
                string years = "";
                DataTable dtFilter = new DataTable();
                dtFilter = (DataTable)Session["CasesSearch"];
                //if (Session["sesKeywordResultsWithin"] != null)
                //    dtFilter = (DataTable)Session["sesKeywordResultsWithin"];
                //else
                //    dtFilter = (DataTable)Session["sesKeywordResults"];

                for (int a = 0; a < chkCourtLst.Items.Count; a++)
                {
                    if (chkCourtLst.Items[a].Selected == true)
                    {
                        courts = courts + "'" + chkCourtLst.Items[a].Value + "'" + ",";

                    }
                }
                for (int a = 0; a < chkLstYear.Items.Count; a++)
                {
                    if (chkLstYear.Items[a].Selected == true)
                    {
                        years = years + chkLstYear.Items[a].Value + ",";
                    }
                }


                //if (!string.IsNullOrEmpty(years))
                //{
                //    years = years.Remove(years.Length - 1);
                //    string[] YearsLenght = years.Split(',');

                //   // DataRow[] filter = dtFilter.Select("Court in (" + courts + ")");
                //    dt.DefaultView.RowFilter = "Year between " + YearsLenght.First().ToString() + " and " + YearsLenght.Last().ToString() + ")";
                //    gvLst.DataSource = dt;
                //    gvLst.DataBind();
                //    Session["Sorting"] = "Yes";
                //    #region Court Group
                //    var query = from row in dt.AsEnumerable()
                //                where !(row.Field<string>("Court") == null || row.Field<string>("Court") == "")
                //                group row by row.Field<string>("Court") into courts3
                //                orderby courts3.Key
                //                select new
                //                {
                //                    Name = courts3.Key.ToString() + " (" + courts3.Count() + ")",
                //                    count = courts3.Count(),
                //                    valfiel = courts3.Key.ToString()
                //                };

                //    chkCourtLst.DataSource = query;
                //    chkCourtLst.DataValueField = "valfiel";
                //    chkCourtLst.DataTextField = "Name";
                //    chkCourtLst.DataBind();
                //    #endregion

                //    return;
                //}
                //else if (!string.IsNullOrEmpty(courts))
                //{
                //    DataTable dtFilter = new DataTable();
                //    courts = courts.Remove(courts.Length - 1);
                //    DataRow[] filter = dt.Select("Court in (" + courts + ")");
                //    //dt.DefaultView.RowFilter = "Court in (" + courts + ")";

                //    foreach (DataRow row in filter)
                //    {
                //        dtFilter.ImportRow(row);
                //    }
                //    gvLst.DataSource = dtFilter;
                //    gvLst.DataBind();
                //    Session["Sorting"] = "Yes";

                //    #region Year Group
                //    var queryYear = from row in dt.AsEnumerable()
                //                    where row.Field<int>("Year") > 200
                //                    group row by row.Field<int>("Year") into YEARS
                //                    orderby YEARS.Key

                //                    select new
                //                    {
                //                        Name = YEARS.Key + " (" + YEARS.Count() + ")",
                //                        count = YEARS.Count(),
                //                        valfiel = YEARS.Key
                //                    };

                //    //chkLstYear.DataSource = queryYear;
                //    //chkLstYear.DataValueField = "valfiel";
                //    //chkLstYear.DataTextField = "Name";
                //    //chkLstYear.DataBind();

                //    chkLstYear.DataSource = CreateYearRange(dt);
                //    chkLstYear.DataValueField = "ValData";
                //    chkLstYear.DataTextField = "Title";
                //    chkLstYear.DataBind();
                //    #endregion
                //    return;
                //}
                //else
                //{

                //    BindSearchResult();
                //}


                if (!string.IsNullOrEmpty(courts) && !string.IsNullOrEmpty(years))
                {
                    courts = courts.Remove(courts.Length - 1);
                    //  years = years.Remove(years.Length - 1);

                    string yy = "";
                    years = years.Remove(years.Length - 1);
                    string[] YearsLenght = years.Split(',');

                    for (int a = 0; a < YearsLenght.Length; a++)
                    {

                        yy = yy + "'" + YearsLenght[a].ToString() + "'" + ",";

                    }
                    yy = yy.Remove(yy.Length - 1);
                    //DataRow[] filter = dtFilter.Select("Court in ("+courts+")");
                    dtFilter.DefaultView.RowFilter = "Court in (" + courts + ") and Year in (" + yy + ")";
                    gvLst.DataSource = dtFilter;
                    gvLst.DataBind();
                    Session["Sorting"] = "Yes";


                    return;
                }
                if (!string.IsNullOrEmpty(courts))
                {
                    courts = courts.Remove(courts.Length - 1);
                    //DataRow[] filter = dtFilter.Select("Court in ("+courts+")");
                    dtFilter.DefaultView.RowFilter = "Court in (" + courts + ")";
                    gvLst.DataSource = dtFilter;
                    gvLst.DataBind();
                    Session["Sorting"] = "Yes";

                    #region Year Group
                    var queryYear = from row in dtFilter.AsEnumerable()
                                    where row.Field<int>("Year") > 200
                                    group row by row.Field<int>("Year") into YEARS
                                    orderby YEARS.Key

                                    select new
                                    {
                                        Name = YEARS.Key + " (" + YEARS.Count() + ")",
                                        count = YEARS.Count(),
                                        valfiel = YEARS.Key
                                    };

                    chkLstYear.DataSource = queryYear;
                    chkLstYear.DataValueField = "valfiel";
                    chkLstYear.DataTextField = "Name";
                    chkLstYear.DataBind();
                    #endregion
                    return;
                }
                if (!string.IsNullOrEmpty(years))
                {
                    string yy = "";
                    years = years.Remove(years.Length - 1);
                    string[] YearsLenght = years.Split(',');

                    for (int a = 0; a < YearsLenght.Length; a++)
                    {

                        yy = yy + "'" + YearsLenght[a].ToString() + "'" + ",";

                    }
                    yy = yy.Remove(yy.Length - 1);
                    //DataRow[] filter = dtFilter.Select("Court in ("+courts+")");
                    dtFilter.DefaultView.RowFilter = "Year in (" + yy + ")";
                    gvLst.DataSource = dtFilter;
                    gvLst.DataBind();
                    Session["Sorting"] = "Yes";
                    #region Court Group
                    var query = from row in dtFilter.AsEnumerable()
                                group row by row.Field<string>("Court") into courts3
                                orderby courts3.Key
                                select new
                                {
                                    Name = courts3.Key.ToString() + " (" + courts3.Count() + ")",
                                    count = courts3.Count(),
                                    valfiel = courts3.Key.ToString()
                                };

                    chkCourtLst.DataSource = query;
                    chkCourtLst.DataValueField = "valfiel";
                    chkCourtLst.DataTextField = "Name";
                    chkCourtLst.DataBind();
                    #endregion

                    return;
                }
                else
                {

                    BindSearchResult();
                }
            }
            catch { }
        }
        protected void chkCourtLst_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterResults();
        }

        protected void chkLstYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterResults();
        }
        protected void btnSearchWithinResult_Click(object sender, EventArgs e)
        {
            SearchWithin(txtSearchWithinResult.Text.Trim());
        }
        protected void txtSearchWithinResult_TextChanged(object sender, EventArgs e)
        {
            SearchWithin(txtSearchWithinResult.Text.Trim());
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
                    chk = objuser.InsertAuditLog(ActType, Action, Request.Url.AbsoluteUri.ToString(), visitorIPAddress, int.Parse(Session["MemberID"].ToString()), Country, Region, City, txt, BrowserName, SourcePlatform, "Desktop Website");
                else
                    chk = objuser.InsertAuditLog(ActType, Action, Request.Url.AbsoluteUri.ToString(), visitorIPAddress, 0, Country, Region, City, txt, BrowserName, SourcePlatform, "Desktop Website");
            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("home/Default.aspx", "InsertAuditLog", e.Message);
            }
        }
        bool CheckUserAccessValidation(int UserID)
        {
            try
            {
                DataTable dt = new DataTable();
                EastLawBL.Users objUsr = new EastLawBL.Users();
                dt = objUsr.UserElementViewReport((DateTime.Now.Date.ToString("MM/dd/yyy")), DateTime.Now.Date.ToString("MM/dd/yyy"), UserID);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        if (!String.IsNullOrEmpty(dt.Rows[0]["NoofSummaryView"].ToString()))
                        {
                            if (dt.Rows[0]["NoofSummaryView"].ToString() != "0")
                            {
                                if (int.Parse(dt.Rows[0]["NoofCaseSummaryView"].ToString()) >= int.Parse(dt.Rows[0]["NoofSummaryView"].ToString()))
                                {
                                    InsertAuditLog("Limit Exceeded", "Case Summary View", "User ID: " + UserID);

                                    return false;

                                }
                                else
                                {
                                    return true;
                                }
                            }
                        }
                    }

                }
                return false;

            }
            catch (Exception ex)
            {
                return false;

            }
        }
        protected void gvLst_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "ViewSummary")
                {

                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow row = gvLst.Rows[index];
                    //HiddenField hdnField = (HiddenField)row.FindControl("hdID");
                    string val = (string)this.gvLst.DataKeys[index]["Id"].ToString();


                    if (CheckUserAccessValidation(int.Parse(Session["MemberID"].ToString())))
                    {

                        InsertAuditLog("Case View Summary", val, "Click on View Summary ");


                        DataTable dt = objcase.GetCases(int.Parse(val));
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            divModelHeader.InnerHtml = "Case Summary of " + dt.Rows[0]["Citation"].ToString();
                            divModelBody.InnerHtml = dt.Rows[0]["CaseSummary"].ToString();




                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none",
                    "<script>$('#myModal').modal('show');</script>", false);
                        }


                    }
                    else
                    {
                        divModelHeader.InnerHtml = "Limit Exceeded";
                        divModelBody.InnerHtml = "You Have Exceed your daily limit, Kindly Upgrade your package";
                    }
                }
            }
            catch { }
        }
    }
}