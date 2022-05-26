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
using Telerik.Web.UI;

namespace EastlawUI_v2
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
                        BindTelerikTree();
                        GetUserFolder(int.Parse(Session["MemberID"].ToString()));
                        GetUsersParentFolders(int.Parse(Session["MemberID"].ToString()));
                        InsertAuditLog("Case", HttpContext.Current.Items["caseid"].ToString(), "");
                        
                    }
                    else
                    {
                        Response.Redirect("/member/member-login?req=" + HttpContext.Current.Request.Url.AbsolutePath);
                    }
                    Validate();
                    ViewState["LastPage"] = Request.UrlReferrer.ToString();
                }
            }
            catch { }
        }
        private void BindTelerikTree()
        {
            try
            {
                tvDept.Nodes.Clear();
                RadTreeNode level0 = new RadTreeNode("Personal Folders", "0");
                level0.ImageUrl = "/style/img/4Inbox.gif";
                tvDept.Nodes.Add(level0);


                DataTable dtlevel1 = objusr.GetUserParentFolderByUser(int.Parse(Session["MemberID"].ToString()));
                if (dtlevel1 != null)
                {
                    for (int l1 = 0; l1 < dtlevel1.Rows.Count; l1++)
                    {
                        string FirstNodeTitle = "";
                        if (dtlevel1.Rows[l1]["CountedItems"].ToString() != "0")
                            FirstNodeTitle = dtlevel1.Rows[l1]["FolderName"].ToString() + " (" + dtlevel1.Rows[l1]["CountedItems"].ToString() + ")";
                        else
                            FirstNodeTitle = dtlevel1.Rows[l1]["FolderName"].ToString();
                        RadTreeNode level1 = new RadTreeNode(FirstNodeTitle, dtlevel1.Rows[l1]["ID"].ToString());
                        level1.ImageUrl = "/style/img/4Inbox.gif";
                        level0.Nodes.Add(level1);


                        DataTable dtlevel2 = objusr.GetUserFolderByParent(int.Parse(dtlevel1.Rows[l1]["ID"].ToString()));
                        if (dtlevel2 != null)
                        {
                            for (int l2 = 0; l2 < dtlevel2.Rows.Count; l2++)
                            {
                                string SecondNodeTitle = "";
                                if (dtlevel2.Rows[l2]["CountedItems"].ToString() != "0")
                                    SecondNodeTitle = dtlevel2.Rows[l2]["FolderName"].ToString() + " (" + dtlevel2.Rows[l2]["CountedItems"].ToString() + ")";
                                else
                                    SecondNodeTitle = dtlevel2.Rows[l2]["FolderName"].ToString();

                                RadTreeNode level2 = new RadTreeNode(SecondNodeTitle, dtlevel2.Rows[l2]["ID"].ToString());
                                level2.ImageUrl = "/style/img/folder.gif";
                                level1.Nodes.Add(level2);

                                //DataTable dtlevel3 = objdept.GetActiveDeptByParent(int.Parse(dtlevel2.Rows[l2]["ID"].ToString()));
                                //if (dtlevel3 != null)
                                //{
                                //    for (int l3 = 0; l3 < dtlevel3.Rows.Count; l3++)
                                //    {
                                //        RadTreeNode level3 = new RadTreeNode(dtlevel3.Rows[l3]["DeptName"].ToString(), dtlevel3.Rows[l3]["ID"].ToString());
                                //        level2.Nodes.Add(level3);
                                //    }
                                //}
                            }
                        }

                    }
                }
                tvDept.ExpandAllNodes();
            }
            catch (Exception Ex) { throw Ex; }
        }
        protected void tvDept_NodeEdit(object sender, RadTreeNodeEditEventArgs e)
        {
            RadTreeNode nodeEdited = e.Node;
            string newText = e.Text;
            nodeEdited.Text = newText;

            objusr.UserID = int.Parse(Session["MemberID"].ToString());
            objusr.ParentFolder = int.Parse(e.Node.Value);
            objusr.FolderName = newText;
            objusr.ModifiedBy = int.Parse(Session["MemberID"].ToString());
            objusr.EditUserFolder(int.Parse(e.Node.Value.ToString()));
            BindTelerikTree();
        }
        protected void tvDept_NodeClick(object sender, RadTreeNodeEventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                // Session["SelectedNode"] = e.Node.Value.ToString();
                //GetSelecteddeptFiles(int.Parse(e.Node.Value.ToString()));


                //GetFolderItemsByFolder(int.Parse(e.Node.Value.ToString()));
                //lblSelectedFolder.Text = e.Node.Text.ToString();
                //ddlParentFolder.SelectedIndex = 0;
                //txtFolderName.Text = "";
                //divAddFolder.Style["Display"] = "none";
                //rfvParentFolder.Enabled = false;
                //rfvFolderName.Enabled = false;

                //divFolderItems.Style["Display"] = "";

                //btnDeleteFolder.Visible = true;
               // lblSelectedFolderID.Text = e.Node.Value.ToString();
                //dt = objdept.GetDepartmentFilesByDepartments(int.Parse(e.Node.Value.ToString()));
                //if(dt.Rows.Count > 0)
                //{
                //    gvFile.DataSource = dt;
                //    gvFile.DataBind();
                //}
                //else
                //{
                //    gvFile.DataSource = null;
                //    gvFile.DataBind();
                //}
            }
            catch
            { }

        }
        protected void tvDept_ContextMenuItemClick(object sender, RadTreeViewContextMenuEventArgs e)
        {
            RadTreeNode clickedNode = e.Node;
            switch (e.MenuItem.Value)
            {
                case "Edit":
                    // Response.Redirect("AddDepartment.aspx?param=" + EncryptDecryptHelper.Encrypt(e.Node.Value.ToString()));
                    break;
                case "NewFolder":
                    objusr.UserID = int.Parse(Session["MemberID"].ToString());
                    objusr.ParentFolder = int.Parse(e.Node.Value);
                    objusr.FolderName = "New Folder";
                    objusr.CreatedBy = int.Parse(Session["MemberID"].ToString());
                    int chk = objusr.InsertUserFolder();
                    BindTelerikTree();

                    break;
                case "Delete":


                    objusr.ModifiedBy = int.Parse(Session["MemberID"].ToString());
                    objusr.DeleteUserFolder(int.Parse(e.Node.Value.ToString()));

                    GetUsersParentFolders(int.Parse(Session["MemberID"].ToString()));
                    
                    BindTelerikTree();
                    e.Node.Remove();

                    //int chk = objdept.DeleteDepartment(int.Parse(e.Node.Value), int.Parse(Session["UserID"].ToString()));
                    //BindTelerikTree(0);
                    // e.Node.Remove();

                    // }
                    if (e.Node.Level == 1)
                    {

                        //int chk = objdept.DeleteDepartment(int.Parse(e.Node.Value), int.Parse(Session["UserID"].ToString()));
                        //e.Node.Remove();

                    }
                    break;
            }
        }
        void Validate()
        {
            try
            {
                if (Session["MemberID"].ToString() == "1959" || Session["MemberID"].ToString() == "3" || Session["MemberID"].ToString() == "26" || Session["MemberID"].ToString() == "2365" || Session["MemberID"].ToString() == "2369" || Session["MemberID"].ToString() == "2378")
                {
                    //btnDownladPDF.Visible = true;
                    //btnPrint.Visible = true;
                }
                else
                {
                    //btnDownladPDF.Visible = false;
                    //btnPrint.Visible = false;
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
                                    Response.Redirect("/restricted/limit-exceeded");
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
        public string LinkedCasesLst()
        {
            try
            {
                DataTable dtLinkedCases = new DataTable();
                dtLinkedCases = objcase.GetLinkedCasesWithDetails(int.Parse(HttpContext.Current.Items["caseid"].ToString()));
                dtLinkedCases = dtLinkedCases.AsEnumerable().GroupBy(r => r.Field<int>("ID")).Select(g => g.First()).CopyToDataTable();



                string txt = "";
                for (int a = 0; a < dtLinkedCases.Rows.Count; a++)
                {
                    string link = "/cases/" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dtLinkedCases.Rows[a]["Appeallant"].ToString(), 3).ToString())).Replace(" ", "-").ToLower() + "VS" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dtLinkedCases.Rows[a]["Respondent"].ToString(), 3).ToString())).Replace(" ", "-").ToLower() + "." + EncryptDecryptHelper.Encrypt(dtLinkedCases.Rows[a]["ID"].ToString());
                    txt = txt +" <li> <a href='" + link + "' target='_blank'>" + dtLinkedCases.Rows[a]["Citation"].ToString() + "</a></li>";
                                               
                   
                }
                
                return txt;
            }


            catch (Exception ex)
            {
                return "";
            }
        }
        public string LinkedCases(string txt)
        {
            try
            {
                DataTable dt = new DataTable();
                //dt = objcase.GetLinkedCases(int.Parse(HttpContext.Current.Items["caseid"].ToString()));
                dt = objcase.GetLinkedCasesWithDetails(int.Parse(HttpContext.Current.Items["caseid"].ToString()));
                string ftxt = txt;
                for (int a = 0; a < dt.Rows.Count; a++)
                {
                    if (!CommonClass.ExactMatch(txt, dt.Rows[a]["CitationFnd"].ToString()))
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
                        string link = "/cases/" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dt.Rows[a]["Appeallant"].ToString(), 3).ToString())).Replace(" ", "-").ToLower() + "VS" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dt.Rows[a]["Respondent"].ToString(), 3).ToString())).Replace(" ", "-").ToLower() + "." + EncryptDecryptHelper.Encrypt(dt.Rows[a]["ID"].ToString());
                        dt.Rows[a]["link"] = "<a href='" + link + "' target='_blank'>" + dt.Rows[a]["citation"].ToString() + "</a>";
                        //dt.Rows[a]["link"] = "<a href='/search/citation/" + dt.Rows[a]["word"].ToString().Replace(" ", "-") + "' target='_blank'>" + dt.Rows[a]["word"].ToString() + "</a>";
                    }
                    dt.AcceptChanges();
                    var myList = dt.AsEnumerable().ToDictionary<DataRow, string, string>(row => row[4].ToString(), row => row[5].ToString());

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
                        dt.Rows[a]["link"] = "<a href='/statutes/" + dt.Rows[a]["Title"].ToString().Replace(" ", "-") + "." + EncryptDecryptHelper.Encrypt(dt.Rows[a]["ID"].ToString()) + "' target='_blank'>" + dt.Rows[a]["Title"].ToString() + "</a>";
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
        public string GetAnnotation(string txt)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objcase.GetUserCaseAnnotation(int.Parse(HttpContext.Current.Items["caseid"].ToString()), int.Parse(Session["MemberID"].ToString()));
                string ftxt = txt;


                if (dt.Rows.Count > 0)
                {
                    dt.Columns.Add("NewText");
                    //for (int a = 0; a < dt.Rows.Count; a++)
                    //{
                    //    dt.Rows[a]["NewText"] = "<div class='tooltip'>" + dt.Rows[a]["SelectedText"].ToString() + "<span class='tooltiptext'>" + dt.Rows[a]["AnnotedText"].ToString() + "</span></div>";
                    //}
                    for (int a = 0; a < dt.Rows.Count; a++)
                    {
                        dt.Rows[a]["NewText"] = " <div class=tooltip> " + dt.Rows[a]["SelectedText"].ToString() + " <span class=tooltiptext>" + dt.Rows[a]["AnnotedText"].ToString() + " </span></div>";
                    }
                    var myList = dt.AsEnumerable().ToDictionary<DataRow, string, string>(row => row[0].ToString(), row => row[2].ToString());

                    var regex = new Regex(String.Join("|", myList.Keys.Select(k => Regex.Escape(k))));
                    return regex.Replace(txt, m => myList[m.Value]);
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
                //if (Search_Str.Contains("\""))
                //    RegExp = new Regex(Search_Str.Replace("\"", "").Trim(), RegexOptions.IgnoreCase);
                ////if (Search_Str.Contains("and"))
                ////    RegExp = new Regex(Search_Str.Replace("and ", "").Trim(), RegexOptions.IgnoreCase);
                //else
                //    RegExp = new Regex(Search_Str.Replace(" ", "|").Trim(), RegexOptions.IgnoreCase);
                //// Regex RegExp = new Regex(Search_Str.Replace(" ", "|").Trim(), RegexOptions.IgnoreCase);

                //string ss = RegExp.Replace(InputTxt, new MatchEvaluator(ReplaceKeyWords));
                //return ss;




                string[] keywords = Search_Str.Split(new string[] { "and" }, StringSplitOptions.None);

                for (int i = 0; i < keywords.Length; i++)
                {


                    if (Search_Str.Contains("\""))
                        RegExp = new Regex(keywords[i].ToString().Replace("\"", "").Trim(), RegexOptions.IgnoreCase);
                    //if (Search_Str.Contains("and"))
                    //    RegExp = new Regex(Search_Str.Replace("and ", "").Trim(), RegexOptions.IgnoreCase);
                    else
                        RegExp = new Regex(keywords[i].ToString().Replace(" ", "|").Trim(), RegexOptions.IgnoreCase);
                    InputTxt = RegExp.Replace(InputTxt, new MatchEvaluator(ReplaceKeyWords));
                }

                //return RegExp.Replace(InputTxt, new MatchEvaluator(ReplaceKeyWords));
                return InputTxt;
            }
            return InputTxt;



            //   return ("<span class=highlight>" + Search_Str + "</span>");
        }
        public string ReplaceKeyWords(Match m)
        {
            return ("<span class=highlight>" + m.Value + "</span>");
        }
       public string GetAlternateCitations(int CaseID)
        {
            try
            {
                DataTable dt = new DataTable();
                string AlternateCitations = "";
                dt = objcase.GetAlternateCitationByCaseID(CaseID);
                if (dt != null && dt.Rows.Count > 0)
                {
                   // AlternateCitations = " - ";
                    for (int a = 0; a < dt.Rows.Count; a++)
                    {
                        AlternateCitations = AlternateCitations + "["+dt.Rows[a]["Citation"].ToString() + "], ";
                    }
                }
                return AlternateCitations;
            }
            catch
            {
                return ""; ;
            }
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


               // return RegExp.Replace(InputTxt, new MatchEvaluator(ReplaceKeyWordsWithin));

                InputTxt = Regex.Replace(InputTxt, Search_Str_WithIn.Replace("\"", "").Trim(), ReplaceKeyWordsWithin, RegexOptions.IgnoreCase);
            }
            return InputTxt;
        }
        public string ReplaceKeyWordsWithin(Match m)
        {
            return ("<span class=highlightBlue>" + m.Value + "</span>");
        }
        public string FormatContent(string InputTxt)
        {
            try
            {
                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(InputTxt);
                foreach (var a in doc.DocumentNode.Descendants("a"))
                {
                    a.Attributes["href"].Value = a.Attributes["href"].Value.Replace("<span class=highlight>", "").Replace("</span>", "").Replace("<span class=highlightBlue>", "");
                }

                var newContent = doc.DocumentNode.InnerHtml;



                return newContent.ToString();
            }
            catch {
                return InputTxt;
            }


            //   return ("<span class=highlight>" + Search_Str + "</span>");
        }
        void GetUsersParentFolders(int UserID)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objusr.GetUserParentFolderByUser(UserID);

                //ddlParentFolder.DataValueField = "ID";
                //ddlParentFolder.DataTextField = "FolderName";
                //ddlParentFolder.DataSource = dt;
                //ddlParentFolder.DataBind();
                //ddlParentFolder.Items.Insert(0, new ListItem("Add as Parent", "0"));

            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("MyFolder.aspx", "GetUsersParentFolders", e.Message);
            }
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
                //else
                //{
                //    lblMsg.Text = "Folder Creation Error..";
                //    lblMsg.ForeColor = System.Drawing.Color.Red;
                //}
            }
            catch (Exception ex)
            {
                //lblMsg.Text = ex.Message;
                //lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }
        void GetUserFolder(int UserID)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objusr.GetUserFolderByUser(UserID);

                //ddlFolders.DataValueField = "ID";
                //ddlFolders.DataTextField = "FolderName";
                //ddlFolders.DataSource = dt;
                //ddlFolders.DataBind();
                //ddlFolders.Items.Insert(0, new ListItem("Select", "0"));

            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("Statuteslist.aspx", "GetUserFolder", e.Message);
            }
        }
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
           // ModalPopupExtender1.Hide();
            //string aa = PopuptxtFirstName.Text;
            //string bb = PopuptxtLastName.Text;

            EastLawBL.ErrorReporting objer = new EastLawBL.ErrorReporting();
            objer.Type = "Element";
            objer.ItemType = "Cases";
            objer.ItemID = int.Parse(HttpContext.Current.Items["caseid"].ToString());
            objer.UserID = int.Parse(Session["MemberID"].ToString());
            objer.UserComment = txtComment.Text;
            objer.WorkflowID = 1;
            int chk = objer.InsertReportError();
            if (chk > 0)
            {
                txtComment.Text = "";
                lblErrorMsg.Text = "Thanks for your valuable error reporting, we will definatily review your comment.";
                
                uppPnlReportError.Update();
                //trErrorReportFields.Style["Display"] = "none";
                //trErrorReport.Style["Display"] = "";
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
           // modelPopupUserComents.Hide();
        }
        protected void btnAddComent_Click(object sender, EventArgs e)
        {
           // modelPopupUserComents.Hide();
            //string aa = PopuptxtFirstName.Text;
            //string bb = PopuptxtLastName.Text;

            EastLawBL.Cases objcase = new EastLawBL.Cases();
            int chk = objcase.AddUserComment("Case", int.Parse(HttpContext.Current.Items["caseid"].ToString()), txtUserComment.Text.Trim(), "Pending", int.Parse(Session["MemberID"].ToString()));
            if (chk > 0)
            {
                txtUserComment.Text = "";
                lblCommentThanks.Text = "Thanks for your valuable comment.";
                upnlSendComment.Update();
                //trErrorReportFields.Style["Display"] = "none";
                //trErrorReport.Style["Display"] = "";

                //Save your record here
                //ScriptManager.RegisterStartupScript(upnlSendComment, upnlSendComment.GetType(), "hide", "$(function () { $('#" + Panel1.ClientID + "').modal('hide'); });", true);
                //upnlSendComment.Update();

                ScriptManager.RegisterStartupScript(this, GetType(), "Close Modal Popup", "CloseCommentBox();", true);
            }

        }
        protected void btnCommentClose_Click(object sender, EventArgs e)
        {
           // ModalPopupExtender1.Hide();
        }

        protected void btnSaveInFolder_Click(object sender, EventArgs e)
        {
            try
            {
                if (tvDept.SelectedValue != null)
                {
                    objusr.UserID = int.Parse(Session["MemberID"].ToString());
                    objusr.FolderID = int.Parse(tvDept.SelectedValue);
                    objusr.ItemType = "Cases";
                    objusr.ItemID = int.Parse(HttpContext.Current.Items["caseid"].ToString());
                    objusr.CreatedBy = int.Parse(Session["MemberID"].ToString());
                    int chk = objusr.InsertUserFolderItem();
                    if (chk > 0)
                    {
                        lblAddToFolder.Text = "Added.";
                        UpdatePanelFolder.Update();
                    }
                }
                else
                {
                    lblAddToFolder.Text = "Please Select Folder.";
                    UpdatePanelFolder.Update();
                }
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
                Response.Redirect("/search/" + Session["SearchMain"].ToString());
            }

        }
        protected void btnCreateFolder_Click(object sender, EventArgs e)
        {
            try
            {
                AddNewFolder();
                GetUsersParentFolders(int.Parse(Session["MemberID"].ToString()));
                GetUserFolder(int.Parse(Session["MemberID"].ToString()));
            }
            catch { }
        }

        protected void btn_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(hd.Value.ToString()))
                {
                    objusr.UserID = int.Parse(Session["MemberID"].ToString());
                    objusr.FolderID = objusr.GetAnnotationFolder(int.Parse(Session["MemberID"].ToString()));
                    objusr.ItemType = "Cases";
                    objusr.ItemID = int.Parse(HttpContext.Current.Items["caseid"].ToString());
                    objusr.CreatedBy = int.Parse(Session["MemberID"].ToString());
                    int chkFolder = objusr.InsertUserFolderItem();

                    int chk = objcase.AddUserCaseAnnotation(int.Parse(HttpContext.Current.Items["caseid"].ToString()), int.Parse(Session["MemberID"].ToString()), hd.Value.ToString().Trim(), txtText.Text.Trim());
                    if (chk > 0)
                    {
                        lblAannotationAdded.Text = "Annotation Added.";
                        lblAannotationAdded.ForeColor = System.Drawing.Color.Green;
                        UpdatePanelAnnotation.Update();


                        BindTelerikTree();
                        UpdatePanelFolder.Update();
                    }
                }
                else
                {
                    lblAannotationAdded.Text = "Annotation Added.";
                    lblAannotationAdded.ForeColor = System.Drawing.Color.Red;
                    UpdatePanelAnnotation.Update();
 
                }
            }
            catch { }
            //string a = hd.Value;

            //Response.Write(newDiv1.InnerText);
            //Response.Write(txtText.Text);
        }

        protected void ibtnDownloadPDF_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                EastLawBL.Cases obj = new EastLawBL.Cases();
                DataTable dt = new DataTable();
                dt = obj.GetCases(int.Parse(HttpContext.Current.Items["caseid"].ToString()));
                string htm = "";
                htm = htm + "<strong>Court:</strong>" + dt.Rows[0]["Court"].ToString() + "<br>";
                htm = htm + "<strong>Judge (s):</strong>" + dt.Rows[0]["JudgeName"].ToString() + "<br>";
                htm = htm + "<strong>" + EastlawUI_v2.CommonClass.MakeFirstCap(dt.Rows[0]["Appeallant"].ToString()) + "<span style='color:red'> VS </span>" + EastlawUI_v2.CommonClass.MakeFirstCap(dt.Rows[0]["Respondent"].ToString()) + "</strong><br>";
                htm = htm + "<strong>Appeal:</strong>" + dt.Rows[0]["Appeal"].ToString() + "<br>";
                htm = htm + "<strong>Judgment Date:</strong>" + dt.Rows[0]["JDate"].ToString() + "<br>";
                htm = htm + "<strong>Citation No:</strong>" + dt.Rows[0]["Citation"].ToString()+" "+ GetAlternateCitations(int.Parse(HttpContext.Current.Items["caseid"].ToString())) + "<br>";
                htm = htm + "<strong>Result:</strong>" + dt.Rows[0]["Result"].ToString() + "<br><br><br>";
                //htm = htm + "<center><strong> " + dt.Rows[0]["Court"].ToString() + "<br/>" + dt.Rows[0]["JudgeName"].ToString() + "</strong></center><br>";
                //htm = htm + "<table border='0' style='width:100%;text-align:center'>";
                //htm = htm + "<tr>";
                //htm = htm + "<td style='width:45%;text-align:right'><strong>" + EastLawUI.CommonClass.MakeFirstCap(dt.Rows[0]["Appeallant"].ToString()) + "</strong></td>";
                //htm = htm + "<td style='width: 10%;color:red'>VS </td>";
                //htm = htm + "<td style='width:45%;text-align:left'><strong>" + EastLawUI.CommonClass.MakeFirstCap(dt.Rows[0]["Respondent"].ToString()) + " </strong></td></tr></table>";

                //htm = htm + "<h6>" + dt.Rows[0]["Appeal"].ToString() + "</span><br/>" + dt.Rows[0]["JDate"].ToString() + "<br/>";
                //if (string.IsNullOrEmpty(dt.Rows[0]["CitationRef"].ToString()))
                //    htm = htm + "<font style='color:red'>Reported As</font> [" + dt.Rows[0]["Citation"].ToString() + "]<br/>";
                //else
                //    htm = htm + "<font style='color:red'>Reported As</font> [" + dt.Rows[0]["CitationRef"].ToString() + "]<br/>";

                //htm = htm + "Keywords: " + dt.Rows[0]["Keywords"].ToString() + "<br><br>Result: " + dt.Rows[0]["Result"].ToString() + "</h6>";
                htm = htm + EastlawUI_v2.CommonClass.AutoCloseHtmlTags(FormatContent(dt.Rows[0]["Judgment"].ToString().Replace("--", " ").Replace("##TS##", " ").Replace("##TE##", "").Replace("<table border=\"0\"", "<table border=\"1\"")));

                InsertAuditLog("Case", HttpContext.Current.Items["caseid"].ToString(), "PDF Download");
                PDFGenerator.GeneratePDF(htm, dt.Rows[0]["Citation"].ToString().Replace(" ", "-"));
            }
            catch { }
        }

        protected void btnDownladPDF_Click(object sender, EventArgs e)
        {
            try
            {
                EastLawBL.Cases obj = new EastLawBL.Cases();
                DataTable dt = new DataTable();
                dt = obj.GetCases(int.Parse(HttpContext.Current.Items["caseid"].ToString()));
                string htm = "";
                htm = htm + "<span style='font-size:8pt;line-height: 7;'><strong>Disclaimer</strong>: This text of the judgment/order is made available merely for information to our subscribers for Educational purposes only. The text is yet to be processed, verified and authenticated on the basis of the certified copy. You may seek to apply from the relevant Court according to the available information. Hence www.eastlaw.pk shall not be liable for any action taken or omitted to be taken or advice rendered or accepted on the basis of this text.</span>";
                htm = htm + "<br><br><div style='font-size:8pt;line-height: 7'>EastLaw Online Web Edition, Copyright © " + DateTime.Now.Year.ToString() + "<br> " + DateTime.Now.ToString("D") + "<br>Printed For: " + Session["MemberName"].ToString() + "</div>";
                htm = htm + "<br><br><strong>Court:</strong>" + dt.Rows[0]["Court"].ToString() + "<br>";
                htm = htm + "<strong>Before: </strong>" + dt.Rows[0]["JudgeName"].ToString() + "<br>";
                htm = htm + "<strong><table width='100%'><tr><td style='width:45%'>" + EastlawUI_v2.CommonClass.MakeFirstCap(dt.Rows[0]["Appeallant"].ToString()) + "</td><td style='width:10%'><span style='color:red'> VS </span></td><td style='width:45%'>" + EastlawUI_v2.CommonClass.MakeFirstCap(dt.Rows[0]["Respondent"].ToString()) + "</td></tr></table></strong><br>";
                htm = htm + "<strong>Appeal:</strong>" + dt.Rows[0]["Appeal"].ToString() + "<br>";
                htm = htm + "<strong>Judgment Date:</strong>" + dt.Rows[0]["JDate"].ToString() + "<br>";
                htm = htm + "<strong>Citation No:</strong>" + dt.Rows[0]["Citation"].ToString()+ " "+ GetAlternateCitations(int.Parse(HttpContext.Current.Items["caseid"].ToString())) + "<br>";
                htm = htm + "<strong>Result:</strong>" + dt.Rows[0]["Result"].ToString() + "<br><br><br>";
                //htm = htm + "<center><strong> " + dt.Rows[0]["Court"].ToString() + "<br/>" + dt.Rows[0]["JudgeName"].ToString() + "</strong></center><br>";
                //htm = htm + "<table border='0' style='width:100%;text-align:center'>";
                //htm = htm + "<tr>";
                //htm = htm + "<td style='width:45%;text-align:right'><strong>" + EastLawUI.CommonClass.MakeFirstCap(dt.Rows[0]["Appeallant"].ToString()) + "</strong></td>";
                //htm = htm + "<td style='width: 10%;color:red'>VS </td>";
                //htm = htm + "<td style='width:45%;text-align:left'><strong>" + EastLawUI.CommonClass.MakeFirstCap(dt.Rows[0]["Respondent"].ToString()) + " </strong></td></tr></table>";

                //htm = htm + "<h6>" + dt.Rows[0]["Appeal"].ToString() + "</span><br/>" + dt.Rows[0]["JDate"].ToString() + "<br/>";
                //if (string.IsNullOrEmpty(dt.Rows[0]["CitationRef"].ToString()))
                //    htm = htm + "<font style='color:red'>Reported As</font> [" + dt.Rows[0]["Citation"].ToString() + "]<br/>";
                //else
                //    htm = htm + "<font style='color:red'>Reported As</font> [" + dt.Rows[0]["CitationRef"].ToString() + "]<br/>";

                //htm = htm + "Keywords: " + dt.Rows[0]["Keywords"].ToString() + "<br><br>Result: " + dt.Rows[0]["Result"].ToString() + "</h6>";
                htm = htm + "<div style='text-align: justify'>" + EastlawUI_v2.CommonClass.AutoCloseHtmlTags(FormatContent(dt.Rows[0]["Judgment"].ToString().Replace("--", " ").Replace("##TS##", " ").Replace("##TE##", "").Replace("</p>", "</p><br><br>"))) + "</div>";

                htm = htm + "<br><br><br><br><br><br><div>EastLaw Online Web Edition, Copyright © " + DateTime.Now.Year.ToString() + "<br> " + DateTime.Now.ToShortDateString() + "<br>Printed For: " + Session["MemberName"].ToString() + "<br>Web Link: <a href='" + Request.UrlReferrer.ToString() + "' target='_blank'>" + Request.UrlReferrer.ToString() + "</a></div>";


                InsertAuditLog("Case", HttpContext.Current.Items["caseid"].ToString(), "PDF Download");
                PDFGenerator.GeneratePDF(htm, dt.Rows[0]["Citation"].ToString().Replace(" ", "-"));
            }
            catch { }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {

        }

        protected void lnkDownload_Click(object sender, EventArgs e)
        {
            EastLawBL.Cases obj = new EastLawBL.Cases();
            DataTable dt = new DataTable();
            dt = obj.GetCases(int.Parse(HttpContext.Current.Items["caseid"].ToString()));
            string htm = "";
            htm = htm + "<span style='font-size:8pt;line-height: 7;'><strong>Disclaimer</strong>: This text of the judgment/order is made available merely for information to our subscribers for Educational purposes only. The text is yet to be processed, verified and authenticated on the basis of the certified copy. You may seek to apply from the relevant Court according to the available information. Hence www.eastlaw.pk shall not be liable for any action taken or omitted to be taken or advice rendered or accepted on the basis of this text.</span>";
            htm = htm + "<br><br><div style='font-size:8pt;line-height: 7'>EastLaw Online Web Edition, Copyright © " + DateTime.Now.Year.ToString() + "<br> " + DateTime.Now.ToString("D") + "<br>Printed For: " + Session["MemberName"].ToString() + "</div>";
            htm = htm + "<br><br><strong>Court:</strong>" + dt.Rows[0]["Court"].ToString() + "<br>";
            htm = htm + "<strong>Before: </strong>" + dt.Rows[0]["JudgeName"].ToString() + "<br>";
            htm = htm + "<strong><table width='100%'><tr><td style='width:45%'>" + EastlawUI_v2.CommonClass.MakeFirstCap(dt.Rows[0]["Appeallant"].ToString()) + "</td><td style='width:10%'><span style='color:red'> VS </span></td><td style='width:45%'>" + EastlawUI_v2.CommonClass.MakeFirstCap(dt.Rows[0]["Respondent"].ToString()) + "</td></tr></table></strong><br>";
            htm = htm + "<strong>Appeal:</strong>" + dt.Rows[0]["Appeal"].ToString() + "<br>";
            htm = htm + "<strong>Judgment Date:</strong>" + dt.Rows[0]["JDate"].ToString() + "<br>";
            htm = htm + "<strong>Citation No:</strong>" + dt.Rows[0]["Citation"].ToString() + " " + GetAlternateCitations(int.Parse(HttpContext.Current.Items["caseid"].ToString())) + "<br>";
            htm = htm + "<strong>Result:</strong>" + dt.Rows[0]["Result"].ToString() + "<br><br><br>";
            //htm = htm + "<center><strong> " + dt.Rows[0]["Court"].ToString() + "<br/>" + dt.Rows[0]["JudgeName"].ToString() + "</strong></center><br>";
            //htm = htm + "<table border='0' style='width:100%;text-align:center'>";
            //htm = htm + "<tr>";
            //htm = htm + "<td style='width:45%;text-align:right'><strong>" + EastLawUI.CommonClass.MakeFirstCap(dt.Rows[0]["Appeallant"].ToString()) + "</strong></td>";
            //htm = htm + "<td style='width: 10%;color:red'>VS </td>";
            //htm = htm + "<td style='width:45%;text-align:left'><strong>" + EastLawUI.CommonClass.MakeFirstCap(dt.Rows[0]["Respondent"].ToString()) + " </strong></td></tr></table>";

            //htm = htm + "<h6>" + dt.Rows[0]["Appeal"].ToString() + "</span><br/>" + dt.Rows[0]["JDate"].ToString() + "<br/>";
            //if (string.IsNullOrEmpty(dt.Rows[0]["CitationRef"].ToString()))
            //    htm = htm + "<font style='color:red'>Reported As</font> [" + dt.Rows[0]["Citation"].ToString() + "]<br/>";
            //else
            //    htm = htm + "<font style='color:red'>Reported As</font> [" + dt.Rows[0]["CitationRef"].ToString() + "]<br/>";

            //htm = htm + "Keywords: " + dt.Rows[0]["Keywords"].ToString() + "<br><br>Result: " + dt.Rows[0]["Result"].ToString() + "</h6>";
            //htm = htm + "<div style='text-align: justify'>" + EastlawUI_v2.CommonClass.AutoCloseHtmlTags(FormatContent(dt.Rows[0]["Judgment"].ToString().Replace("--", " ").Replace("##TS##", " ").Replace("##TE##", "").Replace("</p>", "</p><br><br>"))) + "</div>";
            htm = htm + "<div style='text-align: justify'>" + dt.Rows[0]["Judgment"].ToString().Replace("--", " ").Replace("##TS##", " ").Replace("##TE##", "").Replace("</p>", "</p><br><br>").Replace("<table border=\"0\"", "<table border=\"1\"").Replace("/store/cases", "https://eastlaw.pk/store/cases") + "</div>";

            htm = htm + "<br><br><br><br><br><br><div>EastLaw Online Web Edition, Copyright © " + DateTime.Now.Year.ToString() + "<br> " + DateTime.Now.ToShortDateString() + "<br>Printed For: " + Session["MemberName"].ToString() + "<br>Web Link: <a href='" + Request.UrlReferrer.ToString() + "' target='_blank'>" + Request.UrlReferrer.ToString() + "</a></div>";


            InsertAuditLog("Case", HttpContext.Current.Items["caseid"].ToString(), "PDF Download");
            PDFGenerator.GeneratePDF(htm, dt.Rows[0]["Citation"].ToString().Replace(" ", "-"));
        }
    }
}