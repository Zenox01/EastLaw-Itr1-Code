using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace EastlawUI_v2.m
{
    public partial class Default : System.Web.UI.Page
    {
        EastLawBL.Users objUsr = new EastLawBL.Users();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ValidateRegion();
                if (Request.QueryString["bG9nb3V0"] != null)
                {
                    if (Request.QueryString["bG9nb3V0"] != null)
                    {
                        if (Request.QueryString["bG9nb3V0"].ToString() == "do")
                        {
                            if (Session["MemberID"] != null)
                                InsertAuditLog("Login/Logout", "Logout - " + " Member ID:" + Session["MemberID"].ToString(), "");

                            Logout();
                            //Session.RemoveAll();
                        }
                    }
                }
            }
        }
        void ValidateRegion()
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
                //string APIKey = "76511e33ff8498c62f458bea0a641b144b031bdb1e3eade661df53a39815cb27";
                //string url = string.Format("http://api.ipinfodb.com/v3/ip-city/?key={0}&ip={1}&format=json", APIKey, visitorIPAddress);
                //Location location = new Location();
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
                if (!String.IsNullOrEmpty(Country))
                {
                    //if (Country == "United States" || Country == "Germany" || Country == "Netherlands" || Country == "Sweden" || Country == "Italy" || Country == "Switzerland" || Country == "Canada" || Country == "Turkey")
                    if (Country != "Pakistan" & Country != "United Arab Emirates" & Country != "Canada")
                    {
                        InsertAuditLog("Restricted Region", "Restricted Region Request", "Requested Country:" + Country);
                        Response.Redirect("/Restricted/Restricted-Region");
                    }
                }
                //else
                //{
                //    InsertAuditLog("Restricted Region", "Restricted Region Request", "Requested Country:" + location.CountryName);
                //    Response.Redirect("/Restricted/Restricted-Region");
                //}
            }
            catch { }
        }
        protected void Logout()
        {
            //Session.Remove("MemberID");
            // Session.Abandon();
            Session.RemoveAll();
        }

        protected void btnSingIn_Click(object sender, EventArgs e)
        {
            Response.Redirect("/m/Member/Member-Login");
        }

        protected void btnNewReg_Click(object sender, EventArgs e)
        {
            Response.Redirect("/m/Member/Member-Register");
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

                CommonClass.GetIPLocation(visitorIPAddress, ref Country, ref Region, ref City);
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
                    chk = objUsr.InsertAuditLog(ActType, Action, Request.Url.AbsoluteUri.ToString(), visitorIPAddress, int.Parse(Session["MemberID"].ToString()), Country, Region, City, txt, BrowserName, SourcePlatform, "Mobile Website");
                else
                    chk = objUsr.InsertAuditLog(ActType, Action, Request.Url.AbsoluteUri.ToString(), visitorIPAddress, 0, Country, Region, City, txt, BrowserName, SourcePlatform, "Mobile Website");
            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("home/Default.aspx", "InsertAuditLog", e.Message);
            }
        }
    }
}