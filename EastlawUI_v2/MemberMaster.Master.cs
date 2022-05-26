using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Configuration;

namespace EastlawUI_v2
{
    public partial class MemberMaster : System.Web.UI.MasterPage
    {
        EastLawBL.Users objUsr = new EastLawBL.Users();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                
                ValidateRegion();
                if (!Page.IsPostBack)
                {
                    if (Session["MemberID"] == null)
                    {
                        Response.Redirect("/member/member-login?req=" + HttpContext.Current.Request.Url.AbsolutePath);
                    }
                    else
                    {
                        if (Session["UserLogged"] == null)
                        {
                            Response.Redirect("/member/member-login?req=" + HttpContext.Current.Request.Url.AbsolutePath);
                        }
                        else
                        {
                            if (HttpContext.Current.Application[Session["UserLogged"].ToString()] != null)
                            {
                                if (!HttpContext.Current.Application[Session["UserLogged"].ToString()].Equals(HttpContext.Current.Session.SessionID))
                                {
                                    if (Session["MemberPlanID"] != null && Session["MemberPlanID"].ToString() != "21")
                                    {
                                        logOut();
                                    }
                                }
                            }
                        }
                    }
                    CheckUserLogin();
                   

                }
            }
            catch (Exception ex)
            {
                
            }

        }
        void logOut()
        {

            if (Session["UserLogged"] != null)
            {
                HttpContext.Current.Application.Remove(Session["UserLogged"].ToString());
                //Session["UserLogged"] = null;
                Session.RemoveAll();
                Response.Redirect("/member/member-login?req=" + HttpContext.Current.Request.Url.AbsolutePath, false);
            }
            // Session.Abandon();
        }
        void CheckUserLogin()
        {
            try
            {
                if (Session["MemberID"] != null)
                {
                   
                    lblUserName.Text = Session["MemberName"].ToString();
                    //spanLoginName.Style["Display"] = "";
                }
                else
                {
                    lblUserName.Text = "";
                    //spanLoginName.Style["Display"] = "none";

                }
            }
            catch (Exception ex)
            { }
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
                EastlawUI_v2.CommonClass.GetIPLocation(visitorIPAddress, ref Country, ref Region, ref City);
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
                        Response.Redirect("/restricted/restricted-region");
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
        void InsertAuditLog(string ActType, string Action, string txt)
        {
            try
            {
                string Country = "";
                string Region = "";
                string City = "";
                string visitorIPAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                string visitorIPAddress1 = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (String.IsNullOrEmpty(visitorIPAddress))
                    visitorIPAddress = Request.ServerVariables["REMOTE_ADDR"];
                if (string.IsNullOrEmpty(visitorIPAddress))
                    visitorIPAddress = Request.UserHostAddress;

                EastlawUI_v2.CommonClass.GetIPLocation(visitorIPAddress, ref Country, ref Region, ref City);
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
                    chk = objUsr.InsertAuditLog(ActType, Action, Request.Url.AbsoluteUri.ToString(), visitorIPAddress, int.Parse(Session["MemberID"].ToString()), Country, Region, City, txt, BrowserName, SourcePlatform, "Desktop Website");
                else
                    chk = objUsr.InsertAuditLog(ActType, Action, Request.Url.AbsoluteUri.ToString(), visitorIPAddress, 0, Country, Region, City, txt, BrowserName, SourcePlatform, "Desktop Website");
            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("home/Default.aspx", "InsertAuditLog", e.Message);
            }
        }
    }
}