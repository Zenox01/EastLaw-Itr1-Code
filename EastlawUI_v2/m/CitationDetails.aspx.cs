using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Web.Script.Services;
using System.Web.Services;
using System.Data;

namespace EastlawUI_v2.m
{
    public partial class CitationDetails : System.Web.UI.Page
    {
        EastLawBL.Users objusr = new EastLawBL.Users();
        EastLawBL.Dictionary objdic = new EastLawBL.Dictionary();
        EastLawBL.Cases objcase = new EastLawBL.Cases();
        EastLawBL.Users objUsr = new EastLawBL.Users();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {

                   
                    if (Session["MemberID"] != null)
                    {
                        CheckUserAccessValidation(int.Parse(Session["MemberID"].ToString()));
                        //GetUserFolder(int.Parse(Session["MemberID"].ToString()));
                        GetUsersParentFolders(int.Parse(Session["MemberID"].ToString()));
                        InsertAuditLog("Case", HttpContext.Current.Items["caseid"].ToString(), "");
                    }
                    else
                    {
                        Response.Redirect("/m/Member/Member-Login?req=" + HttpContext.Current.Request.Url.AbsolutePath);
                    }
                    ViewState["LastPage"] = Request.UrlReferrer.ToString();
                }
            }
            catch { }
        }
        void CheckUserAccessValidation(int UserID)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objUsr.UserElementViewReport((DateTime.Now.Date.ToString("MM/dd/yyy")), DateTime.Now.Date.ToString("MM/dd/yyy"), UserID);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        if (!String.IsNullOrEmpty(dt.Rows[0]["noofcasesview_perday"].ToString()))
                        {
                            if (dt.Rows[0]["noofcasesview_perday"].ToString() != "0")
                            {
                                if (int.Parse(dt.Rows[0]["NoofCaseView"].ToString()) >= int.Parse(dt.Rows[0]["noofcasesview_perday"].ToString()))
                                {
                                    InsertAuditLog("Limit Exceeded", "Case View", "User ID: " + UserID);
                                    Response.Redirect("/Restricted/Limit-Exceeded");
                                    return;

                                }
                            }
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("Home/Registration.aspx", "CheckUserAccessValidation", ex.Message);
            }
        }
        public string LinkedCases(string txt)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objcase.GetLinkedCases(int.Parse(HttpContext.Current.Items["caseid"].ToString()));
                string ftxt = txt;
                for (int a = 0; a < dt.Rows.Count; a++)
                {
                    if (!CommonClass.ExactMatch(txt, dt.Rows[a]["word"].ToString()))
                    {
                        dt.Rows[a].Delete();
                    }
                }
                dt.AcceptChanges();
                if (dt.Rows.Count > 0)
                {
                    dt.Columns.Add("link");
                    for (int a = 0; a < dt.Rows.Count; a++)
                    {
                        dt.Rows[a]["link"] = "<a href='/Search/Citation/" + dt.Rows[a]["word"].ToString().Replace(" ", "-") + "' target='_blank'>" + dt.Rows[a]["word"].ToString() + "</a>";
                    }
                    var myList = dt.AsEnumerable().ToDictionary<DataRow, string, string>(row => row[0].ToString(), row => row[2].ToString());

                    var regex = new Regex(String.Join("|", myList.Keys.Select(k => Regex.Escape(k))));


                    return regex.Replace(txt, m => myList[m.Value]);
                    //ftxt = replaced;
                    //for (int a = 0; a < dt.Rows.Count; a++)
                    //{
                    //    if (dt.Rows[a]["Word"].ToString().Length > 4)
                    //    {


                    //        Regex RegExp;
                    //        //string input = ftxt;
                    //        //string pattern = @""+dt.Rows[a]["Word"].ToString()+"";
                    //        //string replace = "<a href='/eastlawdic.aspx?key=" + dt.Rows[a]["Word"].ToString() + "' style='color:blue;text-decoration:underline' onclick='window.open(this.href, '', 'resizable=no,status=no,location=no,toolbar=no,menubar=no,fullscreen=no,scrollbars=yes,dependent=no,width=300,left=250,height=350,top=80'); return false;'><span style='color:#000000'>" + dt.Rows[a]["Word"].ToString() + "</span></a>";
                    //        //string result = Regex.Replace(input, pattern, replace);
                    //        //ftxt=result;
                    //        RegExp = new Regex(dt.Rows[a]["Word"].ToString(), RegexOptions.IgnoreCase);
                    //        //ftxt = ftxt.Replace(dt.Rows[a]["Word"].ToString(), "<a href='/eastlawdic.aspx?key=" + dt.Rows[a]["Word"].ToString() + "' style='color:blue;text-decoration:underline' onclick='window.open(this.href, '', 'resizable=no,status=no,location=no,toolbar=no,menubar=no,fullscreen=no,scrollbars=yes,dependent=no,width=300,left=250,height=350,top=80'); return false;'><span style='color:#000000'>" + dt.Rows[a]["Word"].ToString() + "</span></a>");
                    //        //ftxt = ftxt.Replace(dt.Rows[a]["Word"].ToString(), "<a href='/eastlawdic.aspx?key=" + dt.Rows[a]["Word"].ToString() + "'><span style='color:#000000'>" + dt.Rows[a]["Word"].ToString() + "</span></a>");
                    //        ftxt = ftxt.Replace(dt.Rows[a]["Word"].ToString(), dt.Rows[a]["CaseLink"].ToString());
                    //    }
                    // ftxt = RegExp.Replace(ftxt, new MatchEvaluator(HTMLDicLink));
                    // }
                    // return ftxt;


                }
                return ftxt;
            }


            catch (Exception ex)
            {
                return txt;
            }
        }

        public string LinkedStatutes(string txt)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objcase.GetLinkedStatutes(int.Parse(HttpContext.Current.Items["caseid"].ToString()));
                string ftxt = txt;


                if (dt.Rows.Count > 0)
                {
                    dt.Columns.Add("link");
                    for (int a = 0; a < dt.Rows.Count; a++)
                    {
                        dt.Rows[a]["link"] = "<a href='/Statutes/" + dt.Rows[a]["Title"].ToString().Replace(" ", "-") + "." + EncryptDecryptHelper.Encrypt(dt.Rows[a]["ID"].ToString()) + "' target='_blank'>" + dt.Rows[a]["Title"].ToString() + "</a>";
                    }
                    var myList = dt.AsEnumerable().ToDictionary<DataRow, string, string>(row => row[0].ToString(), row => row[2].ToString());

                    var regex = new Regex(String.Join("|", myList.Keys.Select(k => Regex.Escape(k))));
                    return regex.Replace(txt, m => myList[m.Value]);
                    //ftxt = replaced;
                    //for (int a = 0; a < dt.Rows.Count; a++)
                    //{
                    //    if (dt.Rows[a]["Word"].ToString().Length > 4)
                    //    {


                    //        Regex RegExp;
                    //        //string input = ftxt;
                    //        //string pattern = @""+dt.Rows[a]["Word"].ToString()+"";
                    //        //string replace = "<a href='/eastlawdic.aspx?key=" + dt.Rows[a]["Word"].ToString() + "' style='color:blue;text-decoration:underline' onclick='window.open(this.href, '', 'resizable=no,status=no,location=no,toolbar=no,menubar=no,fullscreen=no,scrollbars=yes,dependent=no,width=300,left=250,height=350,top=80'); return false;'><span style='color:#000000'>" + dt.Rows[a]["Word"].ToString() + "</span></a>";
                    //        //string result = Regex.Replace(input, pattern, replace);
                    //        //ftxt=result;
                    //        RegExp = new Regex(dt.Rows[a]["Word"].ToString(), RegexOptions.IgnoreCase);
                    //        //ftxt = ftxt.Replace(dt.Rows[a]["Word"].ToString(), "<a href='/eastlawdic.aspx?key=" + dt.Rows[a]["Word"].ToString() + "' style='color:blue;text-decoration:underline' onclick='window.open(this.href, '', 'resizable=no,status=no,location=no,toolbar=no,menubar=no,fullscreen=no,scrollbars=yes,dependent=no,width=300,left=250,height=350,top=80'); return false;'><span style='color:#000000'>" + dt.Rows[a]["Word"].ToString() + "</span></a>");
                    //        //ftxt = ftxt.Replace(dt.Rows[a]["Word"].ToString(), "<a href='/eastlawdic.aspx?key=" + dt.Rows[a]["Word"].ToString() + "'><span style='color:#000000'>" + dt.Rows[a]["Word"].ToString() + "</span></a>");
                    //        ftxt = ftxt.Replace(dt.Rows[a]["Word"].ToString(), dt.Rows[a]["CaseLink"].ToString());
                    //    }
                    // ftxt = RegExp.Replace(ftxt, new MatchEvaluator(HTMLDicLink));
                    // }
                    // return ftxt;
                }
                return ftxt;
            }


            catch (Exception ex)
            {
                return txt;
            }
        }
        //public  string DictionaryTag(string txt)
        //  {
        //      try
        //      {
        //          DataTable dt = new DataTable();
        //          dt = objdic.GetDictionaryMatchWordByCase(int.Parse(HttpContext.Current.Items["caseid"].ToString()));
        //          string ftxt = txt;

        //          if(dt.Rows.Count >0)
        //          {
        //              var myList = dt.AsEnumerable().ToDictionary<DataRow, string, string>(row => row[0].ToString(), row => row[1].ToString());

        //              var regex = new Regex(String.Join("|", myList.Keys.Select(k => Regex.Escape(k))));
        //              var replaced = regex.Replace(txt, m => myList[m.Value]);
        //              ftxt = replaced;
        //              //for(int a=0;a<dt.Rows.Count;a++)
        //              //{
        //              //    if (dt.Rows[a]["Word"].ToString().Length > 4)
        //              //    {


        //              //        ////Regex RegExp;
        //              //        ////string input = ftxt;
        //              //        ////string pattern = @""+dt.Rows[a]["Word"].ToString()+"";
        //              //        ////string replace = "<a href='/eastlawdic.aspx?key=" + dt.Rows[a]["Word"].ToString() + "' style='color:blue;text-decoration:underline' onclick='window.open(this.href, '', 'resizable=no,status=no,location=no,toolbar=no,menubar=no,fullscreen=no,scrollbars=yes,dependent=no,width=300,left=250,height=350,top=80'); return false;'><span style='color:#000000'>" + dt.Rows[a]["Word"].ToString() + "</span></a>";
        //              //        ////string result = Regex.Replace(input, pattern, replace);
        //              //        ////ftxt=result;
        //              //        //RegExp = new Regex(dt.Rows[a]["Word"].ToString(), RegexOptions.IgnoreCase);
        //              //        //ftxt = ftxt.Replace(dt.Rows[a]["Word"].ToString(), "<a href='/eastlawdic.aspx?key=" + dt.Rows[a]["Word"].ToString() + "' style='color:blue;text-decoration:underline' onclick='window.open(this.href, '', 'resizable=no,status=no,location=no,toolbar=no,menubar=no,fullscreen=no,scrollbars=yes,dependent=no,width=300,left=250,height=350,top=80'); return false;'><span style='color:#000000'>" + dt.Rows[a]["Word"].ToString() + "</span></a>");
        //              //        //ftxt = ftxt.Replace(dt.Rows[a]["Word"].ToString(), "<a href='/eastlawdic.aspx?key=" + dt.Rows[a]["Word"].ToString() + "'><span style='color:#000000'>" + dt.Rows[a]["Word"].ToString() + "</span></a>");
        //              //    }
        //              //   // ftxt = RegExp.Replace(ftxt, new MatchEvaluator(HTMLDicLink));
        //              //}
        //          }
        //          return ftxt;

        //      }
        //      catch(Exception ex) {
        //          return txt;
        //      }
        //  }

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
        public string HTMLDicLink(Match m)
        {
            //   return ("<span class=highlight>" + m.Value + "</span>");
            return ("<a href='/eastlawdic.aspx?key=" + m.Value + "' onclick='window.open(this.href, '', 'resizable=no,status=no,location=no,toolbar=no,menubar=no,fullscreen=no,scrollbars=yes,dependent=no,width=300,left=250,height=350,top=80'); return false;'><span style='color:blue'>" + m.Value + "</span></a>");
        }

        protected void btnShowPopup_Click(object sender, EventArgs e)
        {
            string message = "Message from server side";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public static void SaveReportError(string comment)
        {
            try
            {
                EastLawBL.ErrorReporting objer = new EastLawBL.ErrorReporting();
                objer.Type = "";
                objer.ItemType = "";
                objer.ItemID = 0;
                objer.UserID = 1;
                objer.UserComment = comment;
                objer.WorkflowID = 1;
                int chk = objer.InsertReportError();

            }
            catch (Exception ex)
            { }
        }
        public string HighlightText(string InputTxt)
        {
            if (Session["SearchMain"] != null)
            {
                string Search_Str = Session["SearchMain"].ToString();
                Session["SearchMain"] = Search_Str.ToString();
                Search_Str = CommonClass.RemoveExtraWordsForHiglight(Search_Str);

                Regex RegExp;//= new Regex();
                if (Search_Str.Contains("\""))
                    RegExp = new Regex(Search_Str.Replace("\"", "").Trim(), RegexOptions.IgnoreCase);
                //if (Search_Str.Contains("and"))
                //    RegExp = new Regex(Search_Str.Replace("and ", "").Trim(), RegexOptions.IgnoreCase);
                else
                    RegExp = new Regex(Search_Str.Replace(" ", "|").Trim(), RegexOptions.IgnoreCase);
                // Regex RegExp = new Regex(Search_Str.Replace(" ", "|").Trim(), RegexOptions.IgnoreCase);

                string ss = RegExp.Replace(InputTxt, new MatchEvaluator(ReplaceKeyWords));
                return ss;
            }
            return InputTxt;



            //   return ("<span class=highlight>" + Search_Str + "</span>");
        }
        public string ReplaceKeyWords(Match m)
        {
            return ("<span class=highlight>" + m.Value + "</span>");
        }
        public string HighlightTextWithin(string InputTxt)
        {
            if (Session["SearchWithIn"] != null)
            {
                string Search_Str_WithIn = "";
                if (Session["SearchWithIn"] != null)
                    Search_Str_WithIn = Session["SearchWithIn"].ToString();


                Session["SearchWithIn"] = Search_Str_WithIn.ToString();
                Search_Str_WithIn = CommonClass.RemoveExtraWordsForHiglight(Search_Str_WithIn);
                Regex RegExp = new Regex(Search_Str_WithIn.Replace(" ", "|").Trim(), RegexOptions.IgnoreCase);


                return RegExp.Replace(InputTxt, new MatchEvaluator(ReplaceKeyWordsWithin));
            }
            return InputTxt;
        }
        public string ReplaceKeyWordsWithin(Match m)
        {
            return ("<span class=highlightBlue>" + m.Value + "</span>");
        }
        public string FormatContent(string InputTxt)
        {
            string content = InputTxt;

            //content = Regex.Replace(content, "</?(b|B).*?>", string.Empty);
            content = Regex.Replace(content, "</?(strong|STRONG).*?>", string.Empty);



            return content;


            //   return ("<span class=highlight>" + Search_Str + "</span>");
        }
        void GetUsersParentFolders(int UserID)
        {
            //try
            //{
            //    DataTable dt = new DataTable();
            //    dt = objusr.GetUserParentFolderByUser(UserID);

            //    ddlParentFolder.DataValueField = "ID";
            //    ddlParentFolder.DataTextField = "FolderName";
            //    ddlParentFolder.DataSource = dt;
            //    ddlParentFolder.DataBind();
            //    ddlParentFolder.Items.Insert(0, new ListItem("Add as Parent", "0"));

            //}
            //catch (Exception e)
            //{
            //    ExceptionHandling.SendErrorReport("MyFolder.aspx", "GetUsersParentFolders", e.Message);
            //}
        }
        void AddNewFolder()
        {
            try
            {
                //objusr.UserID = int.Parse(Session["MemberID"].ToString());
                //objusr.ParentFolder = int.Parse(ddlParentFolder.SelectedValue);
                //objusr.FolderName = txtFolderName.Text.Trim();
                //objusr.CreatedBy = int.Parse(Session["MemberID"].ToString());
                //int chk = objusr.InsertUserFolder();
                //if (chk > 0)
                //{
                //    //lblMsg.Text = "Folder Created..";
                //    //lblMsg.ForeColor = System.Drawing.Color.Green;

                //    ddlParentFolder.SelectedIndex = 0;
                //    txtFolderName.Text = "";
                //}
                ////else
                ////{
                ////    lblMsg.Text = "Folder Creation Error..";
                ////    lblMsg.ForeColor = System.Drawing.Color.Red;
                ////}
            }
            catch (Exception ex)
            {
                //lblMsg.Text = ex.Message;
                //lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }
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
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            EastLawBL.ErrorReporting objer = new EastLawBL.ErrorReporting();
            objer.Type = "";
            objer.ItemType = "";
            objer.ItemID = 0;
            objer.UserID = 1;
            //objer.UserComment = txtComment.Text ;
            objer.WorkflowID = 1;
            int chk = objer.InsertReportError();
        }
        protected void btnFill_Click(object sender, EventArgs e)
        {
            //ModalPopupExtender1.Hide();
            //string aa = PopuptxtFirstName.Text;
            //string bb = PopuptxtLastName.Text;

            //EastLawBL.ErrorReporting objer = new EastLawBL.ErrorReporting();
            //objer.Type = "Element";
            //objer.ItemType = "Cases";
            //objer.ItemID = int.Parse(HttpContext.Current.Items["caseid"].ToString());
            //objer.UserID = int.Parse(Session["MemberID"].ToString());
            //objer.UserComment = txtComment.Text;
            //objer.WorkflowID = 1;
            //int chk = objer.InsertReportError();
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            //modelPopupUserComents.Hide();
        }
        protected void btnAddComent_Click(object sender, EventArgs e)
        {
            //modelPopupUserComents.Hide();
            //string aa = PopuptxtFirstName.Text;
            //string bb = PopuptxtLastName.Text;

            EastLawBL.Cases objcase = new EastLawBL.Cases();
            //int chk = objcase.AddUserComment("Case", int.Parse(HttpContext.Current.Items["caseid"].ToString()), txtUserComment.Text.Trim(), "Pending", int.Parse(Session["MemberID"].ToString()));

        }
        protected void btnCommentClose_Click(object sender, EventArgs e)
        {
            //ModalPopupExtender1.Hide();
        }

        protected void btnSaveInFolder_Click(object sender, EventArgs e)
        {
            try
            {
                //if (ddlFolders.SelectedIndex != 0)
                //{
                //    objusr.UserID = int.Parse(Session["MemberID"].ToString());
                //    objusr.FolderID = int.Parse(ddlFolders.SelectedValue);
                //    objusr.ItemType = "Cases";
                //    objusr.ItemID = int.Parse(HttpContext.Current.Items["caseid"].ToString());
                //    objusr.CreatedBy = int.Parse(Session["MemberID"].ToString());
                //    int chk = objusr.InsertUserFolderItem();
                //    if (chk > 0)
                //    {
                //        lblResult.Text = "Added.";
                //        ddlFolders.SelectedIndex = 0;
                //    }
                //}
                //else
                //{
                //    lblResult.Text = "Please Select Folder.";
                //}
            }
            catch (Exception ex) { }
        }

        protected void lnkBackSearch_Click(object sender, EventArgs e)
        {
            if (ViewState["LastPage"] != null)
            {
                Uri uri = new Uri(ViewState["LastPage"].ToString());
                string filename = System.IO.Path.GetFileName(uri.AbsolutePath);
                Response.Redirect(ViewState["LastPage"].ToString());
            }
            if (Session["SearchMain"] != null)
            {
                Response.Redirect("/Search/" + Session["SearchMain"].ToString());
            }

        }
        protected void btnCreateFolder_Click(object sender, EventArgs e)
        {
            try
            {
                AddNewFolder();
                GetUsersParentFolders(int.Parse(Session["MemberID"].ToString()));
                //GetUserFolder(int.Parse(Session["MemberID"].ToString()));
            }
            catch { }
        }
    }
}