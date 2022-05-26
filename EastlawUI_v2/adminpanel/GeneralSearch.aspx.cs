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

namespace EastlawUI_v2.adminpanel
{
    public partial class GeneralSearch : System.Web.UI.Page
    {
        EastLawBL.Keywords objkeyword = new EastLawBL.Keywords();
        EastLawBL.Users objusr = new EastLawBL.Users();
        EastLawBL.Search objsrc = new EastLawBL.Search();
        EastLawBL.Statutes objstate = new EastLawBL.Statutes();
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
            }
        }
        void BindSearchResult(string SearchText, int startRowIndex, int maximumRows)
        {
            try
            {

                string keyword = CommonClass.FormatSearchWord(SearchText);
               int RecordCount= GetTotalRecords(keyword);
                TextBox txtSearch = (TextBox)Master.FindControl("txtSearch");
                //txtSearch.Text = SearchText;


                DataTable dt = new DataTable();
                
                dt = objkeyword.GetSearchResultsByKeywordTest(keyword, startRowIndex, maximumRows);
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
                                dt.Rows[a]["Link"] = "/cases/" + clsUtilities.RemoveSpecialCharacter(CommonClass.MakeFirstCap(CommonClass.GetWords(dt.Rows[a]["Appeallant"].ToString(), 3).ToString())).Replace(" ", "-") + "VS" + clsUtilities.RemoveSpecialCharacter(CommonClass.MakeFirstCap(CommonClass.GetWords(dt.Rows[a]["Respondent"].ToString(), 3).ToString())).Replace(" ", "-") + "." + EncryptDecryptHelper.Encrypt(dt.Rows[a]["ID"].ToString());

                                string OtherCont = "<strong>Where Reported:</strong>";
                                if (!string.IsNullOrEmpty(dt.Rows[a]["CitationRef"].ToString()))
                                    OtherCont = OtherCont + dt.Rows[a]["CitationRef"].ToString() + "<br />";
                                else
                                    OtherCont = OtherCont + dt.Rows[a]["Citation"].ToString() + "<br />";
                                OtherCont = OtherCont + "<strong>Date:</strong>" + dt.Rows[a]["JDate"].ToString() + "<br /><strong>Court:</strong>" + dt.Rows[a]["Court"].ToString() + " <br /> ";
                                OtherCont = OtherCont + "<strong>Appeal:</strong>" + dt.Rows[a]["Appeal"].ToString() + "<br />";
                                //if (!string.IsNullOrEmpty(dt.Rows[a]["Keywords"].ToString()))
                                //    OtherCont = OtherCont + "<strong>Keywords:</strong><span style='font-size:14px'>" + HighlightText(HighlightTextWithin(dt.Rows[a]["Keywords"].ToString())) + "</span><br /><br />";

                                //if (!string.IsNullOrEmpty(dt.Rows[a]["Result"].ToString()))
                                //    OtherCont = OtherCont + "<strong>Result: </strong>" + dt.Rows[a]["Result"].ToString() + "<br /><br />";
                                //OtherCont = OtherCont + HighlightText(HighlightTextWithin(dt.Rows[a]["ShortDes"].ToString().Replace("</p>", "").Replace("<p>", "").Replace("<br>", "").Replace("<b>", "").Replace("</b>", "")));

                                //dt.Rows[a]["OtherContent"] = OtherCont;

                                if (!string.IsNullOrEmpty(dt.Rows[a]["Result"].ToString()))
                                    OtherCont = OtherCont + "<strong>Result: </strong>" + dt.Rows[a]["Result"].ToString() + "<br /><br />";
                                OtherCont = OtherCont + HighlightText(HighlightTextWithin(dt.Rows[a]["ShortDes"].ToString().Replace("</p>", "").Replace("<p>", "").Replace("<br>", "").Replace("<b>", "").Replace("</b>", "")));

                                dt.Rows[a]["OtherContent"] = OtherCont + HighlightText(HighlightTextWithin(CommonClass.GetShortDesc(dt.Rows[a]["Judgment"].ToString(), txtFreeText.Text.Trim().Replace("</p>", "").Replace("<p>", "").Replace("<br>", "").Replace("<b>", "").Replace("</b>", "").Replace("##TS##", " ").Replace("##TE##", ""))));

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
                        Session["sesKeywordResults"] = null;
                        if (Session["UserID"] != null)
                            chk = objsrc.InsertSearchText(SearchText, 1, GetClientIP(), int.Parse(Session["UserID"].ToString()), RecordCount,"Main Search Backend");
                        else
                            chk = objsrc.InsertSearchText(SearchText, 1, GetClientIP(), 0, RecordCount, "Main Search Backend");
                        //lblSearchWords.Text = SearchText;


                        gvLst.DataSource = dt;
                        gvLst.DataBind();

                        DataTable dtGroup = objkeyword.GetSearchResultsByKeywordGrouping(keyword);
                        dtGroup = ReturnRemoveUserSearchPara(dtGroup);

                        divNoResult.Style["Display"] = "none";
                        divResult.Style["Display"] = "";
                    }
                    else
                    {
                        divNoResult.Style["Display"] = "";
                        divResult.Style["Display"] = "none";
                        if (Session["UserID"] != null)
                            chk = objsrc.InsertSearchText(SearchText, 0, GetClientIP(), int.Parse(Session["UserID"].ToString()), RecordCount, "Main Search Backend");
                        else
                            chk = objsrc.InsertSearchText(SearchText, 0, GetClientIP(), 0, RecordCount, "Main Search Backend");
                    }
                }
                else
                {
                    divNoResult.Style["Display"] = "";
                    divResult.Style["Display"] = "none";
                    if (Session["UserID"] != null)
                        chk = objsrc.InsertSearchText(SearchText, 0, GetClientIP(), int.Parse(Session["UserID"].ToString()), RecordCount, "Main Search Backend");
                    else
                        chk = objsrc.InsertSearchText(SearchText, 0, GetClientIP(), 0, RecordCount, "Main Search Backend");
                }

            }
            catch (Exception ex)
            {

            }
        }
        int GetTotalRecords(string SearchText)
        {
            DataTable dt = objkeyword.GetSearchResultsByKeywordCountTest(SearchText);

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
        public string HighlightText(string InputTxt)
        {
            string Search_Str = txtFreeText.Text.Trim();
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
        protected void gvLst_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                if (Session["Sorting"] != null)
                {
                    if (Session["Sorting"].ToString() == "Yes")
                    {
                        gvLst.PageIndex = e.NewPageIndex;
                      //  FilterResults(txtFreeText.Text.Trim(), e.NewPageIndex, 20);

                    }
                }
                //else if (Session["SearchWithIn"] != null)
                //{

                //    gvLst.PageIndex = e.NewPageIndex;
                //    SearchWithinResult(txtSearchWithinResult.Text, e.NewPageIndex, 20);

                //}
                else
                {
                    gvLst.PageIndex = e.NewPageIndex;
                    BindSearchResult(txtFreeText.Text.Trim(), e.NewPageIndex, 20);
                }
            }
            catch (Exception ex)
            {

            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindSearchResult(txtFreeText.Text.Trim(), 0, 20);
        }
        protected void gvLst_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                //if (e.CommandName == "View")
                //{
                //    int index = Convert.ToInt32(e.CommandArgument);
                //    GridViewRow row = gv.Rows[index];
                //    //HiddenField hdnField = (HiddenField)row.FindControl("hdID");
                //    string val = (string)this.gv.DataKeys[index]["Id"].ToString();
                //    //string url = "StatutesIndex.aspx?dis=" + hdnField.Value;
                //    string url = "ViewCaseDetails.aspx?dis=" + val.ToString();
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( '" + url + "','_blank','height=700px,width=700px,scrollbars=1');", true);
                //}
                if (e.CommandName == "FinalReview")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow row = gvLst.Rows[index];
                    //HiddenField hdnField = (HiddenField)row.FindControl("hdID");
                    string val = (string)this.gvLst.DataKeys[index]["Id"].ToString();
                    //string url = "StatutesIndex.aspx?dis=" + hdnField.Value;
                    string url = "CitationDetailedReview.aspx?param=" +  EncryptDecryptHelper.Encrypt(val.ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( '" + url + "','_blank','height=600px,width=900px,scrollbars=1');", true);

                    //int index = Convert.ToInt32(e.CommandArgument);
                    //GridViewRow row = gv.Rows[index];
                    ////HiddenField hdnField = (HiddenField)row.FindControl("hdID");
                    //string val = (string)this.gv.DataKeys[index]["Id"].ToString();
                    //Response.Redirect("CitationDetailedReview.aspx?param=" + EncryptDecryptHelper.Encrypt(val.ToString()));
                }

            }
            catch (Exception ex)
            {

            }
        }
    }
}