using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.IO;
using System.Data;
namespace EastlawUI_v2
{
    public partial class StatutesIndexDetails_Public : System.Web.UI.Page
    {
        EastLawBL.Users objusr = new EastLawBL.Users();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Validate();
                    InsertAuditLog("Statutes", HttpContext.Current.Items["statutesid"].ToString(), "Public View Statutes");
               
            }

        }
        void Validate()
        {
            try
            {
                EastLawBL.Statutes objstate = new EastLawBL.Statutes();
                DataTable dtStatutes = new DataTable();
                dtStatutes = objstate.GetStatutes(int.Parse((HttpContext.Current.Items["statutesid"].ToString())));
                if (dtStatutes != null && dtStatutes.Rows.Count > 0)
                {
                    if (dtStatutes.Rows[0]["PublicDisplay"].ToString() != "1")
                        Response.Redirect("/");
                }
            }
            catch(Exception ex) { }
        }
        public string FormatContent(string InputTxt)
        {
            string content = InputTxt;

            content = content.Replace("Go to Index Page", "").Replace("eastlawdic.aspx", "/eastlawdic.aspx").ToString();
            content = Regex.Replace(content, "</?(a|A).*?>", string.Empty);
            //content = Regex.Replace(content, "</?(img|IMG).*?>", string.Empty);
            content = Regex.Replace(content, "align=\\s*\".*\";?", string.Empty);
            // htmkWithoutFont = Regex.Replace(htmlWithFont, "font-family:\\s*\".*\";?", string.Empty);

            //  value = Regex.Replace(value, "(<align.+?</style>)|(<script.+?</script>)", "", RegexOptions.IgnoreCase | RegexOptions.Singleline);



            return content;


            //   return ("<span class=highlight>" + Search_Str + "</span>");
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
                //Location location = new Location();
                EastlawUI_v2.CommonClass.GetIPLocation(visitorIPAddress, ref Country, ref Region, ref City);
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
                    chk = objusr.InsertAuditLog(ActType, Action, Request.Url.AbsoluteUri.ToString(), visitorIPAddress, int.Parse(Session["MemberID"].ToString()), Country, Region, City, txt, BrowserName, SourcePlatform, "Desktop Website");
                else
                    chk = objusr.InsertAuditLog(ActType, Action, Request.Url.AbsoluteUri.ToString(), visitorIPAddress, 0, Country, Region, City, txt, BrowserName, SourcePlatform, "Desktop Website");
            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("home/Default.aspx", "InsertAuditLog", e.Message);
            }
        }
        protected void lnkPDF_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(HttpContext.Current.Items["statutespdffilename"].ToString()))
                {
                    InsertAuditLog("Statutes", HttpContext.Current.Items["statutesid"].ToString(), "Download PDF File");
                    DownloadFile("/store/statutesdocs/pdf/", HttpContext.Current.Items["statutespdffilename"].ToString());
                    //Response.Redirect("/store/statutesdocs/pdf/" + HttpContext.Current.Items["statutespdffilename"].ToString());
                }
            }
            catch { }
        }
        protected void lnkWord_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(HttpContext.Current.Items["statuteswordfilename"].ToString()))
                {
                    InsertAuditLog("Statutes", HttpContext.Current.Items["statutesid"].ToString(), "Download Word File");
                    DownloadFile("/store/statutesdocs/word/", HttpContext.Current.Items["statuteswordfilename"].ToString());
                    //  Response.Redirect("/store/statutesdocs/word/" + HttpContext.Current.Items["statuteswordfilename"].ToString());
                }
            }
            catch { }
        }
        void DownloadFile(string FilePath, string FileName)
        {
            string allowedExtensions = ".pdf,.doc,.docx";
            // edit this list to allow file types - do not allow sensitive file types like .cs or .config

            string fileName = FileName;
            string filePath = FilePath;

            //if (Request.QueryString["file"] != null) fileName = Request.QueryString["file"].ToString();
            //if (Request.QueryString["path"] != null) filePath = Request.QueryString["path"].ToString();

            if (fileName != "" && fileName.IndexOf(".") > 0)
            {
                bool extensionAllowed = false;
                // get file extension
                string fileExtension = fileName.Substring(fileName.LastIndexOf('.'), fileName.Length - fileName.LastIndexOf('.'));

                // check that we are allowed to download this file extension
                string[] extensions = allowedExtensions.Split(',');
                for (int a = 0; a < extensions.Length; a++)
                {
                    if (extensions[a] == fileExtension)
                    {
                        extensionAllowed = true;
                        break;
                    }
                }

                if (extensionAllowed)
                {
                    // check to see that the file exists 
                    if (File.Exists(Server.MapPath(filePath + '/' + fileName)))
                    {

                        // for iphones and ipads, this script can cause problems - especially when trying to view videos, so we will redirect to file if on iphone/ipad
                        // if (Request.UserAgent.ToLower().Contains("iphone") || Request.UserAgent.ToLower().Contains("ipad")) { Response.Redirect(filePath + '/' + fileName); }
                        Response.Clear();
                        Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
                        Response.WriteFile(Server.MapPath(filePath + '/' + fileName));
                        Response.End();
                    }
                    else
                    {

                    }
                }
                else
                {

                }
            }
            else
            {

            }
        }
    }
}