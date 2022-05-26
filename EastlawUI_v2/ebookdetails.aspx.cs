using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using System.Text.RegularExpressions;

namespace EastlawUI_v2
{
    public partial class ebookdetails : System.Web.UI.Page
    {
        EastLawBL.EBook objeb = new EastLawBL.EBook();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    BindTelerikTree(int.Parse(HttpContext.Current.Items["ebookid"].ToString()));
                }
            }
            catch { }
            
        }
        private void BindTelerikTree(int BookID)
        {
            try
            {
                tvDept.Nodes.Clear();
                //RadTreeNode level0 = new RadTreeNode("Index", "0");
                ////level0.ImageUrl = "/style/img/4Inbox.gif";
                //tvDept.Nodes.Add(level0);


                DataTable dtlevel1 = objeb.GetEBookParentIndex(BookID, 0);
                if (dtlevel1 != null)
                {
                    lblBookTitle.Text = dtlevel1.Rows[0]["TItle"].ToString();
                    for (int l1 = 0; l1 < dtlevel1.Rows.Count; l1++)
                    {
                        string FirstNodeTitle = "";
                        FirstNodeTitle = dtlevel1.Rows[l1]["IndexTitle"].ToString();
                        RadTreeNode level1 = new RadTreeNode(FirstNodeTitle, dtlevel1.Rows[l1]["ID"].ToString());
                        // level1.ImageUrl = "/style/img/4Inbox.gif";
                        tvDept.Nodes.Add(level1);


                        DataTable dtlevel2 = objeb.GetEBookParentIndex(BookID, int.Parse(dtlevel1.Rows[l1]["ID"].ToString()));
                        if (dtlevel2 != null)
                        {
                            for (int l2 = 0; l2 < dtlevel2.Rows.Count; l2++)
                            {
                                string SecondNodeTitle = "";

                                SecondNodeTitle = dtlevel2.Rows[l2]["IndexTitle"].ToString();

                                RadTreeNode level2 = new RadTreeNode(SecondNodeTitle, dtlevel2.Rows[l2]["ID"].ToString());
                                //level2.ImageUrl = "/style/img/folder.gif";
                                level1.Nodes.Add(level2);
                              //  level1.ExpandParentNodes();

                                DataTable dtlevel3 = objeb.GetEBookParentIndex(BookID, int.Parse(dtlevel2.Rows[l2]["ID"].ToString()));
                                if (dtlevel3 != null)
                                {
                                    for (int l3 = 0; l3 < dtlevel3.Rows.Count; l3++)
                                    {
                                        string ThirdNodeTitle = "";

                                        ThirdNodeTitle = dtlevel3.Rows[l3]["IndexTitle"].ToString();
                                        RadTreeNode level3 = new RadTreeNode(ThirdNodeTitle, dtlevel3.Rows[l3]["ID"].ToString());
                                        level2.Nodes.Add(level3);

                                        DataTable dtlevel4 = objeb.GetEBookParentIndex(BookID, int.Parse(dtlevel3.Rows[l3]["ID"].ToString()));
                                        if (dtlevel4 != null)
                                        {
                                            for (int l4 = 0; l4 < dtlevel4.Rows.Count; l4++)
                                            {
                                                string ForthNodeTitle = "";

                                                ForthNodeTitle = dtlevel4.Rows[l4]["IndexTitle"].ToString();
                                                RadTreeNode level4 = new RadTreeNode(ForthNodeTitle, dtlevel4.Rows[l4]["ID"].ToString());
                                                level3.Nodes.Add(level4);

                                                DataTable dtlevel5 = objeb.GetEBookParentIndex(BookID, int.Parse(dtlevel4.Rows[l4]["ID"].ToString()));
                                                if (dtlevel5 != null)
                                                {
                                                    for (int l5 = 0; l5 < dtlevel5.Rows.Count; l5++)
                                                    {
                                                        string FifthNodeTitle = "";

                                                        FifthNodeTitle = dtlevel5.Rows[l5]["IndexTitle"].ToString();
                                                        RadTreeNode level5 = new RadTreeNode(FifthNodeTitle, dtlevel5.Rows[l5]["ID"].ToString());
                                                        level4.Nodes.Add(level5);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }


                            }
                        }

                    }
                }
                
            }
            catch (Exception Ex) { throw Ex; }
        }
        void GetEBookIndexes(int ID, int EBookID)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objeb.GetEBookIndex(ID, EBookID);

                string cnt = "<iframe src='//eastlaw.pk/ebookflow/aspnet/simple_document.aspx?doc=" + dt.Rows[0]["OutputFle"].ToString() + " ' height='1100px' width='100%'></iframe>";
                divContent.InnerHtml= dt.Rows[0]["IndexContent"].ToString();
                //divContent.InnerHtml = cnt;
                
            }
            catch (Exception e)
            {

            }
        }
        void Search(string word)
        {
            try
            {
                DataTable dt = new DataTable();
                string swrd = CommonClass.FormatSearchWord(word.Trim());
                dt = objeb.GetEBookIndexSearch(int.Parse(HttpContext.Current.Items["ebookid"].ToString()), swrd);
                if (dt.Rows.Count > 0)
                {
                    dt.Columns.Add("Desc");
                    for (int a = 0; a < dt.Rows.Count; a++)
                    {
                        if (!string.IsNullOrEmpty(dt.Rows[a]["IndexContent"].ToString()))
                            dt.Rows[a]["Desc"] = HighlightText(swrd, EastlawUI_v2.CommonClass.GetShortDesc(dt.Rows[a]["IndexContent"].ToString(), swrd));
                    }
                    dt.AcceptChanges();
                    gvLst.DataSource = dt;
                    gvLst.DataBind();

                    lnkBack.Visible = true;
                    divSearchResults.Style["Display"] = "";
                    divContent.Style["Display"] = "none";
                }
            }
            catch { }
        }
        public string HighlightText(string searchword,string InputTxt)
        {
            string Search_Str = searchword;


            
            Search_Str = CommonClass.RemoveExtraWordsForHiglight(Search_Str);
            Regex RegExp;//= new Regex();

            if (Search_Str.Contains("\""))
                RegExp = new Regex(Search_Str.Replace("\"", "").Trim(), RegexOptions.IgnoreCase);
            //if (Search_Str.Contains("and"))
            //    RegExp = new Regex(Search_Str.Replace("and ", "").Trim(), RegexOptions.IgnoreCase);
            else
                RegExp = new Regex(Search_Str.Replace(" ", "|").Trim(), RegexOptions.IgnoreCase);


            return RegExp.Replace(InputTxt, new MatchEvaluator(ReplaceKeyWords));

        }
        public string ReplaceKeyWords(Match m)
        {
            return ("<span class=highlight>" + m.Value + "</span>");
        }
        protected void gv_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                GridViewRow row = gvLst.Rows[e.NewEditIndex];
                HiddenField hd = default(HiddenField);

                if ((row != null))
                {
                    hd = (HiddenField)row.FindControl("hdID");
                    int ID = Convert.ToInt32(hd.Value);

                    GetEBookIndexes(ID, int.Parse(HttpContext.Current.Items["ebookid"].ToString()));

                    divSearchResults.Style["Display"] = "none";
                    divContent.Style["Display"] = "";

                }
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("ManageUsers.aspx", "gv_RowEditing", ex.Message);
            }
        }
        protected void tvDept_ContextMenuItemClick(object sender, RadTreeViewContextMenuEventArgs e)
        {
            RadTreeNode clickedNode = e.Node;

            //switch (e.MenuItem.Value)
            //{
            //    //case "Edit":
            //    //    // Response.Redirect("AddDepartment.aspx?param=" + EncryptDecryptHelper.Encrypt(e.Node.Value.ToString()));
            //    //    break;
            //    //case "NewFolder":
            //    //    objusr.UserID = int.Parse(Session["MemberID"].ToString());
            //    //    objusr.ParentFolder = int.Parse(e.Node.Value);
            //    //    objusr.FolderName = "New Folder";
            //    //    objusr.CreatedBy = int.Parse(Session["MemberID"].ToString());
            //    //    int chk = objusr.InsertUserFolder();
            //    //    BindTelerikTree();

            //    //    break;
            //    //case "Delete":


            //    //    objusr.ModifiedBy = int.Parse(Session["MemberID"].ToString());
            //    //    objusr.DeleteUserFolder(int.Parse(e.Node.Value.ToString()));

            //    //    GetUsersParentFolders(int.Parse(Session["MemberID"].ToString()));
            //    //    GetUsersFolder(int.Parse(Session["MemberID"].ToString()), 0);
            //    //    BindTelerikTree();
            //    //    e.Node.Remove();

            //        //int chk = objdept.DeleteDepartment(int.Parse(e.Node.Value), int.Parse(Session["UserID"].ToString()));
            //        //BindTelerikTree(0);
            //        // e.Node.Remove();

            //        // }
            //        if (e.Node.Level == 1)
            //        {

            //            //int chk = objdept.DeleteDepartment(int.Parse(e.Node.Value), int.Parse(Session["UserID"].ToString()));
            //            //e.Node.Remove();

            //        }
            //        break;
            //}
        }
        protected void tvDept_NodeClick(object sender, RadTreeNodeEventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                // Session["SelectedNode"] = e.Node.Value.ToString();
                //GetSelecteddeptFiles(int.Parse(e.Node.Value.ToString()));

                //GetEBookIndexes(int.Parse(e.Node.Value.ToString()), int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())));
                GetEBookIndexes(int.Parse(e.Node.Value.ToString()), int.Parse(HttpContext.Current.Items["ebookid"].ToString()));
                //GetFolderItemsByFolder(int.Parse(e.Node.Value.ToString()));
                //lblSelectedFolder.Text = e.Node.Text.ToString();
                //ddlParentFolder.SelectedIndex = 0;
                //txtFolderName.Text = "";
                //divAddFolder.Style["Display"] = "none";
                //rfvParentFolder.Enabled = false;
                //rfvFolderName.Enabled = false;

                //divFolderItems.Style["Display"] = "";

                ////btnDeleteFolder.Visible = true;
                //lblSelectedFolderID.Text = e.Node.Value.ToString();
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Search(txtSearch.Text.Trim());
            }
            catch { }
        }
        protected void lnkBack_Click(object sender, EventArgs e)
        {
            try
            {
                divSearchResults.Style["Display"] = "";
                divContent.Style["Display"] = "none";
            }
            catch { }
        }
        
    }
}