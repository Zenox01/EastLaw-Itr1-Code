using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using HtmlAgilityPack;


namespace EastlawUI_v2
{
    public partial class LegalMaxim : System.Web.UI.Page
    {
        EastLawBL.LegalMaxim objleg = new EastLawBL.LegalMaxim();
        EastLawBL.Users objusr = new EastLawBL.Users();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                InsertAuditLog("Hit", "LegalMaxim", "");
                GetActiveLegamMaxim("");
            }
        }
        void GetActiveLegamMaxim(string Search)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objleg.GetActiveLegalMaximWord();
                if (Request.QueryString["alp"] != null)
                {
                    if (Request.QueryString["alp"].ToString() != "all")
                    {
                        DataRow[] rows = dt.Select("Word like  '" + Request.QueryString["alp"].ToString() + "%'");
                        DataTable dtfilter = dt.Clone();
                        foreach (DataRow drow in rows)
                        {
                            dtfilter.ImportRow(drow);
                        }
                        dtfilter.AcceptChanges();
                        dt = dtfilter;
                    }
                }
                else if (!string.IsNullOrEmpty(Search))
                {

                    DataRow[] rows = dt.Select("Word like  '" + Search + "%'");
                    DataTable dtfilter = dt.Clone();
                    foreach (DataRow drow in rows)
                    {
                        dtfilter.ImportRow(drow);
                    }
                    dtfilter.AcceptChanges();
                    dt = dtfilter;

                }
                gvLst.DataSource = dt;
                gvLst.DataBind();
            }
            catch (Exception ex)
            { }
        }
        void InsertAuditLog(string ActType, string Action, string txt)
        {
            try
            {
                string visitorIPAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (String.IsNullOrEmpty(visitorIPAddress))
                    visitorIPAddress = Request.ServerVariables["REMOTE_ADDR"];
                if (string.IsNullOrEmpty(visitorIPAddress))
                    visitorIPAddress = Request.UserHostAddress;

                int chk = 0;
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
                    chk = objusr.InsertAuditLog(ActType, Action, Request.Url.AbsoluteUri.ToString(), visitorIPAddress, int.Parse(Session["MemberID"].ToString()), location.CountryName, location.RegionName, location.CityName, txt, BrowserName, SourcePlatform, "Desktop Website");
                else
                    chk = objusr.InsertAuditLog(ActType, Action, Request.Url.AbsoluteUri.ToString(), visitorIPAddress, 0, location.CountryName, location.RegionName, location.CityName, txt, BrowserName, SourcePlatform, "Desktop Website");
            }
            catch (Exception e)
            {

            }
        }
        protected void gvLst_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvLst.PageIndex = e.NewPageIndex;
                GetActiveLegamMaxim("");
            }
            catch (Exception ex)
            {

            }
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            //  GetActiveDic(txtSearch.Text);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //ScrapFromWeb();
            GetActiveLegamMaxim(txtSearch.Text);

        }

       
    }
}