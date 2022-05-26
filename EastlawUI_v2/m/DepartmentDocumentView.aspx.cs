using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace EastlawUI_v2.m
{
    public partial class DepartmentDocumentView : System.Web.UI.Page
    {
        EastLawBL.Users objusr = new EastLawBL.Users();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["MemberID"] != null)
                {
                    if (HttpContext.Current.Items["dptWordFile"] != null)
                    {
                        InsertAuditLog("Departments", "Document View", "Document ID: " + HttpContext.Current.Items["dptdocid"].ToString() + " FileName: " + HttpContext.Current.Items["dptWordFile"].ToString());
                        //lblCurCrumb.Text = HttpContext.Current.Items["Title"].ToString();
                       // GetBreadCrum();
                    }

                  //  GetUserFolder(int.Parse(Session["MemberID"].ToString()));

                }
            }
        }
        //void GetBreadCrum()
        //{
        //    try
        //    {
        //        System.Data.DataTable dt = new System.Data.DataTable();
        //        EastLawBL.Departments objDept = new EastLawBL.Departments();
        //        dt = objDept.GetDepartmentFileDetailsByID(int.Parse(HttpContext.Current.Items["dptdocid"].ToString()));
        //        if (dt != null)
        //        {
        //            if (dt.Rows.Count > 0)
        //            {
        //                lblCurCrumb1.Text = dt.Rows[0]["ParentDept"].ToString();
        //                lblCurCrumb1Link.HRef = "/Departments/" + dt.Rows[0]["ParentDept"].ToString() + "." + EncryptDecryptHelper.Encrypt(dt.Rows[0]["ParentDeptID"].ToString());


        //                lblCurCrumb2.Text = dt.Rows[0]["ChildDept"].ToString();
        //                lblCurCrumb2Link.HRef = "/Departments/" + dt.Rows[0]["ChildDept"].ToString() + "." + EncryptDecryptHelper.Encrypt(dt.Rows[0]["ChildDeptID"].ToString());


        //                lblCurCrumb3.Text = dt.Rows[0]["DeptName"].ToString();
        //                lblCurCrumb3Link.HRef = "/Departments/" + dt.Rows[0]["DeptName"].ToString() + "." + EncryptDecryptHelper.Encrypt(dt.Rows[0]["DeptID"].ToString());


        //                lblCurCrumb4.Text = dt.Rows[0]["Title"].ToString();


        //                //lblCurCrumb.Text = dt.Rows[0]["ParentDept"].ToString() + " / " + dt.Rows[0]["ChildDept"].ToString() + " / " + dt.Rows[0]["DeptName"].ToString() + " / " + dt.Rows[0]["Title"].ToString();
        //            }
        //        }
        //    }
        //    catch { }
        //}
        public void InsertAuditLog(string ActType, string Action, string txt)
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
        //protected void btnSubmit_Click(object sender, EventArgs e)
        //{
        //    EastLawBL.ErrorReporting objer = new EastLawBL.ErrorReporting();
        //    objer.Type = "";
        //    objer.ItemType = "";
        //    objer.ItemID = 0;
        //    objer.UserID = 1;
        //    //objer.UserComment = txtComment.Text ;
        //    objer.WorkflowID = 1;
        //    int chk = objer.InsertReportError();
        //}
        //protected void btnFill_Click(object sender, EventArgs e)
        //{
        //    ModalPopupExtender1.Hide();
        //    //string aa = PopuptxtFirstName.Text;
        //    //string bb = PopuptxtLastName.Text;

        //    EastLawBL.ErrorReporting objer = new EastLawBL.ErrorReporting();
        //    objer.Type = "Element";
        //    objer.ItemType = "Department";
        //    objer.ItemID = int.Parse(HttpContext.Current.Items["dptdocid"].ToString());
        //    objer.UserID = int.Parse(Session["MemberID"].ToString());
        //    objer.UserComment = txtComment.Text;
        //    objer.WorkflowID = 1;
        //    int chk = objer.InsertReportError();
        //}

        //protected void btnClose_Click(object sender, EventArgs e)
        //{
        //    modelPopupUserComents.Hide();
        //}
        //protected void btnAddComent_Click(object sender, EventArgs e)
        //{
        //    modelPopupUserComents.Hide();
        //    //string aa = PopuptxtFirstName.Text;
        //    //string bb = PopuptxtLastName.Text;

        //    EastLawBL.Cases objcase = new EastLawBL.Cases();
        //    int chk = objcase.AddUserComment("Department", int.Parse(HttpContext.Current.Items["dptdocid"].ToString()), txtUserComment.Text.Trim(), "Pending", int.Parse(Session["MemberID"].ToString()));

        //}
        //protected void btnCommentClose_Click(object sender, EventArgs e)
        //{
        //    ModalPopupExtender1.Hide();
        //}
        //void GetUserFolder(int UserID)
        //{
        //    try
        //    {
        //        DataTable dt = new DataTable();
        //        dt = objusr.GetUserFolderByUser(UserID);

        //        ddlFolders.DataValueField = "ID";
        //        ddlFolders.DataTextField = "FolderName";
        //        ddlFolders.DataSource = dt;
        //        ddlFolders.DataBind();
        //        ddlFolders.Items.Insert(0, new ListItem("Select", "0"));

        //    }
        //    catch (Exception e)
        //    {
        //        ExceptionHandling.SendErrorReport("Statuteslist.aspx", "GetUserFolder", e.Message);
        //    }
        //}
        //protected void btnSaveInFolder_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (ddlFolders.SelectedIndex != 0)
        //        {
        //            objusr.UserID = int.Parse(Session["MemberID"].ToString());
        //            objusr.FolderID = int.Parse(ddlFolders.SelectedValue);
        //            objusr.ItemType = "Department";
        //            objusr.ItemID = int.Parse(HttpContext.Current.Items["dptdocid"].ToString());
        //            objusr.CreatedBy = int.Parse(Session["MemberID"].ToString());
        //            int chk = objusr.InsertUserFolderItem();
        //            if (chk > 0)
        //            {
        //                lblResult.Text = "Added.";
        //                ddlFolders.SelectedIndex = 0;
        //            }
        //        }
        //        else
        //        {
        //            lblResult.Text = "Please Select Folder.";
        //        }
        //    }
        //    catch (Exception ex) { }
        //}
      
    }
}