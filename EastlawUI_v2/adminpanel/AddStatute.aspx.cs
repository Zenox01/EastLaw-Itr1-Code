using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Configuration;

namespace EastlawUI_v2.adminpanel
{
    public partial class AddStatute : System.Web.UI.Page
    {
        EastLawBL.Statutes objstat = new EastLawBL.Statutes();
        EastLawBL.PracticeAreas objPA = new EastLawBL.PracticeAreas();
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
                GetPracticeArea();
                GetStatuesCat(0);
                GetStatuesGroup(0);
                if (Request.QueryString["param"] != null)
                    GetStatutes(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())));

            }

        }
        void GetStatutes(int ID)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objstat.GetStatutes(ID);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        ddlType.SelectedValue = dt.Rows[0]["Pri_Sec"].ToString();
                        ddlCategories.SelectedValue = dt.Rows[0]["CatID"].ToString();
                        txtTitle.Text= dt.Rows[0]["Title"].ToString();
                        txtTitaleVariation1.Text= dt.Rows[0]["TitleVariation1"].ToString();
                        txtTitaleVariation2.Text = dt.Rows[0]["TitleVariation2"].ToString();
                        txtTitaleVariation3.Text = dt.Rows[0]["TitleVariation3"].ToString();
                        txtDate.Text = dt.Rows[0]["Date"].ToString();
                        txtAct.Text = dt.Rows[0]["Act"].ToString();
                        ddlGroup.SelectedValue = dt.Rows[0]["GroupID"].ToString();
                        GetStatuesSubGroupByGroup(int.Parse(dt.Rows[0]["GroupID"].ToString()));
                        ddlSubGroup.SelectedValue = dt.Rows[0]["SubGroupID"].ToString();

                        hypLnkWord.NavigateUrl = "/store/statutesdocs/word/" + dt.Rows[0]["WordFileName"].ToString();
                        hypLnkWord.Text = dt.Rows[0]["WordFileName"].ToString();
                        lblfuploadWord.Text = dt.Rows[0]["WordFileName"].ToString();

                        hypLnkPDF.NavigateUrl = "/store/statutesdocs/pdf/" + dt.Rows[0]["PDFFileName"].ToString();
                        hypLnkPDF.Text = dt.Rows[0]["PDFFileName"].ToString();
                        lblfuploadPDF.Text = dt.Rows[0]["PDFFileName"].ToString();
                        txtLastDoumentUpdateDate.Text = dt.Rows[0]["LastDocumentUpdateDate"].ToString();
                        

                        if (dt.Rows[0]["Active"].ToString() =="1")
                            chkActive.Checked = true;
                        else
                            chkActive.Checked = false;

                        GetPracticeAreaByStatues(ID);
                        GetStatuteOtherCategories(ID);
                        if (dt.Rows[0]["Pri_Sec"].ToString() != "PRIMARY")
                        {
                           
                            GetPrimaryStatutes("PRIMARY");
                            divPrimaryStatutes.Style["Display"] = "";
                            ddlPrimaryStatutes.SelectedValue = dt.Rows[0]["PrimaryStatutes"].ToString();
                        }

                    }
                }

            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("CasesMigrationList.aspx", "GetCases", e.Message);
            }
        }
        void GetStatuteOtherCategories(int StatuteID)
        {
            try
            {
                

                DataTable dtLaws = new DataTable();
                dtLaws = objstat.GetStatutesMultiCategory(StatuteID);
                gvOtherCat.DataSource = dtLaws;
                gvOtherCat.DataBind();
            }
            catch { }
        }
        public void GetPrimaryStatutes(string Primary)
        {
            try
            {

                DataTable dt = objstat.GetActiveStatusByPri_Sec(Primary);
                if (dt.Rows.Count > 0)
                {
                    ddlPrimaryStatutes.DataTextField = "Title";
                    ddlPrimaryStatutes.DataValueField = "ID";
                    ddlPrimaryStatutes.DataSource = dt;
                    ddlPrimaryStatutes.DataBind();
                    ddlPrimaryStatutes.Items.Insert(0, new ListItem("N/A", "0"));
                }


            }
            catch { }

        }
        void GetPracticeArea()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objPA.GetActivePracticeAreaSubCategoriesByCategory(3);
                chkLstPA.DataValueField = "ID";
                chkLstPA.DataTextField = "PracticeAreaSubCatName";
                chkLstPA.DataSource = dt;
                chkLstPA.DataBind();
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("AddGlossary.aspx", "GetPracticeArea", ex.Message);
            }
        }
        public void GetStatuesCat(int ID)
        {
            try
            {

                DataTable dt = objstat.GetStatutesCategories(ID);
                if (dt.Rows.Count > 0)
                {
                    ddlCategories.DataTextField = "CatName";
                    ddlCategories.DataValueField = "ID";
                    ddlCategories.DataSource = dt;
                    ddlCategories.DataBind();
                    ddlCategories.Items.Insert(0, new ListItem("Select", "0"));

                    ddlOtherCategories.DataTextField = "CatName";
                    ddlOtherCategories.DataValueField = "ID";
                    ddlOtherCategories.DataSource = dt;
                    ddlOtherCategories.DataBind();
                    ddlOtherCategories.Items.Insert(0, new ListItem("Select", "0"));
                }


            }
            catch { }

        }
        public void GetStatuesGroup(int ID)
        {
            try
            {

                DataTable dt = objstat.GetStatutesGroup(ID);
                if (dt.Rows.Count > 0)
                {
                    ddlGroup.DataTextField = "Statutes_Category_Group";
                    ddlGroup.DataValueField = "ID";
                    ddlGroup.DataSource = dt;
                    ddlGroup.DataBind();
                    ddlGroup.Items.Insert(0, new ListItem("Select", "0"));
                }


            }
            catch { }

        }
        public void GetStatuesSubGroupByGroup(int GroupID)
        {
            try
            {

                DataTable dt = objstat.GetStatutesSubGroupByGroup(GroupID);
                if (dt.Rows.Count > 0)
                {
                    ddlSubGroup.DataTextField = "Statutes_Category_SubGroup";
                    ddlSubGroup.DataValueField = "ID";
                    ddlSubGroup.DataSource = dt;
                    ddlSubGroup.DataBind();
                    ddlSubGroup.Items.Insert(0, new ListItem("Select", "0"));
                }
                else
                    ddlSubGroup.Items.Insert(0, new ListItem("Select", "0"));


            }
            catch { }

        }

        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetStatuesSubGroupByGroup(int.Parse(ddlGroup.SelectedValue));
            }
            catch { }
        }
        void SaveRecord()
        {
            try
            {
                string wordFile = "";
                string pdf = "";
                string cntnttype = "";
                //if (Session["tempID"] == null)
                //{
                //    SessionNumber();
                //}
                if (fuploadWord.HasFile)
                {
                    string destDir = Server.MapPath("../store/statutesdocs/word/");

                    string FileName = Path.GetFileName(fuploadWord.PostedFile.FileName);
                    string destPath = Path.Combine(destDir, FileName);
                    fuploadWord.SaveAs(destPath);

                    wordFile = fuploadWord.FileName;
                    cntnttype = "File";


                }
                if (fuploadPDF.HasFile)
                {
                    string destDir = Server.MapPath("../store/statutesdocs/pdf/");

                    string FileName = Path.GetFileName(fuploadPDF.PostedFile.FileName);
                    string destPath = Path.Combine(destDir, FileName);
                    fuploadPDF.SaveAs(destPath);

                    pdf = fuploadPDF.FileName;
                    cntnttype = "File";


                }

                objstat.CatName = ddlCategories.SelectedItem.Text;
                objstat.Title = txtTitle.Text;
                objstat.CreatedBy = int.Parse(Session["UserID"].ToString());
                if (chkActive.Checked == true)
                    objstat.Active = 1;
                else
                    objstat.Active = 0;
                objstat.WorkflowID = 1;
                objstat.GroupID = int.Parse(ddlGroup.SelectedValue);
                objstat.SubGroupID = int.Parse(ddlSubGroup.SelectedValue);
                objstat.Primary_Secondary = ddlType.SelectedValue;
                objstat.PrimaryStatutesID = int.Parse(ddlPrimaryStatutes.SelectedValue);
                objstat.StatutesContentType = cntnttype;
                objstat.WordFileName = wordFile;
                objstat.PDFFileName = pdf;
                objstat.Act = txtAct.Text;
                objstat.Date = txtDate.Text;
                objstat.LastUpdatedDocumentDate = txtLastDoumentUpdateDate.Text.Trim();

                int chk = objstat.InsertStatutesUtility();
                if (chk > 0)
                {
                    AddPracticeAreaWithStatues(chk);
                    GetGridItemsOtherCategorysAndSave(chk);
                    divSuccess.Style["Display"] = "";
                    divSuccess.InnerText = "Record Created.. Statutes ID: " + chk.ToString(); 
                    divError.Style["Display"] = "none";
                    // ClearFields();

                }
                else
                {
                    divSuccess.Style["Display"] = "none";

                }
            }
            catch (Exception ex)
            {
                Email.SendMail(ConfigurationManager.AppSettings["regEmailTransferToAbubakar"].ToString(), ex.Message, "eastlaw error statutes upload error - new", "eastlaw.pk", "");
                Email.SendMail(ConfigurationManager.AppSettings["regEastLawTeam"].ToString(), ex.Message, "eastlaw error statutes upload error - new", "eastlaw.pk", "");
            }
        }
        void EditRecord(int ID)
        {
            try
            {
                string wordFile = "";
                string pdf = "";
                string cntnttype = "";
                //if (Session["tempID"] == null)
                //{
                //    SessionNumber();
                //}
                if (fuploadWord.HasFile)
                {
                    string destDir = Server.MapPath("../store/statutesdocs/word/");

                    string FileName = Path.GetFileName(fuploadWord.PostedFile.FileName);
                    string destPath = Path.Combine(destDir, FileName);
                    fuploadWord.SaveAs(destPath);

                    wordFile = fuploadWord.FileName;
                    cntnttype = "File";


                }
                else
                {
                    wordFile = lblfuploadWord.Text;
                }
                if (fuploadPDF.HasFile)
                {
                    string destDir = Server.MapPath("../store/statutesdocs/pdf/");

                    string FileName = Path.GetFileName(fuploadPDF.PostedFile.FileName);
                    string destPath = Path.Combine(destDir, FileName);
                    fuploadPDF.SaveAs(destPath);

                    pdf = fuploadPDF.FileName;
                    cntnttype = "File";


                }
                else
                {
                    pdf = lblfuploadPDF.Text;
                }

                objstat.CatID = int.Parse(ddlCategories.SelectedValue);
                objstat.Title = txtTitle.Text;
                objstat.CreatedBy = int.Parse(Session["UserID"].ToString());
                if (chkActive.Checked == true)
                    objstat.Active = 1;
                else
                    objstat.Active = 0;
                objstat.WorkflowID = 1;
                objstat.GroupID = int.Parse(ddlGroup.SelectedValue);
                objstat.SubGroupID = int.Parse(ddlSubGroup.SelectedValue);
                objstat.Primary_Secondary = ddlType.SelectedValue;
                objstat.PrimaryStatutesID = int.Parse(ddlPrimaryStatutes.SelectedValue);
                //objstat.StatutesContentType = cntnttype;
                objstat.WordFileName = wordFile;
                objstat.PDFFileName = pdf;
                objstat.Act = txtAct.Text;
                objstat.Date = txtDate.Text;
                objstat.LastUpdatedDocumentDate = txtLastDoumentUpdateDate.Text.Trim();

                int chk = objstat.EditStatutes(ID);
                if (chk > 0)
                {
                    AddPracticeAreaWithStatues(ID);
                    divSuccess.Style["Display"] = "";
                    divError.Style["Display"] = "none";
                    // ClearFields();

                }
                else
                {
                    divSuccess.Style["Display"] = "none";

                }
            }
            catch(Exception ex)
            {
                Email.SendMail(ConfigurationManager.AppSettings["regEmailTransferToAbubakar"].ToString(), ex.Message, "eastlaw error statutes upload error - edit", "eastlaw.pk", "");
                Email.SendMail(ConfigurationManager.AppSettings["regEastLawTeam"].ToString(), ex.Message, "eastlaw error statutes upload error - edit", "eastlaw.pk", "");

            }
        }
        void AddPracticeAreaWithStatues(int StatuesID)
        {
            try
            {
                for(int a=0;a<chkLstPA.Items.Count;a++)
                {
                    if (chkLstPA.Items[a].Selected == true)
                        objPA.TagPracticeAreaWithStatues(int.Parse(chkLstPA.Items[a].Value), StatuesID, int.Parse(Session["UserID"].ToString()), 0);
                    else
                        objPA.TagPracticeAreaWithStatues(int.Parse(chkLstPA.Items[a].Value), StatuesID, int.Parse(Session["UserID"].ToString()), 1);
                }
            }
            catch { }
        }
        void GetPracticeAreaByStatues(int StatuesID)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objPA.GetTaggesStatuesWithPracticeAreaByStatuesID(StatuesID);
                for (int a = 0; a < chkLstPA.Items.Count; a++)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (chkLstPA.Items[a].Value == dt.Rows[i]["PracticeAreaID"].ToString())
                            //chkLstPA.SelectedValue = dt.Rows[i]["PracticeAreaID"].ToString();
                            chkLstPA.Items[a].Selected = true;// = dt.Rows[i]["PracticeAreaID"].ToString();

                    }
                }

            }

            catch { }
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

            }
        }

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlType.SelectedValue != "PRIMARY")
                {
                    GetPrimaryStatutes("PRIMARY");
                    divPrimaryStatutes.Style["Display"] = "";
                    //rfvPS.Enabled = true;
                }
                else
                {
                    divPrimaryStatutes.Style["Display"] = "none";
                    //rfvPS.Enabled = false;
                }
            }
            catch { }
        }
        public void CreateTableValuesOtherCategory()
        {
            try
            {
             

                string[] OtherCategoryID = Session["OtherCategoryID"].ToString().Split('|');
                string[] OtherCategoryName = Session["OtherCategoryName"].ToString().Split('|');

                int recordnum = OtherCategoryID.Length;

                DataTable dt = new DataTable("tblOtherCat");
                dt.Columns.Add("OtherCategoryID");
                dt.Columns.Add("OtherCategoryName");
                for (int j = 0; j < recordnum - 1; j++)
                {
                    if (!string.IsNullOrEmpty(OtherCategoryID[j].ToString()))
                    {
                        DataRow dr = dt.NewRow();
                        dr["OtherCategoryID"] = OtherCategoryID[j].ToString();
                        dr["OtherCategoryName"] = OtherCategoryName[j].ToString();
                        dt.Rows.Add(dr);
                    }

                }
                ViewState["dt"] = dt;
                DataTable dt1 = (DataTable)ViewState["dt"];
                dt1 = dt.DefaultView.ToTable(true, "OtherCategoryID", "OtherCategoryName");
                gvOtherCat.DataSource = dt1;
                gvOtherCat.DataBind();



                //gvPracticeAreaSubCat.DataSource = dt.DefaultView;
                //gvPracticeAreaSubCat.DataBind();
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("AddGlossory.aspx", "CreateTableValuesPracticeArea", ex.Message);
            }



        }
        void GetGridItemsOtherCategorysAndSave(int StatuteID)
        {
            try
            {

                //GridViewRow row = default(GridViewRow);
                HiddenField hdID = default(HiddenField);

                foreach (GridViewRow row in gvOtherCat.Rows)
                {
                    if ((row != null))
                    {

                        hdID = (HiddenField)row.FindControl("hdID");

                        int chk = objstat.AddStatuteMultiCategory(StatuteID, int.Parse(hdID.Value), int.Parse(Session["UserID"].ToString()), ref strMsg);

                    }
                }
            }
            catch (Exception ex)
            {
                //    string script = "alert('" + ex.Message() + "')";
                //    ScriptManager.RegisterStartupScript(this, typeof(Page), "RedirectTo", script, true);
            }
        }
        protected void btnOtherCategories_Click(object sender, EventArgs e)
        {
            try
            {
                if ((Request.QueryString["param"] != null))
                {
                    int chk = objstat.AddStatuteMultiCategory(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())), int.Parse(ddlOtherCategories.SelectedValue),int.Parse(Session["UserID"].ToString()),ref strMsg);

                    GetStatuteOtherCategories(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())));
                }
                else
                {
                    Session["OtherCategoryID"] += ddlOtherCategories.SelectedValue + "|";
                    Session["OtherCategoryName"] += ddlOtherCategories.SelectedItem.Text + "|";
                    CreateTableValuesOtherCategory();
                }
            }
            catch { }
        }
    }
}