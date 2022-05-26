using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace EastlawUI_v2.m
{
    public partial class MyFolder : System.Web.UI.Page
    {
        EastLawBL.Users objusr = new EastLawBL.Users();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GetUsersParentFolders(int.Parse(Session["MemberID"].ToString()));
                //GetUsersFolder(int.Parse(Session["MemberID"].ToString()), 0);
               // GeDefaultData();
            }

        }
        //void GeDefaultData()
        //{
        //    try
        //    {
        //        if (tvUserFolder.Nodes.Count > 0)
        //        {
        //           // GetFolderItemsByFolder(int.Parse(tvUserFolder.Nodes[0].Value));
        //            lblSelectedFolder.Text = tvUserFolder.SelectedNode.Text;
        //            ddlParentFolder.SelectedIndex = 0;
        //            txtFolderName.Text = "";
        //            divAddFolder.Style["Display"] = "none";
        //            rfvParentFolder.Enabled = false;
        //            rfvFolderName.Enabled = false;

        //            divFolderItems.Style["Display"] = "";

        //            btnDeleteFolder.Visible = true;
        //            lblSelectedFolderID.Text = tvUserFolder.SelectedNode.Value;
        //        }
        //    }
        //    catch { }
        //}
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
        //void GetUsersFolder(int UserID, int parentid)
        //{
        //    try
        //    {
        //        tvUserFolder.Nodes.Clear();
        //        DataTable dt = new DataTable();
        //        dt = objusr.GetUserParentFolderByUser(UserID);
        //        PopulateTreeView(dt, 0, null);
        //    }
        //    catch { }
        //}
        //private void PopulateTreeView(DataTable dtParent, int parentId, TreeNode treeNode)
        //{
        //    foreach (DataRow row in dtParent.Rows)
        //    {
        //        TreeNode child = new TreeNode
        //        {
        //            Text = row["FolderName"].ToString(),
        //            Value = row["Id"].ToString()
        //        };
        //        if (parentId == 0)
        //        {
        //            tvUserFolder.Nodes.Add(child);
        //            DataTable dtChild = objusr.GetUserFolderByParent(int.Parse(child.Value));
        //            PopulateTreeView(dtChild, int.Parse(child.Value), child);
        //        }
        //        else
        //        {
        //            treeNode.ChildNodes.Add(child);
        //        }
        //    }
        //}
        void GetFolderItemsByFolder(int FolderID)
        {
            try
            {
                DataTable dt = new DataTable();
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
           // GetUsersFolder(int.Parse(Session["MemberID"].ToString()), 0);

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
                if(Request.QueryString["mfd"] != null)
                GetFolderItemsByFolder(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["mfd"].ToString())));
               // lblSelectedFolder.Text = tvUserFolder.SelectedNode.Text;
                ddlParentFolder.SelectedIndex = 0;
                txtFolderName.Text = "";
                divAddFolder.Style["Display"] = "none";
                rfvParentFolder.Enabled = false;
                rfvFolderName.Enabled = false;

                divFolderItems.Style["Display"] = "";

                btnDeleteFolder.Visible = true;
              //  lblSelectedFolderID.Text = tvUserFolder.SelectedNode.Value;

            }
            catch (Exception ex)
            {

            }
        }

        protected void gvLst_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLst.PageIndex = e.NewPageIndex;
            if (Request.QueryString["mfd"] != null)
                GetFolderItemsByFolder(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["mfd"].ToString())));
           // GetFolderItemsByFolder(int.Parse(tvUserFolder.SelectedNode.Value));
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
                if (Request.QueryString["mfd"] != null)
                    GetFolderItemsByFolder(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["mfd"].ToString())));
               // GetFolderItemsByFolder(int.Parse(tvUserFolder.SelectedNode.Value));

            }
            catch (Exception ex)
            {

            }
        }

        protected void btnDeleteFolder_Click(object sender, EventArgs e)
        {
            try
            {
                objusr.ModifiedBy = int.Parse(Session["MemberID"].ToString());
                if (Request.QueryString["mfd"] != null)
                    objusr.DeleteUserFolder(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["mfd"].ToString())));
               // objusr.DeleteUserFolder(int.Parse(tvUserFolder.SelectedNode.Value));

                GetUsersParentFolders(int.Parse(Session["MemberID"].ToString()));
               // GetUsersFolder(int.Parse(Session["MemberID"].ToString()), 0);
                gvLst.DataSource = null;
                gvLst.DataBind();
            }
            catch { }
        }
    }
}