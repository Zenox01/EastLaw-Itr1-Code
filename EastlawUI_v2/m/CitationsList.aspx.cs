using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace EastlawUI_v2.m
{
    public partial class CitationsList : System.Web.UI.Page
    {
        EastLawBL.Users objusr = new EastLawBL.Users();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
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

                ddlFolders.DataValueField = "ID";
                ddlFolders.DataTextField = "FolderName";
                ddlFolders.DataSource = dt;
                ddlFolders.DataBind();
                ddlFolders.Items.Insert(0, new ListItem("Select", "0"));

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
            if (ddlFolders.SelectedValue != "0")
                GetSelectedItems(int.Parse(ddlFolders.SelectedValue));
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
    }
}