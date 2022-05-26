using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


namespace EastlawUI_v2.m
{
    public partial class PracticeAreaSearchGeneral : System.Web.UI.Page
    {
        EastLawBL.News objn = new EastLawBL.News();
        EastLawBL.PracticeAreas objPA = new EastLawBL.PracticeAreas();
        EastLawBL.Users objusr = new EastLawBL.Users();
        EastLawBL.Departments objdpt = new EastLawBL.Departments();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GetDDLTaggedStatutesByPracticeArea();
                if (HttpContext.Current.Items["Title"] != null)
                {
                    lblPA.Text = HttpContext.Current.Items["Title"].ToString();
                    
                    if (Request.QueryString["param"] != null)
                    {
                        lblPAID.Text = Request.QueryString["param"].ToString();

                    }
                    GetDeptTypeGroup(16);
                }
                InsertAuditLog("Practice Area", "Home", HttpContext.Current.Items["Title"].ToString());
            }

        }
        void GetDeptTypeGroup(int DeptID)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objdpt.GetDepartmentDDTypeByDepartmentsParentForPracticeArea(DeptID);
                ddlDeptTypeGroups.DataValueField = "DType";
                ddlDeptTypeGroups.DataTextField = "DType";
                ddlDeptTypeGroups.DataSource = dt;
                ddlDeptTypeGroups.DataBind();
                ddlDeptTypeGroups.Items.Insert(0, new ListItem("Select", "0"));
            }
            catch { }
        }
        public string PracticeAreaNews()
        {
            try
            {
                DataTable dtNews = new DataTable();
                dtNews = objn.GetActiveNewsByPracticeArea(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString()).ToString()));
                string htmtxt = "";
                if (dtNews != null)
                {
                    if (dtNews.Rows.Count > 0)
                    {
                        htmtxt = htmtxt + "<marquee direction='up' scrollamount='1' height='350px' width='350px'><ul>";
                        for (int a = 0; a < dtNews.Rows.Count; a++)
                        {
                            if (!string.IsNullOrEmpty(dtNews.Rows[a]["SourceLink"].ToString()))
                            {
                                if (!string.IsNullOrEmpty(dtNews.Rows[a]["imgfile"].ToString()))
                                {
                                    htmtxt = htmtxt + "<img src='" + dtNews.Rows[a]["imgfile"].ToString() + "' width='42' height='42' align='top' style='padding-right:5px' />";
                                }
                                else
                                {
                                    htmtxt = htmtxt + "<img src='/images/no_image-128.png' width='42' height='42' align='top' style='padding-right:5px' />";
                                }
                                htmtxt = htmtxt + "<a  href='" + dtNews.Rows[a]["SourceLink"].ToString() + "' target='_blank'>" + CommonClass.MakeFirstCap(dtNews.Rows[a]["Title"].ToString()) + "</a>"
                                 + "<br>" + CommonClass.GetWords(dtNews.Rows[a]["FullContent"].ToString(), 40).ToString() + " ...<br><b>" + dtNews.Rows[a]["NDate"].ToString() + "</b><br><i>" + dtNews.Rows[a]["Source"].ToString() + "</i> <hr>";
                            }
                            else
                            {
                                htmtxt = htmtxt + "<a  href='/News/" + dtNews.Rows[a]["Title"].ToString() + "." + EncryptDecryptHelper.Encrypt(dtNews.Rows[a]["ID"].ToString()) + "'>" + dtNews.Rows[a]["Title"].ToString() + "</a>"
                                    + "<br>" + CommonClass.GetWords(dtNews.Rows[a]["FullContent"].ToString(), 40).ToString() + " ...<br><b>" + dtNews.Rows[a]["NDate"].ToString() + "</b><br><i>" + dtNews.Rows[a]["Source"].ToString() + "</i> <hr>";
                            }
                            if (a == 9)
                                break;
                        }
                        htmtxt = htmtxt + "</ul> </marquee>";

                    }
                }
                return htmtxt;
            }
            catch
            {
                return "";
            }
        }
        public void GetDDLTaggedStatutesByPracticeArea()
        {
            try
            {
                DataTable dtStat = new DataTable();
                dtStat = objPA.GetTaggesStatuesWithPracticeArea(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString()).ToString()));
                for (int a = 0; a < dtStat.Rows.Count; a++)
                {
                    dtStat.Rows[a]["Title"] = CommonClass.MakeFirstCap(dtStat.Rows[a]["Title"].ToString());
                }
                dtStat.AcceptChanges();
                ddlTaggedStatutes.DataValueField = "Title";
                ddlTaggedStatutes.DataTextField = "Title";
                ddlTaggedStatutes.DataSource = dtStat;
                ddlTaggedStatutes.DataBind();
                ddlTaggedStatutes.Items.Insert(0, new ListItem("Select", "0"));
            }
            catch { }
        }
        public string GetTaggedStatutesByPracticeArea()
        {
            try
            {
                DataTable dtStat = new DataTable();
                dtStat = objPA.GetTaggesStatuesWithPracticeArea(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString()).ToString()));
                string htmtxt = "";
                if (dtStat != null)
                {
                    if (dtStat.Rows.Count > 0)
                    {
                        htmtxt = htmtxt + "<ul>";
                        for (int a = 0; a < dtStat.Rows.Count; a++)
                        {
                            htmtxt = htmtxt + "<li><a  href='/Statutes/" + dtStat.Rows[a]["Title"].ToString() + "." + EncryptDecryptHelper.Encrypt(dtStat.Rows[a]["ID"].ToString()) + "'>" + CommonClass.MakeFirstCap(dtStat.Rows[a]["Title"].ToString()) + "</a></li>";
                            if (a == 4)
                                break;
                        }
                        htmtxt = htmtxt + "</ul>";

                    }
                }
                return htmtxt;
            }
            catch
            {
                return "";
            }
        }
        public string GetTaggedCasesByPracticeArea()
        {
            try
            {
                DataTable dtCases = new DataTable();
                dtCases = objPA.GetTaggesCasesWithPracticeArea(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString()).ToString()));
                string htmtxt = "";
                if (dtCases != null)
                {
                    dtCases.Columns.Add("Title");
                    dtCases.Columns.Add("Link");
                    for (int a = 0; a < dtCases.Rows.Count; a++)
                    {
                        dtCases.Rows[a]["Title"] = CommonClass.MakeFirstCap(CommonClass.GetWords(dtCases.Rows[a]["Appeallant"].ToString(), 5)) + " VS " + CommonClass.MakeFirstCap(CommonClass.GetWords(dtCases.Rows[a]["Respondent"].ToString(), 5));
                        dtCases.Rows[a]["Link"] = "/Cases/" + clsUtilities.RemoveSpecialCharacter(CommonClass.MakeFirstCap(CommonClass.GetWords(dtCases.Rows[a]["Appeallant"].ToString(), 3).ToString())).Replace(" ", "-") + "VS" + clsUtilities.RemoveSpecialCharacter(CommonClass.MakeFirstCap(CommonClass.GetWords(dtCases.Rows[a]["Respondent"].ToString(), 3).ToString())).Replace(" ", "-") + "." + EncryptDecryptHelper.Encrypt(dtCases.Rows[a]["ID"].ToString());
                    }
                    dtCases.AcceptChanges();
                    if (dtCases.Rows.Count > 0)
                    {
                        htmtxt = htmtxt + "<ul>";
                        for (int a = 0; a < dtCases.Rows.Count; a++)
                        {
                            htmtxt = htmtxt + "<li><a  href='" + dtCases.Rows[a]["Link"].ToString() + "'>" + dtCases.Rows[a]["Title"].ToString() + " <br><b>Court: </b>" + dtCases.Rows[a]["Court"].ToString() + " <br><b>Date: </b> " + dtCases.Rows[a]["JDate"].ToString() + "</a></li>";
                            if (a == 4)
                                break;
                        }
                        htmtxt = htmtxt + "</ul>";

                    }
                }
                return htmtxt;
            }
            catch
            {
                return "";
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("/m/Practice-Area/Search/" + txtSearch.Text.Trim() + "?param=" + lblPAID.Text + "&trans=" + Request.QueryString["trans"].ToString() + "");
            }
            catch { }
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
                    chk = objusr.InsertAuditLog(ActType, Action, Request.Url.AbsoluteUri.ToString(), visitorIPAddress, int.Parse(Session["MemberID"].ToString()), Country, Region, City, txt, BrowserName, SourcePlatform, "Mobile Website");
                else
                    chk = objusr.InsertAuditLog(ActType, Action, Request.Url.AbsoluteUri.ToString(), visitorIPAddress, 0, Country, Region, City, txt, BrowserName, SourcePlatform, "Mobile Website");
            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("home/Default.aspx", "InsertAuditLog", e.Message);
            }
        }
        string SearchPhraseForQuickSearch()
        {
            try
            {
                string word = "section ";
                word = word + txtSection.Text;
                word = word + " of ";
                word = word + ddlTaggedStatutes.SelectedValue;

                return word;

            }
            catch
            {
                return "";
            }
        }

        protected void btnQuickFind_Click(object sender, EventArgs e)
        {
            Response.Redirect("/m/Practice-Area/Search/" + SearchPhraseForQuickSearch() + "?param=" + lblPAID.Text + "&trans=" + Request.QueryString["trans"].ToString() + "");
        }
        protected void btnTypeSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string cri = "Where A.IsDeleted=0";


                if (ddlDeptTypeGroups.SelectedValue != "0")
                {

                    cri = cri + " AND A.DType='" + ddlDeptTypeGroups.SelectedValue + "'";

                    if (!string.IsNullOrEmpty(txtTypesNo.Text.Trim()))
                        cri = cri + " AND  CONTAINS(A.No,'\"" + txtTypesNo.Text + "\"')";

                    //if (ddlYear.SelectedValue != "0")
                    //{
                    //    cri = cri + " AND A.Year='" + ddlYear.SelectedValue + "'";
                    //}

                    cri = cri + " AND B.ParentID=16";


                    DataTable dt = new DataTable();
                    dt = objdpt.DepartmentSearch(cri);
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            Session["DeptSearch"] = dt;
                            Session["DeptSearchKeyWord"] = ddlDeptTypeGroups.SelectedItem.Text + " " + txtTypesNo.Text;
                            Response.Redirect("/m/Departments/Search-Result");
                        }
                    }

                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}