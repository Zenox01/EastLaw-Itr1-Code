using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace EastlawUI_v2
{
    public partial class News : System.Web.UI.Page
    {
        EastLawBL.News objn = new EastLawBL.News();
        EastLawBL.Users objuser = new EastLawBL.Users();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
               // GetNews();
                //if (Session["MemberID"] == null)
                //{
                //    Response.Redirect("/Member/Member-Login");
                //}

                Session["Sorting"] = null;
                InsertAuditLog("Hit", "News", "");
                CheckUserLogin();
            }
        }
        void CheckUserLogin()
        {
            try
            {
                if (Session["MemberID"] != null)
                {
                    loginWelcome.Style["Display"] = "";
                    loginContainer.Style["Display"] = "none";
                    lblUserName.Text = Session["MemberName"].ToString();
                    navWithoutLogin.Style["Display"] = "none";
                    navWithLogin.Style["Display"] = "";
                }
                else
                {
                    loginWelcome.Style["Display"] = "none";
                    loginContainer.Style["Display"] = "";

                    navWithoutLogin.Style["Display"] = "";
                    navWithLogin.Style["Display"] = "none";
                    //spanLoginName.Style["Display"] = "none";

                }
            }
            catch (Exception ex)
            { }
        }
        //void GetNews()
        //{
        //    try
        //    {
        //        DataTable dt = new DataTable();
        //        dt = objn.GetActiveNews();
        //        Session["News"] = dt;

        //        #region Cat Group
        //        var query = from row in dt.AsEnumerable()
        //                    group row by row.Field<string>("PracticeAreaSubCatName") into Cat
        //                    orderby Cat.Key
        //                    select new
        //                    {
        //                        Name = Cat.Key.ToString() + "(" + Cat.Count() + ")",
        //                        count = Cat.Count(),
        //                        valfiel = Cat.Key.ToString()
        //                    };
        //        chkCat.DataSource = query;
        //        chkCat.DataValueField = "valfiel";
        //        chkCat.DataTextField = "Name";
        //        chkCat.DataBind();
        //        #endregion
        //        lblCount.Text = dt.Rows.Count.ToString();
        //        dt.AcceptChanges();

        //        dt.Columns.Add("Nimg");
        //        for (int a = 0; a < dt.Rows.Count; a++)
        //        {
        //            if (!string.IsNullOrEmpty(dt.Rows[a]["imgfile"].ToString()))
        //            {
        //                dt.Rows[a]["Nimg"] = dt.Rows[a]["imgfile"].ToString();
        //            }
        //            else
        //            {
        //                dt.Rows[a]["Nimg"] = "/images/no_image-128.png";
        //            }

        //        }
        //        dt.AcceptChanges();
        //        gvLst.DataSource = dt;
        //        gvLst.DataBind();


        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}


        protected void gvLst_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //try
            //{
            //    if (Session["Sorting"] != null)
            //    {
            //        if (Session["Sorting"].ToString() == "Yes")
            //        {
            //            gvLst.PageIndex = e.NewPageIndex;
            //            FilterResults();

            //        }
            //    }
            //    else
            //    {
            //        gvLst.PageIndex = e.NewPageIndex;
            //        GetNews();
            //    }
            //}
            //catch (Exception ex)
            //{

            //}
        }

        void FilterResults()
        {
            //try
            //{
            //    string cat = "";

            //    DataTable dtFilter = new DataTable();
            //    dtFilter = (DataTable)Session["News"];
            //    for (int a = 0; a < chkCat.Items.Count; a++)
            //    {
            //        if (chkCat.Items[a].Selected == true)
            //        {
            //            cat = cat + "'" + chkCat.Items[a].Value + "'" + ",";
            //        }
            //    }

            //    if (!string.IsNullOrEmpty(cat))
            //    {
            //        cat = cat.Remove(cat.Length - 1);
            //        //DataRow[] filter = dtFilter.Select("Court in ("+courts+")");
            //        dtFilter.DefaultView.RowFilter = "PracticeAreaSubCatName in (" + cat + ")";
            //        //for (int a = 0; a < dtFilter.Rows.Count; a++)
            //        //{
            //        //    if (!string.IsNullOrEmpty(dtFilter.Rows[a]["Date"].ToString()) && !string.IsNullOrEmpty(dtFilter.Rows[a]["Act"].ToString()))
            //        //        dtFilter.Rows[a]["FormatedDateAct"] = "<strong>"+ dtFilter.Rows[a]["Date"].ToString() + " | " + dtFilter.Rows[a]["Act"].ToString()+"</strong>";
            //        //}
            //        dtFilter.AcceptChanges();
            //        gvLst.DataSource = dtFilter;
            //        gvLst.DataBind();
            //        Session["Sorting"] = "Yes";
            //        return;
            //    }
            //    GetNews();

            //}
            //catch { }
        }
        protected void chkCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterResults();
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
                Location location = new Location();
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
                    chk = objuser.InsertAuditLog(ActType, Action, Request.Url.AbsoluteUri.ToString(), visitorIPAddress, int.Parse(Session["MemberID"].ToString()), Country, Region, City, txt, BrowserName, SourcePlatform, "Desktop Website");
                else
                    chk = objuser.InsertAuditLog(ActType, Action, Request.Url.AbsoluteUri.ToString(), visitorIPAddress, 0, Country, Region, City, txt, BrowserName, SourcePlatform, "Desktop Website");
            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("home/Default.aspx", "InsertAuditLog", e.Message);
            }
        }
    }
}