using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace EastlawUI_v2.m
{
    public partial class MemberHome : System.Web.UI.Page
    {
        EastLawBL.Cases objcases = new EastLawBL.Cases();
        EastLawBL.Journals objJo = new EastLawBL.Journals();
        EastLawBL.Statutes objstate = new EastLawBL.Statutes();
        EastLawBL.Departments objdpt = new EastLawBL.Departments();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GetJournals();
            }
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
                ddlJournals.Items.Insert(0, new ListItem("Select", "0"));

            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("MemberHome.aspx", "GetJournals", e.Message);
            }
        }
        void StatutesSearch()
        {
            try
            {
                string cri = "Where A.IsDeleted=0";

                //if (!string.IsNullOrEmpty(txtStatutesTitle.Text.Trim()))
                //    cri = cri + " AND CONTAINS(A.Title,'\"" + txtStatutesTitle.Text + "\"')";

                if (!string.IsNullOrEmpty(txtStatutesTitle.Text.Trim()))
                {
                    string Statutestxt = "";
                    string[] Statutes = txtStatutesTitle.Text.Trim().Split(',');
                    for (int a = 0; a < Statutes.Length - 1; a++)
                    {

                        if (!string.IsNullOrEmpty(Statutes[a].ToString()))
                            Statutestxt = Statutestxt + " \"" + Statutes[a].ToString() + "\" or";

                    }
                    if (Statutes.Length == 1)
                    {
                        Statutestxt = "\"" + txtStatutesTitle.Text.Trim() + "\"";
                    }
                    else
                    {
                        Statutestxt = Statutestxt.Remove(Statutestxt.Length - 3);
                    }

                    cri = cri + " AND  CONTAINS(A.Title,'" + Statutestxt + "')";
                }


                //if (!string.IsNullOrEmpty(txtYear.Text.Trim()))
                //    cri = cri + " AND contains( A.Date,'" + txtYear.Text.Trim() + "')";

                if (chkPri.Checked == true)
                    cri = cri + " AND A.Pri_Sec='PRIMARY'";
                if (chksec.Checked == true)
                    cri = cri + " AND A.Pri_Sec='SECONDARY'";

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

                //if (ddlStatutesCat.SelectedValue != "0")
                //    cri = cri + " AND A.CatID='" + ddlStatutesCat.SelectedValue + "'";


                DataTable dt = new DataTable();
                dt = objstate.GetStatutesSearch(cri);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        Session["StatutesSearch"] = dt;
                        Response.Redirect("/m/Statutes/Search-Result");
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
        void CitationsSearch()
        {
            try
            {
                string cri = "Where A.Citation is not null";
                string forlog = "";

                if (!string.IsNullOrEmpty(txtCitationYear.Text.Trim()))
                {
                    forlog = forlog + " Citation Year: " + txtCitationYear.Text;
                    cri = cri + " AND A.Year='" + txtCitationYear.Text.Trim() + "'";
                }

                if (ddlJournals.SelectedValue != "0")
                {
                    forlog = forlog + " Citation Journal: " + ddlJournals.SelectedItem.Text;
                    cri = cri + " AND A.JournalID='" + ddlJournals.SelectedValue + "'";
                }

                //if (!string.IsNullOrEmpty(txtCitationNumber.Text.Trim()))
                //    cri = cri + " AND A.Citation like '%"+txtCitationNumber.Text.Trim()+"%'";

                if (!string.IsNullOrEmpty(txtCitationNumber.Text.Trim()))
                {
                    forlog = forlog + " Citation No: " + txtCitationNumber.Text;
                    //cri = cri + " AND  (A.PageNo='" + txtCitationNumber.Text.Trim() + "' or CONTAINS (A.Citation, '" + txtCitationNumber.Text.Trim() + "'))";
                    cri = cri + " AND  (A.PageNo='" + txtCitationNumber.Text.Trim() + "')";
                    //cri = cri + " AND  CONTAINS (A.Citation, '" + txtCitationNumber.Text.Trim() + "' )";
                }

                DataTable dt = new DataTable();
                InsertAuditLog("Search", "Search By Citation", forlog);
                dt = objcases.GetCasesSearch(cri,0,30);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        dt.Columns.Add("Link");
                        dt.Columns.Add("Title");
                        for (int a = 0; a < dt.Rows.Count; a++)
                        {

                            dt.Rows[a]["Title"] = CommonClass.MakeFirstCap(dt.Rows[a]["Appeallant"].ToString()) + " VS " + CommonClass.MakeFirstCap(dt.Rows[a]["Respondent"].ToString());
                            dt.Rows[a]["Link"] = "/m/Cases/" + clsUtilities.RemoveSpecialCharacter(CommonClass.MakeFirstCap(CommonClass.GetWords(dt.Rows[a]["Appeallant"].ToString(), 3).ToString())).Replace(" ", "-") + "VS" + clsUtilities.RemoveSpecialCharacter(CommonClass.MakeFirstCap(CommonClass.GetWords(dt.Rows[a]["Respondent"].ToString(), 3).ToString())).Replace(" ", "-") + "." + EncryptDecryptHelper.Encrypt(dt.Rows[a]["ID"].ToString());

                        }
                        dt.AcceptChanges();
                        Session["CasesSearch"] = dt;
                        Session["SearchMain"] = null;
                        Session["SearchWithIn"] = null;
                        lblCitationMsg.Text = "";
                        lblCitationMsg.Visible = false;
                        Response.Redirect("/m/Cases/Search-Result");
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
        protected void txtStatutesTitle_TextChanged(object sender, EventArgs e)
        {
            StatutesSearch();
        }
        protected void btnCitationSearch_Click(object sender, EventArgs e)
        {
            CitationsSearch();
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

                    for (int a = 0; a < chkLst.Items.Count; a++)
                    {
                        if (chkLst.Items[a].Selected == true)
                            strSearch[a] = chkLst.Items[a].Value;
                    }
                    Session["MemberSearchPara"] = strSearch;

                    Response.Redirect("/m/Search/" + CommonClass.RemoveSomeCharacters(txtSearch.Text.Trim()));
                }
            }
            catch (Exception ex)
            { }
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
                    chk = objuser.InsertAuditLog(ActType, Action, Request.Url.AbsoluteUri.ToString(), visitorIPAddress, int.Parse(Session["MemberID"].ToString()), Country, Region, City, txt, BrowserName, SourcePlatform, "Mobile Website");
                else
                    chk = objuser.InsertAuditLog(ActType, Action, Request.Url.AbsoluteUri.ToString(), visitorIPAddress, 0, Country, Region, City, txt, BrowserName, SourcePlatform, "Mobile Website");
            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("home/Default.aspx", "InsertAuditLog", e.Message);
            }
        }
    }
}