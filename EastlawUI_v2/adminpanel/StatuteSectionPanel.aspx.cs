using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
namespace EastlawUI_v2.adminpanel
{
    public partial class StatuteSectionPanel : System.Web.UI.Page
    {
        EastLawBL.Statutes objs = new EastLawBL.Statutes();
        EastLawBL.Cases objcase = new EastLawBL.Cases();
        EastLawBL.Journals objJo = new EastLawBL.Journals();
        string strMsg = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["UserID"] == null)
                {
                    Response.Redirect("default.aspx");
                }
                if (!ValidateUserGroup.ValidateGroup(int.Parse(Session["UserTypeID"].ToString()), ValidateUserGroup.getPageName(Request.Url.AbsolutePath)))
                    Response.Redirect("NotAuthorize.aspx");
                GetStatutes();
                GetJournals();
                if (Request.QueryString["param"] != null)
                {
                    ddlStatute.SelectedValue = EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString());
                    ddlStatute.Enabled = false;

                    GetStatuteIndexes(0, int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())));

                }
            }
        }
        void GetStatutes()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objs.GetActiveStatutesLessInfo();
                ddlStatute.DataValueField = "ID";
                ddlStatute.DataTextField = "Title";
                ddlStatute.DataSource = dt;
                ddlStatute.DataBind();

                ddlStatute.Items.Insert(0, new ListItem("Select", "0"));
            }
            catch { }
        }
        void GetStatuteIndexes(int ID, int StatuteID)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objs.GetStatutesSOAIndex(ID, StatuteID);
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
                    ddlStatute.SelectedValue = dt.Rows[0]["StatuteID"].ToString();
                    txtElementData.Text = dt.Rows[0]["ElementData"].ToString();
                    txtSortOrder.Text = dt.Rows[0]["SortOrder"].ToString();
                    editorIndexContent.Content = dt.Rows[0]["IndexContent"].ToString();
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
        void GetSectionSOA(int SOAID)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objs.GetStatutesSOATaggingbySOAID(SOAID);
               
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

                    
               
            }
            catch (Exception e)
            {

            }
        }
        private void BindTelerikTree(int StatuteID)
        {
            try
            {
                tvDept.Nodes.Clear();
                RadTreeNode level0 = new RadTreeNode("Index", "0");
                //level0.ImageUrl = "/style/img/4Inbox.gif";
                tvDept.Nodes.Add(level0);


                DataTable dtlevel1 = objs.GetStatutesSOAParentIndex(StatuteID, 0);
                if (dtlevel1 != null)
                {
                    for (int l1 = 0; l1 < dtlevel1.Rows.Count; l1++)
                    {
                        string FirstNodeTitle = "";
                        FirstNodeTitle = dtlevel1.Rows[l1]["ElementData"].ToString();
                        RadTreeNode level1 = new RadTreeNode(FirstNodeTitle, dtlevel1.Rows[l1]["ID"].ToString());
                        // level1.ImageUrl = "/style/img/4Inbox.gif";
                        level0.Nodes.Add(level1);


                        DataTable dtlevel2 = objs.GetStatutesSOAParentIndex(StatuteID, int.Parse(dtlevel1.Rows[l1]["ID"].ToString()));
                        if (dtlevel2 != null)
                        {
                            for (int l2 = 0; l2 < dtlevel2.Rows.Count; l2++)
                            {
                                string SecondNodeTitle = "";

                                SecondNodeTitle = dtlevel2.Rows[l2]["ElementData"].ToString();

                                RadTreeNode level2 = new RadTreeNode(SecondNodeTitle, dtlevel2.Rows[l2]["ID"].ToString());
                                //level2.ImageUrl = "/style/img/folder.gif";
                                level1.Nodes.Add(level2);

                                DataTable dtlevel3 = objs.GetStatutesSOAParentIndex(StatuteID, int.Parse(dtlevel2.Rows[l2]["ID"].ToString()));
                                if (dtlevel3 != null)
                                {
                                    for (int l3 = 0; l3 < dtlevel3.Rows.Count; l3++)
                                    {
                                        string ThirdNodeTitle = "";

                                        ThirdNodeTitle = dtlevel3.Rows[l3]["ElementData"].ToString();
                                        RadTreeNode level3 = new RadTreeNode(ThirdNodeTitle, dtlevel3.Rows[l3]["ID"].ToString());
                                        level2.Nodes.Add(level3);

                                        DataTable dtlevel4 = objs.GetStatutesSOAParentIndex(StatuteID, int.Parse(dtlevel3.Rows[l3]["ID"].ToString()));
                                        if (dtlevel4 != null)
                                        {
                                            for (int l4 = 0; l4 < dtlevel4.Rows.Count; l4++)
                                            {
                                                string ForthNodeTitle = "";

                                                ForthNodeTitle = dtlevel4.Rows[l4]["ElementData"].ToString();
                                                RadTreeNode level4 = new RadTreeNode(ForthNodeTitle, dtlevel4.Rows[l4]["ID"].ToString());
                                                level3.Nodes.Add(level4);

                                                DataTable dtlevel5 = objs.GetStatutesSOAParentIndex(StatuteID, int.Parse(dtlevel4.Rows[l4]["ID"].ToString()));
                                                if (dtlevel5 != null)
                                                {
                                                    for (int l5 = 0; l5 < dtlevel5.Rows.Count; l5++)
                                                    {
                                                        string FifthNodeTitle = "";

                                                        FifthNodeTitle = dtlevel5.Rows[l5]["ElementData"].ToString();
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
            catch (Exception Ex)
            { 
                
            }
        }
        private void GetAllIndexByIndex(int StatuteID)
        {
            try
            {
                DataTable dt = objs.GetStatutesSOAIndex(0, StatuteID);

                ddlISourceIndex.DataValueField = "ID";
                ddlISourceIndex.DataTextField = "ElementData";
                ddlISourceIndex.DataSource = dt;
                ddlISourceIndex.DataBind();

                ddlISourceIndex.Items.Insert(0, new ListItem("Select", "0"));


                ddlIDestinationIndex.DataValueField = "ID";
                ddlIDestinationIndex.DataTextField = "ElementData";
                ddlIDestinationIndex.DataSource = dt;
                ddlIDestinationIndex.DataBind();

                ddlIDestinationIndex.Items.Insert(0, new ListItem("Select", "0"));
            }
            catch (Exception Ex)
            {

            }
        }
        void SaveRecord()
        {
            try
            {
                //objs.EBookID = int.Parse(ddlEBook.SelectedValue);
                //objeb.IndexGroup = txtIndexGroup.Text.Trim();
                //objeb.IndexTitle = txtIndexTitle.Text.Trim();
                //objeb.PageNo = txtPageNo.Text.Trim();
                //objeb.IndexContent = editorOIndexContent.Content;
                //objeb.SortOrder = int.Parse(txtSortOrder.Text);

                //if (chkActive.Checked == true)
                //    objeb.Active = 1;
                //else
                //    objeb.Active = 0;

                //objeb.CreatedBy = int.Parse(Session["UserID"].ToString());
               // int chk = objs.InsertStatutesSOA(int.Parse(ddlStatute.SelectedValue)
                //if (chk > 0)
                //{
                //    divSuccess.Style["Display"] = "";
                //    divError.Style["Display"] = "none";
                //    ClearFields();
                //    GetEBookIndexes(0, int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())));
                //}
                //else
                //{
                //    divSuccess.Style["Display"] = "none";
                //    divError.Style["Display"] = "";
                //}
            }
            catch (Exception e)
            {

            }
        }
        void ClearFields()
        {
            ddlStatute.SelectedIndex = 0;
            txtElementData.Text = "";
          
            txtSortOrder.Text = "0";
            editorIndexContent.Content = "";
            
            lblID.Text = "0";
            chkActive.Checked = false;


        }
        void EditRecord(int ID)
        {
            try
            {
               
                int act=0;
                if (chkActive.Checked == true)
                    act = 1;
                else
                    act = 0;

                
                int chk = objs.EditStatutesSOA(ID, txtElementData.Text.Trim(), int.Parse(txtSortOrder.Text.Trim()), editorIndexContent.Content, act, int.Parse(Session["UserID"].ToString()), ref strMsg);
                if (chk > 0)
                {
                    divSuccess.Style["Display"] = "";
                    divError.Style["Display"] = "none";
                   // ClearFields();
                    //GetStatuteIndexes(0, int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())));
                    BindTelerikTree(int.Parse(ddlStatute.SelectedValue));
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
        protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gv.PageIndex = e.NewPageIndex;
                GetStatuteIndexes(0, int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())));
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

                    int chk = objs.DeleteStatutesSOA(ID, int.Parse(Session["UserID"].ToString()));
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
                GetStatuteIndexes(0, int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())));

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
                    GetStatuteIndexes(ID, int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())));



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
            objs.EditStatutesSOA_without_content(int.Parse(e.Node.Value.ToString()), newText.ToString(), int.Parse(Session["UserID"].ToString()),ref strMsg);
            BindTelerikTree(int.Parse(ddlStatute.SelectedValue));
        }
        protected void tvDept_NodeClick(object sender, RadTreeNodeEventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                // Session["SelectedNode"] = e.Node.Value.ToString();
                //GetSelecteddeptFiles(int.Parse(e.Node.Value.ToString()));

                GetStatuteIndexes(int.Parse(e.Node.Value.ToString()), int.Parse(ddlStatute.SelectedValue));
                GetSectionSOA(int.Parse(e.Node.Value.ToString()));

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


                    //objeb.EBookID = int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString()));
                    //objeb.ParentIndex = int.Parse(e.Node.Value);
                    //objeb.IndexGroup = "";
                    //objeb.IndexTitle = "New Index";
                    //objeb.PageNo = "";
                    //objeb.IndexContent = "";
                    //objeb.SortOrder = 0;
                    //objeb.Active = 1;
                    //objeb.CreatedBy = int.Parse(Session["UserID"].ToString());
                    //int chk = objeb.InsertEBookIndex();

                    int chk = objs.InsertStatutesSOA(int.Parse(ddlStatute.SelectedValue), int.Parse(e.Node.Value), "New Section", 0, 1, int.Parse(Session["UserID"].ToString()), ref strMsg);

                    //objusr.UserID = int.Parse(Session["MemberID"].ToString());
                    //objusr.ParentFolder = int.Parse(e.Node.Value);
                    //objusr.FolderName = "New Folder";
                    //objusr.CreatedBy = int.Parse(Session["MemberID"].ToString());
                    //int chk = objusr.InsertUserFolder();
                    BindTelerikTree(int.Parse(ddlStatute.SelectedValue));

                    break;
                case "Delete":


                    //objusr.ModifiedBy = int.Parse(Session["MemberID"].ToString());
                    //objusr.DeleteUserFolder(int.Parse(e.Node.Value.ToString()));

                    //GetUsersParentFolders(int.Parse(Session["MemberID"].ToString()));
                    //GetUsersFolder(int.Parse(Session["MemberID"].ToString()), 0);
                    //BindTelerikTree();
                    //e.Node.Remove();
                    int chk1 = objs.DeleteStatutesSOA(int.Parse(e.Node.Value), int.Parse(Session["UserID"].ToString()));
                    BindTelerikTree(int.Parse(ddlStatute.SelectedValue));
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

        protected void ddlStatute_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindTelerikTree(int.Parse(ddlStatute.SelectedValue));
                GetAllIndexByIndex(int.Parse(ddlStatute.SelectedValue));
            }
            catch { }
        }
        protected void tvDept_HandleDrop(object sender, RadTreeNodeDragDropEventArgs e)
        {
            RadTreeNode sourceNode = e.SourceDragNode;
            RadTreeNode destNode = e.DestDragNode;
            RadTreeViewDropPosition dropPosition = e.DropPosition;


            if (destNode != null) //drag&drop is performed between trees
            {
                string aa = sourceNode.Value.ToString();
                string bb = destNode.Value.ToString();
                //if (ChbBetweenNodes.Checked) //dropped node will at the same level as a destination node
                //{
                if (chkMerging.Checked == false)
                {
                    int chk1 = objs.DragAndDropUpdate(int.Parse(sourceNode.Value), int.Parse(destNode.Value), int.Parse(Session["UserID"].ToString()), ref strMsg);

                    if (sourceNode.TreeView.SelectedNodes.Count <= 1)
                    {
                        PerformDragAndDrop(dropPosition, sourceNode, destNode);
                    }
                    else if (sourceNode.TreeView.SelectedNodes.Count > 1)
                    {
                        if (dropPosition == RadTreeViewDropPosition.Below) //Needed to preserve the order of the dragged items
                        {
                            for (int i = sourceNode.TreeView.SelectedNodes.Count - 1; i >= 0; i--)
                            {
                                PerformDragAndDrop(dropPosition, sourceNode.TreeView.SelectedNodes[i], destNode);
                            }
                        }
                        else
                        {
                            foreach (RadTreeNode node in sourceNode.TreeView.SelectedNodes)
                            {
                                PerformDragAndDrop(dropPosition, node, destNode);
                            }
                        }
                    }
                }
                else if(chkMerging.Checked == true)
                {
                    int chk1 = objs.DragAndDropMerging(int.Parse(sourceNode.Value), int.Parse(destNode.Value), int.Parse(Session["UserID"].ToString()), ref strMsg);

                    BindTelerikTree(int.Parse(ddlStatute.SelectedValue));

                    divSuccess1.Style["Display"] = "";
                    divError1.Style["Display"] = "none";

                    //if (sourceNode.TreeV  iew.SelectedNodes.Count <= 1)
                    //{
                    //    PerformDragAndDrop(dropPosition, sourceNode, destNode);
                    //}
                    //else if (sourceNode.TreeView.SelectedNodes.Count > 1)
                    //{
                    //    if (dropPosition == RadTreeViewDropPosition.Below) //Needed to preserve the order of the dragged items
                    //    {
                    //        for (int i = sourceNode.TreeView.SelectedNodes.Count - 1; i >= 0; i--)
                    //        {
                    //            PerformDragAndDrop(dropPosition, sourceNode.TreeView.SelectedNodes[i], destNode);
                    //        }
                    //    }
                    //    else
                    //    {
                    //        foreach (RadTreeNode node in sourceNode.TreeView.SelectedNodes)
                    //        {
                    //            PerformDragAndDrop(dropPosition, node, destNode);
                    //        }
                    //    }
                    //}
                }
                //}
                //else //dropped node will be a sibling of the destination node
                //{
                //    if (sourceNode.TreeView.SelectedNodes.Count <= 1)
                //    {
                //        if (!sourceNode.IsAncestorOf(destNode))
                //        {
                //            sourceNode.Owner.Nodes.Remove(sourceNode);
                //            destNode.Nodes.Add(sourceNode);
                //        }
                //    }
                //    else if (sourceNode.TreeView.SelectedNodes.Count > 1)
                //    {
                //        foreach (RadTreeNode node in tvDept.SelectedNodes)
                //        {
                //            if (!node.IsAncestorOf(destNode))
                //            {
                //                node.Owner.Nodes.Remove(node);
                //                destNode.Nodes.Add(node);
                //            }
                //        }
                //    }
                //}

                destNode.Expanded = true;
                sourceNode.TreeView.UnselectAllNodes();
            }
            //else if (e.HtmlElementID == RadGrid1.ClientID)
            //{
            //    DataTable dt = (DataTable)Session["DataTable"];
            //    foreach (RadTreeNode node in e.DraggedNodes)
            //    {
            //        AddRowToGrid(dt, node);
            //    }
            //}
        }
        private static void PerformDragAndDrop(RadTreeViewDropPosition dropPosition, RadTreeNode sourceNode,
                                              RadTreeNode destNode)
        {
            if (sourceNode.Equals(destNode) || sourceNode.IsAncestorOf(destNode))
            {
                return;
            }
            sourceNode.Owner.Nodes.Remove(sourceNode);

            switch (dropPosition)
            {
                case RadTreeViewDropPosition.Over:
                    // child
                    if (!sourceNode.IsAncestorOf(destNode))
                    {
                        destNode.Nodes.Add(sourceNode);
                    }
                    break;

                case RadTreeViewDropPosition.Above:
                    // sibling - above                    
                    destNode.InsertBefore(sourceNode);
                    break;

                case RadTreeViewDropPosition.Below:
                    // sibling - below
                    destNode.InsertAfter(sourceNode);
                    break;
            }
        }

        #region "Add Citation"
        void GetJournals()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objJo.GetActiveJournals();
                

                CitationHyperlinkingJournal.DataValueField = "ID";
                CitationHyperlinkingJournal.DataTextField = "JournalName";
                CitationHyperlinkingJournal.DataSource = dt;
                CitationHyperlinkingJournal.DataBind();
                CitationHyperlinkingJournal.Items.Insert(0, new ListItem("Select", "0"));

                ddlTaggedCitationSearchJournal.DataValueField = "ID";
                ddlTaggedCitationSearchJournal.DataTextField = "JournalName";
                ddlTaggedCitationSearchJournal.DataSource = dt;
                ddlTaggedCitationSearchJournal.DataBind();
                ddlTaggedCitationSearchJournal.Items.Insert(0, new ListItem("Select", "0"));

            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("ReviewCasesMigration.aspx", "GetAdvocates", e.Message);
            }
        }
        protected void btnCitationSearch_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = objcase.GetCasesSearch_Year_Journal_PageNo(int.Parse(txtCitationHyperlinkingYear.Text.Trim()), int.Parse(CitationHyperlinkingJournal.SelectedValue), txtCitationHyperlinkingPageNo.Text.Trim());
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    gvCitationSearch.DataSource = dt;
                    gvCitationSearch.DataBind();
                }
            }
        }
        void GetGridItemsCitationsAndSave(int SOAID)
        {
            try
            {
                
                HiddenField hdID = default(HiddenField);
                CheckBox chksel = default(CheckBox);
             

                foreach (GridViewRow row in gvCitationSearch.Rows)
                {
                    if ((row != null))
                    {
                        string str = "";
                        hdID = (HiddenField)row.FindControl("hdID");
                        chksel = (CheckBox)row.FindControl("chksel");


                        if (chksel.Checked == true)
                        {
                            int chk = objs.InsertStatutesSOATagging(SOAID, "Case", int.Parse(hdID.Value.ToString()), 1, int.Parse(Session["UserID"].ToString()), ref str);
                        }



                    }
                }
              
                    divSuccess.Style["Display"] = "";
                    divError.Style["Display"] = "none";
                   
               
            }
            catch (Exception ex)
            {
                //    string script = "alert('" + ex.Message() + "')";
                //    ScriptManager.RegisterStartupScript(this, typeof(Page), "RedirectTo", script, true);
            }
        }
        protected void btnSOACaseSave_Click(object sender, EventArgs e)
        {
            try
            {
                GetGridItemsCitationsAndSave(int.Parse(lblID.Text));
                GetSectionSOA(int.Parse(lblID.Text));
            }
            catch (Exception ex)
            {

            }
        }
        protected void btnMergeMove_Click(object sender, EventArgs e)
        {
            try
            {
                if (radioMove.Checked == false)
                {
                    int chk1 = objs.DragAndDropUpdate(int.Parse(ddlISourceIndex.SelectedValue), int.Parse(ddlIDestinationIndex.SelectedValue), int.Parse(Session["UserID"].ToString()), ref strMsg);
                    BindTelerikTree(int.Parse(ddlStatute.SelectedValue));
                    GetAllIndexByIndex(int.Parse(ddlStatute.SelectedValue));

                }
                if (radioMerge.Checked == true)
                {
                    int chk1 = objs.DragAndDropMerging(int.Parse(ddlISourceIndex.SelectedValue), int.Parse(ddlIDestinationIndex.SelectedValue), int.Parse(Session["UserID"].ToString()), ref strMsg);

                    BindTelerikTree(int.Parse(ddlStatute.SelectedValue));
                    GetAllIndexByIndex(int.Parse(ddlStatute.SelectedValue));


                }
            }
            catch (Exception ex)
            {

            }
        }

        #endregion

        protected void chkSOATagEnable_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlStatute.SelectedIndex != 0)
                {
                    if (chkSOATagEnable.Checked == true)
                    {
                        objs.UpdateStatutesSOATagEnable(int.Parse(ddlStatute.SelectedValue), 1, int.Parse(Session["UserID"].ToString()));
                    }
                    else if (chkSOATagEnable.Checked == false)
                    {
                        objs.UpdateStatutesSOATagEnable(int.Parse(ddlStatute.SelectedValue), 0, int.Parse(Session["UserID"].ToString()));
                    }
                }
            }
            catch { }
        }
        protected void btnTaggedCitationSearch_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = objs.GetStatutesSOATaggingbyCitationSearch(int.Parse(ddlStatute.SelectedValue),int.Parse(txtTaggedCitationSearchYear.Text.Trim()), int.Parse(ddlTaggedCitationSearchJournal.SelectedValue), txtTaggedCitationSearchNo.Text.Trim());
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
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
                }
            }
        }
    }
}