using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

namespace EastlawUI_v2.adminpanel
{
    public partial class AddGeneralAreas : System.Web.UI.Page
    {
        EastLawBL.PracticeAreas objPA = new EastLawBL.PracticeAreas();
        EastLawBL.GeneralArea objGA = new EastLawBL.GeneralArea();
        //DataTable dt;
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
                    RemoveSessions();
                    GetGeneralAreaType(0);
                    GetActivePracticeAreaCat();
                    //if (Request.QueryString["param"] != null)
                    //    GetCompanies(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())));
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("AddGeneralAreas.aspx", "Page_Load", ex.Message);
            }
        }
        void RemoveSessions()
        {
            Session.Remove("ID");
            Session.Remove("Name");
        }
        void SessionNumber()
        {
            int num;
            Random random = new Random();
            num = random.Next(0, 100000);
            Session["tempID"] = num;
        }
        void GetGeneralAreaType(int ID)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objGA.GetGeneralAreaTypes(ID);
                ddlGeneralAreaType.DataValueField = "ID";
                ddlGeneralAreaType.DataTextField = "GeneralAreaType";
                ddlGeneralAreaType.DataSource = dt;
                ddlGeneralAreaType.DataBind();

                ddlGeneralAreaType.Items.Insert(0, new ListItem("Select", "0"));
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("AddGeneralAreas.aspx", "GetGeneralAreaType", ex.Message);
            }
        }
        void GetActivePracticeAreaCat()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objPA.GetActivePracticeAreaCategories();
                ddlPracticeAreaCat.DataValueField = "ID";
                ddlPracticeAreaCat.DataTextField = "PracticeAreaCatName";
                ddlPracticeAreaCat.DataSource = dt;
                ddlPracticeAreaCat.DataBind();

                ddlPracticeAreaCat.Items.Insert(0, new ListItem("Select", "0"));
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("AddGeneralAreas.aspx", "GetGeneralAreaType", ex.Message);
            }
        }
        void GetActivePracticeAreaSubCat(int ID)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objPA.GetActivePracticeAreaSubCategoriesByCategory(ID);
                chkPracticeAreaSubCat.DataValueField = "ID";
                chkPracticeAreaSubCat.DataTextField = "PracticeAreaSubCatName";
                chkPracticeAreaSubCat.DataSource = dt;
                chkPracticeAreaSubCat.DataBind();

            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("AddGeneralAreas.aspx", "GetGeneralAreaType", ex.Message);
            }
        }

        public void CreateTableValuesPracticeArea()
        {
            try
            {

                string[] sa = Session["ID"].ToString().Split('|');
                string[] sb = Session["Name"].ToString().Split('|');

                int recordnum = sa.Length;

                DataTable dt = new DataTable("tblPracticeAreaSubCat");
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
                gvPracticeAreaSubCat.DataSource = dt1;
                gvPracticeAreaSubCat.DataBind();



                //gvPracticeAreaSubCat.DataSource = dt.DefaultView;
                //gvPracticeAreaSubCat.DataBind();
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("AddGeneralAreas.aspx", "CreateTableValuesPracticeArea", ex.Message);
            }



        }
        public void CreateTablevaluesDocuments()
        {
            try
            {
                string[] sa = Session["FileName"].ToString().Split('|');

                int recordnum = sa.Length;

                DataTable dt = new DataTable("tblDocs");
                dt.Columns.Add("ID");
                dt.Columns.Add("DocFileName");

                for (int j = 0; j < recordnum - 1; j++)
                {
                    if (!string.IsNullOrEmpty(sa[j].ToString()))
                    {
                        DataRow dr = dt.NewRow();
                        dr["DocFileName"] = sa[j].ToString();
                        dt.Rows.Add(dr);
                    }

                }
                ViewState["dt"] = dt;
                DataTable dt1 = (DataTable)ViewState["dt"];
                dt1 = dt.DefaultView.ToTable(true, "ID", "DocFileName");
                gvDocuments.DataSource = dt1;
                gvDocuments.DataBind();
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("AddGeneralAreas.aspx", "CreateTablevaluesDocuments", ex.Message);
            }


        }

        void SaveRecord()
        {
            try
            {
                objGA.TypeID = int.Parse(ddlGeneralAreaType.SelectedValue);
                objGA.Subject = txtSubject.Text.Trim();
                objGA.DDate = txtDate.Text;
                objGA.ShortDes = editorshortDes.Content.Trim();
                objGA.FullDes = editorDes.Content.Trim();
                objGA.Author = txtAuthor.Text.Trim();
                objGA.WorkFlowStatus = 1;
                if (chkActive.Checked == true)
                    objGA.Active = 1;
                else
                    objGA.Active = 0;
                objGA.CreatedBy = int.Parse(Session["UserID"].ToString());
                int chk = objGA.InsertGeneralArea();
                if (chk > 0)
                {
                    GetGridItemsPracticeAndSave(chk);
                    GetGridItemsDocumentsAndSave(chk);
                    divSuccess.Style["Display"] = "";
                    divError.Style["Display"] = "none";
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
                ExceptionHandling.SendErrorReport("AddCompany.aspx", "SaveRecord", e.Message);
            }
        }
        void GetGridItemsPracticeAndSave(int GeneralAreaID)
        {
            try
            {

                //GridViewRow row = default(GridViewRow);
                HiddenField hdID = default(HiddenField);
                TextBox SortOrder = default(TextBox);

                foreach (GridViewRow row in gvPracticeAreaSubCat.Rows)
                {
                    if ((row != null))
                    {

                        hdID = (HiddenField)row.FindControl("hdID");
                       // int hdID = int.Parse(hdProdID.Value);
                        objGA.GeneralAreasID = GeneralAreaID;
                        objGA.PracticeAreaSubCatID = int.Parse(hdID.Value);
                        objGA.CreatedBy = int.Parse(Session["UserID"].ToString());

                        int chk = objGA.InsertGeneralAreaPracticeAreaSubCat();


                    }
                }
            }
            catch (Exception ex)
            {
                //    string script = "alert('" + ex.Message() + "')";
                //    ScriptManager.RegisterStartupScript(this, typeof(Page), "RedirectTo", script, true);
            }
        }
        void GetGridItemsDocumentsAndSave(int GeneralAreaID)
        {
            try
            {

                //GridViewRow row = default(GridViewRow);
                HiddenField hdName = default(HiddenField);
                TextBox SortOrder = default(TextBox);

                foreach (GridViewRow row in gvDocuments.Rows)
                {
                    if ((row != null))
                    {

                        hdName = (HiddenField)row.FindControl("hdFileName");
                        // int hdID = int.Parse(hdProdID.Value);
                        objGA.GeneralAreasID = GeneralAreaID;
                        objGA.DocFileName = hdName.Value;
                        objGA.DocTitle = "";
                        objGA.CreatedBy = int.Parse(Session["UserID"].ToString());

                        int chk = objGA.InsertGeneralAreaDocuments();


                    }
                }
            }
            catch (Exception ex)
            {
                //    string script = "alert('" + ex.Message() + "')";
                //    ScriptManager.RegisterStartupScript(this, typeof(Page), "RedirectTo", script, true);
            }
        }
        protected void ddlGeneralAreaType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlGeneralAreaType.SelectedValue == "3")
                    divAuthor.Style["Display"] = "";
                else
                    divAuthor.Style["Display"] = "none";
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("AddGeneralAreas.aspx", "ddlGeneralAreaType_SelectedIndexChanged", ex.Message);
            }
        }

        protected void ddlPracticeAreaCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetActivePracticeAreaSubCat(int.Parse(ddlPracticeAreaCat.SelectedValue));
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("AddGeneralAreas.aspx", "ddlPracticeAreaCat_SelectedIndexChanged", ex.Message);
            }
        }

        protected void btnAddPracticeAreaSubCat_Click(object sender, EventArgs e)
        {
            try
            {
                for (int a = 0; a < chkPracticeAreaSubCat.Items.Count; a++)
                {
                    if (chkPracticeAreaSubCat.Items[a].Selected == true)
                    {
                        Session["ID"] += chkPracticeAreaSubCat.Items[a].Value + "|";
                        Session["Name"] += chkPracticeAreaSubCat.Items[a].Text + "|";
                        CreateTableValuesPracticeArea();
                    }
                }
            }

            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("AddGeneralAreas.aspx", "btnAddPracticeAreaSubCat_Click", ex.Message);
            }
        }



        protected void btnAddDocUpload_Click(object sender, EventArgs e)
        {
            try
            {
                string file = "";
                if (Session["tempID"] == null)
                {
                    SessionNumber();
                }
                if (FDocUpload.HasFile)
                {
                    string destDir = Server.MapPath("../store/generalareadocs/");

                    string FileName = Path.GetFileName(FDocUpload.PostedFile.FileName);
                    string destPath = Path.Combine(destDir, Session["tempID"].ToString() + FileName.Replace(" ", ""));
                    FDocUpload.SaveAs(destPath);

                    file = Session["tempID"].ToString() + FDocUpload.FileName.Replace(" ", "");

                    Session["FileName"] += file + "|";
                    CreateTablevaluesDocuments();


                }
            }

            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("AddGeneralAreas.aspx", "btnAddDocUpload_Click", ex.Message);
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SaveRecord();
            }

            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("AddGeneralAreas.aspx", "btnSave_Click", ex.Message);
            }
        }
    }
}
