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
    public partial class Dictionary : System.Web.UI.Page
    {

        EastLawBL.Dictionary objdic = new EastLawBL.Dictionary();
        EastLawBL.Users objusr = new EastLawBL.Users();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                InsertAuditLog("Hit", "Dictionary", "");
                GetActiveDic("");
            }
        }
        void GetActiveDic(string Search)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objdic.GetActiveDictionaryWord();
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
                GetActiveDic("");
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
             GetActiveDic(txtSearch.Text);

        }

        public void ScrapFromWeb()
        {
            try
            {
                int chk = 0;
                var webGet = new HtmlWeb();

                for (int i = 0; i < 26; i++)
                {
                    //HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
                    //document.Load("Income tax legal definition of income tax.htm");
                    HtmlAgilityPack.HtmlDocument document = webGet.Load("http://legal-dictionary.thefreedictionary.com/" + txtSearch.Text.Trim().Replace(" ", "+"));
                    //HtmlAgilityPack.HtmlDocument document = webGet.Load(Server.MapPath("PakistanCode.htm"));


                    int count = 1;
                    //foreach (HtmlAgilityPack.HtmlNode div in document.DocumentNode.SelectNodes("//div[@class='artlist'] | //div[@class='artdets']"))
                    foreach (HtmlAgilityPack.HtmlNode div in document.DocumentNode.SelectNodes("//section[@data-src='weal']"))
                    {
                        divResult.InnerHtml = div.InnerHtml;
                        lblWord.Text = txtSearch.Text;
                        if (!string.IsNullOrEmpty(div.InnerHtml))
                        {
                            objdic.Word = txtSearch.Text.Trim();
                            objdic.Meaning = div.InnerHtml;
                            objdic.Active = 1;
                            objdic.InsertDictionaryWord();
                        }

                        var doc = new HtmlDocument();
                        string tableTag = div.InnerHtml.ToString();
                        doc.LoadHtml(tableTag);
                        var IndexNo = doc.DocumentNode.SelectSingleNode("//p");

                        // if (string.IsNullOrEmpty(IndexNo.ToString()))
                        // lblResult.Text = IndexNo.InnerText;
                        //lblWord.Text = txtSearch.Text;
                        //else
                        //    lblResult.Text = "Not Found";
                    }
                }
            }
            catch { }


        }
    }
}