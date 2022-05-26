using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using Telerik.Web.UI;

namespace EastlawUI_v2
{
    public partial class MemberHome : System.Web.UI.Page
    {
        EastLawBL.Cases objcases = new EastLawBL.Cases();
        EastLawBL.Journals objJo = new EastLawBL.Journals();
        EastLawBL.Statutes objstate = new EastLawBL.Statutes();
        EastLawBL.Departments objdpt = new EastLawBL.Departments();
        EastLawBL.Search objsrc = new EastLawBL.Search();
        protected void Page_Load(object sender, EventArgs e)
        {
           // BindStatuteTitle();
            if (!Page.IsPostBack)
            {
               
                if (Session["MemberID"] == null)
                {
                    Response.Redirect("/member/member-login?req=" + HttpContext.Current.Request.Url.AbsolutePath, false);
                    return;
                }

                GetJournals();
                GetStatutesCat();
                LoadYears();
                GetResults(int.Parse(Session["MemberID"].ToString()));
                //GetLatestJudgments2();

                // LoadYears();
            }
        }
        //void BindStatuteTitle()
        //{
        //    try
        //    {
        //        DataTable dt = new DataTable();
        //        dt = objstate.GetActiveStatutesLessInfo();
        //        RadAutoCompleteBox1.DataTextField = "Title";
        //        RadAutoCompleteBox1.DataSource = dt;
        //        RadAutoCompleteBox1.DataBind();
        //    }
        //    catch { }
        //}
        void GetLatestJudgments2()
        {
            try
            {
                DataTable dtnew = new DataTable();
                DataTable dtLatest = new DataTable();
                dtLatest = objcases.GetLatestCasesFront();
                if (dtLatest != null)
                {
                    if (dtLatest.Rows.Count > 0)
                    {
                        dtLatest.Columns.Add("Title");
                        dtLatest.Columns.Add("Link");
                        for (int a = 0; a < dtLatest.Rows.Count; a++)
                        {
                            dtLatest.Rows[a]["Title"] = EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Appeallant"].ToString(), 3) + "... VS " + EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Respondent"].ToString(), 3) + "...";
                            dtLatest.Rows[a]["Link"] = "/cases/" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Appeallant"].ToString(), 3).ToString())).Replace(" ", "-").ToLower() + "VS" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Respondent"].ToString(), 3).ToString())).Replace(" ", "-").ToLower() + "." + EncryptDecryptHelper.Encrypt(dtLatest.Rows[a]["CaseID"].ToString());
                        }
                        dtLatest.AcceptChanges();

                       // dtnew = dtLatest.AsEnumerable().Take(15).CopyToDataTable();
                        RadRotator1.DataSource = dtLatest;
                        RadRotator1.DataBind();

                    }
                }
            }
            catch { }
        }
        void GetLatestJudgments()
        {
            try
            {
                //DataTable dtnew = new DataTable();
                //DataTable dtLatest = new DataTable();
                //dtLatest = objcases.GetLatestCases();
                //if (dtLatest != null)
                //{
                //    if (dtLatest.Rows.Count > 0)
                //    {
                //        dtLatest.Columns.Add("Title");
                //        dtLatest.Columns.Add("Link");
                //        for (int a = 0; a < dtLatest.Rows.Count; a++)
                //        {
                //            dtLatest.Rows[a]["Title"] = EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Appeallant"].ToString(), 3) + "... VS " + EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Respondent"].ToString(), 3) + "...";
                //            dtLatest.Rows[a]["Link"] = "/cases/" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Appeallant"].ToString(), 3).ToString())).Replace(" ", "-").ToLower() + "VS" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["Respondent"].ToString(), 3).ToString())).Replace(" ", "-").ToLower() + "." + EncryptDecryptHelper.Encrypt(dtLatest.Rows[a]["ID"].ToString());
                //        }
                //        dtLatest.AcceptChanges();

                //        dtnew = dtLatest.AsEnumerable().Take(15).CopyToDataTable();
                //        rptrLatestJudgements.DataSource = dtnew;
                //        rptrLatestJudgements.DataBind();

                //    }
                //}
            }
            catch { }
        }
        void GetLatestLegislations()
        {
            try
            {
                DataTable dtnew = new DataTable();
                DataTable dtLatestLegislation = new DataTable();
                EastLawBL.Statutes objs = new EastLawBL.Statutes();
                dtLatestLegislation = objs.GetLatestStatutes();
                if (dtLatestLegislation != null)
                {
                    if (dtLatestLegislation.Rows.Count > 0)
                    {
                        
                        dtLatestLegislation.Columns.Add("Link");
                        for (int a = 0; a < dtLatestLegislation.Rows.Count; a++)
                        {
                            dtLatestLegislation.Rows[a]["Title"] = dtLatestLegislation.Rows[a]["Title"].ToString();
                            dtLatestLegislation.Rows[a]["Link"] = "/statutes/" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dtLatestLegislation.Rows[a]["Title"].ToString(), 3).ToString())).Replace(" ", "-").ToLower() + "." + EncryptDecryptHelper.Encrypt(dtLatestLegislation.Rows[a]["ID"].ToString());
                        }
                        dtLatestLegislation.AcceptChanges();

                        dtnew = dtLatestLegislation.AsEnumerable().Take(15).CopyToDataTable();
                        RadRotator2.DataSource = dtnew;
                        RadRotator2.DataBind();

                    }
                }
            }
            catch { }
        }
        protected void TimerTick(object sender, EventArgs e)
        {
           // this.GetLatestJudgments2();
           //// this.GetLatestLegislations();
           // Timer1.Enabled = false;
           // imgLoader.Visible = false;
           // //imgLoaderLegislation.Visible = false;
           // upPnlLatestCase.Update();
        }
        protected void TimerTickLegislation(object sender, EventArgs e)
        {
            //this.GetLatestLegislations();
            //Timer2.Enabled = false;
            //imgLoaderLegislation.Visible = false;
            //upPnlLatestLegislation.Update();
        }
        void GetNews()
        {
            try
            {
                DataTable dtnew = new DataTable();
                System.Data.DataTable dt = new System.Data.DataTable();
                EastLawBL.News objn = new EastLawBL.News();
                dt = objn.GetActiveNews();
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        dt.Columns.Add("imgURL");
                        for (int a = 0; a < dt.Rows.Count; a++)
                        {
                            string imgfilename = "";
                            if (dt.Rows[a]["ImageType"].ToString() == "Local")
                                dt.Rows[a]["imgURL"] = "/store/news/imgs/" + dt.Rows[a]["imgfile"].ToString();
                            else if (dt.Rows[a]["ImageType"].ToString() == "URL")
                                dt.Rows[a]["imgURL"] = "";
                            else
                                dt.Rows[a]["imgURL"] = dt.Rows[a]["imgfile"].ToString();
                        }
                        dt.AcceptChanges();

                        dtnew = dt.AsEnumerable().Take(10).CopyToDataTable();
                        RadRotator3.DataSource = dtnew;
                        RadRotator3.DataBind();

                    }
                }



            }
            catch { }
        }
        protected void TimerTickNews(object sender, EventArgs e)
        {
            this.GetNews();

            TimerNews.Enabled = true;
            imgNews.Visible = false;

        }
        void GetResults(int UserID)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objsrc.GetSearchTxt(UserID);
                dt.Columns.Add("strFoundResult");
                for (int a = 0; a < dt.Rows.Count; a++)
                {
                    if (dt.Rows[a]["FoundResult"].ToString() == "1")
                        dt.Rows[a]["strFoundResult"] = "Yes";
                    else
                        dt.Rows[a]["strFoundResult"] = "No";
                }

                ddlRecentSearch.DataTextField = "SearchText";
                ddlRecentSearch.DataValueField = "SearchText";
                ddlRecentSearch.DataSource = dt;
                ddlRecentSearch.DataBind();
                ddlRecentSearch.Items.Insert(0, new ListItem("Recent Search", "0"));

            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("MySearch.aspx", "GetResults", e.Message);
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchStatuteTitle(string prefixText, int count)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = DBHelper.GetConnectionString();
                using (SqlCommand cmd = new SqlCommand())
                {
                    //cmd.CommandText = "select ContactName from Customers where " +
                    //"ContactName like @SearchText + '%'";

                    cmd.CommandText = "select title from tbl_Statutes where active=1 and Pri_Sec!='Version' and isdeleted=0 and " +
                    "title like @SearchText + '%' order by title";

                    cmd.Parameters.AddWithValue("@SearchText", prefixText);
                    cmd.Connection = conn;
                    conn.Open();
                    List<string> customers = new List<string>();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            customers.Add(sdr["title"].ToString());
                        }
                    }
                    conn.Close();
                    return customers;
                }
            }
        }
        void logOut()
        {

            if (Session["UserLogged"] != null)
            {
                HttpContext.Current.Application.Remove(Session["UserLogged"].ToString());
                //Session["UserLogged"] = null;
                Session.RemoveAll();
                Response.Redirect("/member/member-Login?req=" + HttpContext.Current.Request.Url.AbsolutePath);
            }
            // Session.Abandon();
        }
        void LoadYears()
        {
            //// Clear items:    
            //ddlSROYear.Items.Clear();
            //ddlCircularYear.Items.Clear();
            //// Add default item to the list
            //ddlSROYear.Items.Insert(0, new ListItem("Select Year", "0"));
            //ddlCircularYear.Items.Insert(0, new ListItem("Select Year", "0"));
            //// Start loop
            //for (int i = 0; i < 69; i++)
            //{
            //    // For each pass add an item
            //    // Add a number of years (negative, which will subtract) to current year
            //    ddlSROYear.Items.Add(DateTime.Now.AddYears(-i).Year.ToString());
            //    ddlCircularYear.Items.Add(DateTime.Now.AddYears(-i).Year.ToString());
            //}
        }
        void GetJournals()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objJo.GetActiveJournals();

                ddlJournals.DataValueField = "ID";
                ddlJournals.DataTextField = "JournalName";
                ddlJournals.DataSource = dt;
                ddlJournals.DataBind();
                ddlJournals.Items.Insert(0, new ListItem("Select Journal", "0"));


                ddlCitationJournalMain.DataValueField = "ID";
                ddlCitationJournalMain.DataTextField = "JournalName";
                ddlCitationJournalMain.DataSource = dt;
                ddlCitationJournalMain.DataBind();
                ddlCitationJournalMain.Items.Insert(0, new ListItem("Select Journal", "0"));

            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("MemberHome.aspx", "GetJournals", e.Message);
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
                ExceptionHandling.SendErrorReport("MemberHome.aspx", "GetStatutesCat", e.Message);
            }
        }
        public void GetStatuesGroup(int ID)
        {
            try
            {

                //DataTable dt = objstate.GetStatutesGroup(ID);
                //if (dt.Rows.Count > 0)
                //{
                //    //ddlGroup.DataTextField = "Statutes_Category_Group";
                //    //ddlGroup.DataValueField = "ID";
                //    //ddlGroup.DataSource = dt;
                //    //ddlGroup.DataBind();
                //    //ddlGroup.Items.Insert(0, new ListItem("All", "0"));
                //}


            }
            catch { }

        }
        public void GetStatuesSubGroupByGroup(int GroupID)
        {
            try
            {

                //DataTable dt = objstate.GetStatutesSubGroupByGroup(GroupID);
                //if (dt.Rows.Count > 0)
                //{
                //    ddlSubGroup.DataTextField = "Statutes_Category_SubGroup";
                //    ddlSubGroup.DataValueField = "ID";
                //    ddlSubGroup.DataSource = dt;
                //    ddlSubGroup.DataBind();
                //    ddlSubGroup.Items.Insert(0, new ListItem("All", "0"));
                //}
                //else
                //    ddlSubGroup.Items.Insert(0, new ListItem("All", "0"));


            }
            catch { }

        }
        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
               // GetStatuesSubGroupByGroup(int.Parse(ddlGroup.SelectedValue));
            }
            catch { }
        }
        void CitationsSearch()
        {
            try
            {
                string cri = "Where Citation is not null and Publish=1";
                string forlog = "";

                if (!string.IsNullOrEmpty(txtCitationYear.Text.Trim()))
                {
                    forlog = forlog + " Citation Year: " + txtCitationYear.Text;
                    //cri = cri + " AND A.Year='" + txtCitationYear.Text.Trim() + "'";
                    cri = cri + " AND Year='" + txtCitationYear.Text.Trim() + "'";
                }

                if (ddlJournals.SelectedValue != "0")
                {
                    forlog = forlog + " Citation Journal: " + ddlJournals.SelectedItem.Text;
                    //cri = cri + " AND A.JournalID='" + ddlJournals.SelectedValue + "'";
                    cri = cri + " AND JournalID='" + ddlJournals.SelectedValue + "'";
                }

                //if (!string.IsNullOrEmpty(txtCitationNumber.Text.Trim()))
                //    cri = cri + " AND A.Citation like '%"+txtCitationNumber.Text.Trim()+"%'";

                if (!string.IsNullOrEmpty(txtCitationNumber.Text.Trim()))
                {
                    forlog = forlog + " Citation No: " + txtCitationNumber.Text;
                    //cri = cri + " AND  (A.PageNo='" + txtCitationNumber.Text.Trim() + "' or CONTAINS (A.Citation, '" + txtCitationNumber.Text.Trim() + "'))";
                    //cri = cri + " AND  (A.PageNo='" + txtCitationNumber.Text.Trim() + "')";
                    cri = cri + " AND  (PageNo='" + txtCitationNumber.Text.Trim() + "')";
                    //cri = cri + " AND  CONTAINS (A.Citation, '" + txtCitationNumber.Text.Trim() + "' )";
                }

                DataTable dt = new DataTable();
                InsertAuditLog("Search", "Search By Citation", forlog);
                //dt = objcases.GetCasesSearch(cri,0,30);
                dt = objcases.GetCasesSearch_Year_Journal_PageNo(int.Parse(txtCitationYear.Text.Trim()), int.Parse(ddlJournals.SelectedValue), txtCitationNumber.Text.Trim());
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

                        }
                        dt.AcceptChanges();
                        Session["CasesSearch"] = dt;
                        Session["SearchMain"] = null;
                        Session["SearchWithIn"] = null;
                        lblCitationMsg.Text = "";
                        lblCitationMsg.Visible = false;
                        Response.Redirect("/cases/search-result");
                    }
                    else
                    {
                        lblCitationMsg.Text = "Record not found";
                        lblCitationMsg.Visible = true;
                    }
                }
                else
                {
                    lblCitationMsg.Text = "Record not found";
                    lblCitationMsg.Visible = true;

                }

            }
            catch (Exception ex)
            {

            }
        }
        void StatutesSearch()
        {
            try
            {
                string cri = "Where A.IsDeleted=0 and A.Active=1";
                string keywordtxt = "";
                if (!string.IsNullOrEmpty(txtStatutesTitle.Text.Trim()))
                    cri = cri + " AND CONTAINS(A.Title,'\"" + txtStatutesTitle.Text + "\"')";

                //if (!string.IsNullOrEmpty(RadAutoCompleteBox1.Text.Trim()))
                //{
                //   // string keywordtxt = "";
                //    //string[] Keywords = txtKeyword.Text.Trim().Split(',');
                //    string[] Keywords = RadAutoCompleteBox1.Text.Trim().Split(';');
                //    for (int a = 0; a < Keywords.Length - 1; a++)
                //    {

                //        if (!string.IsNullOrEmpty(Keywords[a].ToString().Trim()))
                //            keywordtxt = keywordtxt + " \"" + Keywords[a].ToString().Trim() + "\" or";

                //    }
                //    if (Keywords.Length < 1)
                //    {
                //        keywordtxt = "\"" + RadAutoCompleteBox1.Text.Trim() + "\"";
                //    }
                //    else
                //    {
                //        keywordtxt = keywordtxt.Remove(keywordtxt.Length - 3);
                //    }

                //    cri = cri + " AND  CONTAINS(A.Title,'" + keywordtxt + "')";
                   
                //}

                //if (!string.IsNullOrEmpty(txtStatutesTitle.Text.Trim()))
                //{
                //    string Statutestxt = "";
                //    string[] Statutes = txtStatutesTitle.Text.Trim().Split(',');
                //    for (int a = 0; a < Statutes.Length - 1; a++)
                //    {

                //        if (!string.IsNullOrEmpty(Statutes[a].ToString()))
                //            Statutestxt = Statutestxt + " \"" + Statutes[a].ToString() + "\" or";

                //    }
                //    if (Statutes.Length == 1)
                //    {
                //        Statutestxt = "\"" + txtStatutesTitle.Text.Trim() + "\"";
                //    }
                //    else
                //    {
                //        Statutestxt = Statutestxt.Remove(Statutestxt.Length - 3);
                //    }

                //    cri = cri + " AND  CONTAINS(A.Title,'" + Statutestxt + "')";
                //}


                if (!string.IsNullOrEmpty(txtYear.Text.Trim()))
                    cri = cri + " AND contains( A.Date,'" + txtYear.Text.Trim() + "')";

                //if (chkPri.Checked == true)
                //    cri = cri + " AND A.Pri_Sec='PRIMARY'";
                //if (chksec.Checked == true)
                //    cri = cri + " AND A.Pri_Sec='SECONDARY'";

                //if (!string.IsNullOrEmpty(txtSRONo.Text.Trim()))
                //    cri = cri + " AND contains( A.Title,'" + txtSRONo.Text.Trim() + "') AND A.SType='SRO' ";

                //if (ddlSROYear.SelectedValue != "0")
                //    cri = cri + " AND A.SYear='" + ddlSROYear.SelectedValue + "'";

                //if (!string.IsNullOrEmpty(txtCircularNo.Text.Trim()))
                //    cri = cri + " AND contains( A.Title,'" + txtCircularNo.Text.Trim() + "') and AND A.SType='Circular'";

                //if (ddlCircularYear.SelectedValue != "0")
                //    cri = cri + " AND A.SYear='" + ddlCircularYear.SelectedValue + "'";


                //if (ddlGroup.SelectedValue != "0")
                //    cri = cri + " AND A.GroupID=" + ddlGroup.SelectedValue + "";

                //if (ddlSubGroup.SelectedValue != "0")
                //    cri = cri + " AND A.SubGroupID=" + ddlSubGroup.SelectedValue + "";

                if (ddlStatutesCat.SelectedValue != "0")
                    cri = cri + " AND A.CatID='" + ddlStatutesCat.SelectedValue + "'";


                DataTable dt = new DataTable();
                dt = objstate.GetStatutesSearch(cri);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        Session["StatutesSearch"] = dt;
                        Response.Redirect("/statutes/search-result");
                    }
                    else
                    {
                        lblLegisLationMsg.Text = "Records not found, please use another term.";
                        lblLegisLationMsg.ForeColor = System.Drawing.Color.Red;
                        lblLegisLationMsg.Visible = true;
                    }
                }
                else
                {
                    lblLegisLationMsg.Text = "Records not found, please use another term.";
                    lblLegisLationMsg.ForeColor = System.Drawing.Color.Red;
                    lblLegisLationMsg.Visible = true;
                }

            }
            catch (Exception ex)
            {

            }
        }

        protected void btnCitationSearch_Click(object sender, EventArgs e)
        {
            CitationsSearch();
        }

        protected void btnCitationSearchMain_Click(object sender, EventArgs e)
        {
            try
            {
                string cri = "Where Citation is not null and Publish=1";
                string forlog = "";

                if (!string.IsNullOrEmpty(txtByCitationYearMain.Text.Trim()))
                {
                    forlog = forlog + " Citation Year: " + txtByCitationYearMain.Text;
                    //cri = cri + " AND A.Year='" + txtByCitationYearMain.Text.Trim() + "'";
                    cri = cri + " AND Year='" + txtByCitationYearMain.Text.Trim() + "'";
                }

                if (ddlCitationJournalMain.SelectedValue != "0")
                {
                    forlog = forlog + " Citation Journal: " + ddlCitationJournalMain.SelectedItem.Text;
                    //cri = cri + " AND A.JournalID='" + ddlCitationJournalMain.SelectedValue + "'";
                    cri = cri + " AND JournalID='" + ddlCitationJournalMain.SelectedValue + "'";
                }

                //if (!string.IsNullOrEmpty(txtCitationNumber.Text.Trim()))
                //    cri = cri + " AND A.Citation like '%"+txtCitationNumber.Text.Trim()+"%'";

                if (!string.IsNullOrEmpty(txtCitationPageMain.Text.Trim()))
                {
                    forlog = forlog + " Citation No: " + txtCitationPageMain.Text;
                    //cri = cri + " AND  (A.PageNo='" + txtCitationNumber.Text.Trim() + "' or CONTAINS (A.Citation, '" + txtCitationNumber.Text.Trim() + "'))";
                    //cri = cri + " AND  (A.PageNo='" + txtCitationPageMain.Text.Trim() + "')";
                    cri = cri + " AND  (PageNo='" + txtCitationPageMain.Text.Trim() + "')";
                    //cri = cri + " AND  CONTAINS (A.Citation, '" + txtCitationNumber.Text.Trim() + "' )";
                }

                DataTable dt = new DataTable();
                InsertAuditLog("Search", "Search By Citation", forlog);
                //dt = objcases.GetCasesSearch(cri, 0, 30);
                dt = objcases.GetCasesSearch_Year_Journal_PageNo(int.Parse(txtByCitationYearMain.Text.Trim()), int.Parse(ddlCitationJournalMain.SelectedValue), txtCitationPageMain.Text.Trim());
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

                        }
                        dt.AcceptChanges();
                        Session["CasesSearch"] = dt;
                        Session["SearchMain"] = null;
                        Session["SearchWithIn"] = null;
                        lblCitaionSearchMain.Text = "";
                        lblCitaionSearchMain.Visible = false;
                        Response.Redirect("/cases/search-result");
                    }
                    else
                    {
                        lblCitaionSearchMain.Text = "Record not found";
                        lblCitaionSearchMain.Visible = true;
                    }
                }
                else
                {
                    lblCitaionSearchMain.Text = "Record not found";
                    lblCitaionSearchMain.Visible = true;

                }

            }
            catch (Exception ex)
            {

            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {

                if (string.IsNullOrEmpty(txtSearch.Text))
                {
                    lblMsg.Visible = true;
                }

                else
                {
                    lblMsg.Visible = false;
                    string[] strSearch = new string[5];
                    strSearch[0] = "Cases";
                    //for (int a = 0; a < chkLst.Items.Count; a++)
                    //{
                    //    if (chkLst.Items[a].Selected == true)
                    //        strSearch[a] = chkLst.Items[a].Value;
                    //}
                    Session["MemberSearchPara"] = strSearch;

                    RegexOptions options = RegexOptions.None;
                    Regex regex = new Regex("[ ]{2,}", options);
                    string Sword = regex.Replace(txtSearch.Text.Trim(), " ");
                    Response.Redirect("/search/" + CommonClass.RemoveSomeCharacters(Sword));
                }
            }
            catch (Exception ex)
            { }
        }

        protected void searchbox_TextChanged(object sender, EventArgs e)
        {
            string[] strSearch = new string[5];
            strSearch[0] = "Cases";
            //for (int a = 0; a < chkLst.Items.Count; a++)
            //{
            //    if (chkLst.Items[a].Selected == true)
            //        strSearch[a] = chkLst.Items[a].Value;
            //}
            Session["MemberSearchPara"] = strSearch;
            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex("[ ]{2,}", options);
            string Sword = regex.Replace(txtSearch.Text.Trim(), " ");
            Response.Redirect("/search/" + CommonClass.RemoveSomeCharacters(Sword));
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {

            string[] strSearch = new string[5];
            strSearch[0] = "Cases";
            //for (int a = 0; a < chkLst.Items.Count; a++)
            //{
            //    if (chkLst.Items[a].Selected == true)
            //        strSearch[a] = chkLst.Items[a].Value;
            //}
            Session["MemberSearchPara"] = strSearch;
            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex("[ ]{2,}", options);
            string Sword = regex.Replace(txtSearch.Text.Trim(), " ");
            Response.Redirect("/search/" + CommonClass.RemoveSomeCharacters(Sword));
        }

        protected void btnStatutesSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtStatutesTitle.Text) && string.IsNullOrEmpty(txtYear.Text))
            {
                lblLegisLationMsg.Text = "Please enter title or year.";
                lblLegisLationMsg.ForeColor = System.Drawing.Color.Red;
                lblLegisLationMsg.Visible = true;
            }
            else
            {
                lblLegisLationMsg.Text = "";
                lblLegisLationMsg.ForeColor = System.Drawing.Color.Red;
                lblLegisLationMsg.Visible = false;
                StatutesSearch();
            }
        }
        void InsertAuditLog(string ActType, string Action, string txt)
        {
            try
            {
                string Country = "";
                string Region = "";
                string City = "";
                EastLawBL.Users objuser = new EastLawBL.Users();
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
                    chk = objuser.InsertAuditLog(ActType, Action, Request.Url.AbsoluteUri.ToString(), visitorIPAddress, int.Parse(Session["MemberID"].ToString()), Country, Region, City, txt, BrowserName, SourcePlatform, "Desktop Website");
                else
                    chk = objuser.InsertAuditLog(ActType, Action, Request.Url.AbsoluteUri.ToString(), visitorIPAddress, 0, Country, Region, City, txt, BrowserName, SourcePlatform, "Desktop Website");
            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("home/Default.aspx", "InsertAuditLog", e.Message);
            }
        }
        protected void btnFill_Click(object sender, EventArgs e)
        {
            //ModalPopupExtender1.Hide();
            ////string aa = PopuptxtFirstName.Text;
            ////string bb = PopuptxtLastName.Text;

            //EastLawBL.ErrorReporting objer = new EastLawBL.ErrorReporting();
            //objer.Type = "Element";
            //objer.ItemType = "Statutes";
            //objer.ItemID = 0;
            //if (Session["MemberID"] != null)
            //    objer.UserID = int.Parse(Session["MemberID"].ToString());
            //else
            //    objer.UserID = 0;
            //objer.UserComment = txtComment.Text;
            //objer.WorkflowID = 1;
            //int chk = objer.InsertReportError();
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            //ModalPopupExtender1.Hide();
        }

        protected void btnFillMissingCitation_Click(object sender, EventArgs e)
        {
            //ModalPopupExtenderCitations.Hide();
            ////string aa = PopuptxtFirstName.Text;
            ////string bb = PopuptxtLastName.Text;

            //EastLawBL.ErrorReporting objer = new EastLawBL.ErrorReporting();
            //objer.Type = "Element";
            //objer.ItemType = "Cases";
            //objer.ItemID = 0;
            //if (Session["MemberID"] != null)
            //    objer.UserID = int.Parse(Session["MemberID"].ToString());
            //else
            //    objer.UserID = 0;
            //objer.UserComment = txtMissingCitation.Text;
            //objer.WorkflowID = 1;
            //int chk = objer.InsertReportError();
        }
        protected void btnCloseMissingCitation_Click(object sender, EventArgs e)
        {
           // ModalPopupExtenderCitations.Hide();
        }
        void DepartmentSearch()
        {
            try
            {
                string cri = "Where A.IsDeleted=0";


                if (!string.IsNullOrEmpty(txtSearch.Text.Trim()))
                {

                    cri = cri + " AND  (CONTAINS(A.Title,'\"" + txtSearch.Text + "\"')";
                    cri = cri + " OR  CONTAINS(A.Year,'\"" + txtSearch.Text + "\"')";
                    cri = cri + " OR  CONTAINS(A.No,'\"" + txtSearch.Text + "\"')";
                    cri = cri + " OR  CONTAINS(A.DDate,'\"" + txtSearch.Text + "\"')";
                    cri = cri + " OR  CONTAINS(A.DType,'\"" + txtSearch.Text + "\"'))";

                    DataTable dt = new DataTable();
                    dt = objdpt.DepartmentSearch(cri);
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            Session["DeptSearch"] = dt;
                            Response.Redirect("/Departments/Search-Result");
                        }
                    }

                }





            }
            catch (Exception ex)
            {

            }
        }

        protected void btnSRONoSearch_Click(object sender, EventArgs e)
        {
            try
            {
                //string cri = "Where A.IsDeleted=0";


                //if (!string.IsNullOrEmpty(txtSRONo.Text.Trim()))
                //{

                //    cri = cri + " AND A.DType='S.R.O.'";
                //    cri = cri + " AND  CONTAINS(A.No,'\"" + txtSRONo.Text + "\"')";
                //    if (ddlSROYear.SelectedValue != "0")
                //        cri = cri + " AND  A.Year='" + ddlSROYear.SelectedValue + "'";

                //    DataTable dt = new DataTable();
                //    dt = objdpt.DepartmentSearch(cri);
                //    if (dt != null)
                //    {
                //        if (dt.Rows.Count > 0)
                //        {
                //            Session["DeptSearch"] = dt;
                //            Response.Redirect("/Departments/Search-Result");
                //        }
                //    }

                //}
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnCircularNoSearch_Click(object sender, EventArgs e)
        {
            try
            {
                //string cri = "Where A.IsDeleted=0";


                //if (!string.IsNullOrEmpty(txtCircularNo.Text.Trim()))
                //{

                //    cri = cri + " AND A.DType='CIRCULAR NO.'";
                //    cri = cri + " AND  CONTAINS(A.No,'\"" + txtCircularNo.Text + "\"')";
                //    if (ddlCircularYear.SelectedValue != "0")
                //        cri = cri + " AND  A.Year='" + ddlCircularYear.SelectedValue + "'";

                //    DataTable dt = new DataTable();
                //    dt = objdpt.DepartmentSearch(cri);
                //    if (dt != null)
                //    {
                //        if (dt.Rows.Count > 0)
                //        {
                //            Session["DeptSearch"] = dt;
                //            Response.Redirect("/departments/search-Result");
                //        }
                //    }

                //}
            }
            catch (Exception ex)
            {

            }
        }

        protected void txtStatutesTitle_TextChanged(object sender, EventArgs e)
        {
            StatutesSearch();
        }

        protected void ddlRecentSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlRecentSearch.SelectedIndex != 0)
                {
                    string[] strSearch = new string[5];
                    strSearch[0] = "Cases";
                    //for (int a = 0; a < chkLst.Items.Count; a++)
                    //{
                    //    if (chkLst.Items[a].Selected == true)
                    //        strSearch[a] = chkLst.Items[a].Value;
                    //}
                    Session["MemberSearchPara"] = strSearch;
                    RegexOptions options = RegexOptions.None;
                    Regex regex = new Regex("[ ]{2,}", options);
                    string Sword = regex.Replace(ddlRecentSearch.Text.Trim(), " ");
                    Response.Redirect("/search/" + CommonClass.RemoveSomeCharacters(Sword));
                }
            }
            catch { }
        }

        protected void radioALlWord_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                txtSearch.Text = "";
                upnlMain.Update();
                
            }
            catch { }
        }

        protected void radioMoreThan_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                txtSearch.Text = "\" \" AND \" \"";
                upnlMain.Update();
               
            }
            catch { }

        }

        protected void radioExactPhrase_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                txtSearch.Text = "\"\"";
                upnlMain.Update();
            }
            catch { }
        }
    }
}