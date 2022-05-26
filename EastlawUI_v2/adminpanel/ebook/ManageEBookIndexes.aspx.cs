using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using System.IO;
using GemBox.Document;
using GemBox.Document.Tables;
using System.Text.RegularExpressions;
using System.Text;
namespace EastlawUI_v2.adminpanel.ebook
{
    public partial class ManageEBookIndexes : System.Web.UI.Page
    {
        EastLawBL.EBook objeb = new EastLawBL.EBook();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!ValidateUserGroup.ValidateGroup(int.Parse(Session["UserTypeID"].ToString()), ValidateUserGroup.getPageName(Request.Url.AbsolutePath)))
                    Response.Redirect("NotAuthorize.aspx");
                GetEBooks();
                BindTelerikTree();
                if (Request.QueryString["param"] != null)
                {
                    ddlEBook.SelectedValue = EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString());
                    ddlEBook.Enabled = false;

                    GetEBookIndexes(0, int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())));

                }
            }
        }
        void GetEBooks()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objeb.GetEBook(0);
                ddlEBook.DataValueField = "ID";
                ddlEBook.DataTextField = "Title";
                ddlEBook.DataSource = dt;
                ddlEBook.DataBind();
            }
            catch { }
        }
        void GetEBookIndexes(int ID,int EBookID)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objeb.GetEBookIndex(ID,EBookID);
                if (ID == 0)
                {
                    dt.Columns.Add("strActive");
                    
                    for (int a = 0; a < dt.Rows.Count; a++)
                    {
                        if (dt.Rows[a]["Active"].ToString() == "1")
                            dt.Rows[a]["strActive"] = "Yes";
                        else
                            dt.Rows[a]["strActive"] = "No";

                      
                    }
                    dt.AcceptChanges();
                    gv.DataSource = dt;
                    gv.DataBind();

                    //ddlParentIndex.DataValueField = "ID";
                    //ddlParentIndex.DataTextField = "";
                    //ddlParentIndex.DataSource = dt;
                    //ddlParentIndex.DataBind();
                    //ddlParentIndex.Items.Insert(0, new ListItem("N/A", "0"));
                }
                else
                {
                    lblID.Text = dt.Rows[0]["ID"].ToString();
                    ddlEBook.SelectedValue = dt.Rows[0]["EBookID"].ToString();
                    txtIndexGroup.Text = dt.Rows[0]["IndexGroup"].ToString();
                    txtIndexTitle.Text = dt.Rows[0]["IndexTitle"].ToString();
                    txtPageNo.Text = dt.Rows[0]["PageNo"].ToString();
                    txtSortOrder.Text = dt.Rows[0]["SortOrder"].ToString();
                    editorOIndexContent.Content = dt.Rows[0]["IndexContent"].ToString();
                    txtIndexGroupTag.Text = dt.Rows[0]["IndexGroupTag"].ToString();
                    if (!string.IsNullOrEmpty(dt.Rows[0]["DataMode"].ToString()))
                        ddlDataMode.SelectedValue = dt.Rows[0]["DataMode"].ToString();
                    lblFilename.Text = dt.Rows[0]["FleName"].ToString();
                   // ddlParentIndex.SelectedValue = dt.Rows[0]["ParentIndex"].ToString();
                    if (dt.Rows[0]["Active"].ToString() == "1")
                        chkActive.Checked = true;
                    else
                        chkActive.Checked = false;

                    divSuccess.Style["Display"] = "none";
                    divError.Style["Display"] = "none";
                }
            }
            catch (Exception e)
            {
                
            }
        }
        private void BindTelerikTree()
        {
            try
            {
                tvDept.Nodes.Clear();
                RadTreeNode level0 = new RadTreeNode("Index", "0");
                //level0.ImageUrl = "/style/img/4Inbox.gif";
                tvDept.Nodes.Add(level0);


                DataTable dtlevel1 = objeb.GetEBookParentIndex(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())),0);
                if (dtlevel1 != null)
                {
                    for (int l1 = 0; l1 < dtlevel1.Rows.Count; l1++)
                    {
                        string FirstNodeTitle = "";
                            FirstNodeTitle = dtlevel1.Rows[l1]["SortOrder"].ToString()+" - "+dtlevel1.Rows[l1]["IndexTitle"].ToString();
                        RadTreeNode level1 = new RadTreeNode(FirstNodeTitle, dtlevel1.Rows[l1]["ID"].ToString());
                       // level1.ImageUrl = "/style/img/4Inbox.gif";
                        level0.Nodes.Add(level1);


                        DataTable dtlevel2 = objeb.GetEBookParentIndex(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())),int.Parse(dtlevel1.Rows[l1]["ID"].ToString()));
                        if (dtlevel2 != null)
                        {
                            for (int l2 = 0; l2 < dtlevel2.Rows.Count; l2++)
                            {
                                string SecondNodeTitle = "";

                                SecondNodeTitle = dtlevel2.Rows[l2]["SortOrder"].ToString() + " - " + dtlevel2.Rows[l2]["IndexTitle"].ToString();

                                RadTreeNode level2 = new RadTreeNode(SecondNodeTitle, dtlevel2.Rows[l2]["ID"].ToString());
                                //level2.ImageUrl = "/style/img/folder.gif";
                                level1.Nodes.Add(level2);

                                DataTable dtlevel3 = objeb.GetEBookParentIndex(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())),int.Parse(dtlevel2.Rows[l2]["ID"].ToString()));
                                if (dtlevel3 != null)
                                {
                                    for (int l3 = 0; l3 < dtlevel3.Rows.Count; l3++)
                                    {
                                        string ThirdNodeTitle = "";

                                        ThirdNodeTitle = dtlevel3.Rows[l3]["SortOrder"].ToString() + " - " + dtlevel3.Rows[l3]["IndexTitle"].ToString();
                                        RadTreeNode level3 = new RadTreeNode(ThirdNodeTitle, dtlevel3.Rows[l3]["ID"].ToString());
                                        level2.Nodes.Add(level3);

                                        DataTable dtlevel4 = objeb.GetEBookParentIndex(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())), int.Parse(dtlevel3.Rows[l3]["ID"].ToString()));
                                        if (dtlevel4 != null)
                                        {
                                            for (int l4 = 0; l4 < dtlevel4.Rows.Count; l4++)
                                            {
                                                string ForthNodeTitle = "";

                                                ForthNodeTitle = dtlevel4.Rows[l4]["SortOrder"].ToString() + " - " + dtlevel4.Rows[l4]["IndexTitle"].ToString();
                                                RadTreeNode level4 = new RadTreeNode(ForthNodeTitle, dtlevel4.Rows[l4]["ID"].ToString());
                                                level3.Nodes.Add(level4);

                                                DataTable dtlevel5 = objeb.GetEBookParentIndex(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())), int.Parse(dtlevel4.Rows[l4]["ID"].ToString()));
                                                if (dtlevel5 != null)
                                                {
                                                    for (int l5 = 0; l5 < dtlevel5.Rows.Count; l5++)
                                                    {
                                                        string FifthNodeTitle = "";

                                                        FifthNodeTitle = dtlevel5.Rows[l5]["SortOrder"].ToString() + " - " + dtlevel5.Rows[l5]["IndexTitle"].ToString();
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
                tvDept.ExpandAllNodes();
            }
            catch (Exception Ex) { throw Ex; }
        }
        void SaveRecord()
        {
            try
            {
                string Img = "";

                if (fuploader.HasFile)
                {
                    string destDir = Server.MapPath("/store/ebook/upload/");

                    string FileName = Path.GetFileName(fuploader.PostedFile.FileName);
                    string destPath = Path.Combine(destDir, FileName);
                    fuploader.SaveAs(destPath);

                    Img = fuploader.FileName;


                    ComponentInfo.SetLicense("DAAN-ECJU-1F8U-002M");

                    DocumentModel document = DocumentModel.Load(Server.MapPath("/store/ebook/upload/"+Img));

                    // In order to achieve the conversion of a loaded Word file to PDF,
                    // or to some other Word format,
                    // we just need to save a DocumentModel object to desired output file format.

                    document.Save(Server.MapPath("/store/ebook/pdf/Convert.pdf"));

                }
                objeb.EBookID =int.Parse(ddlEBook.SelectedValue);
                objeb.IndexGroup = txtIndexGroup.Text.Trim();
                objeb.IndexTitle = txtIndexTitle.Text.Trim();
                objeb.PageNo = txtPageNo.Text.Trim();
                objeb.IndexContent = editorOIndexContent.Content;
                objeb.SortOrder = int.Parse(txtSortOrder.Text);
              
                if (chkActive.Checked == true)
                    objeb.Active = 1;
                else
                    objeb.Active = 0;

                objeb.CreatedBy = int.Parse(Session["UserID"].ToString());
                int chk = objeb.InsertEBookIndex();
                if (chk > 0)
                {
                    divSuccess.Style["Display"] = "";
                    divError.Style["Display"] = "none";
                    ClearFields();
                    GetEBookIndexes(0, int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())));
                }
                else
                {
                    divSuccess.Style["Display"] = "none";
                    divError.Style["Display"] = "";
                }
            }
            catch (Exception e)
            {
                
            }
        }
        void ClearFields()
        {
            ddlEBook.SelectedIndex = 0;
            txtIndexGroup.Text = "";
            txtIndexTitle.Text = "";
            txtPageNo.Text = "";
            txtSortOrder.Text = "0";
            editorOIndexContent.Content = "";
            lblID.Text = "0";
            chkActive.Checked = false;
            
            
        }
        void EditRecord(int ID)
        {
            try
            {
                string Img = "";
                string outfilename="";
                StringBuilder sb = new StringBuilder();

                if (fuploader.HasFile)
                {
                    string destDir = Server.MapPath("/store/ebook/upload/");

                    string FileName = Path.GetFileName(fuploader.PostedFile.FileName);
                    string destPath = Path.Combine(destDir, EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString()) + "_" + FileName);
                    fuploader.SaveAs(destPath);

                    Img = EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString()) + "_" + fuploader.FileName;


                    ComponentInfo.SetLicense("DAAN-ECJU-1F8U-002M");

                    DocumentModel document = DocumentModel.Load(Server.MapPath("/store/ebook/upload/" + Img));

                    // In order to achieve the conversion of a loaded Word file to PDF,
                    // or to some other Word format,
                    // we just need to save a DocumentModel object to desired output file format.
                    outfilename = EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString()) + "_" + fuploader.FileName.ToString().Replace(" ", "_") + ".pdf";
                    document.Save(Server.MapPath("/store/ebook/pdf/" + outfilename));



                    //foreach (Paragraph paragraph in document.GetChildElements(true, ElementType.Paragraph))
                    //{
                    //    foreach (Run run in paragraph.GetChildElements(true, ElementType.Run))
                    //    {
                    //        bool isBold = run.CharacterFormat.Bold;
                    //        string text = run.Text;

                    //        sb.AppendFormat("<p>"+text+"</b>");
                    //        //sb.AppendFormat("{0}{1}{2}", isBold ? "<b>" : "", text, isBold ? "</b>" : "");
                    //    }
                    //    sb.AppendLine();
                    //}


                    foreach (Paragraph paragraph in document.GetChildElements(true, ElementType.Paragraph))
                    {
                        if (!string.IsNullOrEmpty(paragraph.Content.ToString()))
                        {
                            sb.AppendFormat("<p> {0} </p>", paragraph.Content.ToString());
                            sb.AppendLine();
                        }
                    }

                }
                else
                {
                    Img = lblFilename.Text;
                }
                objeb.EBookID = int.Parse(ddlEBook.SelectedValue);
                objeb.IndexGroup = txtIndexGroup.Text.Trim();
                objeb.IndexTitle = txtIndexTitle.Text.Trim();
                objeb.PageNo = txtPageNo.Text.Trim();
                if (ddlDataMode.SelectedValue == "Content")
                objeb.IndexContent = editorOIndexContent.Content;
                else
                    objeb.IndexContent = sb.ToString();
                objeb.SortOrder = int.Parse(txtSortOrder.Text);
                objeb.IndexGroupTag = txtIndexGroupTag.Text.Trim();
                objeb.DataMode = ddlDataMode.SelectedValue;
                objeb.FleName = Img;
                objeb.OutputFle = outfilename;
                if (chkActive.Checked == true)
                    objeb.Active = 1;
                else
                    objeb.Active = 0;

                objeb.ModifiedBy = int.Parse(Session["UserID"].ToString());
                int chk = objeb.EditEBookIndex(ID);
                if (chk > 0)
                {
                    divSuccess.Style["Display"] = "";
                    divError.Style["Display"] = "none";
                    ClearFields();
                    GetEBookIndexes(0, int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())));
                }
                else
                {
                    divSuccess.Style["Display"] = "none";
                    divError.Style["Display"] = "";
                }
            }
            catch (Exception e)
            {
                Response.Write(e.Message);
            }
        }
        protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gv.PageIndex = e.NewPageIndex;
                GetEBookIndexes(0, int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())));
            }
            catch (Exception ex)
            {
                
            }
        }

        protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                GridViewRow row = gv.Rows[e.RowIndex];
                HiddenField hd = default(HiddenField);

                if ((row != null))
                {
                    hd = (HiddenField)row.FindControl("hdID");
                    int ID = Convert.ToInt32(hd.Value);

                    int chk = objeb.DeleteEBookIndex(ID, int.Parse(Session["UserID"].ToString()));
                    if (chk > 0)
                    {
                        divSuccess.Style["Display"] = "";
                        divError.Style["Display"] = "none";

                    }
                    else
                    {
                        divSuccess.Style["Display"] = "none";
                        divError.Style["Display"] = "";
                    }
                }
                gv.EditIndex = -1;
                GetEBookIndexes(0, int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())));

            }
            catch (Exception ex)
            {
                
            }
        }

        protected void gv_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                GridViewRow row = gv.Rows[e.NewEditIndex];
                HiddenField hd = default(HiddenField);

                if ((row != null))
                {
                    hd = (HiddenField)row.FindControl("hdID");
                    int ID = Convert.ToInt32(hd.Value);
                    GetEBookIndexes(ID, int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())));
                    


                }
            }
            catch (Exception ex)
            {
                
            }
        }

        protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //try
            //{
            //    ImageButton imgBtn = default(ImageButton);
            //    string script = null;
            //    script = "";

            //    if (e.Row.RowType == DataControlRowType.DataRow)
            //    {
            //        imgBtn = (ImageButton)e.Row.Controls[0].FindControl("ibtnDelete");
            //        script = "javascript:return(confirm_delete())";
            //        imgBtn.Attributes.Add("onclick", script);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ExceptionHandling.SendErrorReport("ManagePlans.aspx", "gv_RowDataBound", ex.Message);
            //}
        }
        protected void tvDept_NodeEdit(object sender, RadTreeNodeEditEventArgs e)
        {
            RadTreeNode nodeEdited = e.Node;
            string newText = e.Text;
            nodeEdited.Text = newText;
            objeb.EditEBookIndexTitle(int.Parse(e.Node.Value.ToString()),newText.ToString(),int.Parse(Session["UserID"].ToString()));
            BindTelerikTree();
        }
        protected void tvDept_NodeClick(object sender, RadTreeNodeEventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                // Session["SelectedNode"] = e.Node.Value.ToString();
                //GetSelecteddeptFiles(int.Parse(e.Node.Value.ToString()));

                GetEBookIndexes(int.Parse(e.Node.Value.ToString()), int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())));
                divContent.Style["Display"] = "";
                divFile.Style["Display"] = "none";

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
        protected void tvDept_ContextMenuItemClick(object sender, RadTreeViewContextMenuEventArgs e)
        {
            RadTreeNode clickedNode = e.Node;
            switch (e.MenuItem.Value)
            {
                case "Edit":
                    // Response.Redirect("AddDepartment.aspx?param=" + EncryptDecryptHelper.Encrypt(e.Node.Value.ToString()));
                    break;
                case "NewIndex":


                    objeb.EBookID = int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString()));
                    objeb.ParentIndex = int.Parse(e.Node.Value);
                    objeb.IndexGroup = "";
                    objeb.IndexTitle = "New Index";
                    objeb.PageNo = "";
                    objeb.IndexContent = "";
                    objeb.SortOrder = 0;
                    objeb.Active = 1;
                    objeb.CreatedBy = int.Parse(Session["UserID"].ToString());
                    int chk = objeb.InsertEBookIndex();

                    //objusr.UserID = int.Parse(Session["MemberID"].ToString());
                    //objusr.ParentFolder = int.Parse(e.Node.Value);
                    //objusr.FolderName = "New Folder";
                    //objusr.CreatedBy = int.Parse(Session["MemberID"].ToString());
                    //int chk = objusr.InsertUserFolder();
                    BindTelerikTree();

                    break;
                case "Delete":


                    //objusr.ModifiedBy = int.Parse(Session["MemberID"].ToString());
                    //objusr.DeleteUserFolder(int.Parse(e.Node.Value.ToString()));

                    //GetUsersParentFolders(int.Parse(Session["MemberID"].ToString()));
                    //GetUsersFolder(int.Parse(Session["MemberID"].ToString()), 0);
                    //BindTelerikTree();
                    //e.Node.Remove();
                    int chk1 = objeb.DeleteEBookIndex(int.Parse(e.Node.Value), int.Parse(Session["UserID"].ToString()));
                    BindTelerikTree();
                    e.Node.Remove();

                    // }
                    //if (e.Node.Level == 1)
                    //{

                    //    //int chk = objdept.DeleteDepartment(int.Parse(e.Node.Value), int.Parse(Session["UserID"].ToString()));
                    //    //e.Node.Remove();

                    //}
                    break;
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //if (lblID.Text == "0")
                //    SaveRecord();
                //else
                    EditRecord(int.Parse(lblID.Text));
            }
            catch (Exception ex)
            {
                
            }
        }
        protected void btnRefreshIndex_Click(object sender, EventArgs e)
        {
            try
            {
                BindTelerikTree();
            }
            catch (Exception ex)
            {

            }
        }

        protected void ddlDataMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlDataMode.SelectedValue == "Content")
                {
                    divContent.Style["Display"] = "";
                    divFile.Style["Display"] = "none";
                }
                else if (ddlDataMode.SelectedValue == "Word File")
                {
                    divContent.Style["Display"] = "none";
                    divFile.Style["Display"] = "";
                }
            }
            catch { }
        }
    }
}