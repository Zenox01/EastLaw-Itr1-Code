using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Threading;

namespace EastlawUI_v2.m
{
    public partial class PracticeAreaSearchResult : System.Web.UI.Page
    {
        EastLawBL.PracticeAreas objPA = new EastLawBL.PracticeAreas();
        EastLawBL.Users objusr = new EastLawBL.Users();
        EastLawBL.Search objsrc = new EastLawBL.Search();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                InsertAuditLog("Practice Area Search", Request.QueryString["trans"].ToString(), HttpContext.Current.Items["PAkeywordtxt"].ToString());
                Session["SearchWithIn"] = null;
                BindSearchResult(HttpContext.Current.Items["PAkeywordtxt"].ToString(), 0, 20);
                // BindTreeViewControl(HttpContext.Current.Items["keywordtxt"].ToString());
                // GetGroup();
                if (Session["MemberID"] != null)
                {
                    GetUserFolder(int.Parse(Session["MemberID"].ToString()));
                    spanMyFolder.Style["Display"] = "";
                }
                Session["Sorting"] = null;
               // BreadCrumb();
            }
        }
        void GetGroup()
        {
            DataTable dt = new DataTable();
            // dt = objkeyword.GetSearchResultsByKeyword(HttpContext.Current.Items["keywordtxt"].ToString());
            // DataTable dtGroupedBy = GetGroupedBy(dt, "Court", "Court", "Count");
        }
       


        private DataTable GetGroupedBy(DataTable dt, string columnNamesInDt, string groupByColumnNames, string typeOfCalculation)
        {


            //Return its own if the column names are empty
            if (columnNamesInDt == string.Empty || groupByColumnNames == string.Empty)
            {
                return dt;
            }

            //Once the columns are added find the distinct rows and group it bu the numbet
            DataTable _dt = dt.DefaultView.ToTable(true, groupByColumnNames);

            //The column names in data table
            string[] _columnNamesInDt = columnNamesInDt.Split(',');

            for (int i = 0; i < _columnNamesInDt.Length; i = i + 1)
            {
                if (_columnNamesInDt[i] != groupByColumnNames)
                {
                    _dt.Columns.Add(_columnNamesInDt[i]);
                }
            }

            //Gets the collection and send it back
            for (int i = 0; i < _dt.Rows.Count; i = i + 1)
            {
                for (int j = 0; j < _columnNamesInDt.Length; j = j + 1)
                {
                    if (_columnNamesInDt[j] != groupByColumnNames)
                    {
                        _dt.Rows[i][j] = dt.Compute(typeOfCalculation + "(" + _columnNamesInDt[j] + ")", groupByColumnNames + " = '" + _dt.Rows[i][groupByColumnNames].ToString() + "'");
                    }
                }
            }

            return _dt;
        }
        int GetTotalRecords(int PracticeAreaID, string SearchText)
        {
            DataTable dt = objPA.GetSearchResultsByPracticeAreaCount(PracticeAreaID, SearchText);

            if (dt != null)
            {
                dt = ReturnRemoveUserSearchParaCount(dt);
                // Declare an object variable.
                object sumObject;
                sumObject = dt.Compute("Sum(TotalRows)", "");
                lblCount.Text = sumObject.ToString();

                gvLst.VirtualItemCount = int.Parse(sumObject.ToString());
                return int.Parse(sumObject.ToString());
            }
            return 0;
        }
        int GetTotalRecordsWithIn(int PracticeAreaID, string keyWords, string SearchWithin)
        {

            DataTable dt = objPA.GetSearchWithInResultsByPracticeAreaCount(PracticeAreaID, keyWords, SearchWithin);

            if (dt != null)
            {
                dt = ReturnRemoveUserSearchParaCount(dt);
                // Declare an object variable.
                object sumObject;
                sumObject = dt.Compute("Sum(TotalRows)", "");
                lblCount.Text = sumObject.ToString();

                gvLst.VirtualItemCount = int.Parse(sumObject.ToString());
                return int.Parse(sumObject.ToString());
            }
            return 0;

        }
        static bool ExactMatch(string input, string match)
        {

            return Regex.IsMatch(input, string.Format(@"\b{0}\b", Regex.Escape(match)), RegexOptions.IgnoreCase);

        }
        void BindSearchResult(string SearchText, int startRowIndex, int maximumRows)
        {
            try
            {
                // string[] membersearch= new string[5];


                //string keyword = HttpContext.Current.Items["keywordtxt"].ToString();
                //if (keyword.Contains(" "))
                //{
                //    if (!keyword.Contains("\""))
                //    {
                //        if (!ExactMatch(keyword, "and"))
                //        {
                //            if (!ExactMatch(keyword, "or"))
                //            {

                //                if (!keyword.Contains("\""))
                //                {
                //                   // keyword = "\"" + keyword + "\"";
                //                   keyword=  keyword.Replace(" ", " and ");
                //                }
                //                else
                //                {
                //                    keyword = "\"" + keyword + "\"";
                //                }
                //                // }

                //            }


                //        }
                //    }
                //    ////if (!keyword.Contains("and"))
                //    //{

                //    //}
                //    //else if (!ExactMatch(keyword, "AND"))
                //    //{
                //    //    keyword = "\"" + keyword + "\"";
                //    //    // keyword.Replace(" ", "and");
                //    //}
                //}
                string keywordP1 = CommonClass.FormatSearchWordP1(HttpContext.Current.Items["PAkeywordtxt"].ToString());
                string keyword = CommonClass.FormatSearchWord(HttpContext.Current.Items["PAkeywordtxt"].ToString());


                //TextBox txtSearch = (TextBox)Master.FindControl("txtSearch");
                //txtSearch.Text = HttpContext.Current.Items["PAkeywordtxt"].ToString();


                DataTable dt = new DataTable();
                DataTable dtGroup = new DataTable();
                int TotalCount = 0;
                int KeyPTag = 0;
                //dt = objkeyword.GetSearchResultsByKeyword(HttpContext.Current.Items["keywordtxt"].ToString());
                //dt = objkeyword.GetSearchResultsByKeywordTest(SearchText, startRowIndex, maximumRows);

                dt = objPA.GetSearchResultsByPracticeArea(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString()).ToString()), keywordP1, startRowIndex, maximumRows);
                if (dt != null)
                {
                    if (dt.Rows.Count == 0)
                    {
                        TotalCount= GetTotalRecords(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString()).ToString()), keyword);
                        dt = objPA.GetSearchResultsByPracticeArea(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString()).ToString()), keyword, startRowIndex, maximumRows);
                        dtGroup = objPA.GetSearchResultsByPracticeAreaGrouping(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString()).ToString()), keyword);
                        KeyPTag = 2;

                    }
                    else
                    {
                        TotalCount= GetTotalRecords(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString()).ToString()), keywordP1);
                        dtGroup = objPA.GetSearchResultsByPracticeAreaGrouping(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString()).ToString()), keywordP1);
                        KeyPTag = 1;

                    }
                }


                string keyw = SearchText;
                Session["SearchMain"] = SearchText;
                int chk = 0;
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        dt.Columns.Add("Title");
                        dt.Columns.Add("Link");
                        dt.Columns.Add("OtherContent");
                        for (int a = 0; a < dt.Rows.Count; a++)
                        {
                            if (dt.Rows[a]["ResultType"].ToString() == "Cases")
                            {
                                dt.Rows[a]["Title"] = CommonClass.MakeFirstCap(dt.Rows[a]["Appeallant"].ToString()) + " VS " + CommonClass.MakeFirstCap(dt.Rows[a]["Respondent"].ToString());
                                dt.Rows[a]["Link"] = "/Cases/" + clsUtilities.RemoveSpecialCharacter(CommonClass.MakeFirstCap(CommonClass.GetWords(dt.Rows[a]["Appeallant"].ToString(), 3).ToString())).Replace(" ", "-") + "VS" + clsUtilities.RemoveSpecialCharacter(CommonClass.MakeFirstCap(CommonClass.GetWords(dt.Rows[a]["Respondent"].ToString(), 3).ToString())).Replace(" ", "-") + "." + EncryptDecryptHelper.Encrypt(dt.Rows[a]["ID"].ToString());

                                string OtherCont = "<strong>Where Reported:</strong>";
                                if (!string.IsNullOrEmpty(dt.Rows[a]["CitationRef"].ToString()))
                                    OtherCont = OtherCont + dt.Rows[a]["CitationRef"].ToString() + "<br />";
                                else
                                    OtherCont = OtherCont + dt.Rows[a]["Citation"].ToString() + "<br />";
                                OtherCont = OtherCont + "<strong>Date:</strong>" + dt.Rows[a]["JDate"].ToString() + "<br /><strong>Court:</strong>" + dt.Rows[a]["Court"].ToString() + " <br /> ";
                                OtherCont = OtherCont + "<strong>Appeal:</strong>" + dt.Rows[a]["Appeal"].ToString() + "<br />";
                                if (!string.IsNullOrEmpty(dt.Rows[a]["Keywords"].ToString()))
                                {
                                    if (KeyPTag == 1)
                                        OtherCont = OtherCont + "<strong>Keywords:</strong><span style='font-size:14px'>" + HighlightText(HighlightTextWithin(dt.Rows[a]["Keywords"].ToString()), keywordP1) + "</span><br /><br />";
                                    else
                                        OtherCont = OtherCont + "<strong>Keywords:</strong><span style='font-size:14px'>" + HighlightText(HighlightTextWithin(dt.Rows[a]["Keywords"].ToString()), keyword) + "</span><br /><br />";
                                }

                                //if (!string.IsNullOrEmpty(dt.Rows[a]["Result"].ToString()))
                                //    OtherCont = OtherCont + "<strong>Result: </strong>" + dt.Rows[a]["Result"].ToString() + "<br /><br />";
                                //OtherCont = OtherCont + HighlightText(HighlightTextWithin(dt.Rows[a]["ShortDes"].ToString().Replace("</p>", "").Replace("<p>", "").Replace("<br>", "").Replace("<b>", "").Replace("</b>", "")));

                                //dt.Rows[a]["OtherContent"] = OtherCont;

                                if (!string.IsNullOrEmpty(dt.Rows[a]["Result"].ToString()))
                                    OtherCont = OtherCont + "<strong>Result: </strong>" + dt.Rows[a]["Result"].ToString() + "<br /><br />";
                                // OtherCont = OtherCont + HighlightText(HighlightTextWithin(dt.Rows[a]["ShortDes"].ToString().Replace("</p>", "").Replace("<p>", "").Replace("<br>", "").Replace("<b>", "").Replace("</b>", "")));
                                if (KeyPTag == 1)
                                    dt.Rows[a]["OtherContent"] = OtherCont + HighlightText(HighlightTextWithin(CommonClass.GetWords(CommonClass.GetShortDesc(dt.Rows[a]["Judgment"].ToString(), keywordP1.ToString()).Replace("</p>", "").Replace("<p>", "").Replace("<br>", "").Replace("<b>", "").Replace("</b>", ""), 200)), keywordP1);
                                else
                                    dt.Rows[a]["OtherContent"] = OtherCont + HighlightText(HighlightTextWithin(CommonClass.GetWords(CommonClass.GetShortDesc(dt.Rows[a]["Judgment"].ToString(), keyword.ToString()).Replace("</p>", "").Replace("<p>", "").Replace("<br>", "").Replace("<b>", "").Replace("</b>", ""), 200)), keyword);

                            }
                            else if (dt.Rows[a]["ResultType"].ToString() == "Statutes")
                            {
                                dt.Rows[a]["Title"] = CommonClass.MakeFirstCap(dt.Rows[a]["Appeallant"].ToString());
                                dt.Rows[a]["Link"] = "/Statutes/" + clsUtilities.RemoveSpecialCharacter(CommonClass.MakeFirstCap(CommonClass.GetWords(dt.Rows[a]["Appeallant"].ToString(), 3).ToString())).Replace(" ", "-") + "." + EncryptDecryptHelper.Encrypt(dt.Rows[a]["ID"].ToString());
                                //string OtherCont = "Index:" + dt.Rows[a]["Citation"].ToString() +"<br/>";
                                string OtherCont = "<br/>";
                                OtherCont = OtherCont + dt.Rows[a]["JDate"].ToString();
                                if (!string.IsNullOrEmpty(dt.Rows[a]["Respondent"].ToString()))
                                    OtherCont = OtherCont + " | " + dt.Rows[a]["Respondent"].ToString() + "<br />";

                                dt.Rows[a]["OtherContent"] = OtherCont;
                            }
                            else if (dt.Rows[a]["ResultType"].ToString() == "Dictionary")
                            {
                                dt.Rows[a]["Title"] = CommonClass.MakeFirstCap(dt.Rows[a]["Year"].ToString());
                                //dt.Rows[a]["Link"] = "/Statutes/" + clsUtilities.RemoveSpecialCharacter(EastLawUI.CommonClass.MakeFirstCap(EastLawUI.CommonClass.GetWords(dt.Rows[a]["Appeallant"].ToString(), 3).ToString())).Replace(" ", "-") + "." + EncryptDecryptHelper.Encrypt(dt.Rows[a]["ID"].ToString());
                                string OtherCont = "<br/>";
                                OtherCont = OtherCont + dt.Rows[a]["JDate"].ToString();

                                dt.Rows[a]["OtherContent"] = OtherCont;

                            }

                        }
                        dt.AcceptChanges();

                        ///
                        dt = ReturnRemoveUserSearchPara(dt);

                        //Session["sesKeywordResults"] = dt;
                        //  Session["sesKeywordResults"] = null;
                        if (Session["MemberID"] != null)
                            chk = objsrc.InsertSearchText(SearchText, 1, GetClientIP(), int.Parse(Session["MemberID"].ToString()), TotalCount,"Practice Area");
                        else
                            chk = objsrc.InsertSearchText(SearchText, 1, GetClientIP(), 0, TotalCount,"Practice Area");
                        lblSearchWords.Text = SearchText;




                        gvLst.DataSource = dt;
                        gvLst.DataBind();


                        dtGroup = ReturnRemoveUserSearchPara(dtGroup);

                        #region Content Type Group
                        var queryContentType = from row in dtGroup.AsEnumerable()
                                               group row by row.Field<string>("ResultType") into ResultType
                                               orderby ResultType.Key
                                               select new
                                               {
                                                   Name = ResultType.Key.ToString() + " (" + ResultType.Count() + ")",
                                                   count = ResultType.Count(),
                                                   valfiel = ResultType.Key.ToString()
                                               };

                        // print result
                        //foreach (var courts in query)
                        //{
                        //    Response.Write(courts.Name + " " + courts.count);
                        //    //Console.WriteLine("{0}\t{1}", salesman.Name, salesman.CountOfClients);
                        //}
                        chkContentType.DataSource = queryContentType;
                        chkContentType.DataValueField = "valfiel";
                        chkContentType.DataTextField = "Name";
                        chkContentType.DataBind();
                        #endregion

                        #region Court Group
                        var query = from row in dtGroup.AsEnumerable()
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
                        var queryYear = from row in dtGroup.AsEnumerable()
                                        // where row.Field<string>("Year") > 200
                                        group row by row.Field<string>("Year") into YEARS
                                        orderby YEARS.Key

                                        select new
                                        {
                                            Name = YEARS.Key + " (" + YEARS.Count() + ")",
                                            count = YEARS.Count(),
                                            valfiel = YEARS.Key
                                        };



                        chkLstYear.DataSource = CreateYearRange(dtGroup);
                        chkLstYear.DataValueField = "ValData";
                        chkLstYear.DataTextField = "Title";
                        chkLstYear.DataBind();
                        #endregion



                        GetGlossoryTree();

                        divNoResult.Style["Display"] = "none";
                        divResult.Style["Display"] = "";
                    }
                    else
                    {
                        divNoResult.Style["Display"] = "";
                        divResult.Style["Display"] = "none";
                        if (Session["MemberID"] != null)
                            chk = objsrc.InsertSearchText(SearchText, 0, GetClientIP(), int.Parse(Session["MemberID"].ToString()), TotalCount,"Practice Area");
                        else
                            chk = objsrc.InsertSearchText(SearchText, 0, GetClientIP(), 0, TotalCount,"Practice Area");
                    }
                }
                else
                {
                    divNoResult.Style["Display"] = "";
                    divResult.Style["Display"] = "none";
                    if (Session["MemberID"] != null)
                        chk = objsrc.InsertSearchText(SearchText, 0, GetClientIP(), int.Parse(Session["MemberID"].ToString()), TotalCount,"Practice Area");
                    else
                        chk = objsrc.InsertSearchText(SearchText, 0, GetClientIP(), 0, TotalCount,"Practice Area");
                }

            }
            catch (Exception ex)
            {

            }
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
            if (n >= 1956 && n <= 1960)
            {
                Years = "1956,1957,1958,1959,1960";
                return "1956 - 1960";
            }

            if (n >= 1961 && n <= 1965)
            {
                Years = "1961,1962,1963,1964,1965";
                return "1961 - 1965";
            }
            if (n >= 1966 && n <= 1970)
            {
                Years = "1966,1967,1968,1969,1970";
                return "1966 - 1970";
            }
            if (n >= 1971 && n <= 1975)
            {
                Years = "1971,1972,1973,1974,1975";
                return "1971 - 1975";
            }
            if (n >= 1976 && n <= 1980)
            {
                Years = "1976,1977,1978,1979,1980";
                return "1976 - 1980";
            }
            if (n >= 1981 && n <= 1985)
            {
                Years = "1981,1982,1983,1984,1985";
                return "1981 - 1985";
            }
            if (n >= 1986 && n <= 1990)
            {
                Years = "1986,1987,1988,1989,1990";
                return "1986 - 1990";
            }
            if (n >= 1991 && n <= 1995)
            {
                Years = "1991,1992,1993,1994,1995";
                return "1991 - 1995";
            }
            if (n >= 1996 && n <= 2000)
            {
                Years = "1996,1997,1998,1999,2000";
                return "1996 - 2000";
            }
            if (n >= 2001 && n <= 2005)
            {
                Years = "2001,2002,2003,2004,2005";
                return "2001 - 2005";
            }
            if (n >= 2006 && n <= 2010)
            {
                Years = "2006,2007,2008,2009,2010";
                return "2006 - 2010";
            }
            if (n >= 2011 && n <= 2015)
            {
                Years = "2011,2012,2013,2014,2015";
                return "2011 - 2015";
            }

            if (n >= 2016 && n <= 2016)
            {
                Years = "2016,2016";
                return "2016 - 2016";
            }


            return "";
        }
        void SearchWithinResult(string txt, int Start, int End)
        {
            try
            {
                int chk = 0;
                DataTable dt = new DataTable();
                DataTable dtGroup = new DataTable();
                int KeyPTag = 0;
                int TotalCount = 0;
                InsertAuditLog("Practice Area Search", "Search Within", "Main Keyword: " + HttpContext.Current.Items["PAkeywordtxt"].ToString() + " SearchWithin Text: " + txt);

                //string keyword = CommonClass.FormatSearchWord(HttpContext.Current.Items["keywordtxt"].ToString());
                //string keywordSearchWithin = CommonClass.FormatSearchWord(txt);

                string keywordP1 = CommonClass.FormatSearchWordP1(HttpContext.Current.Items["PAkeywordtxt"].ToString());
                string keywordP2 = CommonClass.FormatSearchWord(HttpContext.Current.Items["PAkeywordtxt"].ToString());

                string keywordSearchWithinP1 = CommonClass.FormatSearchWordP1(txt);
                string keywordSearchWithinP2 = CommonClass.FormatSearchWord(txt);

                // objPA.GetSearchWithinResultsByPracticeArea(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString()).ToString()), keywordP1, keywordSearchWithinP1, Start, End);

                dt = objPA.GetSearchWithinResultsByPracticeArea(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString()).ToString()), keywordP1, keywordSearchWithinP1, Start, End);
                if (dt != null)
                {
                    if (dt.Rows.Count == 0)
                    {
                        TotalCount= GetTotalRecordsWithIn(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString()).ToString()), keywordP2, keywordSearchWithinP2);
                        dt = objPA.GetSearchWithinResultsByPracticeArea(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString()).ToString()), keywordP2, keywordSearchWithinP2, Start, End);
                        dtGroup = objPA.GetSearchWithinResultsByPracticeAreaGrouping(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString()).ToString()), keywordP2, keywordSearchWithinP2);
                        KeyPTag = 2;
                        Session["SearchWithIn"] = keywordSearchWithinP2;
                    }
                    else
                    {
                        TotalCount=GetTotalRecordsWithIn(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString()).ToString()), keywordP1, keywordSearchWithinP1);
                        dtGroup = objPA.GetSearchWithinResultsByPracticeAreaGrouping(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString()).ToString()), keywordP1, keywordSearchWithinP1);
                        KeyPTag = 1;
                        Session["SearchWithIn"] = keywordSearchWithinP1;

                    }
                }



                if (dt.Rows.Count > 0)
                {

                    dt.Columns.Add("Title");
                    dt.Columns.Add("Link");
                    dt.Columns.Add("OtherContent");
                    for (int a = 0; a < dt.Rows.Count; a++)
                    {
                        if (dt.Rows[a]["ResultType"].ToString() == "Cases")
                        {
                            dt.Rows[a]["Title"] = CommonClass.MakeFirstCap(dt.Rows[a]["Appeallant"].ToString()) + " VS " + CommonClass.MakeFirstCap(dt.Rows[a]["Respondent"].ToString());
                            dt.Rows[a]["Link"] = "/Cases/" + clsUtilities.RemoveSpecialCharacter(CommonClass.MakeFirstCap(CommonClass.GetWords(dt.Rows[a]["Appeallant"].ToString(), 3).ToString())).Replace(" ", "-") + "VS" + clsUtilities.RemoveSpecialCharacter(CommonClass.MakeFirstCap(CommonClass.GetWords(dt.Rows[a]["Respondent"].ToString(), 3).ToString())).Replace(" ", "-") + "." + EncryptDecryptHelper.Encrypt(dt.Rows[a]["ID"].ToString());

                            string OtherCont = "<strong>Where Reported:</strong>";
                            if (!string.IsNullOrEmpty(dt.Rows[a]["CitationRef"].ToString()))
                                OtherCont = OtherCont + dt.Rows[a]["CitationRef"].ToString() + "<br />";
                            else
                                OtherCont = OtherCont + dt.Rows[a]["Citation"].ToString() + "<br />";
                            OtherCont = OtherCont + "<strong>Date:</strong>" + dt.Rows[a]["JDate"].ToString() + "<br /><strong>Court:</strong>" + dt.Rows[a]["Court"].ToString() + " <br /> ";
                            OtherCont = OtherCont + "<strong>Appeal:</strong>" + dt.Rows[a]["Appeal"].ToString() + "<br />";
                            if (!string.IsNullOrEmpty(dt.Rows[a]["Keywords"].ToString()))
                                OtherCont = OtherCont + "<strong>Keywords:</strong><span style='font-size:14px'>" + HighlightText(HighlightTextWithin(dt.Rows[a]["Keywords"].ToString())) + "</span><br /><br />";

                            //if (!string.IsNullOrEmpty(dt.Rows[a]["Result"].ToString()))
                            //    OtherCont = OtherCont + "<strong>Result: </strong>" + dt.Rows[a]["Result"].ToString() + "<br /><br />";
                            //OtherCont = OtherCont + HighlightText(HighlightTextWithin(dt.Rows[a]["ShortDes"].ToString().Replace("</p>", "").Replace("<p>", "").Replace("<br>", "").Replace("<b>", "").Replace("</b>", "")));

                            //dt.Rows[a]["OtherContent"] = OtherCont;

                            if (!string.IsNullOrEmpty(dt.Rows[a]["Result"].ToString()))
                                OtherCont = OtherCont + "<strong>Result: </strong>" + dt.Rows[a]["Result"].ToString() + "<br /><br />";
                            // OtherCont = OtherCont + HighlightText(HighlightTextWithin(dt.Rows[a]["ShortDes"].ToString().Replace("</p>", "").Replace("<p>", "").Replace("<br>", "").Replace("<b>", "").Replace("</b>", "")));

                            //dt.Rows[a]["OtherContent"] = OtherCont + HighlightText(HighlightTextWithin(EastLawUI.CommonClass.GetWords(EastLawUI.CommonClass.GetShortDesc(dt.Rows[a]["Judgment"].ToString(), HttpContext.Current.Items["keywordtxt"].ToString()).Replace("</p>", "").Replace("<p>", "").Replace("<br>", "").Replace("<b>", "").Replace("</b>", ""), 100)));

                            if (KeyPTag == 1)
                                dt.Rows[a]["OtherContent"] = OtherCont + HighlightText(HighlightTextWithin(CommonClass.GetWords(CommonClass.GetShortDesc(dt.Rows[a]["Judgment"].ToString(), keywordP1.ToString()).Replace("</p>", "").Replace("<p>", "").Replace("<br>", "").Replace("<b>", "").Replace("</b>", ""), 200)), keywordP1);
                            else
                                dt.Rows[a]["OtherContent"] = OtherCont + HighlightText(HighlightTextWithin(CommonClass.GetWords(CommonClass.GetShortDesc(dt.Rows[a]["Judgment"].ToString(), keywordP2.ToString()).Replace("</p>", "").Replace("<p>", "").Replace("<br>", "").Replace("<b>", "").Replace("</b>", ""), 200)), keywordP2);


                        }
                        else if (dt.Rows[a]["ResultType"].ToString() == "Statutes")
                        {
                            dt.Rows[a]["Title"] = CommonClass.MakeFirstCap(dt.Rows[a]["Appeallant"].ToString());
                            dt.Rows[a]["Link"] = "/Statutes/" + clsUtilities.RemoveSpecialCharacter(CommonClass.MakeFirstCap(CommonClass.GetWords(dt.Rows[a]["Appeallant"].ToString(), 3).ToString())).Replace(" ", "-") + "." + EncryptDecryptHelper.Encrypt(dt.Rows[a]["ID"].ToString());
                            string OtherCont = "<strong>Where Reported:</strong>";
                            OtherCont = OtherCont + dt.Rows[a]["JDate"].ToString();
                            if (!string.IsNullOrEmpty(dt.Rows[a]["Respondent"].ToString()))
                                OtherCont = OtherCont + " | " + dt.Rows[a]["Respondent"].ToString() + "<br />";
                        }

                    }
                    dt.AcceptChanges();
                    dt = ReturnRemoveUserSearchPara(dt);

                    Session["sesKeywordResultsWithin"] = dt;
                    if (Session["MemberID"] != null)
                        chk = objsrc.InsertSearchText(txt, 1, GetClientIP(), int.Parse(Session["MemberID"].ToString()), TotalCount,"Practice Area Within");
                    else
                        chk = objsrc.InsertSearchText(txt, 1, GetClientIP(), 0, TotalCount,"Practice Area Within");

                    // lblCount.Text = dt.Rows.Count.ToString();
                    gvLst.DataSource = dt;
                    gvLst.DataBind();





                    dtGroup = ReturnRemoveUserSearchPara(dtGroup);
                    #region Content Type Group
                    var queryContentType = from row in dtGroup.AsEnumerable()
                                           group row by row.Field<string>("ResultType") into ResultType
                                           orderby ResultType.Key
                                           select new
                                           {
                                               Name = ResultType.Key.ToString() + " (" + ResultType.Count() + ")",
                                               count = ResultType.Count(),
                                               valfiel = ResultType.Key.ToString()
                                           };

                    // print result
                    //foreach (var courts in query)
                    //{
                    //    Response.Write(courts.Name + " " + courts.count);
                    //    //Console.WriteLine("{0}\t{1}", salesman.Name, salesman.CountOfClients);
                    //}
                    chkContentType.DataSource = queryContentType;
                    chkContentType.DataValueField = "valfiel";
                    chkContentType.DataTextField = "Name";
                    chkContentType.DataBind();
                    #endregion

                    #region Court Group
                    var query = from row in dtGroup.AsEnumerable()
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
                    var queryYear = from row in dtGroup.AsEnumerable()
                                    // where row.Field<string>("Year") > 200
                                    group row by row.Field<string>("Year") into YEARS
                                    orderby YEARS.Key

                                    select new
                                    {
                                        Name = YEARS.Key + " (" + YEARS.Count() + ")",
                                        count = YEARS.Count(),
                                        valfiel = YEARS.Key
                                    };

                    //chkLstYear.DataSource = queryYear;
                    //chkLstYear.DataValueField = "valfiel";
                    //chkLstYear.DataTextField = "Name";
                    //chkLstYear.DataBind();

                    chkLstYear.DataSource = CreateYearRange(dtGroup);
                    chkLstYear.DataValueField = "ValData";
                    chkLstYear.DataTextField = "Title";
                    chkLstYear.DataBind();
                    #endregion
                    divNoResult.Style["Display"] = "none";
                    divResult.Style["Display"] = "";
                }
                else
                {
                    divNoResult.Style["Display"] = "";
                    divResult.Style["Display"] = "none";
                    if (Session["MemberID"] != null)
                        chk = objsrc.InsertSearchText(HttpContext.Current.Items["PAkeywordtxt"].ToString(), 0, GetClientIP(), int.Parse(Session["MemberID"].ToString()), TotalCount,"Practice Area Within");
                    else
                        chk = objsrc.InsertSearchText(HttpContext.Current.Items["PAkeywordtxt"].ToString(), 0, GetClientIP(), 0, TotalCount,"Practice Area Within");
                }
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

                ddlFolders.DataValueField = "ID";
                ddlFolders.DataTextField = "FolderName";
                ddlFolders.DataSource = dt;
                ddlFolders.DataBind();
                ddlFolders.Items.Insert(0, new ListItem("Select", "0"));

            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("KeywordSearchResults.aspx", "GetUserFolder", e.Message);
            }
        }
        void GetSelectedItems(int FolderID)
        {
            try
            {

                //GridViewRow row = default(GridViewRow);
                CheckBox chkSelect = default(CheckBox);
                HiddenField hdID = default(HiddenField);
                HiddenField hdResultType = default(HiddenField);

                foreach (GridViewRow row in gvLst.Rows)
                {
                    if ((row != null))
                    {

                        hdID = (HiddenField)row.FindControl("hdID");
                        hdResultType = (HiddenField)row.FindControl("hdResultType");


                        chkSelect = (CheckBox)row.FindControl("chkSelect");
                        if (chkSelect.Checked == true)
                        {
                            objusr.UserID = int.Parse(Session["MemberID"].ToString());
                            objusr.FolderID = FolderID;
                            objusr.ItemType = hdResultType.Value.ToString();
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
                        //FilterResults(HttpContext.Current.Items["PAkeywordtxt"].ToString(), e.NewPageIndex, 20);

                    }
                }
                else if (Session["SearchWithIn"] != null)
                {

                    gvLst.PageIndex = e.NewPageIndex;
                    SearchWithinResult(txtSearchWithinResult.Text, e.NewPageIndex, 20);

                }
                else
                {
                    gvLst.PageIndex = e.NewPageIndex;
                    BindSearchResult(HttpContext.Current.Items["PAkeywordtxt"].ToString(), e.NewPageIndex, 20);
                }
            }
            catch (Exception ex)
            {

            }
        }
        public string HighlightText(string InputTxt, string KeywordsForHighlight)
        {
            // string Search_Str = HttpContext.Current.Items["PAkeywordtxt"].ToString();
            string Search_Str = KeywordsForHighlight;// HttpContext.Current.Items["PAkeywordtxt"].ToString();
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
                CommonClass.GetIPLocation(visitorIPAddress, ref Country, ref Region, ref City);
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
        protected void btnSearchWithinResult_Click(object sender, EventArgs e)
        {
            SearchWithinResult(txtSearchWithinResult.Text.Trim(), 0, 20);
        }

        protected void txtSearchWithinResult_TextChanged(object sender, EventArgs e)
        {
            SearchWithinResult(txtSearchWithinResult.Text.Trim(), 0, 20);
        }
        protected void ddlFolders_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFolders.SelectedValue != "0")
                GetSelectedItems(int.Parse(ddlFolders.SelectedValue));
        }
        void GetGlossoryTree()
        {
            try
            {
                //tv.Nodes.Clear();
                //DataTable dt = new DataTable();
                //dt = objkeyword.GetGlossoryByKeywordsSearch(HttpContext.Current.Items["keywordtxt"].ToString());
                //PopulateTreeView(dt, 0, null);
            }
            catch { }
        }
        private void PopulateTreeView(DataTable dtParent, int parentId, TreeNode treeNode)
        {
            foreach (DataRow row in dtParent.Rows)
            {
                TreeNode child = new TreeNode
                {
                    Text = row["GlossoryName"].ToString(),
                    Value = row["ID"].ToString()
                };
                if (parentId == 0)
                {
                    tv.Nodes.Add(child);
                    //DataTable dtChild = objkeyword.GetActiveGlossoryByParent(int.Parse(child.Value));
                    //PopulateTreeView(dtChild, int.Parse(child.Value), child);
                }
                else
                {
                    //treeNode.ChildNodes.Add(child);
                    //DataTable dtChild = objkeyword.GetActiveGlossoryByParent(int.Parse(child.Value));
                    //PopulateTreeView(dtChild, int.Parse(child.Value), child);
                }
            }
        }

        private void BindTreeViewControl(string Keyword)
        {
            try
            {
                //DataTable dt = objkeyword.GetGlossoryByKeywordsSearch(Keyword);
                //if (dt != null)
                //{
                //    DataRow[] Rows = dt.Select("ParentId=0"); // Get all parents nodes
                //    for (int i = 0; i < Rows.Length; i++)
                //    {
                //        TreeNode root = new TreeNode(Rows[i]["GlossoryName"].ToString(), Rows[i]["ID"].ToString());
                //        root.SelectAction = TreeNodeSelectAction.Expand;
                //        CreateNode(root, dt);
                //        tv.Nodes.Add(root);
                //    }
                //}
                //if (Session["MemberID"] != null)
                //{
                //    if (Session["MemberID"].ToString() == "12" || Session["MemberID"].ToString() == "8")
                //    {
                //        AccordionPane2.Visible = true;
                //    }
                //}

            }
            catch (Exception Ex) { throw Ex; }
        }

        public void CreateNode(TreeNode node, DataTable Dt)
        {
            DataRow[] Rows = Dt.Select("ParentId =" + node.Value);
            if (Rows.Length == 0) { return; }
            for (int i = 0; i < Rows.Length; i++)
            {
                TreeNode Childnode = new TreeNode(Rows[i]["GlossoryName"].ToString(), Rows[i]["ID"].ToString());
                Childnode.SelectAction = TreeNodeSelectAction.Expand;
                node.ChildNodes.Add(Childnode);
                CreateNode(Childnode, Dt);
            }
        }
        //void FilterResults(string SearchText, int Start, int End)
        //{
        //    try
        //    {
        //        string contenttype = "";
        //        string courts = "";
        //        string years = "";
        //        DataTable dt = new DataTable();

        //        string keyword = CommonClass.FormatSearchWord(SearchText);
        //        string keywordSearchWithin = CommonClass.FormatSearchWord(txtSearchWithinResult.Text.Trim());
        //        //if (Session["sesKeywordResultsWithin"] != null)
        //        //    dtFilter = (DataTable)Session["sesKeywordResultsWithin"];
        //        //else
        //        //    dtFilter = (DataTable)Session["sesKeywordResults"];
        //        for (int a = 0; a < chkContentType.Items.Count; a++)
        //        {
        //            if (chkContentType.Items[a].Selected == true)
        //            {
        //                contenttype = contenttype + chkContentType.Items[a].Value + ",";
        //            }
        //        }
        //        for (int a = 0; a < chkCourtLst.Items.Count; a++)
        //        {
        //            if (chkCourtLst.Items[a].Selected == true)
        //            {
        //                //courts = courts + "'" + chkCourtLst.Items[a].Value + "'" + ",";
        //                courts = courts + "|" + chkCourtLst.Items[a].Value + "|";
        //            }
        //        }
        //        for (int a = 0; a < chkLstYear.Items.Count; a++)
        //        {
        //            if (chkLstYear.Items[a].Selected == true)
        //            {
        //                years = years + chkLstYear.Items[a].Value + ",";
        //            }
        //        }

        //        if (!string.IsNullOrEmpty(contenttype))
        //        {
        //            contenttype = contenttype.Remove(contenttype.Length - 1);
        //            InsertAuditLog("Search", "Search Filter", "Keyword: " + keyword + " Filter ContentType: " + contenttype);
        //            //DataRow[] filter = dtFilter.Select("Court in ("+courts+")");
        //            //   dtFilter.DefaultView.RowFilter = "Court in (" + courts + ")";

        //            //dt = objkeyword.GetSearchResultsByKeywordTestFilter(SearchText, Start, End, 0,0, courts);
        //            dt = objkeyword.GetSearchResultsByKeywordFilterContentType(contenttype, keyword, Start, End);
        //            if (dt.Rows.Count > 0)
        //            {
        //                dt.Columns.Add("Title");
        //                dt.Columns.Add("Link");
        //                dt.Columns.Add("OtherContent");
        //                for (int a = 0; a < dt.Rows.Count; a++)
        //                {
        //                    if (dt.Rows[a]["ResultType"].ToString() == "Cases")
        //                    {
        //                        dt.Rows[a]["Title"] = EastLawUI.CommonClass.MakeFirstCap(dt.Rows[a]["Appeallant"].ToString()) + " VS " + EastLawUI.CommonClass.MakeFirstCap(dt.Rows[a]["Respondent"].ToString());
        //                        dt.Rows[a]["Link"] = "/Cases/" + clsUtilities.RemoveSpecialCharacter(EastLawUI.CommonClass.MakeFirstCap(EastLawUI.CommonClass.GetWords(dt.Rows[a]["Appeallant"].ToString(), 3).ToString())).Replace(" ", "-") + "VS" + clsUtilities.RemoveSpecialCharacter(EastLawUI.CommonClass.MakeFirstCap(EastLawUI.CommonClass.GetWords(dt.Rows[a]["Respondent"].ToString(), 3).ToString())).Replace(" ", "-") + "." + EncryptDecryptHelper.Encrypt(dt.Rows[a]["ID"].ToString());

        //                        string OtherCont = "<strong>Where Reported:</strong>";
        //                        if (!string.IsNullOrEmpty(dt.Rows[a]["CitationRef"].ToString()))
        //                            OtherCont = OtherCont + dt.Rows[a]["CitationRef"].ToString() + "<br />";
        //                        else
        //                            OtherCont = OtherCont + dt.Rows[a]["Citation"].ToString() + "<br />";
        //                        OtherCont = OtherCont + "<strong>Date:</strong>" + dt.Rows[a]["JDate"].ToString() + "<br /><strong>Court:</strong>" + dt.Rows[a]["Court"].ToString() + " <br /> ";
        //                        OtherCont = OtherCont + "<strong>Appeal:</strong>" + dt.Rows[a]["Appeal"].ToString() + "<br />";
        //                        if (!string.IsNullOrEmpty(dt.Rows[a]["Keywords"].ToString()))
        //                            OtherCont = OtherCont + "<strong>Keywords:</strong><span style='font-size:14px'>" + HighlightText(HighlightTextWithin(dt.Rows[a]["Keywords"].ToString())) + "</span><br /><br />";

        //                        //if (!string.IsNullOrEmpty(dt.Rows[a]["Result"].ToString()))
        //                        //    OtherCont = OtherCont + "<strong>Result: </strong>" + dt.Rows[a]["Result"].ToString() + "<br /><br />";
        //                        //OtherCont = OtherCont + HighlightText(HighlightTextWithin(dt.Rows[a]["ShortDes"].ToString().Replace("</p>", "").Replace("<p>", "").Replace("<br>", "").Replace("<b>", "").Replace("</b>", "")));

        //                        //dt.Rows[a]["OtherContent"] = OtherCont;

        //                        if (!string.IsNullOrEmpty(dt.Rows[a]["Result"].ToString()))
        //                            OtherCont = OtherCont + "<strong>Result: </strong>" + dt.Rows[a]["Result"].ToString() + "<br /><br />";
        //                        OtherCont = OtherCont + HighlightText(HighlightTextWithin(dt.Rows[a]["ShortDes"].ToString().Replace("</p>", "").Replace("<p>", "").Replace("<br>", "").Replace("<b>", "").Replace("</b>", "")));

        //                        dt.Rows[a]["OtherContent"] = OtherCont + HighlightText(HighlightTextWithin(EastLawUI.CommonClass.GetWords(EastLawUI.CommonClass.GetShortDesc(dt.Rows[a]["Judgment"].ToString(), HttpContext.Current.Items["keywordtxt"].ToString()).Replace("</p>", "").Replace("<p>", "").Replace("<br>", "").Replace("<b>", "").Replace("</b>", ""), 100)));


        //                    }
        //                    else if (dt.Rows[a]["ResultType"].ToString() == "Statutes")
        //                    {
        //                        dt.Rows[a]["Title"] = EastLawUI.CommonClass.MakeFirstCap(dt.Rows[a]["Appeallant"].ToString());
        //                        dt.Rows[a]["Link"] = "/Statutes/" + clsUtilities.RemoveSpecialCharacter(EastLawUI.CommonClass.MakeFirstCap(EastLawUI.CommonClass.GetWords(dt.Rows[a]["Appeallant"].ToString(), 3).ToString())).Replace(" ", "-") + "." + EncryptDecryptHelper.Encrypt(dt.Rows[a]["ID"].ToString());
        //                        string OtherCont = "Index:" + dt.Rows[a]["Citation"].ToString() + "<br/>";
        //                        OtherCont = OtherCont + dt.Rows[a]["JDate"].ToString();
        //                        if (!string.IsNullOrEmpty(dt.Rows[a]["Respondent"].ToString()))
        //                            OtherCont = OtherCont + " | " + dt.Rows[a]["Respondent"].ToString() + "<br />";

        //                        dt.Rows[a]["OtherContent"] = OtherCont;
        //                    }
        //                    else if (dt.Rows[a]["ResultType"].ToString() == "Dictionary")
        //                    {
        //                        dt.Rows[a]["Title"] = EastLawUI.CommonClass.MakeFirstCap(dt.Rows[a]["Year"].ToString());
        //                        //dt.Rows[a]["Link"] = "/Statutes/" + clsUtilities.RemoveSpecialCharacter(EastLawUI.CommonClass.MakeFirstCap(EastLawUI.CommonClass.GetWords(dt.Rows[a]["Appeallant"].ToString(), 3).ToString())).Replace(" ", "-") + "." + EncryptDecryptHelper.Encrypt(dt.Rows[a]["ID"].ToString());
        //                        string OtherCont = "<br/>";
        //                        OtherCont = OtherCont + dt.Rows[a]["JDate"].ToString();

        //                        dt.Rows[a]["OtherContent"] = OtherCont;

        //                    }

        //                }
        //                dt.AcceptChanges();
        //                dt = ReturnRemoveUserSearchPara(dt);

        //                if (dt != null)
        //                {
        //                    DataTable dtCount = objkeyword.GetSearchResultsByKeywordFilterContentTypeCount(contenttype, keyword);

        //                    if (dtCount != null)
        //                    {
        //                        dtCount = ReturnRemoveUserSearchParaCount(dtCount);
        //                        // Declare an object variable.
        //                        object sumObject;
        //                        sumObject = dtCount.Compute("Sum(TotalRows)", "");
        //                        lblCount.Text = sumObject.ToString();

        //                        gvLst.VirtualItemCount = int.Parse(sumObject.ToString());


        //                    }
        //                }
        //                gvLst.DataSource = dt;
        //                gvLst.DataBind();
        //                Session["Sorting"] = "Yes";

        //                #region Court Group
        //                var query = from row in dt.AsEnumerable()
        //                            where !(row.Field<string>("Court") == null || row.Field<string>("Court") == "")
        //                            group row by row.Field<string>("Court") into courts3
        //                            orderby courts3.Key
        //                            select new
        //                            {
        //                                Name = courts3.Key.ToString() + " (" + courts3.Count() + ")",
        //                                count = courts3.Count(),
        //                                valfiel = courts3.Key.ToString()
        //                            };

        //                chkCourtLst.DataSource = query;
        //                chkCourtLst.DataValueField = "valfiel";
        //                chkCourtLst.DataTextField = "Name";
        //                chkCourtLst.DataBind();
        //                #endregion

        //                #region Year Group
        //                var queryYear = from row in dt.AsEnumerable()
        //                                where row.Field<int>("Year") > 200
        //                                group row by row.Field<int>("Year") into YEARS
        //                                orderby YEARS.Key

        //                                select new
        //                                {
        //                                    Name = YEARS.Key + " (" + YEARS.Count() + ")",
        //                                    count = YEARS.Count(),
        //                                    valfiel = YEARS.Key
        //                                };

        //                //chkLstYear.DataSource = queryYear;
        //                //chkLstYear.DataValueField = "valfiel";
        //                //chkLstYear.DataTextField = "Name";
        //                //chkLstYear.DataBind();

        //                chkLstYear.DataSource = CreateYearRange(dt);
        //                chkLstYear.DataValueField = "ValData";
        //                chkLstYear.DataTextField = "Title";
        //                chkLstYear.DataBind();
        //                #endregion
        //                divNoResult.Style["Display"] = "none";
        //                divResult.Style["Display"] = "";
        //                return;
        //            }
        //            else
        //            {
        //                divNoResult.Style["Display"] = "";
        //                divResult.Style["Display"] = "none";
        //            }
        //        }
        //        else if (!string.IsNullOrEmpty(courts) && !string.IsNullOrEmpty(years))
        //        {
        //            // courts = courts.Remove(courts.Length - 1);
        //            years = years.Remove(years.Length - 1);
        //            string[] YearsLenght = years.Split(',');
        //            InsertAuditLog("Search", "Search Filter", "Keyword: " + keyword + " Filter Year: " + YearsLenght.First().ToString() + " - " + YearsLenght.Last().ToString() + " Court: " + courts.ToString());
        //            if (Session["SearchWithIn"] != null)
        //            {
        //                dt = objkeyword.GetSearchWithinResultsByKeywordFilter_Court_Year(keyword, keywordSearchWithin, Start, End, int.Parse(YearsLenght.First().ToString()), int.Parse(YearsLenght.Last().ToString()), courts);
        //            }
        //            else
        //            {
        //                dt = objkeyword.GetSearchResultsByKeywordFilter_Court_Year(keyword, Start, End, int.Parse(YearsLenght.First().ToString()), int.Parse(YearsLenght.Last().ToString()), courts);
        //            }
        //            if (dt.Rows.Count > 0)
        //            {
        //                dt.Columns.Add("Title");
        //                dt.Columns.Add("Link");
        //                dt.Columns.Add("OtherContent");
        //                for (int a = 0; a < dt.Rows.Count; a++)
        //                {
        //                    if (dt.Rows[a]["ResultType"].ToString() == "Cases")
        //                    {
        //                        dt.Rows[a]["Title"] = EastLawUI.CommonClass.MakeFirstCap(dt.Rows[a]["Appeallant"].ToString()) + " VS " + EastLawUI.CommonClass.MakeFirstCap(dt.Rows[a]["Respondent"].ToString());
        //                        dt.Rows[a]["Link"] = "/Cases/" + clsUtilities.RemoveSpecialCharacter(EastLawUI.CommonClass.MakeFirstCap(EastLawUI.CommonClass.GetWords(dt.Rows[a]["Appeallant"].ToString(), 3).ToString())).Replace(" ", "-") + "VS" + clsUtilities.RemoveSpecialCharacter(EastLawUI.CommonClass.MakeFirstCap(EastLawUI.CommonClass.GetWords(dt.Rows[a]["Respondent"].ToString(), 3).ToString())).Replace(" ", "-") + "." + EncryptDecryptHelper.Encrypt(dt.Rows[a]["ID"].ToString());

        //                        string OtherCont = "<strong>Where Reported:</strong>";
        //                        if (!string.IsNullOrEmpty(dt.Rows[a]["CitationRef"].ToString()))
        //                            OtherCont = OtherCont + dt.Rows[a]["CitationRef"].ToString() + "<br />";
        //                        else
        //                            OtherCont = OtherCont + dt.Rows[a]["Citation"].ToString() + "<br />";
        //                        OtherCont = OtherCont + "<strong>Date:</strong>" + dt.Rows[a]["JDate"].ToString() + "<br /><strong>Court:</strong>" + dt.Rows[a]["Court"].ToString() + " <br /> ";
        //                        OtherCont = OtherCont + "<strong>Appeal:</strong>" + dt.Rows[a]["Appeal"].ToString() + "<br />";
        //                        if (!string.IsNullOrEmpty(dt.Rows[a]["Keywords"].ToString()))
        //                            OtherCont = OtherCont + "<strong>Keywords:</strong><span style='font-size:14px'>" + HighlightText(HighlightTextWithin(dt.Rows[a]["Keywords"].ToString())) + "</span><br /><br />";

        //                        //if (!string.IsNullOrEmpty(dt.Rows[a]["Result"].ToString()))
        //                        //    OtherCont = OtherCont + "<strong>Result: </strong>" + dt.Rows[a]["Result"].ToString() + "<br /><br />";
        //                        //OtherCont = OtherCont + HighlightText(HighlightTextWithin(dt.Rows[a]["ShortDes"].ToString().Replace("</p>", "").Replace("<p>", "").Replace("<br>", "").Replace("<b>", "").Replace("</b>", "")));

        //                        //dt.Rows[a]["OtherContent"] = OtherCont;

        //                        if (!string.IsNullOrEmpty(dt.Rows[a]["Result"].ToString()))
        //                            OtherCont = OtherCont + "<strong>Result: </strong>" + dt.Rows[a]["Result"].ToString() + "<br /><br />";
        //                        OtherCont = OtherCont + HighlightText(HighlightTextWithin(dt.Rows[a]["ShortDes"].ToString().Replace("</p>", "").Replace("<p>", "").Replace("<br>", "").Replace("<b>", "").Replace("</b>", "")));

        //                        dt.Rows[a]["OtherContent"] = OtherCont + HighlightText(HighlightTextWithin(EastLawUI.CommonClass.GetWords(EastLawUI.CommonClass.GetShortDesc(dt.Rows[a]["Judgment"].ToString(), HttpContext.Current.Items["keywordtxt"].ToString()).Replace("</p>", "").Replace("<p>", "").Replace("<br>", "").Replace("<b>", "").Replace("</b>", ""), 100)));


        //                    }
        //                    else if (dt.Rows[a]["ResultType"].ToString() == "Statutes")
        //                    {
        //                        dt.Rows[a]["Title"] = EastLawUI.CommonClass.MakeFirstCap(dt.Rows[a]["Appeallant"].ToString());
        //                        dt.Rows[a]["Link"] = "/Statutes/" + clsUtilities.RemoveSpecialCharacter(EastLawUI.CommonClass.MakeFirstCap(EastLawUI.CommonClass.GetWords(dt.Rows[a]["Appeallant"].ToString(), 3).ToString())).Replace(" ", "-") + "." + EncryptDecryptHelper.Encrypt(dt.Rows[a]["ID"].ToString());
        //                        string OtherCont = "Index:" + dt.Rows[a]["Citation"].ToString() + "<br/>";
        //                        OtherCont = OtherCont + dt.Rows[a]["JDate"].ToString();
        //                        if (!string.IsNullOrEmpty(dt.Rows[a]["Respondent"].ToString()))
        //                            OtherCont = OtherCont + " | " + dt.Rows[a]["Respondent"].ToString() + "<br />";

        //                        dt.Rows[a]["OtherContent"] = OtherCont;
        //                    }
        //                    else if (dt.Rows[a]["ResultType"].ToString() == "Dictionary")
        //                    {
        //                        dt.Rows[a]["Title"] = EastLawUI.CommonClass.MakeFirstCap(dt.Rows[a]["Year"].ToString());
        //                        //dt.Rows[a]["Link"] = "/Statutes/" + clsUtilities.RemoveSpecialCharacter(EastLawUI.CommonClass.MakeFirstCap(EastLawUI.CommonClass.GetWords(dt.Rows[a]["Appeallant"].ToString(), 3).ToString())).Replace(" ", "-") + "." + EncryptDecryptHelper.Encrypt(dt.Rows[a]["ID"].ToString());
        //                        string OtherCont = "<br/>";
        //                        OtherCont = OtherCont + dt.Rows[a]["JDate"].ToString();

        //                        dt.Rows[a]["OtherContent"] = OtherCont;

        //                    }

        //                }
        //                dt.AcceptChanges();
        //                dt = ReturnRemoveUserSearchPara(dt);

        //                if (dt != null)
        //                {
        //                    DataTable dtCount = new DataTable();

        //                    if (Session["SearchWithIn"] != null)
        //                    {

        //                        dtCount = objkeyword.GetSearchWithinResultsByKeywordFilter_Court_Year_Count(keyword, keywordSearchWithin, int.Parse(YearsLenght.First().ToString()), int.Parse(YearsLenght.Last().ToString()), courts);
        //                    }
        //                    else
        //                    {
        //                        dtCount = objkeyword.GetSearchResultsByKeywordFilter_Court_YearCount(keyword, int.Parse(YearsLenght.First().ToString()), int.Parse(YearsLenght.Last().ToString()), courts);
        //                    }


        //                    if (dtCount != null)
        //                    {
        //                        dtCount = ReturnRemoveUserSearchParaCount(dtCount);
        //                        // Declare an object variable.
        //                        object sumObject;
        //                        sumObject = dtCount.Compute("Sum(TotalRows)", "");
        //                        lblCount.Text = sumObject.ToString();

        //                        gvLst.VirtualItemCount = int.Parse(sumObject.ToString());


        //                    }
        //                    // Declare an object variable.
        //                    //object sumObject;
        //                    //sumObject = dt.Compute("Sum(TotalRows)", "");
        //                    //lblCount.Text = sumObject.ToString();

        //                    // gvLst.VirtualItemCount = int.Parse(dt.Rows.Count.ToString());
        //                }
        //                gvLst.DataSource = dt;
        //                gvLst.DataBind();
        //                Session["Sorting"] = "Yes";
        //                #region Court Group
        //                var query = from row in dt.AsEnumerable()
        //                            where !(row.Field<string>("Court") == null || row.Field<string>("Court") == "")
        //                            group row by row.Field<string>("Court") into courts3
        //                            orderby courts3.Key
        //                            select new
        //                            {
        //                                Name = courts3.Key.ToString() + " (" + courts3.Count() + ")",
        //                                count = courts3.Count(),
        //                                valfiel = courts3.Key.ToString()
        //                            };

        //                chkCourtLst.DataSource = query;
        //                chkCourtLst.DataValueField = "valfiel";
        //                chkCourtLst.DataTextField = "Name";
        //                chkCourtLst.DataBind();
        //                #endregion
        //                divNoResult.Style["Display"] = "none";
        //                divResult.Style["Display"] = "";

        //                return;
        //            }
        //            else
        //            {
        //                divNoResult.Style["Display"] = "";
        //                divResult.Style["Display"] = "none";
        //            }



        //        }
        //        else if (!string.IsNullOrEmpty(years))
        //        {
        //            years = years.Remove(years.Length - 1);
        //            string[] YearsLenght = years.Split(',');
        //            //DataRow[] filter = dtFilter.Select("Court in ("+courts+")");
        //            // dtFilter.DefaultView.RowFilter = "Year in (" + years + ")";
        //            InsertAuditLog("Search", "Search Filter", "Keyword: " + keyword + " Filter Year: " + YearsLenght.First().ToString() + " - " + YearsLenght.Last().ToString());
        //            if (Session["SearchWithIn"] != null)
        //            {
        //                dt = objkeyword.GetSearchWithinResultsByKeywordTestFilter(keyword, keywordSearchWithin, Start, End, int.Parse(YearsLenght.First().ToString()), int.Parse(YearsLenght.Last().ToString()), "");
        //            }
        //            else
        //            {
        //                dt = objkeyword.GetSearchResultsByKeywordTestFilter(keyword, Start, End, int.Parse(YearsLenght.First().ToString()), int.Parse(YearsLenght.Last().ToString()), "");
        //            }
        //            if (dt.Rows.Count > 0)
        //            {
        //                dt.Columns.Add("Title");
        //                dt.Columns.Add("Link");
        //                dt.Columns.Add("OtherContent");
        //                for (int a = 0; a < dt.Rows.Count; a++)
        //                {
        //                    if (dt.Rows[a]["ResultType"].ToString() == "Cases")
        //                    {
        //                        dt.Rows[a]["Title"] = EastLawUI.CommonClass.MakeFirstCap(dt.Rows[a]["Appeallant"].ToString()) + " VS " + EastLawUI.CommonClass.MakeFirstCap(dt.Rows[a]["Respondent"].ToString());
        //                        dt.Rows[a]["Link"] = "/Cases/" + clsUtilities.RemoveSpecialCharacter(EastLawUI.CommonClass.MakeFirstCap(EastLawUI.CommonClass.GetWords(dt.Rows[a]["Appeallant"].ToString(), 3).ToString())).Replace(" ", "-") + "VS" + clsUtilities.RemoveSpecialCharacter(EastLawUI.CommonClass.MakeFirstCap(EastLawUI.CommonClass.GetWords(dt.Rows[a]["Respondent"].ToString(), 3).ToString())).Replace(" ", "-") + "." + EncryptDecryptHelper.Encrypt(dt.Rows[a]["ID"].ToString());

        //                        string OtherCont = "<strong>Where Reported:</strong>";
        //                        if (!string.IsNullOrEmpty(dt.Rows[a]["CitationRef"].ToString()))
        //                            OtherCont = OtherCont + dt.Rows[a]["CitationRef"].ToString() + "<br />";
        //                        else
        //                            OtherCont = OtherCont + dt.Rows[a]["Citation"].ToString() + "<br />";
        //                        OtherCont = OtherCont + "<strong>Date:</strong>" + dt.Rows[a]["JDate"].ToString() + "<br /><strong>Court:</strong>" + dt.Rows[a]["Court"].ToString() + " <br /> ";
        //                        OtherCont = OtherCont + "<strong>Appeal:</strong>" + dt.Rows[a]["Appeal"].ToString() + "<br />";
        //                        if (!string.IsNullOrEmpty(dt.Rows[a]["Keywords"].ToString()))
        //                            OtherCont = OtherCont + "<strong>Keywords:</strong><span style='font-size:14px'>" + HighlightText(HighlightTextWithin(dt.Rows[a]["Keywords"].ToString())) + "</span><br /><br />";

        //                        //if (!string.IsNullOrEmpty(dt.Rows[a]["Result"].ToString()))
        //                        //    OtherCont = OtherCont + "<strong>Result: </strong>" + dt.Rows[a]["Result"].ToString() + "<br /><br />";
        //                        //OtherCont = OtherCont + HighlightText(HighlightTextWithin(dt.Rows[a]["ShortDes"].ToString().Replace("</p>", "").Replace("<p>", "").Replace("<br>", "").Replace("<b>", "").Replace("</b>", "")));

        //                        //dt.Rows[a]["OtherContent"] = OtherCont;

        //                        if (!string.IsNullOrEmpty(dt.Rows[a]["Result"].ToString()))
        //                            OtherCont = OtherCont + "<strong>Result: </strong>" + dt.Rows[a]["Result"].ToString() + "<br /><br />";
        //                        OtherCont = OtherCont + HighlightText(HighlightTextWithin(dt.Rows[a]["ShortDes"].ToString().Replace("</p>", "").Replace("<p>", "").Replace("<br>", "").Replace("<b>", "").Replace("</b>", "")));

        //                        dt.Rows[a]["OtherContent"] = OtherCont + HighlightText(HighlightTextWithin(EastLawUI.CommonClass.GetWords(EastLawUI.CommonClass.GetShortDesc(dt.Rows[a]["Judgment"].ToString(), HttpContext.Current.Items["keywordtxt"].ToString()).Replace("</p>", "").Replace("<p>", "").Replace("<br>", "").Replace("<b>", "").Replace("</b>", ""), 100)));


        //                    }
        //                    else if (dt.Rows[a]["ResultType"].ToString() == "Statutes")
        //                    {
        //                        dt.Rows[a]["Title"] = EastLawUI.CommonClass.MakeFirstCap(dt.Rows[a]["Appeallant"].ToString());
        //                        dt.Rows[a]["Link"] = "/Statutes/" + clsUtilities.RemoveSpecialCharacter(EastLawUI.CommonClass.MakeFirstCap(EastLawUI.CommonClass.GetWords(dt.Rows[a]["Appeallant"].ToString(), 3).ToString())).Replace(" ", "-") + "." + EncryptDecryptHelper.Encrypt(dt.Rows[a]["ID"].ToString());
        //                        string OtherCont = "Index:" + dt.Rows[a]["Citation"].ToString() + "<br/>";
        //                        OtherCont = OtherCont + dt.Rows[a]["JDate"].ToString();
        //                        if (!string.IsNullOrEmpty(dt.Rows[a]["Respondent"].ToString()))
        //                            OtherCont = OtherCont + " | " + dt.Rows[a]["Respondent"].ToString() + "<br />";

        //                        dt.Rows[a]["OtherContent"] = OtherCont;
        //                    }
        //                    else if (dt.Rows[a]["ResultType"].ToString() == "Dictionary")
        //                    {
        //                        dt.Rows[a]["Title"] = EastLawUI.CommonClass.MakeFirstCap(dt.Rows[a]["Year"].ToString());
        //                        //dt.Rows[a]["Link"] = "/Statutes/" + clsUtilities.RemoveSpecialCharacter(EastLawUI.CommonClass.MakeFirstCap(EastLawUI.CommonClass.GetWords(dt.Rows[a]["Appeallant"].ToString(), 3).ToString())).Replace(" ", "-") + "." + EncryptDecryptHelper.Encrypt(dt.Rows[a]["ID"].ToString());
        //                        string OtherCont = "<br/>";
        //                        OtherCont = OtherCont + dt.Rows[a]["JDate"].ToString();

        //                        dt.Rows[a]["OtherContent"] = OtherCont;

        //                    }

        //                }
        //                dt.AcceptChanges();
        //                dt = ReturnRemoveUserSearchPara(dt);

        //                if (dt != null)
        //                {
        //                    DataTable dtCount = new DataTable();

        //                    if (Session["SearchWithIn"] != null)
        //                    {

        //                        dtCount = objkeyword.GetSearchWithinResultsByKeywordTestFilterCount(keyword, keywordSearchWithin, int.Parse(YearsLenght.First().ToString()), int.Parse(YearsLenght.Last().ToString()), "");
        //                    }
        //                    else
        //                    {
        //                        dtCount = objkeyword.GetSearchResultsByKeywordTestFilterCount(keyword, int.Parse(YearsLenght.First().ToString()), int.Parse(YearsLenght.Last().ToString()), "");
        //                    }


        //                    if (dtCount != null)
        //                    {
        //                        dtCount = ReturnRemoveUserSearchParaCount(dtCount);
        //                        // Declare an object variable.
        //                        object sumObject;
        //                        sumObject = dtCount.Compute("Sum(TotalRows)", "");
        //                        lblCount.Text = sumObject.ToString();

        //                        gvLst.VirtualItemCount = int.Parse(sumObject.ToString());


        //                    }
        //                    // Declare an object variable.
        //                    //object sumObject;
        //                    //sumObject = dt.Compute("Sum(TotalRows)", "");
        //                    //lblCount.Text = sumObject.ToString();

        //                    // gvLst.VirtualItemCount = int.Parse(dt.Rows.Count.ToString());
        //                }
        //                gvLst.DataSource = dt;
        //                gvLst.DataBind();
        //                Session["Sorting"] = "Yes";
        //                #region Court Group
        //                var query = from row in dt.AsEnumerable()
        //                            where !(row.Field<string>("Court") == null || row.Field<string>("Court") == "")
        //                            group row by row.Field<string>("Court") into courts3
        //                            orderby courts3.Key
        //                            select new
        //                            {
        //                                Name = courts3.Key.ToString() + " (" + courts3.Count() + ")",
        //                                count = courts3.Count(),
        //                                valfiel = courts3.Key.ToString()
        //                            };

        //                chkCourtLst.DataSource = query;
        //                chkCourtLst.DataValueField = "valfiel";
        //                chkCourtLst.DataTextField = "Name";
        //                chkCourtLst.DataBind();
        //                #endregion
        //                divNoResult.Style["Display"] = "none";
        //                divResult.Style["Display"] = "";

        //                return;
        //            }
        //            else
        //            {
        //                divNoResult.Style["Display"] = "";
        //                divResult.Style["Display"] = "none";
        //            }
        //        }
        //        else if (!string.IsNullOrEmpty(courts))
        //        {
        //            // courts = courts.Remove(courts.Length - 1);
        //            //DataRow[] filter = dtFilter.Select("Court in ("+courts+")");
        //            //   dtFilter.DefaultView.RowFilter = "Court in (" + courts + ")";
        //            InsertAuditLog("Search", "Search Filter", "Keyword: " + keyword + " Filter Court: " + courts);
        //            //dt = objkeyword.GetSearchResultsByKeywordTestFilter(SearchText, Start, End, 0,0, courts);
        //            if (Session["SearchWithIn"] != null)
        //            {

        //                dt = objkeyword.GetSearchWithinResultsByKeywordFilter_Court(keyword, keywordSearchWithin, Start, End, 0, 0, courts);
        //            }
        //            else
        //            {
        //                dt = objkeyword.GetSearchResultsByKeywordTestFilterCourt(keyword, Start, End, 0, 0, courts);
        //            }

        //            // dt = objkeyword.GetSearchResultsByKeywordTestFilterCourt(keyword, Start, End, 0, 0, courts);
        //            if (dt.Rows.Count > 0)
        //            {
        //                dt.Columns.Add("Title");
        //                dt.Columns.Add("Link");
        //                dt.Columns.Add("OtherContent");
        //                for (int a = 0; a < dt.Rows.Count; a++)
        //                {
        //                    if (dt.Rows[a]["ResultType"].ToString() == "Cases")
        //                    {
        //                        dt.Rows[a]["Title"] = EastLawUI.CommonClass.MakeFirstCap(dt.Rows[a]["Appeallant"].ToString()) + " VS " + EastLawUI.CommonClass.MakeFirstCap(dt.Rows[a]["Respondent"].ToString());
        //                        dt.Rows[a]["Link"] = "/Cases/" + clsUtilities.RemoveSpecialCharacter(EastLawUI.CommonClass.MakeFirstCap(EastLawUI.CommonClass.GetWords(dt.Rows[a]["Appeallant"].ToString(), 3).ToString())).Replace(" ", "-") + "VS" + clsUtilities.RemoveSpecialCharacter(EastLawUI.CommonClass.MakeFirstCap(EastLawUI.CommonClass.GetWords(dt.Rows[a]["Respondent"].ToString(), 3).ToString())).Replace(" ", "-") + "." + EncryptDecryptHelper.Encrypt(dt.Rows[a]["ID"].ToString());

        //                        string OtherCont = "<strong>Where Reported:</strong>";
        //                        if (!string.IsNullOrEmpty(dt.Rows[a]["CitationRef"].ToString()))
        //                            OtherCont = OtherCont + dt.Rows[a]["CitationRef"].ToString() + "<br />";
        //                        else
        //                            OtherCont = OtherCont + dt.Rows[a]["Citation"].ToString() + "<br />";
        //                        OtherCont = OtherCont + "<strong>Date:</strong>" + dt.Rows[a]["JDate"].ToString() + "<br /><strong>Court:</strong>" + dt.Rows[a]["Court"].ToString() + " <br /> ";
        //                        OtherCont = OtherCont + "<strong>Appeal:</strong>" + dt.Rows[a]["Appeal"].ToString() + "<br />";
        //                        if (!string.IsNullOrEmpty(dt.Rows[a]["Keywords"].ToString()))
        //                            OtherCont = OtherCont + "<strong>Keywords:</strong><span style='font-size:14px'>" + HighlightText(HighlightTextWithin(dt.Rows[a]["Keywords"].ToString())) + "</span><br /><br />";

        //                        //if (!string.IsNullOrEmpty(dt.Rows[a]["Result"].ToString()))
        //                        //    OtherCont = OtherCont + "<strong>Result: </strong>" + dt.Rows[a]["Result"].ToString() + "<br /><br />";
        //                        //OtherCont = OtherCont + HighlightText(HighlightTextWithin(dt.Rows[a]["ShortDes"].ToString().Replace("</p>", "").Replace("<p>", "").Replace("<br>", "").Replace("<b>", "").Replace("</b>", "")));

        //                        //dt.Rows[a]["OtherContent"] = OtherCont;

        //                        if (!string.IsNullOrEmpty(dt.Rows[a]["Result"].ToString()))
        //                            OtherCont = OtherCont + "<strong>Result: </strong>" + dt.Rows[a]["Result"].ToString() + "<br /><br />";
        //                        OtherCont = OtherCont + HighlightText(HighlightTextWithin(dt.Rows[a]["ShortDes"].ToString().Replace("</p>", "").Replace("<p>", "").Replace("<br>", "").Replace("<b>", "").Replace("</b>", "")));

        //                        dt.Rows[a]["OtherContent"] = OtherCont + HighlightText(HighlightTextWithin(EastLawUI.CommonClass.GetWords(EastLawUI.CommonClass.GetShortDesc(dt.Rows[a]["Judgment"].ToString(), HttpContext.Current.Items["keywordtxt"].ToString()).Replace("</p>", "").Replace("<p>", "").Replace("<br>", "").Replace("<b>", "").Replace("</b>", ""), 200)));


        //                    }
        //                    else if (dt.Rows[a]["ResultType"].ToString() == "Statutes")
        //                    {
        //                        dt.Rows[a]["Title"] = EastLawUI.CommonClass.MakeFirstCap(dt.Rows[a]["Appeallant"].ToString());
        //                        dt.Rows[a]["Link"] = "/Statutes/" + clsUtilities.RemoveSpecialCharacter(EastLawUI.CommonClass.MakeFirstCap(EastLawUI.CommonClass.GetWords(dt.Rows[a]["Appeallant"].ToString(), 3).ToString())).Replace(" ", "-") + "." + EncryptDecryptHelper.Encrypt(dt.Rows[a]["ID"].ToString());
        //                        string OtherCont = "Index:" + dt.Rows[a]["Citation"].ToString() + "<br/>";
        //                        OtherCont = OtherCont + dt.Rows[a]["JDate"].ToString();
        //                        if (!string.IsNullOrEmpty(dt.Rows[a]["Respondent"].ToString()))
        //                            OtherCont = OtherCont + " | " + dt.Rows[a]["Respondent"].ToString() + "<br />";

        //                        dt.Rows[a]["OtherContent"] = OtherCont;
        //                    }
        //                    else if (dt.Rows[a]["ResultType"].ToString() == "Dictionary")
        //                    {
        //                        dt.Rows[a]["Title"] = EastLawUI.CommonClass.MakeFirstCap(dt.Rows[a]["Year"].ToString());
        //                        //dt.Rows[a]["Link"] = "/Statutes/" + clsUtilities.RemoveSpecialCharacter(EastLawUI.CommonClass.MakeFirstCap(EastLawUI.CommonClass.GetWords(dt.Rows[a]["Appeallant"].ToString(), 3).ToString())).Replace(" ", "-") + "." + EncryptDecryptHelper.Encrypt(dt.Rows[a]["ID"].ToString());
        //                        string OtherCont = "<br/>";
        //                        OtherCont = OtherCont + dt.Rows[a]["JDate"].ToString();

        //                        dt.Rows[a]["OtherContent"] = OtherCont;

        //                    }

        //                }
        //                dt.AcceptChanges();
        //                dt = ReturnRemoveUserSearchPara(dt);

        //                if (dt != null)
        //                {
        //                    //DataTable dtCount = objkeyword.GetSearchResultsByKeywordTestFilterCourtCount(keyword, 0, 0, courts);

        //                    DataTable dtCount = new DataTable();

        //                    if (Session["SearchWithIn"] != null)
        //                    {
        //                        dtCount = objkeyword.GetSearchWithinResultsByKeywordFilter_Court_Count(keyword, keywordSearchWithin, 0, 0, courts);
        //                    }
        //                    else
        //                    {
        //                        dtCount = objkeyword.GetSearchResultsByKeywordTestFilterCourtCount(keyword, 0, 0, courts);
        //                    }
        //                    if (dtCount != null)
        //                    {
        //                        dtCount = ReturnRemoveUserSearchParaCount(dtCount);
        //                        // Declare an object variable.
        //                        object sumObject;
        //                        sumObject = dtCount.Compute("Sum(TotalRows)", "");
        //                        lblCount.Text = sumObject.ToString();

        //                        gvLst.VirtualItemCount = int.Parse(sumObject.ToString());


        //                    }
        //                    // Declare an object variable.
        //                    //object sumObject;
        //                    //sumObject = dt.Compute("Sum(TotalRows)", "");
        //                    //lblCount.Text = sumObject.ToString();

        //                    // gvLst.VirtualItemCount = int.Parse(dt.Rows.Count.ToString());
        //                }
        //                gvLst.DataSource = dt;
        //                gvLst.DataBind();
        //                Session["Sorting"] = "Yes";

        //                #region Year Group
        //                var queryYear = from row in dt.AsEnumerable()
        //                                where row.Field<int>("Year") > 200
        //                                group row by row.Field<int>("Year") into YEARS
        //                                orderby YEARS.Key

        //                                select new
        //                                {
        //                                    Name = YEARS.Key + " (" + YEARS.Count() + ")",
        //                                    count = YEARS.Count(),
        //                                    valfiel = YEARS.Key
        //                                };

        //                //chkLstYear.DataSource = queryYear;
        //                //chkLstYear.DataValueField = "valfiel";
        //                //chkLstYear.DataTextField = "Name";
        //                //chkLstYear.DataBind();

        //                chkLstYear.DataSource = CreateYearRange(dt);
        //                chkLstYear.DataValueField = "ValData";
        //                chkLstYear.DataTextField = "Title";
        //                chkLstYear.DataBind();
        //                #endregion
        //                divNoResult.Style["Display"] = "none";
        //                divResult.Style["Display"] = "";
        //                return;
        //            }
        //            else
        //            {
        //                divNoResult.Style["Display"] = "";
        //                divResult.Style["Display"] = "none";
        //            }
        //        }
        //        else
        //        {

        //            BindSearchResult(HttpContext.Current.Items["keywordtxt"].ToString(), 0, 20);
        //        }
        //    }
        //    catch { }
        //}

        protected void chkCourtLst_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterResults(HttpContext.Current.Items["PAkeywordtxt"].ToString(), 0, 20);
        }

        protected void chkLstYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterResults(HttpContext.Current.Items["PAkeywordtxt"].ToString(), 0, 20);
        }

        protected void chkContentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterResults(HttpContext.Current.Items["PAkeywordtxt"].ToString(), 0, 20);
        }

        protected void lnkClearFilter_Click(object sender, EventArgs e)
        {
            TextBox txtSearch = (TextBox)Master.FindControl("txtSearch");
            InsertAuditLog("Search", "Clear Filter", "");
            Response.Redirect("/Search/" + txtSearch.Text);
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
        DataTable ReturnRemoveUserSearchParaCount(DataTable dt)
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
                            dt.Rows[0].Delete();

                            dt.AcceptChanges();
                        }
                        if (string.IsNullOrEmpty(membersearch[1]))
                        {
                            dt.Rows[1].Delete();
                            dt.AcceptChanges();
                        }

                    }

                }
            }
            catch { }
            return dt;
        }
        void FilterResults(string SearchText, int Start, int End)
        {
            try
            {
                string contenttype = "";
                string courts = "";
                string years = "";
                DataTable dt = new DataTable();

                string keyword = CommonClass.FormatSearchWord(SearchText);
                string keywordP1 = CommonClass.FormatSearchWordP1(SearchText);
                string keywordSearchWithin = CommonClass.FormatSearchWord(txtSearchWithinResult.Text.Trim());
                //if (Session["sesKeywordResultsWithin"] != null)
                //    dtFilter = (DataTable)Session["sesKeywordResultsWithin"];
                //else
                //    dtFilter = (DataTable)Session["sesKeywordResults"];
                for (int a = 0; a < chkContentType.Items.Count; a++)
                {
                    if (chkContentType.Items[a].Selected == true)
                    {
                        contenttype = contenttype + chkContentType.Items[a].Value + ",";
                    }
                }
                for (int a = 0; a < chkCourtLst.Items.Count; a++)
                {
                    if (chkCourtLst.Items[a].Selected == true)
                    {
                        //courts = courts + "'" + chkCourtLst.Items[a].Value + "'" + ",";
                        courts = courts + "|" + chkCourtLst.Items[a].Value + "|";
                    }
                }
                for (int a = 0; a < chkLstYear.Items.Count; a++)
                {
                    if (chkLstYear.Items[a].Selected == true)
                    {
                        years = years + chkLstYear.Items[a].Value + ",";
                    }
                }

                #region ContentType
                if (!string.IsNullOrEmpty(contenttype))
                {
                    contenttype = contenttype.Remove(contenttype.Length - 1);
                    InsertAuditLog("Search", "Search Filter", "Keyword: " + keyword + " Filter ContentType: " + contenttype);
                    //DataRow[] filter = dtFilter.Select("Court in ("+courts+")");
                    //   dtFilter.DefaultView.RowFilter = "Court in (" + courts + ")";

                    //dt = objkeyword.GetSearchResultsByKeywordTestFilter(SearchText, Start, End, 0,0, courts);
                    dt = objPA.GetSearchResultsByPracticeAreaFilterContentType(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString()).ToString()), contenttype, keyword, Start, End);
                    if (dt.Rows.Count > 0)
                    {
                        dt.Columns.Add("Title");
                        dt.Columns.Add("Link");
                        dt.Columns.Add("OtherContent");
                        for (int a = 0; a < dt.Rows.Count; a++)
                        {
                            if (dt.Rows[a]["ResultType"].ToString() == "Cases")
                            {
                                dt.Rows[a]["Title"] = CommonClass.MakeFirstCap(dt.Rows[a]["Appeallant"].ToString()) + " VS " + CommonClass.MakeFirstCap(dt.Rows[a]["Respondent"].ToString());
                                dt.Rows[a]["Link"] = "/Cases/" + clsUtilities.RemoveSpecialCharacter(CommonClass.MakeFirstCap(CommonClass.GetWords(dt.Rows[a]["Appeallant"].ToString(), 3).ToString())).Replace(" ", "-") + "VS" + clsUtilities.RemoveSpecialCharacter(CommonClass.MakeFirstCap(CommonClass.GetWords(dt.Rows[a]["Respondent"].ToString(), 3).ToString())).Replace(" ", "-") + "." + EncryptDecryptHelper.Encrypt(dt.Rows[a]["ID"].ToString());

                                string OtherCont = "<strong>Where Reported:</strong>";
                                if (!string.IsNullOrEmpty(dt.Rows[a]["CitationRef"].ToString()))
                                    OtherCont = OtherCont + dt.Rows[a]["CitationRef"].ToString() + "<br />";
                                else
                                    OtherCont = OtherCont + dt.Rows[a]["Citation"].ToString() + "<br />";
                                OtherCont = OtherCont + "<strong>Date:</strong>" + dt.Rows[a]["JDate"].ToString() + "<br /><strong>Court:</strong>" + dt.Rows[a]["Court"].ToString() + " <br /> ";
                                OtherCont = OtherCont + "<strong>Appeal:</strong>" + dt.Rows[a]["Appeal"].ToString() + "<br />";
                                if (!string.IsNullOrEmpty(dt.Rows[a]["Keywords"].ToString()))
                                    OtherCont = OtherCont + "<strong>Keywords:</strong><span style='font-size:14px'>" + HighlightText(HighlightTextWithin(dt.Rows[a]["Keywords"].ToString())) + "</span><br /><br />";

                                //if (!string.IsNullOrEmpty(dt.Rows[a]["Result"].ToString()))
                                //    OtherCont = OtherCont + "<strong>Result: </strong>" + dt.Rows[a]["Result"].ToString() + "<br /><br />";
                                //OtherCont = OtherCont + HighlightText(HighlightTextWithin(dt.Rows[a]["ShortDes"].ToString().Replace("</p>", "").Replace("<p>", "").Replace("<br>", "").Replace("<b>", "").Replace("</b>", "")));

                                //dt.Rows[a]["OtherContent"] = OtherCont;

                                if (!string.IsNullOrEmpty(dt.Rows[a]["Result"].ToString()))
                                    OtherCont = OtherCont + "<strong>Result: </strong>" + dt.Rows[a]["Result"].ToString() + "<br /><br />";
                                OtherCont = OtherCont + HighlightText(HighlightTextWithin(dt.Rows[a]["ShortDes"].ToString().Replace("</p>", "").Replace("<p>", "").Replace("<br>", "").Replace("<b>", "").Replace("</b>", "")));

                                dt.Rows[a]["OtherContent"] = OtherCont + HighlightText(HighlightTextWithin(CommonClass.GetWords(CommonClass.GetShortDesc(dt.Rows[a]["Judgment"].ToString(), HttpContext.Current.Items["keywordtxt"].ToString()).Replace("</p>", "").Replace("<p>", "").Replace("<br>", "").Replace("<b>", "").Replace("</b>", ""), 100)));


                            }
                            else if (dt.Rows[a]["ResultType"].ToString() == "Statutes")
                            {
                                dt.Rows[a]["Title"] = CommonClass.MakeFirstCap(dt.Rows[a]["Appeallant"].ToString());
                                dt.Rows[a]["Link"] = "/Statutes/" + clsUtilities.RemoveSpecialCharacter(CommonClass.MakeFirstCap(CommonClass.GetWords(dt.Rows[a]["Appeallant"].ToString(), 3).ToString())).Replace(" ", "-") + "." + EncryptDecryptHelper.Encrypt(dt.Rows[a]["ID"].ToString());
                                string OtherCont = "Index:" + dt.Rows[a]["Citation"].ToString() + "<br/>";
                                OtherCont = OtherCont + dt.Rows[a]["JDate"].ToString();
                                if (!string.IsNullOrEmpty(dt.Rows[a]["Respondent"].ToString()))
                                    OtherCont = OtherCont + " | " + dt.Rows[a]["Respondent"].ToString() + "<br />";

                                dt.Rows[a]["OtherContent"] = OtherCont;
                            }
                            else if (dt.Rows[a]["ResultType"].ToString() == "Dictionary")
                            {
                                dt.Rows[a]["Title"] = CommonClass.MakeFirstCap(dt.Rows[a]["Year"].ToString());
                                //dt.Rows[a]["Link"] = "/Statutes/" + clsUtilities.RemoveSpecialCharacter(EastLawUI.CommonClass.MakeFirstCap(EastLawUI.CommonClass.GetWords(dt.Rows[a]["Appeallant"].ToString(), 3).ToString())).Replace(" ", "-") + "." + EncryptDecryptHelper.Encrypt(dt.Rows[a]["ID"].ToString());
                                string OtherCont = "<br/>";
                                OtherCont = OtherCont + dt.Rows[a]["JDate"].ToString();

                                dt.Rows[a]["OtherContent"] = OtherCont;

                            }

                        }
                        dt.AcceptChanges();
                        dt = ReturnRemoveUserSearchPara(dt);

                        if (dt != null)
                        {
                            DataTable dtCount = objPA.GetSearchResultsByPracticeAreaFilterContentTypeCount(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString()).ToString()), contenttype, keyword);

                            if (dtCount != null)
                            {
                                dtCount = ReturnRemoveUserSearchParaCount(dtCount);
                                // Declare an object variable.
                                object sumObject;
                                sumObject = dtCount.Compute("Sum(TotalRows)", "");
                                lblCount.Text = sumObject.ToString();

                                gvLst.VirtualItemCount = int.Parse(sumObject.ToString());


                            }
                        }
                        gvLst.DataSource = dt;
                        gvLst.DataBind();
                        Session["Sorting"] = "Yes";

                        #region Court Group
                        var query = from row in dt.AsEnumerable()
                                    where !(row.Field<string>("Court") == null || row.Field<string>("Court") == "")
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

                        #region Year Group
                        var queryYear = from row in dt.AsEnumerable()
                                        where row.Field<int>("Year") > 200
                                        group row by row.Field<int>("Year") into YEARS
                                        orderby YEARS.Key

                                        select new
                                        {
                                            Name = YEARS.Key + " (" + YEARS.Count() + ")",
                                            count = YEARS.Count(),
                                            valfiel = YEARS.Key
                                        };

                        //chkLstYear.DataSource = queryYear;
                        //chkLstYear.DataValueField = "valfiel";
                        //chkLstYear.DataTextField = "Name";
                        //chkLstYear.DataBind();

                        chkLstYear.DataSource = CreateYearRange(dt);
                        chkLstYear.DataValueField = "ValData";
                        chkLstYear.DataTextField = "Title";
                        chkLstYear.DataBind();
                        #endregion
                        divNoResult.Style["Display"] = "none";
                        divResult.Style["Display"] = "";
                        return;
                    }
                    else
                    {
                        divNoResult.Style["Display"] = "";
                        divResult.Style["Display"] = "none";
                    }
                }
                #endregion
                #region Court_And_Year
                else if (!string.IsNullOrEmpty(courts) && !string.IsNullOrEmpty(years))
                {
                    // courts = courts.Remove(courts.Length - 1);
                    years = years.Remove(years.Length - 1);
                    string[] YearsLenght = years.Split(',');
                    InsertAuditLog("Search", "Search Filter", "Keyword: " + keyword + " Filter Year: " + YearsLenght.First().ToString() + " - " + YearsLenght.Last().ToString() + " Court: " + courts.ToString());

                    DataTable dtCount = new DataTable();
                    int KeyPTag = 0;
                    if (Session["SearchWithIn"] != null)
                    {
                        dt = objPA.GetSearchWithinResultsByPracticeAreaFilter_Court_Year(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString()).ToString()), keyword, keywordSearchWithin, Start, End, int.Parse(YearsLenght.First().ToString()), int.Parse(YearsLenght.Last().ToString()), courts);
                    }
                    else
                    {
                        dt = objPA.GetSearchResultsByPracticeAreaFilter_Court_Year(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString()).ToString()), keywordP1, Start, End, int.Parse(YearsLenght.First().ToString()), int.Parse(YearsLenght.Last().ToString()), courts);
                        if (dt != null)
                        {
                            if (dt.Rows.Count == 0)
                            {
                                dt = objPA.GetSearchResultsByPracticeAreaFilter_Court_Year(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString()).ToString()), keyword, Start, End, int.Parse(YearsLenght.First().ToString()), int.Parse(YearsLenght.Last().ToString()), courts);

                                if (Session["SearchWithIn"] != null)
                                {

                                    dtCount = objPA.GetSearchWithinResultsByPracticeAreaFilter_Court_Year_Count(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString()).ToString()), keyword, keywordSearchWithin, int.Parse(YearsLenght.First().ToString()), int.Parse(YearsLenght.Last().ToString()), courts);
                                }
                                else
                                {
                                    dtCount = objPA.GetSearchResultsByPracticeAreaFilter_Court_YearCount(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString()).ToString()), keyword, int.Parse(YearsLenght.First().ToString()), int.Parse(YearsLenght.Last().ToString()), courts);
                                }
                                KeyPTag = 2;
                            }
                            else
                            {
                                if (Session["SearchWithIn"] != null)
                                {

                                    dtCount = objPA.GetSearchWithinResultsByPracticeAreaFilter_Court_Year_Count(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString()).ToString()), keywordP1, keywordSearchWithin, int.Parse(YearsLenght.First().ToString()), int.Parse(YearsLenght.Last().ToString()), courts);
                                }
                                else
                                {
                                    dtCount = objPA.GetSearchResultsByPracticeAreaFilter_Court_YearCount(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString()).ToString()), keywordP1, int.Parse(YearsLenght.First().ToString()), int.Parse(YearsLenght.Last().ToString()), courts);
                                }
                                KeyPTag = 1;

                            }
                        }
                    }
                    if (dt.Rows.Count > 0)
                    {
                        dt.Columns.Add("Title");
                        dt.Columns.Add("Link");
                        dt.Columns.Add("OtherContent");
                        for (int a = 0; a < dt.Rows.Count; a++)
                        {
                            if (dt.Rows[a]["ResultType"].ToString() == "Cases")
                            {
                                dt.Rows[a]["Title"] = CommonClass.MakeFirstCap(dt.Rows[a]["Appeallant"].ToString()) + " VS " + CommonClass.MakeFirstCap(dt.Rows[a]["Respondent"].ToString());
                                dt.Rows[a]["Link"] = "/Cases/" + clsUtilities.RemoveSpecialCharacter(CommonClass.MakeFirstCap(CommonClass.GetWords(dt.Rows[a]["Appeallant"].ToString(), 3).ToString())).Replace(" ", "-") + "VS" + clsUtilities.RemoveSpecialCharacter(CommonClass.MakeFirstCap(CommonClass.GetWords(dt.Rows[a]["Respondent"].ToString(), 3).ToString())).Replace(" ", "-") + "." + EncryptDecryptHelper.Encrypt(dt.Rows[a]["ID"].ToString());

                                string OtherCont = "<strong>Where Reported:</strong>";
                                if (!string.IsNullOrEmpty(dt.Rows[a]["CitationRef"].ToString()))
                                    OtherCont = OtherCont + dt.Rows[a]["CitationRef"].ToString() + "<br />";
                                else
                                    OtherCont = OtherCont + dt.Rows[a]["Citation"].ToString() + "<br />";
                                OtherCont = OtherCont + "<strong>Date:</strong>" + dt.Rows[a]["JDate"].ToString() + "<br /><strong>Court:</strong>" + dt.Rows[a]["Court"].ToString() + " <br /> ";
                                OtherCont = OtherCont + "<strong>Appeal:</strong>" + dt.Rows[a]["Appeal"].ToString() + "<br />";
                                if (!string.IsNullOrEmpty(dt.Rows[a]["Keywords"].ToString()))
                                    OtherCont = OtherCont + "<strong>Keywords:</strong><span style='font-size:14px'>" + HighlightText(HighlightTextWithin(dt.Rows[a]["Keywords"].ToString())) + "</span><br /><br />";

                                //if (!string.IsNullOrEmpty(dt.Rows[a]["Result"].ToString()))
                                //    OtherCont = OtherCont + "<strong>Result: </strong>" + dt.Rows[a]["Result"].ToString() + "<br /><br />";
                                //OtherCont = OtherCont + HighlightText(HighlightTextWithin(dt.Rows[a]["ShortDes"].ToString().Replace("</p>", "").Replace("<p>", "").Replace("<br>", "").Replace("<b>", "").Replace("</b>", "")));

                                //dt.Rows[a]["OtherContent"] = OtherCont;

                                if (!string.IsNullOrEmpty(dt.Rows[a]["Result"].ToString()))
                                    OtherCont = OtherCont + "<strong>Result: </strong>" + dt.Rows[a]["Result"].ToString() + "<br /><br />";
                                OtherCont = OtherCont + HighlightText(HighlightTextWithin(dt.Rows[a]["ShortDes"].ToString().Replace("</p>", "").Replace("<p>", "").Replace("<br>", "").Replace("<b>", "").Replace("</b>", "")));

                                if (KeyPTag == 1)
                                    dt.Rows[a]["OtherContent"] = OtherCont + HighlightText(HighlightTextWithin(CommonClass.GetWords(CommonClass.GetShortDesc(dt.Rows[a]["Judgment"].ToString(), keywordP1).Replace("</p>", "").Replace("<p>", "").Replace("<br>", "").Replace("<b>", "").Replace("</b>", ""), 100)));
                                else
                                    dt.Rows[a]["OtherContent"] = OtherCont + HighlightText(HighlightTextWithin(CommonClass.GetWords(CommonClass.GetShortDesc(dt.Rows[a]["Judgment"].ToString(), keyword).Replace("</p>", "").Replace("<p>", "").Replace("<br>", "").Replace("<b>", "").Replace("</b>", ""), 100)));

                            }
                            else if (dt.Rows[a]["ResultType"].ToString() == "Statutes")
                            {
                                dt.Rows[a]["Title"] = CommonClass.MakeFirstCap(dt.Rows[a]["Appeallant"].ToString());
                                dt.Rows[a]["Link"] = "/Statutes/" + clsUtilities.RemoveSpecialCharacter(CommonClass.MakeFirstCap(CommonClass.GetWords(dt.Rows[a]["Appeallant"].ToString(), 3).ToString())).Replace(" ", "-") + "." + EncryptDecryptHelper.Encrypt(dt.Rows[a]["ID"].ToString());
                                string OtherCont = "Index:" + dt.Rows[a]["Citation"].ToString() + "<br/>";
                                OtherCont = OtherCont + dt.Rows[a]["JDate"].ToString();
                                if (!string.IsNullOrEmpty(dt.Rows[a]["Respondent"].ToString()))
                                    OtherCont = OtherCont + " | " + dt.Rows[a]["Respondent"].ToString() + "<br />";

                                dt.Rows[a]["OtherContent"] = OtherCont;
                            }
                            else if (dt.Rows[a]["ResultType"].ToString() == "Dictionary")
                            {
                                dt.Rows[a]["Title"] = CommonClass.MakeFirstCap(dt.Rows[a]["Year"].ToString());
                                //dt.Rows[a]["Link"] = "/Statutes/" + clsUtilities.RemoveSpecialCharacter(EastLawUI.CommonClass.MakeFirstCap(EastLawUI.CommonClass.GetWords(dt.Rows[a]["Appeallant"].ToString(), 3).ToString())).Replace(" ", "-") + "." + EncryptDecryptHelper.Encrypt(dt.Rows[a]["ID"].ToString());
                                string OtherCont = "<br/>";
                                OtherCont = OtherCont + dt.Rows[a]["JDate"].ToString();

                                dt.Rows[a]["OtherContent"] = OtherCont;

                            }

                        }
                        dt.AcceptChanges();
                        dt = ReturnRemoveUserSearchPara(dt);

                        if (dt != null)
                        {



                            if (dtCount != null)
                            {
                                dtCount = ReturnRemoveUserSearchParaCount(dtCount);
                                // Declare an object variable.
                                object sumObject;
                                sumObject = dtCount.Compute("Sum(TotalRows)", "");
                                lblCount.Text = sumObject.ToString();

                                gvLst.VirtualItemCount = int.Parse(sumObject.ToString());


                            }
                            // Declare an object variable.
                            //object sumObject;
                            //sumObject = dt.Compute("Sum(TotalRows)", "");
                            //lblCount.Text = sumObject.ToString();

                            // gvLst.VirtualItemCount = int.Parse(dt.Rows.Count.ToString());
                        }
                        gvLst.DataSource = dt;
                        gvLst.DataBind();
                        Session["Sorting"] = "Yes";
                        #region Court Group
                        var query = from row in dt.AsEnumerable()
                                    where !(row.Field<string>("Court") == null || row.Field<string>("Court") == "")
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
                        divNoResult.Style["Display"] = "none";
                        divResult.Style["Display"] = "";

                        return;
                    }
                    else
                    {
                        divNoResult.Style["Display"] = "";
                        divResult.Style["Display"] = "none";
                    }



                }
                #endregion
                #region Year
                else if (!string.IsNullOrEmpty(years))
                {
                    years = years.Remove(years.Length - 1);
                    string[] YearsLenght = years.Split(',');
                    //DataRow[] filter = dtFilter.Select("Court in ("+courts+")");
                    // dtFilter.DefaultView.RowFilter = "Year in (" + years + ")";
                    InsertAuditLog("Search", "Search Filter", "Keyword: " + keyword + " Filter Year: " + YearsLenght.First().ToString() + " - " + YearsLenght.Last().ToString());
                    DataTable dtCount = new DataTable();
                    int KeyPTag = 0;
                    if (Session["SearchWithIn"] != null)
                    {
                        dt = objPA.GetSearchWithinResultsByPracticeAreaFilter(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString()).ToString()), keyword, keywordSearchWithin, Start, End, int.Parse(YearsLenght.First().ToString()), int.Parse(YearsLenght.Last().ToString()), "");
                    }
                    else
                    {
                        dt = objPA.GetSearchResultsByPracticeAreaFilter(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString()).ToString()), keywordP1, Start, End, int.Parse(YearsLenght.First().ToString()), int.Parse(YearsLenght.Last().ToString()), "");
                        if (dt != null)
                        {
                            if (dt.Rows.Count == 0)
                            {
                                dt = objPA.GetSearchResultsByPracticeAreaFilter(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString()).ToString()), keyword, Start, End, int.Parse(YearsLenght.First().ToString()), int.Parse(YearsLenght.Last().ToString()), "");

                                if (Session["SearchWithIn"] != null)
                                {

                                    dtCount = objPA.GetSearchWithinResultsByPracticeAreaFilterCount(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString()).ToString()), keyword, keywordSearchWithin, int.Parse(YearsLenght.First().ToString()), int.Parse(YearsLenght.Last().ToString()), "");
                                }
                                else
                                {
                                    dtCount = objPA.GetSearchResultsByPracticeAreaFilterCount(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString()).ToString()), keyword, int.Parse(YearsLenght.First().ToString()), int.Parse(YearsLenght.Last().ToString()), "");
                                }
                                KeyPTag = 2;
                            }
                            else
                            {
                                if (Session["SearchWithIn"] != null)
                                {

                                    dtCount = objPA.GetSearchWithinResultsByPracticeAreaFilterCount(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString()).ToString()), keyword, keywordSearchWithin, int.Parse(YearsLenght.First().ToString()), int.Parse(YearsLenght.Last().ToString()), "");
                                }
                                else
                                {
                                    dtCount = objPA.GetSearchResultsByPracticeAreaFilterCount(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString()).ToString()), keywordP1, int.Parse(YearsLenght.First().ToString()), int.Parse(YearsLenght.Last().ToString()), "");
                                }
                                KeyPTag = 1;
                            }
                        }
                    }
                    if (dt.Rows.Count > 0)
                    {
                        dt.Columns.Add("Title");
                        dt.Columns.Add("Link");
                        dt.Columns.Add("OtherContent");
                        for (int a = 0; a < dt.Rows.Count; a++)
                        {
                            if (dt.Rows[a]["ResultType"].ToString() == "Cases")
                            {
                                dt.Rows[a]["Title"] = CommonClass.MakeFirstCap(dt.Rows[a]["Appeallant"].ToString()) + " VS " + CommonClass.MakeFirstCap(dt.Rows[a]["Respondent"].ToString());
                                dt.Rows[a]["Link"] = "/Cases/" + clsUtilities.RemoveSpecialCharacter(CommonClass.MakeFirstCap(CommonClass.GetWords(dt.Rows[a]["Appeallant"].ToString(), 3).ToString())).Replace(" ", "-") + "VS" + clsUtilities.RemoveSpecialCharacter(CommonClass.MakeFirstCap(CommonClass.GetWords(dt.Rows[a]["Respondent"].ToString(), 3).ToString())).Replace(" ", "-") + "." + EncryptDecryptHelper.Encrypt(dt.Rows[a]["ID"].ToString());

                                string OtherCont = "<strong>Where Reported:</strong>";
                                if (!string.IsNullOrEmpty(dt.Rows[a]["CitationRef"].ToString()))
                                    OtherCont = OtherCont + dt.Rows[a]["CitationRef"].ToString() + "<br />";
                                else
                                    OtherCont = OtherCont + dt.Rows[a]["Citation"].ToString() + "<br />";
                                OtherCont = OtherCont + "<strong>Date:</strong>" + dt.Rows[a]["JDate"].ToString() + "<br /><strong>Court:</strong>" + dt.Rows[a]["Court"].ToString() + " <br /> ";
                                OtherCont = OtherCont + "<strong>Appeal:</strong>" + dt.Rows[a]["Appeal"].ToString() + "<br />";
                                if (!string.IsNullOrEmpty(dt.Rows[a]["Keywords"].ToString()))
                                    OtherCont = OtherCont + "<strong>Keywords:</strong><span style='font-size:14px'>" + HighlightText(HighlightTextWithin(dt.Rows[a]["Keywords"].ToString())) + "</span><br /><br />";

                                //if (!string.IsNullOrEmpty(dt.Rows[a]["Result"].ToString()))
                                //    OtherCont = OtherCont + "<strong>Result: </strong>" + dt.Rows[a]["Result"].ToString() + "<br /><br />";
                                //OtherCont = OtherCont + HighlightText(HighlightTextWithin(dt.Rows[a]["ShortDes"].ToString().Replace("</p>", "").Replace("<p>", "").Replace("<br>", "").Replace("<b>", "").Replace("</b>", "")));

                                //dt.Rows[a]["OtherContent"] = OtherCont;

                                if (!string.IsNullOrEmpty(dt.Rows[a]["Result"].ToString()))
                                    OtherCont = OtherCont + "<strong>Result: </strong>" + dt.Rows[a]["Result"].ToString() + "<br /><br />";
                                OtherCont = OtherCont + HighlightText(HighlightTextWithin(dt.Rows[a]["ShortDes"].ToString().Replace("</p>", "").Replace("<p>", "").Replace("<br>", "").Replace("<b>", "").Replace("</b>", "")));

                                if (KeyPTag == 1)
                                    dt.Rows[a]["OtherContent"] = OtherCont + HighlightText(HighlightTextWithin(CommonClass.GetWords(CommonClass.GetShortDesc(dt.Rows[a]["Judgment"].ToString(), keywordP1).Replace("</p>", "").Replace("<p>", "").Replace("<br>", "").Replace("<b>", "").Replace("</b>", ""), 100)));
                                else
                                    dt.Rows[a]["OtherContent"] = OtherCont + HighlightText(HighlightTextWithin(CommonClass.GetWords(CommonClass.GetShortDesc(dt.Rows[a]["Judgment"].ToString(), keyword).Replace("</p>", "").Replace("<p>", "").Replace("<br>", "").Replace("<b>", "").Replace("</b>", ""), 100)));


                            }
                            else if (dt.Rows[a]["ResultType"].ToString() == "Statutes")
                            {
                                dt.Rows[a]["Title"] = CommonClass.MakeFirstCap(dt.Rows[a]["Appeallant"].ToString());
                                dt.Rows[a]["Link"] = "/Statutes/" + clsUtilities.RemoveSpecialCharacter(CommonClass.MakeFirstCap(CommonClass.GetWords(dt.Rows[a]["Appeallant"].ToString(), 3).ToString())).Replace(" ", "-") + "." + EncryptDecryptHelper.Encrypt(dt.Rows[a]["ID"].ToString());
                                string OtherCont = "Index:" + dt.Rows[a]["Citation"].ToString() + "<br/>";
                                OtherCont = OtherCont + dt.Rows[a]["JDate"].ToString();
                                if (!string.IsNullOrEmpty(dt.Rows[a]["Respondent"].ToString()))
                                    OtherCont = OtherCont + " | " + dt.Rows[a]["Respondent"].ToString() + "<br />";

                                dt.Rows[a]["OtherContent"] = OtherCont;
                            }
                            else if (dt.Rows[a]["ResultType"].ToString() == "Dictionary")
                            {
                                dt.Rows[a]["Title"] = CommonClass.MakeFirstCap(dt.Rows[a]["Year"].ToString());
                                //dt.Rows[a]["Link"] = "/Statutes/" + clsUtilities.RemoveSpecialCharacter(EastLawUI.CommonClass.MakeFirstCap(EastLawUI.CommonClass.GetWords(dt.Rows[a]["Appeallant"].ToString(), 3).ToString())).Replace(" ", "-") + "." + EncryptDecryptHelper.Encrypt(dt.Rows[a]["ID"].ToString());
                                string OtherCont = "<br/>";
                                OtherCont = OtherCont + dt.Rows[a]["JDate"].ToString();

                                dt.Rows[a]["OtherContent"] = OtherCont;

                            }

                        }
                        dt.AcceptChanges();
                        dt = ReturnRemoveUserSearchPara(dt);

                        if (dt != null)
                        {




                            if (dtCount != null)
                            {
                                dtCount = ReturnRemoveUserSearchParaCount(dtCount);
                                // Declare an object variable.
                                object sumObject;
                                sumObject = dtCount.Compute("Sum(TotalRows)", "");
                                lblCount.Text = sumObject.ToString();

                                gvLst.VirtualItemCount = int.Parse(sumObject.ToString());


                            }
                            // Declare an object variable.
                            //object sumObject;
                            //sumObject = dt.Compute("Sum(TotalRows)", "");
                            //lblCount.Text = sumObject.ToString();

                            // gvLst.VirtualItemCount = int.Parse(dt.Rows.Count.ToString());
                        }
                        gvLst.DataSource = dt;
                        gvLst.DataBind();
                        Session["Sorting"] = "Yes";
                        #region Court Group
                        var query = from row in dt.AsEnumerable()
                                    where !(row.Field<string>("Court") == null || row.Field<string>("Court") == "")
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
                        divNoResult.Style["Display"] = "none";
                        divResult.Style["Display"] = "";

                        return;
                    }
                    else
                    {
                        divNoResult.Style["Display"] = "";
                        divResult.Style["Display"] = "none";
                    }
                }
                #endregion
                #region Court
                else if (!string.IsNullOrEmpty(courts))
                {
                    // courts = courts.Remove(courts.Length - 1);
                    //DataRow[] filter = dtFilter.Select("Court in ("+courts+")");
                    //   dtFilter.DefaultView.RowFilter = "Court in (" + courts + ")";
                    InsertAuditLog("Search", "Search Filter", "Keyword: " + keyword + " Filter Court: " + courts);
                    //dt = objkeyword.GetSearchResultsByKeywordTestFilter(SearchText, Start, End, 0,0, courts);

                    DataTable dtCount = new DataTable();
                    int KeyPTag = 0;
                    if (Session["SearchWithIn"] != null)
                    {

                        dt = objPA.GetSearchWithinResultsByPracticeAreaFilter_Court(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString()).ToString()), keyword, keywordSearchWithin, Start, End, 0, 0, courts);
                    }
                    else
                    {

                        dt = objPA.GetSearchResultsByPracticeAreaFilterCourt(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString()).ToString()), keywordP1, Start, End, 0, 0, courts);
                        if (dt != null)
                        {
                            if (dt.Rows.Count == 0)
                            {
                                dt = objPA.GetSearchResultsByPracticeAreaFilterCourt(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString()).ToString()), keyword, Start, End, 0, 0, courts);

                                if (Session["SearchWithIn"] != null)
                                {
                                    dtCount = objPA.GetSearchWithinResultsByPracticeAreaFilter_Court_Count(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString()).ToString()), keyword, keywordSearchWithin, 0, 0, courts);
                                }
                                else
                                {
                                    dtCount = objPA.GetSearchResultsByPracticeAreaFilterCourtCount(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString()).ToString()), keyword, 0, 0, courts);
                                }
                                KeyPTag = 2;
                            }
                            else
                            {
                                if (Session["SearchWithIn"] != null)
                                {
                                    dtCount = objPA.GetSearchWithinResultsByPracticeAreaFilter_Court_Count(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString()).ToString()), keyword, keywordSearchWithin, 0, 0, courts);
                                }
                                else
                                {
                                    dtCount = objPA.GetSearchResultsByPracticeAreaFilterCourtCount(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString()).ToString()), keywordP1, 0, 0, courts);
                                }
                                KeyPTag = 1;
                            }
                        }
                    }

                    // dt = objkeyword.GetSearchResultsByKeywordTestFilterCourt(keyword, Start, End, 0, 0, courts);
                    if (dt.Rows.Count > 0)
                    {
                        dt.Columns.Add("Title");
                        dt.Columns.Add("Link");
                        dt.Columns.Add("OtherContent");
                        for (int a = 0; a < dt.Rows.Count; a++)
                        {
                            if (dt.Rows[a]["ResultType"].ToString() == "Cases")
                            {
                                dt.Rows[a]["Title"] = CommonClass.MakeFirstCap(dt.Rows[a]["Appeallant"].ToString()) + " VS " + CommonClass.MakeFirstCap(dt.Rows[a]["Respondent"].ToString());
                                dt.Rows[a]["Link"] = "/Cases/" + clsUtilities.RemoveSpecialCharacter(CommonClass.MakeFirstCap(CommonClass.GetWords(dt.Rows[a]["Appeallant"].ToString(), 3).ToString())).Replace(" ", "-") + "VS" + clsUtilities.RemoveSpecialCharacter(CommonClass.MakeFirstCap(CommonClass.GetWords(dt.Rows[a]["Respondent"].ToString(), 3).ToString())).Replace(" ", "-") + "." + EncryptDecryptHelper.Encrypt(dt.Rows[a]["ID"].ToString());

                                string OtherCont = "<strong>Where Reported:</strong>";
                                if (!string.IsNullOrEmpty(dt.Rows[a]["CitationRef"].ToString()))
                                    OtherCont = OtherCont + dt.Rows[a]["CitationRef"].ToString() + "<br />";
                                else
                                    OtherCont = OtherCont + dt.Rows[a]["Citation"].ToString() + "<br />";
                                OtherCont = OtherCont + "<strong>Date:</strong>" + dt.Rows[a]["JDate"].ToString() + "<br /><strong>Court:</strong>" + dt.Rows[a]["Court"].ToString() + " <br /> ";
                                OtherCont = OtherCont + "<strong>Appeal:</strong>" + dt.Rows[a]["Appeal"].ToString() + "<br />";
                                if (!string.IsNullOrEmpty(dt.Rows[a]["Keywords"].ToString()))
                                    OtherCont = OtherCont + "<strong>Keywords:</strong><span style='font-size:14px'>" + HighlightText(HighlightTextWithin(dt.Rows[a]["Keywords"].ToString())) + "</span><br /><br />";

                                //if (!string.IsNullOrEmpty(dt.Rows[a]["Result"].ToString()))
                                //    OtherCont = OtherCont + "<strong>Result: </strong>" + dt.Rows[a]["Result"].ToString() + "<br /><br />";
                                //OtherCont = OtherCont + HighlightText(HighlightTextWithin(dt.Rows[a]["ShortDes"].ToString().Replace("</p>", "").Replace("<p>", "").Replace("<br>", "").Replace("<b>", "").Replace("</b>", "")));

                                //dt.Rows[a]["OtherContent"] = OtherCont;

                                if (!string.IsNullOrEmpty(dt.Rows[a]["Result"].ToString()))
                                    OtherCont = OtherCont + "<strong>Result: </strong>" + dt.Rows[a]["Result"].ToString() + "<br /><br />";
                                OtherCont = OtherCont + HighlightText(HighlightTextWithin(dt.Rows[a]["ShortDes"].ToString().Replace("</p>", "").Replace("<p>", "").Replace("<br>", "").Replace("<b>", "").Replace("</b>", "")));

                                if (KeyPTag == 1)
                                    dt.Rows[a]["OtherContent"] = OtherCont + HighlightText(HighlightTextWithin(CommonClass.GetWords(CommonClass.GetShortDesc(dt.Rows[a]["Judgment"].ToString(), keywordP1).Replace("</p>", "").Replace("<p>", "").Replace("<br>", "").Replace("<b>", "").Replace("</b>", ""), 200)));
                                else
                                    dt.Rows[a]["OtherContent"] = OtherCont + HighlightText(HighlightTextWithin(CommonClass.GetWords(CommonClass.GetShortDesc(dt.Rows[a]["Judgment"].ToString(), keyword).Replace("</p>", "").Replace("<p>", "").Replace("<br>", "").Replace("<b>", "").Replace("</b>", ""), 200)));


                            }
                            else if (dt.Rows[a]["ResultType"].ToString() == "Statutes")
                            {
                                dt.Rows[a]["Title"] = CommonClass.MakeFirstCap(dt.Rows[a]["Appeallant"].ToString());
                                dt.Rows[a]["Link"] = "/Statutes/" + clsUtilities.RemoveSpecialCharacter(CommonClass.MakeFirstCap(CommonClass.GetWords(dt.Rows[a]["Appeallant"].ToString(), 3).ToString())).Replace(" ", "-") + "." + EncryptDecryptHelper.Encrypt(dt.Rows[a]["ID"].ToString());
                                string OtherCont = "Index:" + dt.Rows[a]["Citation"].ToString() + "<br/>";
                                OtherCont = OtherCont + dt.Rows[a]["JDate"].ToString();
                                if (!string.IsNullOrEmpty(dt.Rows[a]["Respondent"].ToString()))
                                    OtherCont = OtherCont + " | " + dt.Rows[a]["Respondent"].ToString() + "<br />";

                                dt.Rows[a]["OtherContent"] = OtherCont;
                            }
                            else if (dt.Rows[a]["ResultType"].ToString() == "Dictionary")
                            {
                                dt.Rows[a]["Title"] = CommonClass.MakeFirstCap(dt.Rows[a]["Year"].ToString());
                                //dt.Rows[a]["Link"] = "/Statutes/" + clsUtilities.RemoveSpecialCharacter(EastLawUI.CommonClass.MakeFirstCap(EastLawUI.CommonClass.GetWords(dt.Rows[a]["Appeallant"].ToString(), 3).ToString())).Replace(" ", "-") + "." + EncryptDecryptHelper.Encrypt(dt.Rows[a]["ID"].ToString());
                                string OtherCont = "<br/>";
                                OtherCont = OtherCont + dt.Rows[a]["JDate"].ToString();

                                dt.Rows[a]["OtherContent"] = OtherCont;

                            }

                        }
                        dt.AcceptChanges();
                        dt = ReturnRemoveUserSearchPara(dt);

                        if (dt != null)
                        {
                            //DataTable dtCount = objkeyword.GetSearchResultsByKeywordTestFilterCourtCount(keyword, 0, 0, courts);


                            if (dtCount != null)
                            {
                                dtCount = ReturnRemoveUserSearchParaCount(dtCount);
                                // Declare an object variable.
                                object sumObject;
                                sumObject = dtCount.Compute("Sum(TotalRows)", "");
                                lblCount.Text = sumObject.ToString();

                                gvLst.VirtualItemCount = int.Parse(sumObject.ToString());


                            }
                            // Declare an object variable.
                            //object sumObject;
                            //sumObject = dt.Compute("Sum(TotalRows)", "");
                            //lblCount.Text = sumObject.ToString();

                            // gvLst.VirtualItemCount = int.Parse(dt.Rows.Count.ToString());
                        }
                        gvLst.DataSource = dt;
                        gvLst.DataBind();
                        Session["Sorting"] = "Yes";

                        #region Year Group
                        var queryYear = from row in dt.AsEnumerable()
                                        where row.Field<int>("Year") > 200
                                        group row by row.Field<int>("Year") into YEARS
                                        orderby YEARS.Key

                                        select new
                                        {
                                            Name = YEARS.Key + " (" + YEARS.Count() + ")",
                                            count = YEARS.Count(),
                                            valfiel = YEARS.Key
                                        };

                        //chkLstYear.DataSource = queryYear;
                        //chkLstYear.DataValueField = "valfiel";
                        //chkLstYear.DataTextField = "Name";
                        //chkLstYear.DataBind();

                        chkLstYear.DataSource = CreateYearRange(dt);
                        chkLstYear.DataValueField = "ValData";
                        chkLstYear.DataTextField = "Title";
                        chkLstYear.DataBind();
                        #endregion
                        divNoResult.Style["Display"] = "none";
                        divResult.Style["Display"] = "";
                        return;
                    }
                    else
                    {
                        divNoResult.Style["Display"] = "";
                        divResult.Style["Display"] = "none";
                    }
                }
                #endregion
                else
                {

                    BindSearchResult(HttpContext.Current.Items["PAkeywordtxt"].ToString(), 0, 20);
                }
            }
            catch { }
        }
        public string HighlightText(string InputTxt)
        {
            string Search_Str = HttpContext.Current.Items["PAkeywordtxt"].ToString();
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

    }
}