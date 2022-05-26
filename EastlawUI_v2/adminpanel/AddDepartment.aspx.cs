using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AjaxControlToolkit;

using Microsoft.Office;
using Microsoft.Office.Interop.Word;
using System.IO;
using System.Text.RegularExpressions;

namespace EastlawUI_v2.adminpanel
{
    public partial class AddDepartment : System.Web.UI.Page
    {
        EastLawBL.Departments objdpt = new EastLawBL.Departments();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    if (Session["UserID"] == null)
                    {
                        Response.Redirect("default.aspx");
                    }
                    if (!ValidateUserGroup.ValidateGroup(int.Parse(Session["UserTypeID"].ToString()), ValidateUserGroup.getPageName(Request.Url.AbsolutePath)))
                        Response.Redirect("NotAuthorize.aspx");
                    if ((Request.QueryString["param"] != null))
                        GetDepartment(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())));

                }
            }
            catch { }
        }
        void GetDepartment(int ID)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objdpt.GetDepartments(ID);
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["ParentID"].ToString() == "0")
                    {
                        ddlType.SelectedValue = "L1";
                    }
                    if (dt.Rows[0]["ParentID"].ToString() != "0")
                    {
                        ddlType.SelectedValue = "L2";
                        //GetGlossoryByParent(int.Parse(dt.Rows[0]["ParentID"].ToString()));
                        GetParentDepartments();

                        DataTable dtL2 = new DataTable();
                        dtL2 = objdpt.GetDepartments(int.Parse(dt.Rows[0]["ParentID"].ToString()));
                        ddlLevel1.SelectedValue = dtL2.Rows[0]["ID"].ToString();

                        divLevel1.Style["Display"] = "";
                        rfvddlLevel1.Enabled = true;

                        divLevel2.Style["Display"] = "none";
                        rfvddlLeve2.Enabled = false;

                        if (dtL2.Rows[0]["ParentID"].ToString() != "0")
                        {

                            ddlType.SelectedValue = "L2";
                            //GetGlossoryByParent(int.Parse(dt.Rows[0]["ParentID"].ToString()));
                            GetParentDepartments();

                            //DataTable dtL2 = new DataTable();
                            dtL2 = objdpt.GetDepartments(int.Parse(dt.Rows[0]["ParentID"].ToString()));
                            ddlLevel1.SelectedValue = dtL2.Rows[0]["ParentID"].ToString();

                            divLevel1.Style["Display"] = "";
                            rfvddlLevel1.Enabled = true;

                            divLevel2.Style["Display"] = "none";
                            rfvddlLeve2.Enabled = false;


                            ddlType.SelectedValue = "L3";
                            GetDepartmentByParent(int.Parse(dtL2.Rows[0]["ParentID"].ToString()));

                            DataTable dtL3 = new DataTable();
                            dtL3 = objdpt.GetDepartments(int.Parse(dtL2.Rows[0]["ParentID"].ToString()));
                            //ddlLevel1.SelectedValue = dtL2.Rows[0]["ID"].ToString();
                            ddlLevel2.SelectedValue = dtL2.Rows[0]["ID"].ToString();

                            divLevel2.Style["Display"] = "";
                            rfvddlLeve2.Enabled = true;
                        }
                    }

                    txtTitle.Text = dt.Rows[0]["Deptname"].ToString();
                    //txtTitle.Text = dt.Rows[0]["Title"].ToString();
                    //txtYear.Text = dt.Rows[0]["Year"].ToString();
                    //txtNo.Text = dt.Rows[0]["No"].ToString();
                    //txtDate.Text = dt.Rows[0]["DDate"].ToString();
                    //txtType.Text = dt.Rows[0]["DType"].ToString();

                    //editorContent.Content = dt.Rows[0]["FileContent"].ToString();
                    if (dt.Rows[0]["Active"].ToString() == "1")
                        chkActive.Checked = true;
                    else
                        chkActive.Checked = false;

                    //if (dt.Rows[0]["dateformated"].ToString() == "1")
                    //    chkDateFormated.Checked = true;
                    //else
                    //    chkDateFormated.Checked = false;
                    ///GetGlossaryTagging(ID);
                }
            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("ManageDepartmentFile.aspx", "GetDeptFileDetails", e.Message);
            }
        }
        void GetParentDepartments()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objdpt.GetActiveDeptParent();
                ddlLevel1.DataValueField = "ID";
                ddlLevel1.DataTextField = "DeptName";
                ddlLevel1.DataSource = dt;
                ddlLevel1.DataBind();

                ddlLevel1.Items.Insert(0, new ListItem("Select", "0"));
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("AddDepartment.aspx", "GetParentDepartments", ex.Message);
            }
        }
        void GetDepartmentByParent(int ParentID)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objdpt.GetActiveDeptByParent(ParentID);
                ddlLevel2.DataValueField = "ID";
                ddlLevel2.DataTextField = "DeptName";
                ddlLevel2.DataSource = dt;
                ddlLevel2.DataBind();

                ddlLevel2.Items.Insert(0, new ListItem("Select", "0"));
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("AddDepartment.aspx", "GetDepartmentByParent", ex.Message);
            }
        }
        public void CreateTableValuesFiles()
        {
            try
            {

                string[] sa = Session["ID"].ToString().Split('|');
                string[] sb = Session["Name"].ToString().Split('|');

                int recordnum = sa.Length;

                DataTable dt = new DataTable("tblkeywords");
                dt.Columns.Add("ID");
                dt.Columns.Add("Name");
                for (int j = 0; j < recordnum - 1; j++)
                {
                    if (!string.IsNullOrEmpty(sa[j].ToString()))
                    {
                        DataRow dr = dt.NewRow();
                        dr["ID"] = sa[j].ToString();
                        dr["Name"] = sb[j].ToString();
                        dt.Rows.Add(dr);
                    }

                }
                ViewState["dt"] = dt;
                DataTable dt1 = (DataTable)ViewState["dt"];
                dt1 = dt.DefaultView.ToTable(true, "Id", "Name");
                gvKeywords.DataSource = dt1;
                gvKeywords.DataBind();



                //gvPracticeAreaSubCat.DataSource = dt.DefaultView;
                //gvPracticeAreaSubCat.DataBind();
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("AddGeneralAreas.aspx", "CreateTableValuesPracticeArea", ex.Message);
            }
        }
        //string ReadWordFile_AddHTMLReturnContent(string wordfile,ref string HTMLFileName)
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
        //       // File.Delete(Server.MapPath("~/Temp/") + Path.GetFileName(FileUpload1.PostedFile.FileName));

        //        //Read the Html File as Byte Array and Display it on browser
        //        byte[] bytes;
        //        using (FileStream fs = new FileStream(htmlFilePath.ToString(), FileMode.Open, FileAccess.Read))
        //        {
        //            BinaryReader reader = new BinaryReader(fs);
        //            bytes = reader.ReadBytes((int)fs.Length);
        //            fs.Close();
        //        }
        //        var str = System.Text.Encoding.Default.GetString(bytes);
        //       // str = CleanHtml(str);
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
        //       // Response.End();

        //        return str;
        //    }
        //    catch (Exception ex)
        //    {
        //        Email.SendMail("umar.mughal83@gmail.com", ex.Message, "eastlaw error", "eastlaw.pk", "");
        //        Response.Write(ex.Message);
        //        return "";
        //    }
        //}
        void GetGridItemsFilesAndSave(int DeptID)
        {
            try
            {

                //GridViewRow row = default(GridViewRow);
                HiddenField hdID = default(HiddenField);
                

                foreach (GridViewRow row in gvKeywords.Rows)
                {
                    if ((row != null))
                    {
                       

                        hdID = (HiddenField)row.FindControl("hdID");
                        string htmlfileName = "";
                        string content = "";// ReadWordFile_AddHTMLReturnContent(hdID.Value.ToString(), ref htmlfileName);
                        objdpt.DeptID = DeptID;
                        objdpt.HTMLFile = htmlfileName;
                        objdpt.WordFile = hdID.Value.ToString();
                        objdpt.FileContent = content;
                        objdpt.Active = 1;

                        string fileName = hdID.Value.ToString();
                        int fileExtPos = fileName.LastIndexOf(".");
                        if (fileExtPos >= 0)
                            fileName = fileName.Substring(0, fileExtPos);

                        objdpt.Title = fileName.ToString();
                         objdpt.CreatedBy = int.Parse(Session["UserID"].ToString());
                         int chk = objdpt.InsertDepartmentFile();
                    }
                }
            }
            catch (Exception ex)
            {
                //    string script = "alert('" + ex.Message() + "')";
                //    ScriptManager.RegisterStartupScript(this, typeof(Page), "RedirectTo", script, true);
            }
        }
        void SaveRecord()
        {
            try
            {
                if (ddlType.SelectedValue == "L1")
                {
                    objdpt.ParentID = 0;
                }
                else if (ddlType.SelectedValue == "L2")
                {
                    objdpt.ParentID = int.Parse(ddlLevel1.SelectedValue);
                }
                else if (ddlType.SelectedValue == "L3")
                {
                    objdpt.ParentID = int.Parse(ddlLevel2.SelectedValue);
                }

                objdpt.DeptName = txtTitle.Text.Trim();
               
                if (chkActive.Checked == true)
                    objdpt.Active = 1;
                else
                    objdpt.Active = 0;
                // objkey.CreatedBy = 0;
                objdpt.CreatedBy = int.Parse(Session["UserID"].ToString());
                int chk = objdpt.InsertDepartment();
                if (chk > 0)
                {
                    GetGridItemsFilesAndSave(chk);
                  
                    divSuccess.Style["Display"] = "";
                    divError.Style["Display"] = "none";
                   // ClearFields();
                    // ClearFields();

                }
                else
                {
                    divSuccess.Style["Display"] = "none";
                    divError.Style["Display"] = "";
                }
            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("AddGlossary.aspx", "SaveRecord", e.Message);
            }
        }
        void EditRecord(int ID)
        {
            try
            {
                if (ddlType.SelectedValue == "L1")
                {
                    objdpt.ParentID = 0;
                }
                else if (ddlType.SelectedValue == "L2")
                {
                    objdpt.ParentID = int.Parse(ddlLevel1.SelectedValue);
                }
                else if (ddlType.SelectedValue == "L3")
                {
                    objdpt.ParentID = int.Parse(ddlLevel2.SelectedValue);
                }

                objdpt.DeptName = txtTitle.Text.Trim();

                if (chkActive.Checked == true)
                    objdpt.Active = 1;
                else
                    objdpt.Active = 0;
                // objkey.CreatedBy = 0;
                objdpt.ModifiedBy = int.Parse(Session["UserID"].ToString());
                int chk = objdpt.EditDepartment(ID);
                if (chk > 0)
                {
                

                    divSuccess.Style["Display"] = "";
                    divError.Style["Display"] = "none";
                    // ClearFields();
                    // ClearFields();

                }
                else
                {
                    divSuccess.Style["Display"] = "none";
                    divError.Style["Display"] = "";
                }
            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("AddGlossary.aspx", "SaveRecord", e.Message);
            }
        }
        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlType.SelectedValue == "L1")
            {
                divLevel1.Style["Display"] = "none";
                rfvddlLevel1.Enabled = false;

                divLevel2.Style["Display"] = "none";
                rfvddlLeve2.Enabled = false;
            }
            else if (ddlType.SelectedValue == "L2")
            {
                GetParentDepartments();
                divLevel1.Style["Display"] = "";
                rfvddlLevel1.Enabled = true;

                divLevel2.Style["Display"] = "none";
                rfvddlLeve2.Enabled = false;
            }
            else if (ddlType.SelectedValue == "L3")
            {
                GetParentDepartments();
                divLevel1.Style["Display"] = "";
                rfvddlLevel1.Enabled = true;

                divLevel2.Style["Display"] = "";
                rfvddlLeve2.Enabled = true;
            }
        }

        protected void ddlLevel1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetDepartmentByParent(int.Parse(ddlLevel1.SelectedValue));
            }
            catch { }
        }
        //protected void OnUploadComplete(object sender, AjaxFileUploadEventArgs e)
        //{
        //    string fileName = Path.GetFileName(e.FileName);
        //    AjaxFileUpload11.SaveAs(Server.MapPath("~/store/departments/wordfiles/" + fileName));
        //}
        void SessionNumber()
        {
            int num;
            Random random = new Random();
            num = random.Next(0, 10000);
            Session["tempID"] = num;
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string Img = "";
                //if (Session["tempID"] == null)
                //{
                //    SessionNumber();
                //}
                if (fuploader.HasFile)
                {
                    string destDir = Server.MapPath("/store/departments/wordfiles/");

                    string FileName = Path.GetFileName(fuploader.PostedFile.FileName);
                    string destPath = Path.Combine(destDir,FileName);
                    fuploader.SaveAs(destPath);

                    Img =fuploader.FileName;

                }
                Session["ID"] += Img + "|";
                Session["Name"] += Img + "|";
                CreateTableValuesFiles();
            }
            catch { }
        }
        void ClearFields()
        {
            ddlType.SelectedIndex = 0;
            ddlLevel1.SelectedIndex = 0;
            ddlLevel2.SelectedIndex = 0;
            txtTitle.Text = "";
            
            chkActive.Checked = false;
            divLevel1.Style["Display"] = "none";
            rfvddlLevel1.Enabled = false;

            divLevel2.Style["Display"] = "none";
            rfvddlLeve2.Enabled = false;

            gvKeywords.DataSource = null;
            gvKeywords.DataBind();

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["param"] == null)
                    SaveRecord();
                else
                    EditRecord(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())));
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("AddCompany.aspx", "btnSave_Click", ex.Message);
            }
        }
    }
}