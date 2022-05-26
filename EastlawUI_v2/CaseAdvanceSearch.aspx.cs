using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace EastlawUI_v2
{
    public partial class CaseAdvanceSearch : System.Web.UI.Page
    {
        EastLawBL.Cases objcase = new EastLawBL.Cases();
        EastLawBL.Statutes objstate = new EastLawBL.Statutes();
        EastLawBL.Users objuser = new EastLawBL.Users();
        EastLawBL.Keywords objkey = new EastLawBL.Keywords();
        string strMessage = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            BindKeywords();
            BindCourt();
            if (!Page.IsPostBack)
            {
                InsertAuditLog("Search", "Case Advance Search", "");
                Validate();

            }

        }
        void Validate()
        {
            if (Session["MemberID"] != null)
            {
                //divWithLogin.Style["Display"] = "";
                //divWithoutlogin.Style["Display"] = "none";
                GetCourts();

            }
            else
            {
                //divWithLogin.Style["Display"] = "none";
                //divWithoutlogin.Style["Display"] = "";
            }

            //divWithLogin.Style["Display"] = "";
            //divWithoutlogin.Style["Display"] = "none";
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
        void GetCourts()
        {
            try
            {
                DataTable dt = new DataTable();
                //dt = objcase.GetCasesCourtsGroup();
                //chkLstCourt.DataValueField = "court";
                //chkLstCourt.DataTextField = "court";
                //chkLstCourt.DataSource = dt;
                //chkLstCourt.DataBind();
                // chkLstCourt.Items.Insert(0, new ListItem("All", "0"));
            }
            catch { }
        }
        void BindKeywords()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objkey.GetActiveKeywords();
                radtxtKeywords.DataTextField = "Keywords";
                radtxtKeywords.DataSource = dt;
                radtxtKeywords.DataBind();
            }
            catch { }
        }

        void BindCourt()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objcase.GetCasesCourtsGroup();
                //radAutoCompleteCourt.DataTextField = "court";
                //radAutoCompleteCourt.DataSource = dt;
                //radAutoCompleteCourt.DataBind();

                ddlPartyCourt.DataTextField = "court";
                ddlPartyCourt.DataSource = dt;
                ddlPartyCourt.DataBind();
                ddlPartyCourt.Items.Insert(0, new ListItem("Select Court", "0"));
            }
            catch { }
        }
        //void CitationsSearch()
        //{
        //    try
        //    {
        //        string cri = "Where A.Citation is not null";

        //        if (!string.IsNullOrEmpty(txtFreeText.Text.Trim()))
        //            cri = cri + " AND  CONTAINS(A.Keywords,'"+txtFreeText.Text+"') or CONTAINS(A.Headnotes,'"+txtFreeText.Text+"')";

        //        //if (ddlJournals.SelectedValue != "0")
        //        //    cri = cri + " AND A.JournalID='" + ddlJournals.SelectedValue + "'";

        //        ////if (!string.IsNullOrEmpty(txtCitationNumber.Text.Trim()))
        //        ////    cri = cri + " AND A.Citation like '%"+txtCitationNumber.Text.Trim()+"%'";

        //        //if (!string.IsNullOrEmpty(txtCitationNumber.Text.Trim()))
        //        //    cri = cri + " AND  CONTAINS (A.Citation, '" + txtCitationNumber.Text.Trim() + "' )";

        //        DataTable dt = new DataTable();
        //        dt = objcase.GetCasesAdvanceSearch(cri);
        //        if (dt != null)
        //        {
        //            if (dt.Rows.Count > 0)
        //            {
        //                Session["CasesSearch"] = dt;
        //                Response.Redirect("/Cases/Search-Result");
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}



        protected void btnSearchFreetext_Click(object sender, EventArgs e)
        {
            try
            {
                //if (string.IsNullOrEmpty(txtFreeText.Text))
                //{
                //    lblMsgFreeText.Visible = true;
                //}
                //else
                //{
                //    lblMsgFreeText.Visible = false;
                string[] strSearch = new string[5];
                strSearch[0] = "Cases";
                Session["MemberSearchPara"] = strSearch;
                Response.Redirect("/search/" + txtFreeText.Text.Trim());
                // }

            }
            catch (Exception ex)
            { }

        }

        protected void btnKeywordSearch_Click(object sender, EventArgs e)
        {
            string cri = "Where Citation is not null";

            if (!string.IsNullOrEmpty(radtxtKeywords.Text.Trim()))
                cri = cri + " AND  CONTAINS(Keywords,'" + radtxtKeywords.Text + "')";

            DataTable dt = new DataTable();
            dt = objcase.GetCasesSearch(cri,0,30);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    Session["CasesSearch"] = dt;
                    Response.Redirect("/cases/search-result");
                }
            }
        }

        protected void btnPartyNamesSearch_Click(object sender, EventArgs e)
        {
            string cri = "Where Citation is not null";

            if (!string.IsNullOrEmpty(txtPartyNames.Text.Trim()))
                cri = cri + " AND  Appeallant='" + txtPartyNames.Text + "'";

            DataTable dt = new DataTable();
            dt = objcase.GetCasesSearch(cri,0,30);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    Session["CasesSearch"] = dt;
                    Response.Redirect("/cases/search-result");
                }
            }
        }

        protected void btnCitationSearch_Click(object sender, EventArgs e)
        {

            //string cri = "Where A.Citation is not null";

            //if (!string.IsNullOrEmpty(txtCitation.Text.Trim()))
            //    cri = cri + " AND  CONTAINS (A.Citation, '" + txtCitation.Text.Trim() + "' )";

            //DataTable dt = new DataTable();
            //dt = objcase.GetCasesSearch(cri);
            //if (dt != null)
            //{
            //    if (dt.Rows.Count > 0)
            //    {
            //        Session["CasesSearch"] = dt;
            //        Response.Redirect("/Cases/Search-Result");
            //    }
            //}
        }

        protected void btnCourt_Click(object sender, EventArgs e)
        {

            //string cri = "Where A.Citation is not null";

            //if (!string.IsNullOrEmpty(txtCourt.Text.Trim()))
            //    cri = cri + " AND  CONTAINS (A.Court, '\"" + txtCourt.Text.Trim() + "\"' )";

            //DataTable dt = new DataTable();
            //dt = objcase.GetCasesSearch(cri);
            //if (dt != null)
            //{
            //    if (dt.Rows.Count > 0)
            //    {
            //        Session["CasesSearch"] = dt;
            //        Response.Redirect("/cases/search-Result");
            //    }
            //}
        }

        protected void txtAppealRespSearch_Click(object sender, EventArgs e)
        {
            //string cri = "Where A.Citation is not null";

            //if (!string.IsNullOrEmpty(txtAppeallant.Text.Trim()))
            //    cri = cri + " AND  A.Appeallant='" + txtAppeallant.Text + "'";

            //if (!string.IsNullOrEmpty(txtRespondent.Text.Trim()))
            //    cri = cri + " AND  A.Respondent='" + txtRespondent.Text + "'";

            //DataTable dt = new DataTable();
            //dt = objcase.GetCasesSearch(cri);
            //if (dt != null)
            //{
            //    if (dt.Rows.Count > 0)
            //    {
            //        Session["CasesSearch"] = dt;
            //        Response.Redirect("/Cases/Search-Result");
            //    }
            //}
        }
        void StatutesSearch()
        {
            try
            {
                //string cri = "Where A.IsDeleted=0";

                //if (!string.IsNullOrEmpty(txtLegTitle.Text.Trim()))
                //    cri = cri + " AND CONTAINS(A.Title,'\"" + txtLegTitle.Text + "\"')";

                //if (ddlLegProv.SelectedValue != "Any")
                //    cri = cri + " AND A.Prei_Sec='SECONDARY' AND contains( A.Title,'" + ddlLegProv.SelectedValue + "') AND CONTAINS(A.Title,'\"" + txtLegProNo.Text + "\"') ";


                ////if (!string.IsNullOrEmpty(txtSRONo.Text.Trim()))
                ////    cri = cri + " AND contains( A.Title,'" + txtSRONo.Text.Trim() + "')";

                ////if (!string.IsNullOrEmpty(txtCircularNo.Text.Trim()))
                ////    cri = cri + " AND contains( A.Title,'" + txtCircularNo.Text.Trim() + "')";


                ////if (ddlGroup.SelectedValue != "0")
                ////    cri = cri + " AND A.GroupID=" + ddlGroup.SelectedValue + "";

                ////if (ddlSubGroup.SelectedValue != "0")
                ////    cri = cri + " AND A.SubGroupID=" + ddlSubGroup.SelectedValue + "";

                ////if (ddlStatutesCat.SelectedValue != "0")
                ////    cri = cri + " AND A.CatID='" + ddlStatutesCat.SelectedValue + "'";


                //DataTable dt = new DataTable();
                //dt = objstate.GetStatutesSearch(cri);
                //if (dt != null)
                //{
                //    if (dt.Rows.Count > 0)
                //    {
                //        Session["StatutesSearch"] = dt;
                //        Response.Redirect("/statutes/search-Result");
                //    }
                //}

            }
            catch (Exception ex)
            {

            }
        }

        protected void btnLegSearch_Click(object sender, EventArgs e)
        {
            StatutesSearch();
        }
        void CitationsSearch()
        {
            try
            {
                string cri = "Where A.Citation is not null and A.Publish=1";
                string forlog = "";

                
                //for (int a = 0; a < chkLstCourt.Items.Count; a++)
                //{
                //    if (chkLstCourt.Items[a].Selected == true)
                //    {
                //        courts = courts + "'" + chkLstCourt.Items[a].Value + "'" + ",";
                //        //courts = courts + "|" + chkLstCourt.Items[a].Value + "|";
                //    }
                //}

                string courts = "";
                string[] CourtNames = radAutoCompleteCourt.Text.Trim().Split(';');
                 if (CourtNames.Length > 0)
                 {
                     for (int a = 0; a < CourtNames.Length; a++)
                     {
                         if (!string.IsNullOrEmpty(CourtNames[a].ToString().Trim()))
                         {
                             courts = courts + "'" + CourtNames[a].ToString().Trim() + "'" + ",";
                         }
 
                     }
                 }

                //if (!string.IsNullOrEmpty(courts)  && string.IsNullOrEmpty(radtxtKeywords.Text.Trim()))
                //{
                //    lblCaseAdvanceMsg.Text = "Please enter keyword with court.";
                //    lblCaseAdvanceMsg.ForeColor = System.Drawing.Color.Red;
                //    lblCaseAdvanceMsg.Visible = true;

                //}
                //else
                //{
                    lblCaseAdvanceMsg.Visible = false;
                    if (!string.IsNullOrEmpty(txtFreeText.Text.Trim()))
                    {
                        forlog = forlog + " Freetext: " + txtFreeText.Text;
                        cri = cri + " AND  (CONTAINS(Judgment,'\"" + txtFreeText.Text + "\"') or CONTAINS(A.Headnotes,'\"" + txtFreeText.Text + "\"'))";
                    }


                    if (!string.IsNullOrEmpty(radtxtKeywords.Text.Trim()))
                    {
                        string keywordtxt = "";
                        //string[] Keywords = txtKeyword.Text.Trim().Split(',');
                        string[] Keywords = radtxtKeywords.Text.Trim().Split(';');
                        for (int a = 0; a < Keywords.Length - 1; a++)
                        {

                            if (!string.IsNullOrEmpty(Keywords[a].ToString().Trim()))
                                keywordtxt = keywordtxt + " \"" + Keywords[a].ToString().Trim() + "\" and";

                        }
                        if (Keywords.Length < 1)
                        {
                            keywordtxt = "\"" + radtxtKeywords.Text.Trim() + "\"";
                        }
                        else
                        {
                            keywordtxt = keywordtxt.Remove(keywordtxt.Length - 3);
                        }
                        forlog = forlog + " Keyword: " + keywordtxt;
                        cri = cri + " AND  CONTAINS(Judgment,'" + keywordtxt + "')";
                        //cri = cri + " AND  CONTAINS(A.Keywords,'\"" + txtKeyword.Text + "\"')";
                    }

                    //if (!string.IsNullOrEmpty(txtAppeallant.Text.Trim()))
                    //{
                    //    forlog = forlog + " Appeallant: " + txtAppeallant.Text;
                    //    cri = cri + " AND  CONTAINS(A.Appeallant,'\"" + txtAppeallant.Text + "\"')";
                    //}

                    //if (!string.IsNullOrEmpty(txtRespondent.Text.Trim()))
                    //{
                    //    forlog = forlog + " Respondent: " + txtRespondent.Text;
                    //    cri = cri + " AND  CONTAINS(A.Respondent,'\"" + txtRespondent.Text + "\"')";
                    //}

                    //if (!string.IsNullOrEmpty(txtCitation.Text.Trim()))
                    //{
                    //    forlog = forlog + " Citation: " + txtCitation.Text;
                    //    cri = cri + " AND ( CONTAINS (A.Citation, '\"" + txtCitation.Text + "\"' ) or CONTAINS (A.CitationRef, '\"" + txtCitation.Text + "\"' ))";
                    //}
                    //if (!string.IsNullOrEmpty(txtCitedCase.Text.Trim()))
                    //{
                    //    forlog = forlog + " CitedCase: " + txtCitedCase.Text;
                    //    cri = cri + " AND  CONTAINS (A.Judgment, '\"" + txtCitedCase.Text + "\"' )";
                    //}



                    //if (!string.IsNullOrEmpty(courts))
                    //    cri = cri + " AND  "+courts+" LIKE '%|' + A.Court + '|%'";
                    if (!string.IsNullOrEmpty(courts))
                    {
                        courts = courts.Remove(courts.Length - 1);
                        forlog = forlog + " Courts: " + courts;
                        cri = cri + " AND Court in (" + courts + ")";
                    }
                    DataTable dt = new DataTable();
                    InsertAuditLog("Search", "Case Advance Search", forlog);
                    dt = objcase.GetCasesSearch(cri,0,30);
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            dt.Columns.Add("Link");
                            dt.Columns.Add("Title");
                            for (int a = 0; a < dt.Rows.Count; a++)
                            {

                                dt.Rows[a]["Title"] = EastlawUI_v2.CommonClass.MakeFirstCap(dt.Rows[a]["Appeallant"].ToString()) + " VS " + EastlawUI_v2.CommonClass.MakeFirstCap(dt.Rows[a]["Respondent"].ToString());
                                dt.Rows[a]["Link"] = "/cases/" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dt.Rows[a]["Appeallant"].ToString(), 3).ToString())).Replace(" ", "-") + "VS" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dt.Rows[a]["Respondent"].ToString(), 3).ToString())).Replace(" ", "-") + "." + EncryptDecryptHelper.Encrypt(dt.Rows[a]["ID"].ToString());
                                //dt.Rows[a]["ShortDes"] = EastLawUI.CommonClass.GetWords(EastLawUI.CommonClass.GetShortDesc(dt.Rows[a]["Judgment"].ToString(), txtKeyword.Text).Replace("</p>", "").Replace("<p>", "").Replace("<br>", "").Replace("<b>", "").Replace("</b>", ""), 200);

                            }
                            dt.AcceptChanges();

                            Session["CasesSearch"] = dt;
                            string[] strSearch = new string[5];
                            strSearch[1] = "Cases";
                            Response.Redirect("/cases/search-result");
                        }
                        else
                        {
                            strMessage = "Recrods not found.";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup('" + strMessage + "');", true);

                            lblCaseAdvanceMsg.Text = "Recrods not found";
                            lblCaseAdvanceMsg.ForeColor = System.Drawing.Color.Red;
                            lblCaseAdvanceMsg.Visible = true;
                        }
                    }
                    else
                    {

                        strMessage = "Recrods not found.";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup('" + strMessage + "');", true);

                        lblCaseAdvanceMsg.Text = "Recrods not found";
                        lblCaseAdvanceMsg.ForeColor = System.Drawing.Color.Red;
                        lblCaseAdvanceMsg.Visible = true;
                    }
              //  }

            }
            catch (Exception ex)
            {

            }
        }
        void CitationsSearchPartyName()
        {
            try
            {
                string cri = "Where Citation is not null and Publish=1";
                string forlog = "";


                //for (int a = 0; a < chkLstCourt.Items.Count; a++)
                //{
                //    if (chkLstCourt.Items[a].Selected == true)
                //    {
                //        courts = courts + "'" + chkLstCourt.Items[a].Value + "'" + ",";
                //        //courts = courts + "|" + chkLstCourt.Items[a].Value + "|";
                //    }
                //}

                string courts = "";
                //string[] CourtNames = radCourtParty.Text.Trim().Split(';');
                //if (CourtNames.Length > 0)
                //{
                //    for (int a = 0; a < CourtNames.Length; a++)
                //    {
                //        if (!string.IsNullOrEmpty(CourtNames[a].ToString().Trim()))
                //        {
                //            courts = courts + "'" + CourtNames[a].ToString().Trim() + "'" + ",";
                //        }

                //    }
                //}

                courts = courts + "'" + ddlPartyCourt.SelectedValue + "'" + ",";

                //if (!string.IsNullOrEmpty(courts)  && string.IsNullOrEmpty(radtxtKeywords.Text.Trim()))
                //{
                //    lblCaseAdvanceMsg.Text = "Please enter keyword with court.";
                //    lblCaseAdvanceMsg.ForeColor = System.Drawing.Color.Red;
                //    lblCaseAdvanceMsg.Visible = true;

                //}
                //else
                //{
                lblCaseAdvanceMsg.Visible = false;
                if (!string.IsNullOrEmpty(txtPartyNames.Text.Trim()))
                {
                    forlog = forlog + " Freetext: " + txtFreeText.Text;
                    cri = cri + " AND Appeallant like '%" + txtPartyNames.Text.Trim() + "%' or Respondent like '%" + txtPartyNames.Text.Trim() + "%' ";
                }


              

                



                //if (!string.IsNullOrEmpty(courts))
                //    cri = cri + " AND  "+courts+" LIKE '%|' + A.Court + '|%'";
                if (!string.IsNullOrEmpty(courts))
                {
                    courts = courts.Remove(courts.Length - 1);
                    forlog = forlog + " Courts: " + courts;
                    cri = cri + " AND Court in (" + courts + ")";
                }
                DataTable dt = new DataTable();
                InsertAuditLog("Search", "Case Advance Search", forlog);
                dt = objcase.GetCasesSearch(cri, 0, 30);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        dt.Columns.Add("Link");
                        dt.Columns.Add("Title");
                        for (int a = 0; a < dt.Rows.Count; a++)
                        {

                            dt.Rows[a]["Title"] = EastlawUI_v2.CommonClass.MakeFirstCap(dt.Rows[a]["Appeallant"].ToString()) + " VS " + EastlawUI_v2.CommonClass.MakeFirstCap(dt.Rows[a]["Respondent"].ToString());
                            dt.Rows[a]["Link"] = "/cases/" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dt.Rows[a]["Appeallant"].ToString(), 3).ToString())).Replace(" ", "-") + "VS" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dt.Rows[a]["Respondent"].ToString(), 3).ToString())).Replace(" ", "-") + "." + EncryptDecryptHelper.Encrypt(dt.Rows[a]["ID"].ToString());
                            //dt.Rows[a]["ShortDes"] = EastLawUI.CommonClass.GetWords(EastLawUI.CommonClass.GetShortDesc(dt.Rows[a]["Judgment"].ToString(), txtKeyword.Text).Replace("</p>", "").Replace("<p>", "").Replace("<br>", "").Replace("<b>", "").Replace("</b>", ""), 200);

                        }
                        dt.AcceptChanges();

                        Session["CasesSearch"] = dt;
                        string[] strSearch = new string[5];
                        strSearch[1] = "Cases";
                        Response.Redirect("/cases/search-result");
                    }
                    else
                    {
                        strMessage = "Recrods not found.";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup('" + strMessage + "');", true);

                        lblPartySearch.Text = "Recrods not found";
                        lblPartySearch.ForeColor = System.Drawing.Color.Red;
                        lblPartySearch.Visible = true;
                    }
                }
                else
                {

                    strMessage = "Recrods not found.";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup('" + strMessage + "');", true);

                    lblPartySearch.Text = "Recrods not found";
                    lblPartySearch.ForeColor = System.Drawing.Color.Red;
                    lblPartySearch.Visible = true;
                }
                //  }

            }
            catch (Exception ex)
            {

            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            CitationsSearch();
        }
        protected void gvLst_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {


                //Button btnShowSummary = default(Button);
                HiddenField hdSummary = default(HiddenField);
                System.Web.UI.HtmlControls.HtmlButton btnShowSummary = default(System.Web.UI.HtmlControls.HtmlButton);
                string script = null;
                script = "";

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    btnShowSummary = (System.Web.UI.HtmlControls.HtmlButton)e.Row.Controls[0].FindControl("btnshowsumary");
                    hdSummary = (HiddenField)e.Row.Controls[0].FindControl("hdSummary");

                    if (!string.IsNullOrEmpty(hdSummary.Value.ToString()))
                    {
                        btnShowSummary.Visible = true;
                    }
                    else
                    {
                        btnShowSummary.Visible = false;
                    }
                }
            }
            catch { }
        }

        protected void btnPartySearch_Click(object sender, EventArgs e)
        {
            CitationsSearchPartyName();
        }

        protected void btnExactPhrase_Click(object sender, EventArgs e)
        {
            try
            {
                string cri = "Where A.Citation is not null and A.Publish=1";
                string forlog = "";

               
                lblCaseAdvanceMsg.Visible = false;
                if (!string.IsNullOrEmpty(txtExactPhrase.Text.Trim()))
                {
                    forlog = forlog + " Freetext: " + txtExactPhrase.Text;
                    //cri = cri + " AND  (CONTAINS(A.Judgment,'\"" + txtExactPhrase.Text + "\"') or CONTAINS(A.Headnotes,'\"" + txtExactPhrase.Text + "\"'))";
                    InsertAuditLog("Search", "Case Advance Search", forlog);
                    string[] strSearch = new string[5];
                    strSearch[0] = "Cases";
                    Session["MemberSearchPara"] = strSearch;
                    Response.Redirect("/search/" + "\"" + txtExactPhrase.Text + "\"");
                }

                DataTable dt = new DataTable();
                

                

                //dt = objcase.GetCasesSearch(cri, 0, 30);
                //if (dt != null)
                //{
                //    if (dt.Rows.Count > 0)
                //    {
                //        dt.Columns.Add("Link");
                //        dt.Columns.Add("Title");
                //        for (int a = 0; a < dt.Rows.Count; a++)
                //        {

                //            dt.Rows[a]["Title"] = EastlawUI_v2.CommonClass.MakeFirstCap(dt.Rows[a]["Appeallant"].ToString()) + " VS " + EastlawUI_v2.CommonClass.MakeFirstCap(dt.Rows[a]["Respondent"].ToString());
                //            dt.Rows[a]["Link"] = "/cases/" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dt.Rows[a]["Appeallant"].ToString(), 3).ToString())).Replace(" ", "-") + "VS" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dt.Rows[a]["Respondent"].ToString(), 3).ToString())).Replace(" ", "-") + "." + EncryptDecryptHelper.Encrypt(dt.Rows[a]["ID"].ToString());
                //            //dt.Rows[a]["ShortDes"] = EastLawUI.CommonClass.GetWords(EastLawUI.CommonClass.GetShortDesc(dt.Rows[a]["Judgment"].ToString(), txtKeyword.Text).Replace("</p>", "").Replace("<p>", "").Replace("<br>", "").Replace("<b>", "").Replace("</b>", ""), 200);

                //        }
                //        dt.AcceptChanges();

                //        Session["CasesSearch"] = dt;
                //        string[] strSearch = new string[5];
                //        strSearch[1] = "Cases";
                //        Response.Redirect("/cases/search-result");
                //    }
                //    else
                //    {
                //        strMessage = "Recrods not found.";
                //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup('" + strMessage + "');", true);

                //        lblCaseAdvanceMsg.Text = "Recrods not found";
                //        lblCaseAdvanceMsg.ForeColor = System.Drawing.Color.Red;
                //        lblCaseAdvanceMsg.Visible = true;
                //    }
                //}
                //else
                //{

                //    strMessage = "Recrods not found.";
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup('" + strMessage + "');", true);

                //    lblCaseAdvanceMsg.Text = "Recrods not found";
                //    lblCaseAdvanceMsg.ForeColor = System.Drawing.Color.Red;
                //    lblCaseAdvanceMsg.Visible = true;
                //}
                //  }

            }
            catch { }
        }

        protected void btnExactPhraseMore_Click(object sender, EventArgs e)
        {
            try
            {
                string cri = "Where A.Citation is not null and A.Publish=1";
                string forlog = "";


                lblCaseAdvanceMsg.Visible = false;

                string keywordtxt = "";
                if (!string.IsNullOrEmpty(txtExactPhraseMore.Text.Trim()) && !string.IsNullOrEmpty(txtExactPhraseMore2.Text.Trim()))
                {
                    
                    //string[] Keywords = txtKeyword.Text.Trim().Split(',');

                    keywordtxt = keywordtxt + " \"" + txtExactPhraseMore.Text.Trim() + "\" and " + "\"" + txtExactPhraseMore2.Text.Trim() + "\"";

                  
                        //keywordtxt = "\"" + txtExactPhraseMore2.Text.Trim() + "\"";
                  
                    forlog = forlog + " Keyword: " + keywordtxt;
                  //  cri = cri + " AND  CONTAINS(A.Judgment,'" + keywordtxt + "')";
                    //cri = cri + " AND  CONTAINS(A.Keywords,'\"" + txtKeyword.Text + "\"')";
                    InsertAuditLog("Search", "Case Advance Search", forlog);

                    string[] strSearch = new string[5];
                    strSearch[0] = "Cases";
                    Session["MemberSearchPara"] = strSearch;
                    Response.Redirect("/search/" + keywordtxt);
                }


                //if (!string.IsNullOrEmpty(txtExactPhraseMore.Text.Trim()))
                //{
                //    forlog = forlog + " Freetext: " + txtExactPhraseMore.Text;
                //    cri = cri + " AND  (CONTAINS(A.Judgment,'" + txtExactPhraseMore.Text + "') or CONTAINS(A.Headnotes,'" + txtExactPhraseMore.Text + "'))";
                //}

                DataTable dt = new DataTable();
               

                //dt = objcase.GetCasesSearch(cri, 0, 30);
                //if (dt != null)
                //{
                //    if (dt.Rows.Count > 0)
                //    {
                //        dt.Columns.Add("Link");
                //        dt.Columns.Add("Title");
                //        for (int a = 0; a < dt.Rows.Count; a++)
                //        {

                //            dt.Rows[a]["Title"] = EastlawUI_v2.CommonClass.MakeFirstCap(dt.Rows[a]["Appeallant"].ToString()) + " VS " + EastlawUI_v2.CommonClass.MakeFirstCap(dt.Rows[a]["Respondent"].ToString());
                //            dt.Rows[a]["Link"] = "/cases/" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dt.Rows[a]["Appeallant"].ToString(), 3).ToString())).Replace(" ", "-") + "VS" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dt.Rows[a]["Respondent"].ToString(), 3).ToString())).Replace(" ", "-") + "." + EncryptDecryptHelper.Encrypt(dt.Rows[a]["ID"].ToString());
                //            //dt.Rows[a]["ShortDes"] = EastLawUI.CommonClass.GetWords(EastLawUI.CommonClass.GetShortDesc(dt.Rows[a]["Judgment"].ToString(), txtKeyword.Text).Replace("</p>", "").Replace("<p>", "").Replace("<br>", "").Replace("<b>", "").Replace("</b>", ""), 200);

                //        }
                //        dt.AcceptChanges();

                //        Session["CasesSearch"] = dt;
                //        string[] strSearch = new string[5];
                //        strSearch[1] = "Cases";
                //        Response.Redirect("/cases/search-result");
                //    }
                //    else
                //    {
                //        strMessage = "Recrods not found.";
                //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup('" + strMessage + "');", true);

                //        lblCaseAdvanceMsg.Text = "Recrods not found";
                //        lblCaseAdvanceMsg.ForeColor = System.Drawing.Color.Red;
                //        lblCaseAdvanceMsg.Visible = true;
                //    }
                //}
                //else
                //{

                //    strMessage = "Recrods not found.";
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup('" + strMessage + "');", true);

                //    lblCaseAdvanceMsg.Text = "Recrods not found";
                //    lblCaseAdvanceMsg.ForeColor = System.Drawing.Color.Red;
                //    lblCaseAdvanceMsg.Visible = true;
                //}
               
            }
            catch { }
        }

        protected void txtExactPhrase_TextChanged(object sender, EventArgs e)
        {
            
        }
        protected void btnMultiKeywords_Click(object sender, EventArgs e)
        {
            CitationsSearch();
        }

    }
}