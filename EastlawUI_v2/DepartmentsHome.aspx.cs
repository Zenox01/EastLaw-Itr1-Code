using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace EastlawUI_v2
{
    public partial class DepartmentsHome : System.Web.UI.Page
    {
        EastLawBL.Departments objdpt = new EastLawBL.Departments();
        EastLawBL.Users objusr = new EastLawBL.Users();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                InsertAuditLog("Departments", "Departments Home", "");
                if (Session["MemberID"] != null)
                {
                    GetDeptTypeGroup();
                    LoadYears();
                    GetLatestAndBind();

                }

                if (Request.QueryString["dptn"] != null)
                {
                    lblDepTitle.Text = CommonClass.MakeFirstCap(Request.QueryString["dptn"].ToString().Replace("-", " "));
                }
                else
                    lblDepTitle.Text = "Departments";

            }
        }
        void GetLatestAndBind()
        {
            try
            {
                DataTable dtlatest = objdpt.GetDepartmentFilesLatest();
                dtlatest.Columns.Add("Lnk");
                for (int a = 0; a < dtlatest.Rows.Count; a++)
                {
                    dtlatest.Rows[a]["DType"] = EastlawUI_v2.CommonClass.MakeFirstCap(dtlatest.Rows[a]["DType"].ToString());
                    dtlatest.Rows[a]["Title"] = EastlawUI_v2.CommonClass.MakeFirstCap(dtlatest.Rows[a]["Title"].ToString());
                    dtlatest.Rows[a]["Lnk"] = "/departments/" + clsUtilities.RemoveSpecialCharacter(dtlatest.Rows[a]["DeptName"].ToString()) + "/" + clsUtilities.RemoveSpecialCharacter(dtlatest.Rows[a]["WordFile"].ToString()) + "." + EncryptDecryptHelper.Encrypt(dtlatest.Rows[a]["ID"].ToString());
                }
                dtlatest.AcceptChanges();
                //RadRotator1.DataSource = dtlatest;
                //RadRotator1.DataBind();
            }
            catch { }
        }
        void LoadYears()
        {
            // Clear items:    
            ddlYear.Items.Clear();
            // Add default item to the list
            ddlYear.Items.Insert(0, new ListItem("Year", "0"));
            // Start loop
            for (int i = 0; i < 69; i++)
            {
                // For each pass add an item
                // Add a number of years (negative, which will subtract) to current year
                ddlYear.Items.Add(DateTime.Now.AddYears(-i).Year.ToString());
            }
        }
        void GetDeptTypeGroup()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objdpt.GetDepartmentFilesTypesGroup();
                ddlDeptTypeGroups.DataValueField = "DType";
                ddlDeptTypeGroups.DataTextField = "DType";
                ddlDeptTypeGroups.DataSource = dt;
                ddlDeptTypeGroups.DataBind();
                ddlDeptTypeGroups.Items.Insert(0, new ListItem("Select", "0"));
            }
            catch { }
        }
        void DepartmentSearch()
        {
            try
            {
                string cri = "Where A.IsDeleted=0";


                if (!string.IsNullOrEmpty(txtSearch.Text.Trim()))
                {
                    cri = cri + " AND  (CONTAINS(A.Title,'\"" + txtSearch.Text + "\"')";
                    cri = cri + " OR  CONTAINS(A.Year,'\"" + txtSearch.Text + "\"')";
                    cri = cri + " OR  CONTAINS(A.No,'\"" + txtSearch.Text + "\"')";
                    cri = cri + " OR  CONTAINS(A.DDate,'\"" + txtSearch.Text + "\"')";
                    cri = cri + " OR  CONTAINS(A.DType,'\"" + txtSearch.Text + "\"'))";


                    DataTable dt = new DataTable();
                    dt = objdpt.DepartmentSearch(cri);
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            lblMsg.Text = "";
                            lblMsg.ForeColor = System.Drawing.Color.Red;
                            lblMsg.Visible = false;

                            Session["DeptSearchKeyWord"] = txtSearch.Text;
                            Session["DeptSearch"] = dt;
                            Response.Redirect("/departments/search-result");
                        }
                        else
                        {
                            lblMsg.Text = "No Records Found !";
                            lblMsg.ForeColor = System.Drawing.Color.Red;
                            lblMsg.Visible = true;
                        }
                    }

                }





            }
            catch (Exception ex)
            {

            }
        }
        void DepartmentFreeTextSearch()
        {
            try
            {
                string cri = "Where A.IsDeleted=0";


                if (!string.IsNullOrEmpty(txtFreeTextSearch.Text.Trim()))
                {
                    cri = cri + " AND  (CONTAINS(A.FileContent,'\"" + txtFreeTextSearch.Text + "\"'))";

                    Session["DeptSearchKeyWord"] = txtFreeTextSearch.Text;
                   // Response.Redirect("/departments/search-result?lng=" + EncryptDecryptHelper.Encrypt(txtFreeTextSearch.Text).ToString());

                    DataTable dt = new DataTable();
                    dt = objdpt.DepartmentSearch(cri);
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {

                            lblMsg.Text = "";
                            lblMsg.ForeColor = System.Drawing.Color.Red;
                            lblMsg.Visible = false;


                            Session["DeptSearch"] = dt;
                            Session["DeptSearchKeyWord"] = txtFreeTextSearch.Text;
                            Response.Redirect("/departments/search-result?lng=" + EncryptDecryptHelper.Encrypt(txtFreeTextSearch.Text));
                        }
                        else
                        {
                            lblMsg.Text = "No Records Found !";
                            lblMsg.ForeColor = System.Drawing.Color.Red;
                            lblMsg.Visible = true;
                        }
                    }

                }





            }
            catch (Exception ex)
            {

            }
        }
        void DepartmentSearchByDate()
        {
            try
            {
                string cri = "Where A.IsDeleted=0";


                if (!string.IsNullOrEmpty(txtSearchByDate.Text.Trim()))
                {
                    cri = cri + " AND  A.DateFormated=1 and convert(varchar(20),convert(datetime, A.DDate, 103),103)='" + txtSearchByDate.Text + "'";

                    Session["DeptSearchKeyWord"] = txtSearchByDate.Text;
                    //  Response.Redirect("/Departments/Search-Result?lng=" + EncryptDecryptHelper.Encrypt(txtFreeTextSearch.Text).ToString());

                    DataTable dt = new DataTable();
                    dt = objdpt.DepartmentSearch(cri);
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {

                            lblMsg.Text = "";
                            lblMsg.ForeColor = System.Drawing.Color.Red;
                            lblMsg.Visible = false;


                            Session["DeptSearch"] = dt;
                            Session["DeptSearchKeyWord"] = txtSearchByDate.Text;
                            Response.Redirect("/departments/search-result?lng=" + EncryptDecryptHelper.Encrypt(txtSearchByDate.Text));
                        }
                        else
                        {
                            lblMsg.Text = "No Records Found !";
                            lblMsg.ForeColor = System.Drawing.Color.Red;
                            lblMsg.Visible = true;
                        }
                    }

                }





            }
            catch (Exception ex)
            {

            }
        }
        protected void btnFreeTextSearch_Click(object sender, EventArgs e)
        {
            DepartmentSearch();
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

                    if (ddlYear.SelectedValue != "0")
                    {
                        cri = cri + " AND A.Year='" + ddlYear.SelectedValue + "'";
                    }

                    DataTable dt = new DataTable();
                    dt = objdpt.DepartmentSearch(cri);
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            Session["DeptSearch"] = dt;
                            Session["DeptSearchKeyWord"] = ddlDeptTypeGroups.SelectedItem.Text + " " + ddlYear.SelectedValue + " " + txtTypesNo.Text;
                            Response.Redirect("/departments/search-result");
                        }
                    }

                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            DepartmentSearch();
        }

        protected void txtFreeTextSearch_TextChanged(object sender, EventArgs e)
        {
            DepartmentFreeTextSearch();
        }

        protected void btnTextSearch_Click(object sender, EventArgs e)
        {
            DepartmentFreeTextSearch();
        }
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
                EastlawUI_v2.CommonClass.GetIPLocation(visitorIPAddress, ref Country, ref Region, ref City);
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
                    chk = objusr.InsertAuditLog(ActType, Action, Request.Url.AbsoluteUri.ToString(), visitorIPAddress, int.Parse(Session["MemberID"].ToString()), Country, Region, City, txt, BrowserName, SourcePlatform, "Desktop Website");
                else
                    chk = objusr.InsertAuditLog(ActType, Action, Request.Url.AbsoluteUri.ToString(), visitorIPAddress, 0, Country, Region, City, txt, BrowserName, SourcePlatform, "Desktop Website");
            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("home/Default.aspx", "InsertAuditLog", e.Message);
            }
        }

        protected void btnSearchByDate_Click(object sender, EventArgs e)
        {
            DepartmentSearchByDate();
        }
    }
}