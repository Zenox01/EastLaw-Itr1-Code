using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
namespace EastlawUI_v2
{
    public partial class MyFolders : System.Web.UI.Page
    {
        EastLawBL.Users objusr = new EastLawBL.Users();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["MemberID"] == null)
                    Response.Redirect("/member/member-login?req=" + HttpContext.Current.Request.Url.AbsolutePath, false);

                BindTelerikTree();
                GetUsersParentFolders(int.Parse(Session["MemberID"].ToString()));
                GetUsersFolder(int.Parse(Session["MemberID"].ToString()), 0);
                //GeDefaultData();
                if (Request.QueryString["fl"] != null)
                    GetFolderItemsByFolder(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["fl"].ToString())));

            }

        }
        void GeDefaultData()
        {
            try
            {
                //if (tvUserFolder.Nodes.Count > 0)
                //{
                //    GetFolderItemsByFolder(int.Parse(tvUserFolder.Nodes[0].Value));
                //    lblSelectedFolder.Text = tvUserFolder.SelectedNode.Text;
                //    ddlParentFolder.SelectedIndex = 0;
                //    txtFolderName.Text = "";
                //    divAddFolder.Style["Display"] = "none";
                //    rfvParentFolder.Enabled = false;
                //    rfvFolderName.Enabled = false;

                //    divFolderItems.Style["Display"] = "";

                //    btnDeleteFolder.Visible = true;
                //    lblSelectedFolderID.Text = tvUserFolder.SelectedNode.Value;
                //}
            }
            catch { }
        }
        void GetUsersParentFolders(int UserID)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objusr.GetUserParentFolderByUser(UserID);

                ddlParentFolder.DataValueField = "ID";
                ddlParentFolder.DataTextField = "FolderName";
                ddlParentFolder.DataSource = dt;
                ddlParentFolder.DataBind();
                ddlParentFolder.Items.Insert(0, new ListItem("Add as Parent", "0"));

            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("MyFolder.aspx", "GetUsersParentFolders", e.Message);
            }
        }
        void GetUsersFolder(int UserID, int parentid)
        {
            try
            {
                //tvUserFolder.Nodes.Clear();
                //DataTable dt = new DataTable();
                //dt = objusr.GetUserParentFolderByUser(UserID);
                //PopulateTreeView(dt, 0, null);
            }
            catch { }
        }
        private void PopulateTreeView(DataTable dtParent, int parentId, TreeNode treeNode)
        {
            //foreach (DataRow row in dtParent.Rows)
            //{
            //    TreeNode child = new TreeNode
            //    {
            //        Text = row["FolderName"].ToString(),
            //        Value = row["Id"].ToString()
            //    };
            //    if (parentId == 0)
            //    {
            //        tvUserFolder.Nodes.Add(child);
            //        DataTable dtChild = objusr.GetUserFolderByParent(int.Parse(child.Value));
            //        PopulateTreeView(dtChild, int.Parse(child.Value), child);
            //    }
            //    else
            //    {
            //        treeNode.ChildNodes.Add(child);
            //    }
            //}
        }

        private void BindTelerikTree()
        {
            try
            {
                tvDept.Nodes.Clear();
                RadTreeNode level0 = new RadTreeNode("Personal Folders","0");
                level0.ImageUrl = "/style/img/4Inbox.gif";
                tvDept.Nodes.Add(level0);
                

                DataTable dtlevel1 = objusr.GetUserParentFolderByUser(int.Parse(Session["MemberID"].ToString()));
                if (dtlevel1 != null)
                {
                    for (int l1 = 0; l1 < dtlevel1.Rows.Count; l1++)
                    {
                        string FirstNodeTitle="";
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
        protected void tvDept_NodeClick(object sender, RadTreeNodeEventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
               // Session["SelectedNode"] = e.Node.Value.ToString();
                //GetSelecteddeptFiles(int.Parse(e.Node.Value.ToString()));


                GetFolderItemsByFolder(int.Parse(e.Node.Value.ToString()));
                lblSelectedFolder.Text = e.Node.Text.ToString();
                ddlParentFolder.SelectedIndex = 0;
                txtFolderName.Text = "";
                divAddFolder.Style["Display"] = "none";
                rfvParentFolder.Enabled = false;
                rfvFolderName.Enabled = false;

                divFolderItems.Style["Display"] = "";

                //btnDeleteFolder.Visible = true;
                lblSelectedFolderID.Text = e.Node.Value.ToString();
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
                        GetUsersFolder(int.Parse(Session["MemberID"].ToString()), 0);
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
        void GetFolderItemsByFolder(int FolderID)
        {
            try
            {
                
                DataTable dt = new DataTable();
                if (FolderID != 0)
                {
                    dt = objusr.GetUserFolderItemsByFolder(FolderID);
                    for (int a = 0; a < dt.Rows.Count; a++)
                    {
                        if (dt.Rows[a]["ItemType"].ToString() == "Cases")
                        {
                            if (!string.IsNullOrEmpty(dt.Rows[a]["CitationRef"].ToString()))
                                dt.Rows[a]["Court"] = "Reported as: " + dt.Rows[a]["CitationRef"].ToString() + " | Court: " + dt.Rows[a]["Court"].ToString();
                            else
                                dt.Rows[a]["Court"] = "Reported as: " + dt.Rows[a]["Citation"].ToString() + " | Court: " + dt.Rows[a]["Court"].ToString();
                        }
                    }
                }

                divAddFolder.Style["Display"] = "none";
                rfvParentFolder.Enabled = false;
                rfvFolderName.Enabled = false;

                divFolderItems.Style["Display"] = "";

                dt.AcceptChanges();
                gvLst.DataSource = dt;
                gvLst.DataBind();

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
                objusr.UserID = int.Parse(Session["MemberID"].ToString());
                objusr.ParentFolder = int.Parse(ddlParentFolder.SelectedValue);
                objusr.FolderName = txtFolderName.Text.Trim();
                objusr.CreatedBy = int.Parse(Session["MemberID"].ToString());
                int chk = objusr.InsertUserFolder();
                if (chk > 0)
                {
                    lblMsg.Text = "Folder Created..";
                    lblMsg.ForeColor = System.Drawing.Color.Green;

                    ddlParentFolder.SelectedIndex = 0;
                    txtFolderName.Text = "";
                }
                else
                {
                    lblMsg.Text = "Folder Creation Error..";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnCreateFolder_Click(object sender, EventArgs e)
        {
            AddNewFolder();
            GetUsersParentFolders(int.Parse(Session["MemberID"].ToString()));
            GetUsersFolder(int.Parse(Session["MemberID"].ToString()), 0);

        }

        protected void btnAddFolder_Click(object sender, EventArgs e)
        {
            divAddFolder.Style["Display"] = "";
            rfvParentFolder.Enabled = true;
            rfvFolderName.Enabled = true;

            divFolderItems.Style["Display"] = "none";
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ddlParentFolder.SelectedIndex = 0;
            txtFolderName.Text = "";
            divAddFolder.Style["Display"] = "none";
            rfvParentFolder.Enabled = false;
            rfvFolderName.Enabled = false;
        }

        protected void tvUserFolder_SelectedNodeChanged(object sender, EventArgs e)
        {
            try
            {
                //GetFolderItemsByFolder(int.Parse(tvUserFolder.SelectedNode.Value));
                //lblSelectedFolder.Text = tvUserFolder.SelectedNode.Text;
                //ddlParentFolder.SelectedIndex = 0;
                //txtFolderName.Text = "";
                //divAddFolder.Style["Display"] = "none";
                //rfvParentFolder.Enabled = false;
                //rfvFolderName.Enabled = false;

                //divFolderItems.Style["Display"] = "";

                //btnDeleteFolder.Visible = true;
                //lblSelectedFolderID.Text = tvUserFolder.SelectedNode.Value;

            }
            catch (Exception ex)
            {

            }
        }

        protected void gvLst_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLst.PageIndex = e.NewPageIndex;
            BindTelerikTree();
            //GetFolderItemsByFolder(int.Parse(tvUserFolder.SelectedNode.Value));
        }
        protected void gvLst_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                ImageButton imgBtn = default(ImageButton);
                string script = null;
                script = "";

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    imgBtn = (ImageButton)e.Row.Controls[0].FindControl("ibtnDelete");
                    script = "javascript:return(confirm_delete())";
                    imgBtn.Attributes.Add("onclick", script);
                }
            }
            catch (Exception ex)
            {

            }
        }
        protected void gvLst_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                GridViewRow row = gvLst.Rows[e.RowIndex];
                HiddenField hd = default(HiddenField);

                if ((row != null))
                {
                    hd = (HiddenField)row.FindControl("hdID");
                    int ID = Convert.ToInt32(hd.Value);

                    int chk = objusr.DeleteUserFolderItem(ID, int.Parse(Session["MemberID"].ToString()));
                    if (chk > 0)
                    {
                        //divSuccess.Style["Display"] = "";
                        //divError.Style["Display"] = "none";

                    }
                    else
                    {
                        //divSuccess.Style["Display"] = "none";
                        //divError.Style["Display"] = "";
                    }
                }
                gvLst.EditIndex = -1;
                BindTelerikTree();
                //GetFolderItemsByFolder(int.Parse(tvUserFolder.SelectedNode.Value));

            }
            catch (Exception ex)
            {

            }
        }

        protected void btnDeleteFolder_Click(object sender, EventArgs e)
        {
            try
            {
                //objusr.ModifiedBy = int.Parse(Session["MemberID"].ToString());
                //objusr.DeleteUserFolder(int.Parse(tvUserFolder.SelectedNode.Value));

                //GetUsersParentFolders(int.Parse(Session["MemberID"].ToString()));
                //GetUsersFolder(int.Parse(Session["MemberID"].ToString()), 0);
                //gvLst.DataSource = null;
                //gvLst.DataBind();
            }
            catch { }
        }
    }
}