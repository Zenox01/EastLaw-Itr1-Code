using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using AjaxControlToolkit;

using Microsoft.Office;
using Microsoft.Office.Interop.Word;
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;



namespace EastlawUI_v2.adminpanel
{
    public partial class ManageDepartments : System.Web.UI.Page
    {
        EastLawBL.Departments objdept = new EastLawBL.Departments();
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
                BindTelerikTree(0);
                LoadYears();
                GetDeptTypeGroup();
                //BindTreeViewControl(0);
                //GetDepartments(0);

            }
        }
        private void AddTreeViewItems()
        {
            
        }
        void LoadYears()
        {
            // Clear items:    
            ddlSYear.Items.Clear();
            // Add default item to the list
            ddlSYear.Items.Insert(0, new ListItem("Year", "0"));
            // Start loop
            for (int i = 0; i < 69; i++)
            {
                // For each pass add an item
                // Add a number of years (negative, which will subtract) to current year
                ddlSYear.Items.Add(DateTime.Now.AddYears(-i).Year.ToString());
            }
        }
        void GetDeptTypeGroup()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objdept.GetDepartmentFilesTypesGroup();
                ddlSType.DataValueField = "DType";
                ddlSType.DataTextField = "DType";
                ddlSType.DataSource = dt;
                ddlSType.DataBind();
                ddlSType.Items.Insert(0, new ListItem("Select", "0"));
                ddlSType.Items.Insert(1, new ListItem("Rules and Regulations", "Rules and Regulations"));
                ddlSType.Items.Insert(2, new ListItem("Guidelines", "Guidelines"));
                ddlSType.Items.Insert(3, new ListItem("Circular Letters instead of C.No", "Circular Letters instead of C.No"));
                ddlSType.Items.Insert(4, new ListItem("Directives instead of Direction No.", "Directives instead of Direction No."));
              

                ddlDeptTypeGroups.DataValueField = "DType";
                ddlDeptTypeGroups.DataTextField = "DType";
                ddlDeptTypeGroups.DataSource = dt;
                ddlDeptTypeGroups.DataBind();
                ddlDeptTypeGroups.Items.Insert(0, new ListItem("Other", "Other"));
                ddlDeptTypeGroups.Items.Insert(1, new ListItem("Rules and Regulations", "Rules and Regulations"));
                ddlDeptTypeGroups.Items.Insert(2, new ListItem("Guidelines", "Guidelines"));
                ddlDeptTypeGroups.Items.Insert(3, new ListItem("Circular Letters instead of C.No", "Circular Letters instead of C.No"));
                ddlDeptTypeGroups.Items.Insert(4, new ListItem("Directives instead of Direction No.", "Directives instead of Direction No."));
                
            }
            catch { }
        }
        private void BindTelerikTree(int ID)
        {
            try
            {
                DataTable dtlevel1 = objdept.GetActiveDeptByParent(0);
                if (dtlevel1 != null)
                {
                    for (int l1 = 0; l1 < dtlevel1.Rows.Count; l1++)
                   {
                       RadTreeNode level1 = new RadTreeNode(dtlevel1.Rows[l1]["DeptName"].ToString(), dtlevel1.Rows[l1]["ID"].ToString());
                       tvDept.Nodes.Add(level1);


                           DataTable dtlevel2 = objdept.GetActiveDeptByParent(int.Parse(dtlevel1.Rows[l1]["ID"].ToString()));
                           if (dtlevel2 != null)
                           {
                               for (int l2 = 0; l2 < dtlevel2.Rows.Count; l2++)
                               {
                                   RadTreeNode level2 = new RadTreeNode(dtlevel2.Rows[l2]["DeptName"].ToString(), dtlevel2.Rows[l2]["ID"].ToString());
                                   level1.Nodes.Add(level2);

                                   DataTable dtlevel3 = objdept.GetActiveDeptByParent(int.Parse(dtlevel2.Rows[l2]["ID"].ToString()));
                                   if (dtlevel3 != null)
                                   {
                                       for (int l3 = 0; l3 < dtlevel3.Rows.Count; l3++)
                                       {
                                           RadTreeNode level3 = new RadTreeNode(dtlevel3.Rows[l3]["DeptName"].ToString(), dtlevel3.Rows[l3]["ID"].ToString());
                                           level2.Nodes.Add(level3);
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
        private void BindTreeViewControl(int ID)
        {
            try
            {
              DataTable  dt = objdept.GetDepartments(ID);
                if (dt != null)
                {
                    DataRow[] Rows = dt.Select("ParentId=0"); // Get all parents nodes
                    for (int i = 0; i < Rows.Length; i++)
                    {
                        TreeNode root = new TreeNode(Rows[i]["DeptName"].ToString(), Rows[i]["ID"].ToString());
                        root.SelectAction = TreeNodeSelectAction.Expand;
                        CreateNode(root, dt);
                        tv.Nodes.Add(root);
                    }
                }
            }
            catch (Exception Ex) { throw Ex; }
        }
        public void CreateNode(TreeNode node, DataTable Dt)
        {
            DataRow[] Rows = Dt.Select("ParentId =" + node.Value);
            if (Rows.Length == 0) { return; }
            for (int i = 0; i < Rows.Length; i++)
            {
                TreeNode Childnode = new TreeNode(Rows[i]["DeptName"].ToString(), Rows[i]["ID"].ToString());
                Childnode.SelectAction = TreeNodeSelectAction.Expand;
                node.ChildNodes.Add(Childnode);
                CreateNode(Childnode, Dt);
            }
        }
        void GetDepartments(int ID)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objdept.GetDepartments(ID);
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
                    lblCount.Text = dt.Rows.Count.ToString();
                    gv.DataSource = dt;
                    gv.DataBind();
                }

            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("ManageDepartment.aspx", "GetDepartments", e.Message);
            }
        }
        protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gv.PageIndex = e.NewPageIndex;
                GetDepartments(0);
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("ManageDepartment.aspx", "gv_PageIndexChanging", ex.Message);
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

                    int chk = objdept.DeleteDepartment(ID, int.Parse(Session["UserID"].ToString()));
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
                GetDepartments(0);

            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("ManageDepartment.aspx", "gv_RowDeleting", ex.Message);
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

                    Response.Redirect("AddDepartment.aspx?param=" + EncryptDecryptHelper.Encrypt(ID.ToString()));



                }
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("ManageDepartments.aspx", "gv_RowEditing", ex.Message);
            }
        }

        protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
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
                ExceptionHandling.SendErrorReport("ManageDepartments.aspx", "gv_RowDataBound", ex.Message);
            }
        }

        protected void tv_SelectedNodeChanged(object sender, EventArgs e)
        {
           
            Response.Write(tv.SelectedNode.Text.ToString());
        }
        void GetSelecteddeptFiles(int DeptID)
        {
            try
            {
                DataTable dt = new DataTable();

                dt = objdept.GetDepartmentFilesByDepartments(DeptID);
                if (dt.Rows.Count > 0)
                {
                    gvFile.DataSource = dt;
                    gvFile.DataBind();
                }
                else
                {
                    gvFile.DataSource = null;
                    gvFile.DataBind();
                }
            }
            catch { }
        }
        protected void tvDept_NodeClick(object sender, RadTreeNodeEventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                Session["SelectedNode"] = e.Node.Value.ToString();
                GetSelecteddeptFiles(int.Parse(e.Node.Value.ToString()));
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
            switch (e.MenuItem.Value)
            {
                case "Edit":
                    Response.Redirect("AddDepartment.aspx?param=" + EncryptDecryptHelper.Encrypt(e.Node.Value.ToString()));
                    break;
                case "Upload":
                    divFileUpload.Style["Display"] = "";
                  
                    break;
                case "Delete":
                    if (e.Node.Level == 0)
                    {
                        int chk = objdept.DeleteDepartment(int.Parse(e.Node.Value), int.Parse(Session["UserID"].ToString()));
                        BindTelerikTree(0);
                    }
                    if (e.Node.Level == 1)
                    {
                        int chk = objdept.DeleteDepartment(int.Parse(e.Node.Value), int.Parse(Session["UserID"].ToString()));
                        e.Node.Remove();
                    }
                    break;
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkDateFormated.Checked == true)
                {
                    if (!ValidateDate(txtDate.Text))
                    {
                        lblDeptFileAdd.Text = "Invalid Date format.";
                        lblDeptFileAdd.ForeColor = System.Drawing.Color.Red;
                        return;
                    }
                }
                string Img = "";
               
                if (fuploader.HasFile)
                {
                    string destDir = Server.MapPath("/store/departments/wordfiles/");

                    string FileName = Path.GetFileName(fuploader.PostedFile.FileName);
                    string destPath = Path.Combine(destDir, FileName);
                    fuploader.SaveAs(destPath);

                    Img = fuploader.FileName;

                }
                if (ddlFileType.SelectedValue == "Word")
                {
                    string htmlfileName = "";
                    //string content = ReadWordFile_AddHTMLReturnContent(Img, ref htmlfileName);
                    string content = WordToHtml(Img, ref htmlfileName);
                    if (Session["SelectedNode"] != null)
                        objdept.DeptID = int.Parse(Session["SelectedNode"].ToString());
                    objdept.HTMLFile = htmlfileName;
                    objdept.WordFile = Img;
                    objdept.FileContent = content;

                    string fileName = Img;
                    int fileExtPos = fileName.LastIndexOf(".");
                    if (fileExtPos >= 0)
                        fileName = fileName.Substring(0, fileExtPos);
                    objdept.FileType = "Word";
                }
                else if(ddlFileType.SelectedValue == "PDF")
                {
                    if (Session["SelectedNode"] != null)
                        objdept.DeptID = int.Parse(Session["SelectedNode"].ToString());
                    objdept.WordFile = Img;
                    objdept.FileType = "PDF";
                }
                objdept.Title = txtTitle.Text;
                objdept.Year = txtYear.Text;
                objdept.No = txtNo.Text;
                objdept.DDate = txtDate.Text;
                objdept.DType = ddlDeptTypeGroups.SelectedValue;// txtType.Text;
                objdept.Active = 1;
                objdept.CreatedBy = int.Parse(Session["UserID"].ToString());
                if (chkDateFormated.Checked == true)
                    objdept.DateFormated = 1;
                else
                    objdept.DateFormated = 0;
                int chk = objdept.InsertDepartmentFile();
                lblDeptFileAdd.Text = "";
                lblDeptFileAdd.ForeColor = System.Drawing.Color.Black;

                GetSelecteddeptFiles(int.Parse(Session["SelectedNode"].ToString()));
            }
            catch { }
        }
        bool ValidateDate(string Date)
        {
            DateTime dateValue;
            if (DateTime.TryParseExact(Date, "dd/MM/yyyy", new CultureInfo("en-US"), DateTimeStyles.None, out dateValue))
                return true;
            else
                return false;
        }
        string WordToHtml(string WordFileName,ref string HtmlFileName )
        {
            try
            {
                string fileName = DateTime.Now.Ticks.ToString();
                HtmlFileName = fileName + ".html";

                Spire.Doc.Document document = new Spire.Doc.Document();

                string path = Server.MapPath("/store/departments/wordfiles/" + WordFileName);
                document.LoadFromFile(path);

                //Save doc file to html

                document.SaveToFile(Server.MapPath("/store/departments/depttemp/" + HtmlFileName), Spire.Doc.FileFormat.Html);
                string html = System.IO.File.ReadAllText(Server.MapPath("/store/departments/depttemp/" + HtmlFileName));
                return html.Replace("contenteditable='false'","contenteditable='false'");
                // Response.Write(EastLawUI.CommonClass.CleanHtml(html));
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        protected void gvFile_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                GridViewRow row = gvFile.Rows[e.RowIndex];
                HiddenField hd = default(HiddenField);

                if ((row != null))
                {
                    hd = (HiddenField)row.FindControl("hdID");
                    int ID = Convert.ToInt32(hd.Value);

                    int chk = objdept.DeleteDepartmentFile(ID, int.Parse(Session["UserID"].ToString()));
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
                gvFile.EditIndex = -1;
                if (Session["SelectedNode"] != null)
                    GetSelecteddeptFiles(int.Parse(Session["SelectedNode"].ToString()));
 
            }
            catch { }
        }

        protected void gvFile_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                GridViewRow row = gvFile.Rows[e.NewEditIndex];
                HiddenField hd = default(HiddenField);

                if ((row != null))
                {
                    hd = (HiddenField)row.FindControl("hdID");
                    int ID = Convert.ToInt32(hd.Value);

                    Response.Redirect("ManageDepartmentFile.aspx?param=" + EncryptDecryptHelper.Encrypt(ID.ToString()));



                }
 
            }
            catch { }
        }
        void DepartmentSearch()
        {
            try
            {
                string cri = "Where A.IsDeleted=0";


                if (!string.IsNullOrEmpty(txtSTitle.Text.Trim()))
                {
                    cri = cri + " AND  CONTAINS(A.Title,'\"" + txtSTitle.Text + "\"')";
                }
                if (ddlSYear.SelectedValue != "0")
                {
                    cri = cri + " AND  CONTAINS(A.Year,'\"" + ddlSYear.SelectedValue + "\"')";
                }
                if (!string.IsNullOrEmpty(txtSNumber.Text.Trim()))
                {
                    cri = cri + " AND  CONTAINS(A.No,'\"" + txtSNumber.Text + "\"')";
                }
                if (ddlSType.SelectedValue != "0")
                {
                    cri = cri + " AND  CONTAINS(A.DType,'\"" + ddlSType.SelectedValue + "\"')";
                }
                //if (Session["SelectedNode"] != null)
                //{
                //    cri = cri + " AND  A.DType=" + Session["SelectedNode"].ToString() + "";
                //}
                
                DataTable dt = new DataTable();
                dt = objdept.DepartmentSearch(cri);
                if (dt != null)
                {

                    gvFile.DataSource = dt;
                    gvFile.DataBind();

                }

            }

            catch (Exception ex)
            {

            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                DepartmentSearch();
            }
            catch { }
        }
        //string ReadWordFile_AddHTMLReturnContent(string wordfile, ref string HTMLFileName)
        //{
        //    try
        //    {
        //        object missingType = Type.Missing;
        //        object readOnly = true;
        //        object isVisible = false;
        //        object documentFormat = 8;
        //        string randomName = DateTime.Now.Ticks.ToString();
        //        object htmlFilePath = Server.MapPath("/store/departments/htmlfiles/") + randomName + ".htm";
        //        string directoryPath = Server.MapPath("/store/departments/htmlfiles/") + randomName + "_files";

        //        HTMLFileName = randomName + ".htm";

        //        object fileName = Server.MapPath("/store/departments/wordfiles/") + wordfile;// FileUpload1.PostedFile.FileName;

        //        //Open the word document in background
        //        ApplicationClass applicationclass = new ApplicationClass();
        //        applicationclass.Documents.Open(ref fileName,
        //                                        ref readOnly,
        //                                        ref missingType, ref missingType, ref missingType,
        //                                        ref missingType, ref missingType, ref  missingType,
        //                                        ref missingType, ref missingType, ref isVisible,
        //                                        ref missingType, ref missingType, ref missingType,
        //                                        ref missingType, ref missingType);
        //        applicationclass.Visible = false;
        //        Document document = applicationclass.ActiveDocument;

        //        //Save the word document as HTML file
        //        document.SaveAs(ref htmlFilePath, ref documentFormat, ref missingType,
        //                        ref missingType, ref missingType, ref missingType,
        //                        ref missingType, ref missingType, ref missingType,
        //                        ref missingType, ref missingType, ref missingType,
        //                        ref missingType, ref missingType, ref missingType,
        //                        ref missingType);

        //        //Close the word document
        //        document.Close(ref missingType, ref missingType, ref missingType);

        //        //Delete the Uploaded Word File
        //        // File.Delete(Server.MapPath("~/Temp/") + Path.GetFileName(FileUpload1.PostedFile.FileName));

        //        //Read the Html File as Byte Array and Display it on browser
        //        byte[] bytes;
        //        using (FileStream fs = new FileStream(htmlFilePath.ToString(), FileMode.Open, FileAccess.Read))
        //        {
        //            BinaryReader reader = new BinaryReader(fs);
        //            bytes = reader.ReadBytes((int)fs.Length);
        //            fs.Close();
        //        }
        //        var str = System.Text.Encoding.Default.GetString(bytes);
        //        // str = CleanHtml(str);
        //        //Response.Write(str);
        //        //Response.BinaryWrite(bytes);
        //        //Response.Flush();

        //        //Delete the Html File
        //        // File.Delete(htmlFilePath.ToString());
        //        foreach (string file in Directory.GetFiles(directoryPath))
        //        {
        //            File.Delete(file);
        //        }
        //        Directory.Delete(directoryPath);
        //        // Response.End();

        //        return str;
        //    }
        //    catch (Exception ex)
        //    {
        //        Email.SendMail("umar.mughal83@gmail.com", ex.Message, "eastlaw error", "eastlaw.pk", "");
        //        Response.Write(ex.Message);
        //        return "";
        //    }
        //}

       
    }
}