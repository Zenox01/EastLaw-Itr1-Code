using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;

namespace EastlawUI_v2
{
    public partial class CaseSearchBySection : System.Web.UI.Page
    {
        EastLawBL.Statutes objstat = new EastLawBL.Statutes();
        EastLawBL.Users objuser = new EastLawBL.Users();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                InsertAuditLog("Search", "Case Search By Section", "");
            }

        }
        void GetStatutesListByType(string Type)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objstat.GetStatutesListBySection(Type);
                ddlStatutes.DataValueField = "StatutesID";
                ddlStatutes.DataTextField = "Title";
                ddlStatutes.DataSource = dt;
                ddlStatutes.DataBind();
                ddlStatutes.Items.Insert(0, new ListItem("Select Statutes", "0"));
            }
            catch { }
        }
        void GetSectionsListByStatutesNew(int StatueID)
        {
            try
            {
                DataTable dt = new DataTable();
                string data = "";
                dt = objstat.GetStatutesSOAIndex(0,StatueID);

                ddlSections.DataValueField = "ID";
                ddlSections.DataTextField = "ElementData";
                ddlSections.DataSource = dt;
                ddlSections.DataBind();
                ddlSections.Items.Insert(0, new ListItem("Select", "0"));

            }
            catch { }
        }
        void GetSectionsListByStatutes(int StatueID)
        {
            try
            {
                DataTable dt = new DataTable();
                string data = "";
                dt = objstat.GetTaggedSectionsByStatutes(StatueID);
                for (int a = 0; a < dt.Rows.Count; a++)
                {
                    data = data + dt.Rows[a]["AttriLink"].ToString()+",";

                }
                String[] lst = data.Split(',');
                var sList = new ArrayList();

                for (int i = 0; i < lst.Length; i++)
                {
                    if (lst[i].Contains("Order") || lst[i].Contains("Rule") || lst[i].Contains("Section") || lst[i].Contains("Article"))
                    {
                        if (!string.IsNullOrEmpty(lst[i]))
                        {
                            if (sList.Contains(lst[i].Trim()) == false)
                            {
                                sList.Add(lst[i].Trim());
                            }
                        }
                    }
                }
                sList.Sort();

               
                //    lstSectionsList.DataValueField = "ID";
                //lstSectionsList.DataTextField = "AttriLink";
                lstSectionsList.DataSource = sList;
                lstSectionsList.DataBind();



                ddlSections.DataSource = sList;
                ddlSections.DataBind();
              
            }
            catch { }
        }

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetStatutesListByType(ddlType.SelectedValue);
            }
            catch { }
        }

        protected void ddlStatutes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetSectionsListByStatutesNew(int.Parse(ddlStatutes.SelectedValue));
            }
            catch { }
        }
        void CitationsSearchNew()
        {
            try
            {
               

                
                DataTable dt = new DataTable();
                InsertAuditLog("Search", "Case Search By Section", "Type: " + ddlType.SelectedItem.Text + "Statute: " + ddlStatutes.SelectedItem.Text + " Section: " + ddlSections.SelectedItem.Text);
                dt = objstat.GetCasesSearchByStatueAndSectionNew(int.Parse(ddlStatutes.SelectedValue),int.Parse(ddlSections.SelectedValue));
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        dt.Columns.Add("Link");
                        dt.Columns.Add("Title");
                        for (int a = 0; a < dt.Rows.Count; a++)
                        {

                            dt.Rows[a]["Title"] = EastlawUI_v2.CommonClass.MakeFirstCap(dt.Rows[a]["Appeallant"].ToString()) + " VS " + EastlawUI_v2.CommonClass.MakeFirstCap(dt.Rows[a]["Respondent"].ToString());
                            dt.Rows[a]["Link"] = "/cases/" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dt.Rows[a]["Appeallant"].ToString(), 3).ToString())).Replace(" ", "-") + "VS" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dt.Rows[a]["Respondent"].ToString(), 3).ToString())).Replace(" ", "-") + "." + EncryptDecryptHelper.Encrypt(dt.Rows[a]["ID"].ToString());
                            //dt.Rows[a]["ShortDes"] = EastLawUI.CommonClass.GetWords(EastLawUI.CommonClass.GetShortDesc(dt.Rows[a]["Judgment"].ToString(), txtKeyword.Text).Replace("</p>", "").Replace("<p>", "").Replace("<br>", "").Replace("<b>", "").Replace("</b>", ""), 200);

                        }
                        dt.AcceptChanges();

                        Session["CasesSectionSearch"] = dt;
                        string[] strSearch = new string[5];
                        strSearch[1] = "Cases";
                        Response.Redirect("/cases/search-by-section-result");
                    }
                    else
                    {
                        //strMessage = "Recrods not found.";
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup('" + strMessage + "');", true);

                        lblMsg.Text = "Recrods not found";
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                        lblMsg.Visible = true;
                    }
                }
                else
                {

                    //strMessage = "Recrods not found.";
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup('" + strMessage + "');", true);

                    lblMsg.Text = "Recrods not found";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    lblMsg.Visible = true;
                }
                //  }

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Visible = true;
            }
        }
        void CitationsSearch()
        {
            try
            {
                string selectedsection = "";
                for (int a = 0; a < lstSectionsList.Items.Count; a++)
                {
                    if (lstSectionsList.Items[a].Selected == true)
                    {
                        selectedsection = lstSectionsList.Items[a].Text;
                    }
                }
                if (!string.IsNullOrEmpty(selectedsection))
                {
                    selectedsection = "\"" + selectedsection + "\"";
                    //selectedsection = selectedsection.Replace(" "," AND ");
                }

                selectedsection = "\"" + ddlSections.SelectedItem.Text + "\"";
                DataTable dt = new DataTable();
                InsertAuditLog("Search", "Case Search By Section", "Type: " + ddlType.SelectedItem.Text + "Statute: " + ddlStatutes.SelectedItem.Text + " Section: " + selectedsection);
                dt = objstat.GetCasesSearchByStatueAndSection(int.Parse(ddlStatutes.SelectedValue), selectedsection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        dt.Columns.Add("Link");
                        dt.Columns.Add("Title");
                        for (int a = 0; a < dt.Rows.Count; a++)
                        {

                            dt.Rows[a]["Title"] = EastlawUI_v2.CommonClass.MakeFirstCap(dt.Rows[a]["Appeallant"].ToString()) + " VS " + EastlawUI_v2.CommonClass.MakeFirstCap(dt.Rows[a]["Respondent"].ToString());
                            dt.Rows[a]["Link"] = "/cases/" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dt.Rows[a]["Appeallant"].ToString(), 3).ToString())).Replace(" ", "-") + "VS" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dt.Rows[a]["Respondent"].ToString(), 3).ToString())).Replace(" ", "-") + "." + EncryptDecryptHelper.Encrypt(dt.Rows[a]["ID"].ToString());
                            //dt.Rows[a]["ShortDes"] = EastLawUI.CommonClass.GetWords(EastLawUI.CommonClass.GetShortDesc(dt.Rows[a]["Judgment"].ToString(), txtKeyword.Text).Replace("</p>", "").Replace("<p>", "").Replace("<br>", "").Replace("<b>", "").Replace("</b>", ""), 200);

                        }
                        dt.AcceptChanges();

                        Session["CasesSectionSearch"] = dt;
                        string[] strSearch = new string[5];
                        strSearch[1] = "Cases";
                        Response.Redirect("/cases/search-by-section-result");
                    }
                    else
                    {
                        //strMessage = "Recrods not found.";
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup('" + strMessage + "');", true);

                        lblMsg.Text = "Recrods not found";
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                        lblMsg.Visible = true;
                    }
                }
                else
                {

                    //strMessage = "Recrods not found.";
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup('" + strMessage + "');", true);

                    lblMsg.Text = "Recrods not found";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    lblMsg.Visible = true;
                }
                //  }

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Visible = true;
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            CitationsSearchNew();
            
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
                EastlawUI_v2.CommonClass.GetIPLocation(visitorIPAddress, ref Country, ref Region, ref City);
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