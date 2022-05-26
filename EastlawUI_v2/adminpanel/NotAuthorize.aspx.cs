using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EastlawUI_v2.adminpanel
{
    public partial class NotAuthorize : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)

                InsertBackEndAuditLog("Not Authorize Access", "Security - " + Session["UserName"].ToString(), "");
        }
        void InsertBackEndAuditLog(string ActType, string Action, string txt)
        {
            try
            {
                EastLawBL.Users objusr = new EastLawBL.Users();
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
                if (Session["UserID"] != null)
                    chk = objusr.InsertBackendAuditLog(ActType, Action, Request.Url.AbsoluteUri.ToString(), visitorIPAddress, int.Parse(Session["UserID"].ToString()), location.CountryName, location.RegionName, location.CityName, txt);
                else
                    chk = objusr.InsertBackendAuditLog(ActType, Action, Request.Url.AbsoluteUri.ToString(), visitorIPAddress, 0, location.CountryName, location.RegionName, location.CityName, txt);
            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("home/Default.aspx", "InsertAuditLog", e.Message);
            }
        }
    }
}