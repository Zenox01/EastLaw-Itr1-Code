using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace EastlawUI_v2.adminpanel
{
    public partial class AddGlossary : System.Web.UI.Page
    {
        EastLawBL.Keywords objkey = new EastLawBL.Keywords();
        EastLawBL.PracticeAreas objPA = new EastLawBL.PracticeAreas();
        EastLawBL.Journals objJo = new EastLawBL.Journals();
        EastLawBL.Cases objcases = new EastLawBL.Cases();
        EastLawBL.Statutes objstat = new EastLawBL.Statutes();
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
                    GetJournals();
                    GetPracticeArea();
                    GetKweywords();
                    GetLaws();
                    if ((Request.QueryString["param"] != null))
                        GetGlossaryDetails(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())));

                }
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("AddGlossary.aspx", "Page_Load", ex.Message);
            }
           
        }
        void GetGlossaryDetails(int ID)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objkey.GetGlossory(ID);
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
                        GetParentGlossory();

                        DataTable dtL2 = new DataTable();
                        dtL2 = objkey.GetGlossory(int.Parse(dt.Rows[0]["ParentID"].ToString()));
                        ddlLevel1.SelectedValue = dtL2.Rows[0]["ID"].ToString();

                        divLevel1.Style["Display"] = "";
                        rfvddlLevel1.Enabled = true;

                        divLevel2.Style["Display"] = "none";
                        rfvddlLeve2.Enabled = false;

                        if (dtL2.Rows[0]["ParentID"].ToString() != "0")
                        {

                            ddlType.SelectedValue = "L2";
                            //GetGlossoryByParent(int.Parse(dt.Rows[0]["ParentID"].ToString()));
                            GetParentGlossory();

                            //DataTable dtL2 = new DataTable();
                            dtL2 = objkey.GetGlossory(int.Parse(dt.Rows[0]["ParentID"].ToString()));
                            ddlLevel1.SelectedValue = dtL2.Rows[0]["ParentID"].ToString();

                            divLevel1.Style["Display"] = "";
                            rfvddlLevel1.Enabled = true;

                            divLevel2.Style["Display"] = "none";
                            rfvddlLeve2.Enabled = false;


                            ddlType.SelectedValue = "L3";
                            GetGlossoryByParent(int.Parse(dtL2.Rows[0]["ParentID"].ToString()));
                            
                            DataTable dtL3 = new DataTable();
                            dtL3 = objkey.GetGlossory(int.Parse(dtL2.Rows[0]["ParentID"].ToString()));
                            //ddlLevel1.SelectedValue = dtL2.Rows[0]["ID"].ToString();
                            ddlLevel2.SelectedValue = dtL2.Rows[0]["ID"].ToString();

                            divLevel2.Style["Display"] = "";
                            rfvddlLeve2.Enabled = true;
                        }
                    }
                    
                    txtTitle.Text = dt.Rows[0]["GlossoryName"].ToString();
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
                    GetGlossaryTagging(ID);
                }
            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("ManageDepartmentFile.aspx", "GetDeptFileDetails", e.Message);
            }
        }
        void GetGlossaryTagging(int GlossaryID)
        {
            try
            {
                DataTable dtKeywords = new DataTable();
                dtKeywords = objkey.GetGlossoryKeyword(GlossaryID);
                gvKeywords.DataSource = dtKeywords;
                gvKeywords.DataBind();

                DataTable dtCitation = new DataTable();
                dtCitation = objkey.GetGlossoryCitation(GlossaryID);
                gvCitations.DataSource = dtCitation;
                gvCitations.DataBind();

                DataTable dtLaws = new DataTable();
                dtLaws = objkey.GetGlossoryLaw(GlossaryID);
                gvLaws.DataSource = dtLaws;
                gvLaws.DataBind();
            }
            catch { }
        }
        void GetLaws()
        {
            try
            {
                string cri = "Where A.IsDeleted=0";


                DataTable dt = new DataTable();
                dt = objstat.GetActiveStatutesLessInfo();
              
                ddlLaws.DataValueField = "ID";
                ddlLaws.DataTextField = "Title";
                ddlLaws.DataSource = dt;
                ddlLaws.DataBind();

                ddlLaws.Items.Insert(0, new ListItem("Not Applicable", "0"));
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("AddCompany.aspx", "GetOrgTypes", ex.Message);
            }
        }
        void GetJournals()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objJo.GetActiveJournals();

                ddlJournals.DataValueField = "ID";
                ddlJournals.DataTextField = "JournalName";
                ddlJournals.DataSource = dt;
                ddlJournals.DataBind();
                ddlJournals.Items.Insert(0, new ListItem("Select", "0"));

            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("MemberHome.aspx", "GetJournals", e.Message);
            }
        }
        void GetPracticeArea()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objPA.GetActivePracticeAreaSubCategoriesByCategory(3);
                ddlPA.DataValueField = "ID";
                ddlPA.DataTextField = "PracticeAreaSubCatName";
                ddlPA.DataSource = dt;
                ddlPA.DataBind();

                ddlPA.Items.Insert(0, new ListItem("Not Applicable", "0"));
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("AddGlossary.aspx", "GetPracticeArea", ex.Message);
            }
        }
        void GetParentGlossory()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objkey.GetActiveGlossoryParent();
                ddlLevel1.DataValueField = "ID";
                ddlLevel1.DataTextField = "GlossoryName";
                ddlLevel1.DataSource = dt;
                ddlLevel1.DataBind();

                ddlLevel1.Items.Insert(0, new ListItem("Select", "0"));
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("AddGlossary.aspx", "GetParentGlossory", ex.Message);
            }
        }
        void GetGlossoryByParent(int ParentID)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objkey.GetActiveGlossoryByParent(ParentID);
                ddlLevel2.DataValueField = "ID";
                ddlLevel2.DataTextField = "GlossoryName";
                ddlLevel2.DataSource = dt;
                ddlLevel2.DataBind();

                ddlLevel2.Items.Insert(0, new ListItem("Select", "0"));
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("AddCompany.aspx", "GetOrgTypes", ex.Message);
            }
        }
        void GetKweywords()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objkey.GetKeywords(0);
                ddlKeywords.DataValueField = "ID";
                ddlKeywords.DataTextField = "Keywords";
                ddlKeywords.DataSource = dt;
                ddlKeywords.DataBind();

                ddlKeywords.Items.Insert(0, new ListItem("Select", "0"));
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("AddCompany.aspx", "GetOrgTypes", ex.Message);
            }
        }
        void RemoveSessions()
        {
            Session.Remove("ID");
            Session.Remove("Name");
            Session.Remove("CID");
            Session.Remove("CName");
            Session.Remove("LCID");
            Session.Remove("LCName");
        }
        public void CreateTableValuesKeywords()
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
        public void CreateTableValuesCitation()
        {
            try
            {

                string[] sa = Session["CID"].ToString().Split('|');
                string[] sb = Session["CName"].ToString().Split('|');

                int recordnum = sa.Length;

                DataTable dt = new DataTable("tblCitation");
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
                ViewState["dtCitation"] = dt;
                DataTable dt1 = (DataTable)ViewState["dtCitation"];
                dt1 = dt.DefaultView.ToTable(true, "Id", "Name");
                gvCitations.DataSource = dt1;
                gvCitations.DataBind();



                //gvPracticeAreaSubCat.DataSource = dt.DefaultView;
                //gvPracticeAreaSubCat.DataBind();
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("AddGeneralAreas.aspx", "CreateTableValuesPracticeArea", ex.Message);
            }



        }
        public void CreateTableValuesLaws()
        {
            try
            {

                string[] sa = Session["LID"].ToString().Split('|');
                string[] sb = Session["LName"].ToString().Split('|');

                int recordnum = sa.Length;

                DataTable dt = new DataTable("tblLaws");
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
                gvLaws.DataSource = dt1;
                gvLaws.DataBind();



                //gvPracticeAreaSubCat.DataSource = dt.DefaultView;
                //gvPracticeAreaSubCat.DataBind();
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("AddGlossory.aspx", "CreateTableValuesPracticeArea", ex.Message);
            }



        }
        void SaveRecord()
        {
            try
            {
                if (ddlType.SelectedValue == "L1")
                {
                    objkey.ParentID = 0;
                }
                else if (ddlType.SelectedValue == "L2")
                {
                    objkey.ParentID = int.Parse(ddlLevel1.SelectedValue);
                }
                else if (ddlType.SelectedValue == "L3")
                {
                    objkey.ParentID = int.Parse(ddlLevel2.SelectedValue);
                }
                
                objkey.GlossoryName = txtTitle.Text.Trim();
                objkey.PracticeAreaSubCatID = int.Parse(ddlPA.SelectedValue);
                
                if (chkActive.Checked == true)
                    objkey.Active = 1;
                else
                    objkey.Active = 0;
               // objkey.CreatedBy = 0;
                objkey.CreatedBy = int.Parse(Session["UserID"].ToString());
                int chk = objkey.InsertGlossory();
                if (chk > 0)
                {
                    GetGridItemskeywordsAndSave(chk);
                    GetGridItemsCitationsAndSave(chk);
                    GetGridItemsLawssAndSave(chk);
                    //GetGridItemsDocumentsAndSave(chk);
                    divSuccess.Style["Display"] = "";
                    divError.Style["Display"] = "none";
                    ClearFields();
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
                    objkey.ParentID = 0;
                }
                else if (ddlType.SelectedValue == "L2")
                {
                    objkey.ParentID = int.Parse(ddlLevel1.SelectedValue);
                }
                else if (ddlType.SelectedValue == "L3")
                {
                    objkey.ParentID = int.Parse(ddlLevel2.SelectedValue);
                }

                objkey.GlossoryName = txtTitle.Text.Trim();
                objkey.PracticeAreaSubCatID = int.Parse(ddlPA.SelectedValue);

                if (chkActive.Checked == true)
                    objkey.Active = 1;
                else
                    objkey.Active = 0;
                // objkey.CreatedBy = 0;
                objkey.ModifiedBy = int.Parse(Session["UserID"].ToString());
                int chk = objkey.EditGlossory(ID);
                if (chk > 0)
                {
                   
                    //GetGridItemsDocumentsAndSave(chk);
                    divSuccess.Style["Display"] = "";
                    divError.Style["Display"] = "none";
                    ClearFields();
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
        void GetGridItemskeywordsAndSave(int GlossoryID)
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
                      
                        int chk = objkey.InsertGlossoryKeywords(GlossoryID, int.Parse(hdID.Value));


                    }
                }
            }
            catch (Exception ex)
            {
                //    string script = "alert('" + ex.Message() + "')";
                //    ScriptManager.RegisterStartupScript(this, typeof(Page), "RedirectTo", script, true);
            }
        }
        void GetGridItemsCitationsAndSave(int GlossoryID)
        {
            try
            {

                //GridViewRow row = default(GridViewRow);
                HiddenField hdID = default(HiddenField);

                foreach (GridViewRow row in gvCitations.Rows)
                {
                    if ((row != null))
                    {

                        hdID = (HiddenField)row.FindControl("hdID");

                        int chk = objkey.InsertGlossoryCitation(GlossoryID, int.Parse(hdID.Value));


                    }
                }
            }
            catch (Exception ex)
            {
                //    string script = "alert('" + ex.Message() + "')";
                //    ScriptManager.RegisterStartupScript(this, typeof(Page), "RedirectTo", script, true);
            }
        }
        void GetGridItemsLawssAndSave(int GlossoryID)
        {
            try
            {

                //GridViewRow row = default(GridViewRow);
                HiddenField hdID = default(HiddenField);

                foreach (GridViewRow row in gvLaws.Rows)
                {
                    if ((row != null))
                    {

                        hdID = (HiddenField)row.FindControl("hdID");

                        int chk = objkey.InsertGlossoryLaw(GlossoryID, int.Parse(hdID.Value));


                    }
                }
            }
            catch (Exception ex)
            {
                //    string script = "alert('" + ex.Message() + "')";
                //    ScriptManager.RegisterStartupScript(this, typeof(Page), "RedirectTo", script, true);
            }
        }
        void ClearFields()
        {
            ddlType.SelectedIndex = 0;
            ddlLevel1.SelectedIndex = 0;
            ddlLevel2.SelectedIndex = 0;
            txtTitle.Text = "";
            ddlKeywords.SelectedIndex = 0;
            chkActive.Checked = false;
            divLevel1.Style["Display"] = "none";
            rfvddlLevel1.Enabled = false;

            divLevel2.Style["Display"] = "none";
            rfvddlLeve2.Enabled = false;

            gvKeywords.DataSource = null;
            gvKeywords.DataBind();
            
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //Response.Write(ddlKeywords.SelectedValue);
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

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if ((Request.QueryString["param"] != null))
                {
                    int chk = objkey.InsertGlossoryKeywords(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())), int.Parse(ddlKeywords.SelectedValue));
                    DataTable dtKeywords = new DataTable();
                    dtKeywords = objkey.GetGlossoryKeyword(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())));
                    gvKeywords.DataSource = dtKeywords;
                    gvKeywords.DataBind();

                }
                else
                {
                    Session["ID"] += ddlKeywords.SelectedValue + "|";
                    Session["Name"] += ddlKeywords.SelectedItem.Text + "|";
                    CreateTableValuesKeywords();
                }
            }
            catch { }
        }

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlType.SelectedValue =="L1")
            {
                divLevel1.Style["Display"] = "none";
                rfvddlLevel1.Enabled = false;

                divLevel2.Style["Display"] = "none";
                rfvddlLeve2.Enabled = false;
            }
            else if (ddlType.SelectedValue == "L2")
            {
                GetParentGlossory();
                divLevel1.Style["Display"] = "";
                rfvddlLevel1.Enabled = true;

                divLevel2.Style["Display"] = "none";
                rfvddlLeve2.Enabled = false;
            }
            else if (ddlType.SelectedValue == "L3")
            {
                GetParentGlossory();
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
                GetGlossoryByParent(int.Parse(ddlLevel1.SelectedValue));
            }
            catch { }
        }

        protected void btnAddCitation_Click(object sender, EventArgs e)
        {
            
            try
            {
                string cri = "Where A.Citation is not null";
              
                if (!string.IsNullOrEmpty(txtCitationYear.Text.Trim()))
                    cri = cri + " AND A.Year='" + txtCitationYear.Text.Trim() + "'";

                if (ddlJournals.SelectedValue != "0")
                    cri = cri + " AND A.JournalID='" + ddlJournals.SelectedValue + "'";

                //if (!string.IsNullOrEmpty(txtCitationNumber.Text.Trim()))
                //    cri = cri + " AND A.Citation like '%"+txtCitationNumber.Text.Trim()+"%'";

                if (!string.IsNullOrEmpty(txtCitationNumber.Text.Trim()))
                    cri = cri + " AND  CONTAINS (A.Citation, '" + txtCitationNumber.Text.Trim() + "' )";

                DataTable dt = new DataTable();
                dt = objcases.GetCasesSearch(cri,0,30);
                if(dt != null)
                {
                    if(dt.Rows.Count > 0)
                    {
                        if ((Request.QueryString["param"] != null))
                        {
                            int chk = objkey.InsertGlossoryCitation(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())), int.Parse(dt.Rows[0]["ID"].ToString()));
                          
                            DataTable dtCitation = new DataTable();
                            dtCitation = objkey.GetGlossoryCitation(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())));
                            gvCitations.DataSource = dtCitation;
                            gvCitations.DataBind();

                        }
                        else
                        {
                            Session["CID"] += dt.Rows[0]["ID"].ToString() + "|";
                            Session["CName"] += dt.Rows[0]["CitationRef"].ToString() + " " + dt.Rows[0]["Appeallant"].ToString() + "|";
                            CreateTableValuesCitation();
                        }
                    }
                }

            }
            catch (Exception ex)
            {

            }
        

                
            
        }

        protected void btnAddLaws_Click(object sender, EventArgs e)
        {
            try
            {
                if ((Request.QueryString["param"] != null))
                {
                    int chk = objkey.InsertGlossoryLaw(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())), int.Parse(ddlLaws.SelectedValue));
                  
                    DataTable dtLaws = new DataTable();
                    dtLaws = objkey.GetGlossoryLaw(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())));
                    gvLaws.DataSource = dtLaws;
                    gvLaws.DataBind();
                }
                else
                {
                    Session["LID"] += ddlLaws.SelectedValue + "|";
                    Session["LName"] += ddlLaws.SelectedItem.Text + "|";
                    CreateTableValuesLaws();
                }
            }
            catch { }
        }
    }
}