using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

namespace EastlawUI_v2.adminpanel
{
    public partial class ReviewCasesMigration : System.Web.UI.Page
    {
        EastLawBL.Cases objCases = new EastLawBL.Cases();
        EastLawBL.Judges objJudge = new EastLawBL.Judges();
        EastLawBL.Advocates objAdv = new EastLawBL.Advocates();
        EastLawBL.Journals objJo = new EastLawBL.Journals();
        EastLawBL.PracticeAreas objPA = new EastLawBL.PracticeAreas();
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
                    GetJournals();
                    LoadYears();
                    GetCourts();
                    GetCourtMaster();
                    //  GetAdvocates(0);
                    //  GetJudges(0);
                    GetActivePracticeCategories();

                    if (Request.QueryString["param"] != null)
                        GetCases(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())));
                    else
                    {
                        txtJudgeName.Visible = true;
                        txtAdvA.Visible = true;
                        txtAdvR.Visible = true;
                        //GetAdvocates(0);
                        //GetJudges(0);
                    }
                    Session.Remove("CID");
                    Session.Remove("CName");
                    Session.Remove("CKey");

                    Session.Remove("AltYear");
                    Session.Remove("AltJournalID");
                    Session.Remove("AltJournalName");
                    Session.Remove("AltPageNo");
                    Session.Remove("AltCitation");

                    DataTable dt = new DataTable();
                    dt.Columns.AddRange(new DataColumn[3] { new DataColumn("Citation"), new DataColumn("LinkedCaseID"), new DataColumn("Journal") });
                    ViewState["CitationHyperlinking"] = dt;
                    this.BindGridHyperlinking();

                    DataTable dtPA = new DataTable();
                    dtPA.Columns.AddRange(new DataColumn[3] { new DataColumn("ID"), new DataColumn("PACat"), new DataColumn("PASubCat") });
                    ViewState["PALst"] = dtPA;
                    this.BindPraciceAreaGrid();
                    


                }
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("ReviewCasesMigration.aspx", "Page_Load", ex.Message);
            }
        }
        void GetCourtMaster()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objCases.GetActiveCourtMasters();

                ddlCourtMaster.DataSource = dt;
                ddlCourtMaster.DataTextField = "CourtName";
                ddlCourtMaster.DataValueField = "ID";
                ddlCourtMaster.DataBind();

                ddlCourtMaster.Items.Insert(0, new ListItem("Select", "0"));
            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("ManageJudges.aspx", "GetJudges", e.Message);
            }
        }
        void GetJournals()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objJo.GetActiveJournals();
                    ddlJournal.DataValueField = "ID";
                    ddlJournal.DataTextField = "JournalName";
                    ddlJournal.DataSource = dt;
                    ddlJournal.DataBind();
                    ddlJournal.Items.Insert(0, new ListItem("Select", "0"));

                    ddlJournalSearch.DataValueField = "ID";
                    ddlJournalSearch.DataTextField = "JournalName";
                    ddlJournalSearch.DataSource = dt;
                    ddlJournalSearch.DataBind();
                    ddlJournalSearch.Items.Insert(0, new ListItem("Select", "0"));

                    ddlAlternateCaseJournal.DataValueField = "ID";
                    ddlAlternateCaseJournal.DataTextField = "JournalName";
                    ddlAlternateCaseJournal.DataSource = dt;
                    ddlAlternateCaseJournal.DataBind();
                    ddlAlternateCaseJournal.Items.Insert(0, new ListItem("Select", "0"));

                    ddlSourceAlternateCaseJournal.DataValueField = "ID";
                    ddlSourceAlternateCaseJournal.DataTextField = "JournalName";
                    ddlSourceAlternateCaseJournal.DataSource = dt;
                    ddlSourceAlternateCaseJournal.DataBind();
                    ddlSourceAlternateCaseJournal.Items.Insert(0, new ListItem("Select", "0"));

                    CitationHyperlinkingJournal.DataValueField = "ID";
                    CitationHyperlinkingJournal.DataTextField = "JournalName";
                    CitationHyperlinkingJournal.DataSource = dt;
                    CitationHyperlinkingJournal.DataBind();
                    CitationHyperlinkingJournal.Items.Insert(0, new ListItem("Select", "0"));

            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("ReviewCasesMigration.aspx", "GetAdvocates", e.Message);
            }
        }
        void GetJudgesByCourtMaster(int CourtMasterID)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objJudge.GetJudgeNewByCourtMaster(CourtMasterID);

                ddlNewJudge1.DataSource = dt;
                ddlNewJudge1.DataTextField = "JudgeName";
                ddlNewJudge1.DataValueField = "ID";
                ddlNewJudge1.DataBind();
                ddlNewJudge1.Items.Insert(0, new ListItem("Select", "0"));

                ddlNewJudge2.DataSource = dt;
                ddlNewJudge2.DataTextField = "JudgeName";
                ddlNewJudge2.DataValueField = "ID";
                ddlNewJudge2.DataBind();
                ddlNewJudge2.Items.Insert(0, new ListItem("Select", "0"));

                ddlNewJudge3.DataSource = dt;
                ddlNewJudge3.DataTextField = "JudgeName";
                ddlNewJudge3.DataValueField = "ID";
                ddlNewJudge3.DataBind();
                ddlNewJudge3.Items.Insert(0, new ListItem("Select", "0"));

                ddlNewJudge4.DataSource = dt;
                ddlNewJudge4.DataTextField = "JudgeName";
                ddlNewJudge4.DataValueField = "ID";
                ddlNewJudge4.DataBind();
                ddlNewJudge4.Items.Insert(0, new ListItem("Select", "0"));

                ddlNewJudge5.DataSource = dt;
                ddlNewJudge5.DataTextField = "JudgeName";
                ddlNewJudge5.DataValueField = "ID";
                ddlNewJudge5.DataBind();
                ddlNewJudge5.Items.Insert(0, new ListItem("Select", "0"));

                ddlNewJudge6.DataSource = dt;
                ddlNewJudge6.DataTextField = "JudgeName";
                ddlNewJudge6.DataValueField = "ID";
                ddlNewJudge6.DataBind();
                ddlNewJudge6.Items.Insert(0, new ListItem("Select", "0"));

                ddlNewJudge7.DataSource = dt;
                ddlNewJudge7.DataTextField = "JudgeName";
                ddlNewJudge7.DataValueField = "ID";
                ddlNewJudge7.DataBind();
                ddlNewJudge7.Items.Insert(0, new ListItem("Select", "0"));
            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("ManageJudges.aspx", "GetJudges", e.Message);
            }
        }
        void GetCourts()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objCases.GetCasesCourtsGroup();

                DataTable dt1 = new DataTable();
                dt1 = objCases.GetActiveCourtMasters();

                //ddlRadCourts.DataValueField = "CourtName";
                //ddlRadCourts.DataTextField = "CourtName";
                //ddlRadCourts.DataSource = dt1;
                //ddlRadCourts.DataBind();
                //ddlRadCourts.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem("Select", "0"));

                ddlCourts.DataValueField = "court";
                ddlCourts.DataTextField = "court";
                ddlCourts.DataSource = dt;
                ddlCourts.DataBind();
                ddlCourts.Items.Insert(0, new ListItem("Select", "0"));



                ddlSearchCourt.DataValueField = "court";
                ddlSearchCourt.DataTextField = "court";
                ddlSearchCourt.DataSource = dt;
                ddlSearchCourt.DataBind();
                ddlSearchCourt.Items.Insert(0, new ListItem("Select", "0"));


            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("ReviewCasesMigration.aspx", "GetAdvocates", e.Message);
            }
        }
        void LoadYears()
        {
            // Clear items:    
            ddlYear.Items.Clear();
            // Add default item to the list
            ddlYear.Items.Insert(0, new ListItem("Select", "0"));
            // Start loop
            for (int i = 0; i < 69; i++)
            {
                // For each pass add an item
                // Add a number of years (negative, which will subtract) to current year
                ddlYear.Items.Add(DateTime.Now.AddYears(-i).Year.ToString());
            }

            for (int i = 1; i < 201; i++)
            {
                // For each pass add an item
                // Add a number of years (negative, which will subtract) to current year
                ddlYear.Items.Add(i.ToString());

            }
        }
        void GetAdvocates(int ID)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objAdv.GetAdvocatesForLoad(ID);
                if (ID == 0)
                {
                    ddlAdvA.DataValueField = "ID";
                    ddlAdvA.DataTextField = "AdvocateName";
                    ddlAdvA.DataSource = dt;
                    ddlAdvA.DataBind();
                    ddlAdvA.Items.Insert(0, new ListItem("Select", "0"));

                    ddlAdvR.DataValueField = "ID";
                    ddlAdvR.DataTextField = "AdvocateName";
                    ddlAdvR.DataSource = dt;
                    ddlAdvR.DataBind();
                    ddlAdvR.Items.Insert(0, new ListItem("Select", "0"));
                }
                else
                {
                }
            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("ReviewCasesMigration.aspx", "GetAdvocates", e.Message);
            }
        }
        void GetJudges(int ID)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objJudge.GetJudgeForLoad(ID);
                if (ID == 0)
                {
                    ddlJudge.DataValueField = "ID";
                    ddlJudge.DataTextField = "JudgeName";
                    ddlJudge.DataSource = dt;
                    ddlJudge.DataBind();
                    ddlJudge.Items.Insert(0, new ListItem("Select", "0"));
                }
                else
                {
                  
                }
            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("ReviewCasesMigration.aspx", "GetJudges", e.Message);
            }
        }
       
        void GetCases(int ID)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objCases.GetCases(ID);
                if (ID == 0)
                {
                    //dt.Columns.Add("strActive");
                    //for (int a = 0; a < dt.Rows.Count; a++)
                    //{
                    //    if (dt.Rows[a]["Active"].ToString() == "1")
                    //        dt.Rows[a]["strActive"] = "Yes";
                    //    else
                    //        dt.Rows[a]["strActive"] = "No";
                    //}
                    //dt.AcceptChanges();
                    //gv.DataSource = dt;
                    //gv.DataBind();
                }
                else
                {
                    ddlJournal.SelectedValue = dt.Rows[0]["JournalID"].ToString();
                    ddlYear.SelectedValue = dt.Rows[0]["Year"].ToString();
                    txtCitation.Text = dt.Rows[0]["Citation"].ToString();
                    txtAutoGeneratedCitation.Text = dt.Rows[0]["CitationRef"].ToString();
                   // ddlJudge.SelectedValue = dt.Rows[0]["Judge"].ToString();
                    lblJudgeID.Text = dt.Rows[0]["Judge"].ToString();
                    lblJudge.Text = dt.Rows[0]["JudgeName"].ToString();

                    txtAppeal.Text = dt.Rows[0]["Appeal"].ToString();
                    txtAppeallant1.Text = dt.Rows[0]["Appeallant"].ToString();
                    txtAppeallant2.Text = dt.Rows[0]["Appeallant2"].ToString();
                    txtAppeallant3.Text = dt.Rows[0]["Appeallant3"].ToString();
                    txtAppeallantType.Text = dt.Rows[0]["AppeallantType"].ToString();
                    txtRespondent1.Text = dt.Rows[0]["Respondent"].ToString();
                    txtRespondent2.Text = dt.Rows[0]["Respondent2"].ToString();
                    txtRespondent3.Text = dt.Rows[0]["Respondent3"].ToString();
                    txtJDate.Text = dt.Rows[0]["JDate"].ToString();
                   // ddlAdvA.SelectedValue = dt.Rows[0]["AdvocateA"].ToString();
                    lblAdvAID.Text = dt.Rows[0]["AdvocateA"].ToString();
                    lblaAdvA.Text = dt.Rows[0]["AdvA"].ToString();

                   // ddlAdvR.SelectedValue = dt.Rows[0]["AdvocateR"].ToString();
                    lblAdvRID.Text = dt.Rows[0]["AdvocateR"].ToString();
                    lblAdvR.Text = dt.Rows[0]["AdvR"].ToString();

                    txtHearDate.Text = dt.Rows[0]["HearDate"].ToString();
                    editorHeadNotes.Content = dt.Rows[0]["HeadNotes"].ToString();
                    editorJudgment.Content = dt.Rows[0]["Judgment"].ToString();
                    txtJudgmentType.Text = dt.Rows[0]["JudgmentType"].ToString();
                    txtResult.Text = dt.Rows[0]["Result"].ToString();
                    ddlCourts.SelectedValue = dt.Rows[0]["Court"].ToString();
                    txtCourtCityName.Text = dt.Rows[0]["Court_Area"].ToString();
                }

            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("ReviewCasesMigration.aspx", "GetCases", e.Message);
            }
        }
        void SaveRecord()
        {
            try
            {
                if (objCases.isCitationExist(txtCitation.Text.Trim()) == true)
                {
                    
                    divError.InnerHtml= "Citation Already Exist in Main Case DB.";
                    divSuccess.Style["Display"] = "none";
                    divError.Style["Display"] = "";

                    return;
                }
                if (objCases.isCitationExistInTemp(txtCitation.Text.Trim()) == true)
                {

                    divError.InnerHtml = "Citation Already Exist in Temp DB.";
                    divSuccess.Style["Display"] = "none";
                    divError.Style["Display"] = "";

                    return;
                }

                objCases.Status = 2;
                objCases.JournalID = int.Parse(ddlJournal.SelectedValue);
                objCases.Year = int.Parse(ddlYear.SelectedValue);
                objCases.Citation = txtCitation.Text.Trim();
                objCases.JudgeStr = txtJudgeName.Text;
                //objCases.Judge = int.Parse(lblJudgeID.Text);

                objCases.Appeal = txtAppeal.Text.Trim();
                objCases.Appeallant = txtAppeallant1.Text.Trim();
                objCases.Appeallant2 = txtAppeallant2.Text.Trim();
                objCases.Appeallant3 = txtAppeallant2.Text.Trim();
                objCases.AppeallantType = txtAppeallantType.Text.Trim();
                objCases.Respondent = txtRespondent1.Text.Trim();
                objCases.Respondent2 = txtRespondent2.Text.Trim();
                objCases.Respondent3 = txtRespondent3.Text.Trim();
                objCases.JDate = txtJDate.Text.Trim();
                objCases.AdvocateASTR = txtAdvA.Text;
                //objCases.AdvocateA = int.Parse(lblAdvAID.Text);
                objCases.AdvocateRSTR = txtAdvR.Text;
               // objCases.AdvocateR = int.Parse(lblAdvRID.Text);
                objCases.HearDate = txtHearDate.Text.Trim();
                objCases.HeadNotes = editorHeadNotes.Content;
                objCases.Judgment = editorJudgment.Content;
                objCases.JudgmentType = txtJudgmentType.Text.Trim();
                objCases.Result = txtResult.Text.Trim();
                objCases.Court = ddlCourts.SelectedValue;//.Text.Trim();// ddlRadCourts.SelectedValue;
                objCases.CreatedBy = int.Parse(Session["UserID"].ToString());

                if(chkManualCaseSummary.Checked==false)
                objCases.CaseSummary =  CommonClass.GetCaseSummary(System.Text.RegularExpressions.Regex.Replace(editorJudgment.Content, @"\r\n?|\n", "<br/>").Trim());
                else
                    objCases.CaseSummary = editortxtSummary.Content;

                objCases.CourtCityName = txtCourtCityName.Text;
                objCases.DateFormated = ValidateDate(txtJDate.Text.Trim());
                //objCases.PageNo = txtCitation.Text.Trim().Split(' ').Last();
                objCases.PageNo = txtCitationNo.Text.Trim();
                if (chkPriorityTagging.Checked == true)
                    objCases.PriorityTagging = 1;
                else
                    objCases.PriorityTagging = 0;
                //int chk = objCases.InsertCaseMigrate();
                int chk = objCases.InsertCaseMigrateTemp();
                if (chk > 0)
                {
                    GetGridItemsHyperLinking(chk);
                    GetGridItemsAlternateCitationsAndSave(chk);
                    AddNewJudges(chk);
                    GetGridItemsPracticeArea(chk);
                    divSuccess.Style["Display"] = "";
                    divError.Style["Display"] = "none";
                    // ClearFields();

                }
                else
                {
                    divSuccess.Style["Display"] = "none";
                    divError.InnerHtml = "Transaction Failed.";
                    divError.Style["Display"] = "";
                }
            }
            catch (Exception ex)
            { }
        }
        void EditRecord(int CaseID)
        {
            try
            {
               
                objCases.Status = 2;
                objCases.JournalID = int.Parse(ddlJournal.SelectedValue);
                objCases.Year = int.Parse(ddlYear.SelectedValue);
                objCases.Citation = txtCitation.Text.Trim();
                //objCases.Judge = int.Parse(ddlJudge.SelectedValue);
                objCases.Judge = int.Parse(lblJudgeID.Text);

                objCases.Appeal = txtAppeal.Text.Trim();
                objCases.Appeallant = txtAppeallant1.Text.Trim();
                objCases.AppeallantType = txtAppeallantType.Text.Trim();
                objCases.Respondent = txtRespondent1.Text.Trim();
                objCases.JDate = txtJDate.Text.Trim();
                //objCases.AdvocateA =int.Parse(ddlAdvA.SelectedValue);
                objCases.AdvocateA = int.Parse(lblAdvAID.Text);
                //objCases.AdvocateR = int.Parse(ddlAdvR.SelectedValue);
                objCases.AdvocateR = int.Parse(lblAdvRID.Text);
                objCases.HearDate = txtHearDate.Text.Trim();
                objCases.HeadNotes = editorHeadNotes.Content;
                objCases.Judgment = editorJudgment.Content;
                objCases.JudgmentType = txtJudgmentType.Text.Trim();
                objCases.Result = txtResult.Text.Trim() ;
                objCases.Court = ddlCourts.SelectedValue;//.Text.Trim(); //ddlRadCourts.SelectedValue;
                objCases.ModifiedBy = int.Parse(Session["UserID"].ToString());
                objCases.CourtCityName = txtCourtCityName.Text;
                int chk = objCases.EditCase(CaseID);
                if (chk > 0)
                {
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
            catch(Exception ex)
            { }
        }
        void AddNewJudges(int CaseID)
        {
            try
            {
                string str = "";

                int chk = 0;
                if (ddlNewJudge1.SelectedValue != "0")
                    chk = objJudge.TaggeCaseinJudgesNewTemp(int.Parse(ddlNewJudge1.SelectedValue), CaseID, int.Parse(Session["UserID"].ToString()), 0, ref str);
                    //chk = objJudge.TaggeCaseinJudgesNew(int.Parse(ddlNewJudge1.SelectedValue), CaseID, int.Parse(Session["UserID"].ToString()), 0, ref str);
                if (ddlNewJudge2.SelectedValue != "0")
                    chk = objJudge.TaggeCaseinJudgesNewTemp(int.Parse(ddlNewJudge2.SelectedValue), CaseID, int.Parse(Session["UserID"].ToString()), 0, ref str);
                if (ddlNewJudge3.SelectedValue != "0")
                    chk = objJudge.TaggeCaseinJudgesNewTemp(int.Parse(ddlNewJudge3.SelectedValue), CaseID, int.Parse(Session["UserID"].ToString()), 0, ref str);
                if (ddlNewJudge4.SelectedValue != "0")
                    chk = objJudge.TaggeCaseinJudgesNewTemp(int.Parse(ddlNewJudge4.SelectedValue), CaseID, int.Parse(Session["UserID"].ToString()), 0, ref str);
                if (ddlNewJudge5.SelectedValue != "0")
                    chk = objJudge.TaggeCaseinJudgesNewTemp(int.Parse(ddlNewJudge5.SelectedValue), CaseID, int.Parse(Session["UserID"].ToString()), 0, ref str);
                if (ddlNewJudge6.SelectedValue != "0")
                    chk = objJudge.TaggeCaseinJudgesNewTemp(int.Parse(ddlNewJudge6.SelectedValue), CaseID, int.Parse(Session["UserID"].ToString()), 0, ref str);
                if (ddlNewJudge7.SelectedValue != "0")
                    chk = objJudge.TaggeCaseinJudgesNewTemp(int.Parse(ddlNewJudge7.SelectedValue), CaseID, int.Parse(Session["UserID"].ToString()), 0, ref str);




            }
            catch (Exception ex)
            {
                //    string script = "alert('" + ex.Message() + "')";
                //    ScriptManager.RegisterStartupScript(this, typeof(Page), "RedirectTo", script, true);
            }
        }
        int ValidateDate(string Date)
        {
            DateTime dateValue;
            if (DateTime.TryParseExact(Date, "dd/MM/yyyy", new CultureInfo("en-US"), DateTimeStyles.None, out dateValue))
                return 1;
            else
                return 0;
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //ClearFields();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["param"] != null)
                    EditRecord(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())));
                else
                    SaveRecord();
                    
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("ReviewCasesMigration.aspx", "btnSave_Click", ex.Message);
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int chk = objCases.DeleteCasesID(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())),int.Parse(Session["UserID"].ToString()));
                if (chk > 0)
                {
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
            catch { }
        }
        void CitationSearch()
        {
            try
            {
                string cri = "Where Citation is not null";

                if (!string.IsNullOrEmpty(txtCitationYear.Text.Trim()))
                    cri = cri + " AND Year='" + txtCitationYear.Text.Trim() + "'";

                if (ddlJournalSearch.SelectedValue != "0")
                    cri = cri + " AND JournalID='" + ddlJournalSearch.SelectedValue + "'";

                //if (!string.IsNullOrEmpty(txtCitationNumber.Text.Trim()))
                //    cri = cri + " AND A.Citation like '%"+txtCitationNumber.Text.Trim()+"%'";

                if (!string.IsNullOrEmpty(txtCitationNumber.Text.Trim()))
                    cri = cri + " AND  (PageNo='" + txtCitationNumber.Text.Trim() + "')";
                   // cri = cri + " AND  CONTAINS (Citation, '" + txtCitationNumber.Text.Trim() + "' )";

                DataTable dt = new DataTable();
                //dt = objCases.GetCasesSearch(cri, 0, 30);
                dt = objCases.GetCasesSearch_Year_Journal_PageNo(int.Parse(txtCitationYear.Text.Trim()), int.Parse(ddlJournalSearch.SelectedValue), txtCitationNumber.Text.Trim());
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        if ((Request.QueryString["param"] != null))
                        {

                            //DataTable dtCitation = new DataTable();
                            //dtCitation = objkey.GetGlossoryCitation(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())));
                            //gvCitations.DataSource = dtCitation;
                            //gvCitations.DataBind();

                        }
                        else
                        {
                            int num;
                            Random random = new Random();
                            num = random.Next(0, 10000);


                            Session["CID"] += dt.Rows[0]["ID"].ToString() + "|";
                            Session["CName"] += dt.Rows[0]["Citation"].ToString() + " " + dt.Rows[0]["Appeallant"].ToString() + "|";
                            Session["CKey"] += "##" + num.ToString() + "##" + "|";
                            CreateTableValuesCitation();
                        }
                    }
                }

            }
            catch (Exception ex)
            {

            }
        }
        void CitationSearchLinkedAppeal()
        {
            try
            {
                string cri = "Where Citation is not null";

                if (!string.IsNullOrEmpty(txtCitationYear.Text.Trim()))
                    cri = cri + " AND Year='" + txtCitationYear.Text.Trim() + "'";

                if (ddlJournalSearch.SelectedValue != "0")
                    cri = cri + " AND JournalID='" + ddlJournalSearch.SelectedValue + "'";

                //if (!string.IsNullOrEmpty(txtCitationNumber.Text.Trim()))
                //    cri = cri + " AND A.Citation like '%"+txtCitationNumber.Text.Trim()+"%'";

                if (!string.IsNullOrEmpty(txtCitationNumber.Text.Trim()))
                    cri = cri + " AND  (PageNo='" + txtCitationNumber.Text.Trim() + "')";
                // cri = cri + " AND  CONTAINS (Citation, '" + txtCitationNumber.Text.Trim() + "' )";

                if (!string.IsNullOrEmpty(txtAppeal.Text.Trim()))
                    cri = cri + " AND Appeal='" + txtAppeal.Text.Trim() + "'";

                if (!string.IsNullOrEmpty(txtLinkedAppeadJDate.Text.Trim()))
                    cri = cri + " AND JDate='" + txtLinkedAppeadJDate.Text.Trim() + "'";

                if (ddlSearchCourt.SelectedIndex !=0)
                    cri = cri + " AND Court='" + ddlSearchCourt.SelectedValue + "'";

                DataTable dt = new DataTable();
                dt = objCases.GetCasesSearch(cri, 0, 30);
                //dt = objCases.GetCasesSearch_Year_Journal_PageNo(int.Parse(txtCitationYear.Text.Trim()), int.Parse(ddlJournalSearch.SelectedValue), txtCitationNumber.Text.Trim());
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        if ((Request.QueryString["param"] != null))
                        {

                            //DataTable dtCitation = new DataTable();
                            //dtCitation = objkey.GetGlossoryCitation(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())));
                            //gvCitations.DataSource = dtCitation;
                            //gvCitations.DataBind();

                        }
                        else
                        {
                            //int num;
                            //Random random = new Random();
                            //num = random.Next(0, 10000);


                            //Session["CID"] += dt.Rows[0]["ID"].ToString() + "|";
                            //Session["CName"] += dt.Rows[0]["Citation"].ToString() + " " + dt.Rows[0]["Appeallant"].ToString() + "|";
                            //Session["CKey"] += "##" + num.ToString() + "##" + "|";
                            //CreateTableValuesCitation();


                            gvCitations.DataSource = dt;
                            gvCitations.DataBind();
                        }
                    }
                }

            }
            catch (Exception ex)
            {

            }
        }
        void AlternateCitationAddTemp()
        {
            try
            {
               
                
                        //if ((Request.QueryString["param"] != null))
                        //{

                        //    //DataTable dtCitation = new DataTable();
                        //    //dtCitation = objkey.GetGlossoryCitation(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())));
                        //    //gvCitations.DataSource = dtCitation;
                        //    //gvCitations.DataBind();
                        //    string str = "";
                        //    int chk = objCases.InsertCaseAlternate(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())), 
                        //        int.Parse(ddlAlternateCaseJournal.SelectedValue), int.Parse(txtAlternateCaseYear.Text),txtAlternateCasePageNo.Text,
                        //        txtAlternateCaseYear.Text + " " + ddlAlternateCaseJournal.SelectedItem.Text + " " + txtAlternateCasePageNo.Text, 1,
                        //   int.Parse(Session["UserID"].ToString()), ref str);
                            

                        //}
                        //else
                        //{
                        

                            Session["AltYear"] += txtAlternateCaseYear.Text.Trim() + "|";
                            Session["AltJournalID"] += ddlAlternateCaseJournal.SelectedValue + "|";
                            Session["AltJournalName"] += ddlAlternateCaseJournal.SelectedItem.Text + "|";
                            Session["AltPageNo"] += txtAlternateCasePageNo.Text.Trim()+"|";
                            Session["AltCitation"] += txtAlternateCaseYear.Text.Trim() + " " + ddlAlternateCaseJournal.SelectedItem.Text+" "+txtAlternateCasePageNo.Text.Trim() + "|";
                            CreateTableValuesAlternateCitation();
                        //}
            }

            catch (Exception ex)
            {

            }
        }
        public void CreateTableValuesCitation()
        {
            try
            {

                string[] sa = Session["CID"].ToString().Split('|');
                string[] sb = Session["CName"].ToString().Split('|');
                string[] sbK = Session["CKey"].ToString().Split('|');

                int recordnum = sa.Length;

                DataTable dt = new DataTable("tblCitation");
                dt.Columns.Add("ID");
                dt.Columns.Add("Name");
                dt.Columns.Add("Key");
                for (int j = 0; j < recordnum - 1; j++)
                {
                    if (!string.IsNullOrEmpty(sa[j].ToString()))
                    {
                        DataRow dr = dt.NewRow();
                        dr["ID"] = sa[j].ToString();
                        dr["Name"] = sb[j].ToString();
                        dr["Key"] = sbK[j].ToString();
                        dt.Rows.Add(dr);
                    }

                }
                ViewState["dtCitation"] = dt;
                DataTable dt1 = (DataTable)ViewState["dtCitation"];
                dt1 = dt.DefaultView.ToTable(true, "Id", "Name","Key");
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
        public void CreateTableValuesAlternateCitation()
        {
            try
            {

                string[] sa = Session["AltYear"].ToString().Split('|');
                string[] sb = Session["AltJournalID"].ToString().Split('|');
                string[] sbK = Session["AltJournalName"].ToString().Split('|');
                string[] sbc = Session["AltPageNo"].ToString().Split('|');
                string[] sbcit = Session["AltCitation"].ToString().Split('|');
                

                int recordnum = sa.Length;

                DataTable dt = new DataTable("tblAlternateCitation");
                dt.Columns.Add("ID");
                dt.Columns.Add("Year");
                dt.Columns.Add("JournalID");
                dt.Columns.Add("JournalName");
                dt.Columns.Add("PageNo");
                dt.Columns.Add("Citation");
                for (int j = 0; j < recordnum - 1; j++)
                {
                    if (!string.IsNullOrEmpty(sa[j].ToString()))
                    {
                        DataRow dr = dt.NewRow();
                        dr["ID"] = "0";
                        dr["Year"] = sa[j].ToString();
                        dr["JournalID"] = sb[j].ToString();
                        dr["JournalName"] = sbK[j].ToString();
                        dr["PageNo"] = sbc[j].ToString();
                        dr["Citation"] = sbcit[j].ToString();
                        dt.Rows.Add(dr);
                    }

                }
                ViewState["dtAlternateCitation"] = dt;
                DataTable dt1 = (DataTable)ViewState["dtAlternateCitation"];
                dt1 = dt.DefaultView.ToTable(true, "Id", "Year", "JournalID", "JournalName", "PageNo", "Citation");
                gvAlternateCaseLst.DataSource = dt1;
                gvAlternateCaseLst.DataBind();


                txtAlternateCaseYear.Text = "";
                ddlAlternateCaseJournal.SelectedIndex = 0;
                txtAlternateCasePageNo.Text = "";


                //gvPracticeAreaSubCat.DataSource = dt.DefaultView;
                //gvPracticeAreaSubCat.DataBind();
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("AddGeneralAreas.aspx", "CreateTableValuesPracticeArea", ex.Message);
            }



        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //CitationSearch();
            CitationSearchLinkedAppeal();

        }
        

        protected void btnAddAlternateCase_Click(object sender, EventArgs e)
        {
            AlternateCitationAddTemp();

        }
        void GetGridItemsAlternateCitationsAndSave(int CaseID)
        {
            try
            {
                HiddenField hdID = default(HiddenField);
                HiddenField hdJournalID = default(HiddenField);
                Label lblJournalName = default(Label);
                Label lblYear = default(Label);
                Label lblPageNo = default(Label);
                TextBox txtCitation = default(TextBox);

                foreach (GridViewRow row in gvAlternateCaseLst.Rows)
                {
                    if ((row != null))
                    {
                        string str="";
                        hdID = (HiddenField)row.FindControl("hdID");
                        hdJournalID = (HiddenField)row.FindControl("hdJournalID");
                        lblJournalName = (Label)row.FindControl("lblJournalName");
                        lblYear = (Label)row.FindControl("lblYear");
                        lblPageNo = (Label)row.FindControl("lblPageNo");
                        txtCitation = (TextBox)row.FindControl("txtCitation");


                        int chk = objCases.InsertCaseAlternate(CaseID, int.Parse(hdJournalID.Value.ToString()), int.Parse(lblYear.Text), lblPageNo.Text,txtCitation.Text.Trim(), 1,
                            int.Parse(Session["UserID"].ToString()), ref str);
                            


                    }
                }
                Session.Remove("AltYear");
                Session.Remove("AltJournalID");
                Session.Remove("AltJournalName");
                Session.Remove("AltPageNo");
                Session.Remove("AltCitation");
            }
            catch (Exception ex)
            {
                //    string script = "alert('" + ex.Message() + "')";
                //    ScriptManager.RegisterStartupScript(this, typeof(Page), "RedirectTo", script, true);
            }
        }
        void GetGridItemsHyperLinking(int CaseID)
        {
            try
            {
                TextBox txtCitation = default(TextBox);
                TextBox txtLinkedCaseID = default(TextBox);
                Label lblJournal = default(Label);

                foreach (GridViewRow row in gvCitationLinking.Rows)
                {
                    if ((row != null))
                    {
                        string str = "";
                        txtCitation = (TextBox)row.FindControl("txtCitation");
                        txtLinkedCaseID = (TextBox)row.FindControl("txtLinkedCaseID");
                        lblJournal = (Label)row.FindControl("lblJournal");

                        if (!string.IsNullOrEmpty(txtCitation.Text.Trim()))
                        {
                            int chk = objCases.AddCasesInsideCitationsTemp(CaseID, txtCitation.Text.Trim(), "", int.Parse(txtLinkedCaseID.Text.ToString()), lblJournal.Text);
                        }



                    }
                }
                Session.Remove("AltYear");
                Session.Remove("AltJournalID");
                Session.Remove("AltJournalName");
                Session.Remove("AltPageNo");
                Session.Remove("AltCitation");
            }
            catch (Exception ex)
            {
                //    string script = "alert('" + ex.Message() + "')";
                //    ScriptManager.RegisterStartupScript(this, typeof(Page), "RedirectTo", script, true);
            }
        }
        protected void btnSourceAddAlternateCase_Click(object sender, EventArgs e)
        {
            try
            {
                string cri = "Where Citation is not null";

                if (!string.IsNullOrEmpty(txtSourceAlternateCaseYear.Text.Trim()))
                    cri = cri + " AND Year='" + txtSourceAlternateCaseYear.Text.Trim() + "'";

                if (ddlSourceAlternateCaseJournal.SelectedValue != "0")
                    cri = cri + " AND JournalID='" + ddlSourceAlternateCaseJournal.SelectedValue + "'";

                //if (!string.IsNullOrEmpty(txtCitationNumber.Text.Trim()))
                //    cri = cri + " AND A.Citation like '%"+txtCitationNumber.Text.Trim()+"%'";

                if (!string.IsNullOrEmpty(txtSourceAlternateCasePageNo.Text.Trim()))
                    cri = cri + " AND  (PageNo='" + txtSourceAlternateCasePageNo.Text.Trim() + "')";
                //cri = cri + " AND  CONTAINS (Citation, '" + txtSourceAlternateCasePageNo.Text.Trim() + "' )";

                DataTable dt = new DataTable();
                //dt = objCases.GetCasesSearch(cri, 0, 30);
                dt = objCases.GetCasesSearch_Year_Journal_PageNo(int.Parse(txtSourceAlternateCaseYear.Text.Trim()), int.Parse(ddlSourceAlternateCaseJournal.SelectedValue), txtSourceAlternateCasePageNo.Text.Trim());
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {

                        lblSourceCitation.Text = dt.Rows[0]["Citation"].ToString();
                        lblSourceCitationID.Text = dt.Rows[0]["ID"].ToString();

                        gvSourceCitation.DataSource = dt;
                        gvSourceCitation.DataBind();

                    }
                    else
                    {
                        gvSourceCitation.DataSource = null;
                        gvSourceCitation.DataBind();
                    }
                }

            }
            catch (Exception ex)
            {

            }

        }
        
        
        protected void btnSaveAlternateCitations_Click(object sender, EventArgs e)
        {
            HiddenField hdCaseID = default(HiddenField);
            RadioButton radioChk = default(RadioButton);

            foreach (GridViewRow row in gvSourceCitation.Rows)
            {
                if ((row != null))
                {
                    string str = "";
                    hdCaseID = (HiddenField)row.FindControl("hdID");
                    radioChk = (RadioButton)row.FindControl("radioSel");

                    if (radioChk != null)
                    {
                        if (radioChk.Checked == true)
                        {
                            //GetGridItemsAlternateCitationsAndSave(int.Parse(lblSourceCitationID.Text));
                            GetGridItemsAlternateCitationsAndSave(int.Parse(hdCaseID.Value.ToString()));
                            divSuccessAlternateCitation.Style["Display"] = "";
                            divErrorAlternateCitation.Style["Display"] = "none";

                        }
                    }
                    
                }
            }

        }
        protected void ddlCitationType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCitationType.SelectedValue == "1")
            {
                divNewCitation.Style["Display"] = "";
                divAlternateCitation.Style["Display"] = "none";
            }
            else if (ddlCitationType.SelectedValue == "2")
            {
                divNewCitation.Style["Display"] = "none";
                divAlternateCitation.Style["Display"] = "";
            }
        }
        protected void ddlCourtMaster_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetJudgesByCourtMaster(int.Parse(ddlCourtMaster.SelectedValue));
            }
            catch { }
        }

        protected void chkManualCaseSummary_CheckedChanged(object sender, EventArgs e)
        {
            if (chkManualCaseSummary.Checked == true)
            {
                divCaseSummary.Style["Display"] = "";
            }
            if (chkManualCaseSummary.Checked == false)
            {
                divCaseSummary.Style["Display"] = "none";
                editortxtSummary.Content = "";
            }

        }
        #region Check Hyperlinking
        protected void btnFindHyperlinking_Click(object sender, EventArgs e)
        {
            try
            {
                InsideCitaionsSearch(editorHeadNotes.Content, editorJudgment.Content);
            }
            catch { }
        }
        public void InsideCitaionsSearch(string HeadNote, string Judgment)
        {
            try
            {
                EastLawBL.Cases objcase = new EastLawBL.Cases();
                CitationsSearchJournalWise objJSearch = new CitationsSearchJournalWise();
                //string findcitation = "";
                
                    //SCMR
                    try
                    {
                     //string  findcitation=findcitation+"!"+ objJSearch.GetSCMRJournalSearch(HeadNote,Judgment);
                     string findcitation =  objJSearch.GetSCMRJournalSearch(HeadNote, Judgment);

                         string[] citations = findcitation.Split('!');
                         if (citations != null)
                         {
                             if (citations.Length > 0)
                             {
                                 DataTable _myDataTable = new DataTable();

                                 DataTable dt = (DataTable)ViewState["CitationHyperlinking"];

                                 //   _myDataTable.Columns.Add("CitationRef");


                                 for (int j = 0; j < citations.Length; j++)
                                 {

                                     if (!string.IsNullOrEmpty(citations[j]))
                                     {
                                         dt.Rows.Add(citations[j], "0", "SCMR");
                                         ViewState["CitationHyperlinking"] = dt;

                                     }
                                 }
                             }
                         }
                    }
                    catch { }

                    //CLC
                    try
                    {
                        //string findcitation = findcitation + "!" + objJSearch.GetCLCJournalSearch(HeadNote, Judgment);
                        string findcitation =  objJSearch.GetCLCJournalSearch(HeadNote, Judgment);

                        string[] citations = findcitation.Split('!');
                        if (citations != null)
                        {
                            if (citations.Length > 0)
                            {
                                DataTable _myDataTable = new DataTable();

                                DataTable dt = (DataTable)ViewState["CitationHyperlinking"];

                                //   _myDataTable.Columns.Add("CitationRef");


                                for (int j = 0; j < citations.Length; j++)
                                {

                                    if (!string.IsNullOrEmpty(citations[j]))
                                    {
                                        dt.Rows.Add(citations[j], "0", "CLC");
                                        ViewState["CitationHyperlinking"] = dt;

                                    }
                                }
                            }
                        }
                    }
                    catch { }

                    //PLD
                    try
                    {
                        //string  findcitation = findcitation + "!" + objJSearch.GetPLDJournalSearch(HeadNote, Judgment);
                        string findcitation =  objJSearch.GetPLDJournalSearch(HeadNote, Judgment);

                        string[] citations = findcitation.Split('!');
                        if (citations != null)
                        {
                            if (citations.Length > 0)
                            {
                                DataTable _myDataTable = new DataTable();

                                DataTable dt = (DataTable)ViewState["CitationHyperlinking"];

                                //   _myDataTable.Columns.Add("CitationRef");


                                for (int j = 0; j < citations.Length; j++)
                                {

                                    if (!string.IsNullOrEmpty(citations[j]))
                                    {
                                        dt.Rows.Add(citations[j], "0", "PLD");
                                        ViewState["CitationHyperlinking"] = dt;

                                    }
                                }
                            }
                        }
                    }
                    catch { }

                    //PTD
                    try
                    {
                        //string findcitation = findcitation + "!" + objJSearch.GetPTDJournalSearch(HeadNote, Judgment);
                        string findcitation =  objJSearch.GetPTDJournalSearch(HeadNote, Judgment);

                        string[] citations = findcitation.Split('!');
                        if (citations != null)
                        {
                            if (citations.Length > 0)
                            {
                                DataTable _myDataTable = new DataTable();

                                DataTable dt = (DataTable)ViewState["CitationHyperlinking"];

                                //   _myDataTable.Columns.Add("CitationRef");


                                for (int j = 0; j < citations.Length ; j++)
                                {

                                    if (!string.IsNullOrEmpty(citations[j]))
                                    {
                                        dt.Rows.Add(citations[j], "0", "PTD");
                                        ViewState["CitationHyperlinking"] = dt;

                                    }
                                }
                            }
                        }
                    }
                    catch { }

                    //PLC
                    try
                    {
                        //string findcitation = findcitation + "!" + objJSearch.GetPLCJournalSearch(HeadNote, Judgment);
                        string findcitation = objJSearch.GetPLCJournalSearch(HeadNote, Judgment);

                        string[] citations = findcitation.Split('!');
                        if (citations != null)
                        {
                            if (citations.Length > 0)
                            {
                                DataTable _myDataTable = new DataTable();

                                DataTable dt = (DataTable)ViewState["CitationHyperlinking"];

                                //   _myDataTable.Columns.Add("CitationRef");


                                for (int j = 0; j < citations.Length ; j++)
                                {

                                    if (!string.IsNullOrEmpty(citations[j]))
                                    {
                                        dt.Rows.Add(citations[j], "0", "PLC");
                                        ViewState["CitationHyperlinking"] = dt;

                                    }
                                }
                            }
                        }
                    }
                    catch { }

                    //YLR
                    try
                    {
                        //string findcitation = findcitation + "!" + objJSearch.GetYLRJournalSearch(HeadNote, Judgment);
                        string findcitation =  objJSearch.GetYLRJournalSearch(HeadNote, Judgment);

                        string[] citations = findcitation.Split('!');
                        if (citations != null)
                        {
                            if (citations.Length > 0)
                            {
                                DataTable _myDataTable = new DataTable();

                                DataTable dt = (DataTable)ViewState["CitationHyperlinking"];

                                //   _myDataTable.Columns.Add("CitationRef");


                                for (int j = 0; j < citations.Length; j++)
                                {

                                    if (!string.IsNullOrEmpty(citations[j]))
                                    {
                                        dt.Rows.Add(citations[j], "0", "YLR");
                                        ViewState["CitationHyperlinking"] = dt;

                                    }
                                }
                            }
                        }
                    }
                    catch { }

                    //MLD
                    try
                    {
                        //string findcitation = findcitation + "!" + objJSearch.GetMLDJournalSearch(HeadNote, Judgment);
                        string findcitation =  objJSearch.GetMLDJournalSearch(HeadNote, Judgment);

                        string[] citations = findcitation.Split('!');
                        if (citations != null)
                        {
                            if (citations.Length > 0)
                            {
                                DataTable _myDataTable = new DataTable();

                                DataTable dt = (DataTable)ViewState["CitationHyperlinking"];

                                //   _myDataTable.Columns.Add("CitationRef");


                                for (int j = 0; j < citations.Length ; j++)
                                {

                                    if (!string.IsNullOrEmpty(citations[j]))
                                    {
                                        dt.Rows.Add(citations[j], "0", "MLD");
                                        ViewState["CitationHyperlinking"] = dt;

                                    }
                                }
                            }
                        }
                    }
                    catch { }

                    //CLD
                    try
                    {
                        //string findcitation = findcitation + "!" + objJSearch.GetCLDJournalSearch(HeadNote, Judgment);
                        string findcitation =  objJSearch.GetCLDJournalSearch(HeadNote, Judgment);

                        string[] citations = findcitation.Split('!');
                        if (citations != null)
                        {
                            if (citations.Length > 0)
                            {
                                DataTable _myDataTable = new DataTable();

                                DataTable dt = (DataTable)ViewState["CitationHyperlinking"];

                                //   _myDataTable.Columns.Add("CitationRef");


                                for (int j = 0; j < citations.Length; j++)
                                {

                                    if (!string.IsNullOrEmpty(citations[j]))
                                    {
                                        dt.Rows.Add(citations[j], "0", "CLD");
                                        ViewState["CitationHyperlinking"] = dt;

                                    }
                                }
                            }
                        }
                    }
                    catch { }

                    //PCrLJ
                    try
                    {
                        //string findcitation = findcitation + "!" + objJSearch.GetPCrLJJournalSearch(HeadNote, Judgment);
                        string findcitation =  objJSearch.GetPCrLJJournalSearch(HeadNote, Judgment);

                        string[] citations = findcitation.Split('!');
                        if (citations != null)
                        {
                            if (citations.Length > 0)
                            {
                                DataTable _myDataTable = new DataTable();

                                DataTable dt = (DataTable)ViewState["CitationHyperlinking"];

                                //   _myDataTable.Columns.Add("CitationRef");


                                for (int j = 0; j < citations.Length ; j++)
                                {

                                    if (!string.IsNullOrEmpty(citations[j]))
                                    {
                                        dt.Rows.Add(citations[j], "0", "PCRLJ");
                                        ViewState["CitationHyperlinking"] = dt;

                                    }
                                }
                            }
                        }
                    }
                    catch { }

                    DataTable dt1 = (DataTable)ViewState["CitationHyperlinking"];
                    gvCitationLinking.DataSource = RemoveDuplicateRows(dt1, "Citation");
                    gvCitationLinking.DataBind();

                    //string[] citations = findcitation.Split('!');
                    //if (citations != null)
                    //{
                    //    if (citations.Length > 0)
                    //    {
                    //        DataTable _myDataTable = new DataTable();

                    //        DataTable dt = (DataTable)ViewState["CitationHyperlinking"];

                    //     //   _myDataTable.Columns.Add("CitationRef");


                    //        for (int j = 0; j < citations.Length - 1; j++)
                    //        {
                                
                    //            if (!string.IsNullOrEmpty(citations[j]))
                    //            {
                    //                dt.Rows.Add(citations[j],"0");
                    //                ViewState["CitationHyperlinking"] = dt;

                    //            }
                    //        }


                    //        gvCitationLinking.DataSource = RemoveDuplicateRows(dt, "Citation");
                    //        gvCitationLinking.DataBind();
                   
                    //    }
                    //}
                    

                


            }
            catch { }

        }
        public DataTable RemoveDuplicateRows(DataTable dTable, string colName)
        {
           System.Collections.Hashtable hTable = new System.Collections.Hashtable();
           System.Collections.ArrayList duplicateList = new System.Collections.ArrayList();

            //Add list of all the unique item value to hashtable, which stores combination of key, value pair.
            //And add duplicate item value in arraylist.
            foreach (DataRow drow in dTable.Rows)
            {
                if (hTable.Contains(drow[colName]))
                    duplicateList.Add(drow);
                else
                    hTable.Add(drow[colName], string.Empty);
            }

            //Removing a list of duplicate items from datatable.
            foreach (DataRow dRow in duplicateList)
                dTable.Rows.Remove(dRow);

            //Datatable which contains unique records will be return as output.
            return dTable;
        }
        protected void btnCitationHyperlinkingSearch_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = objCases.GetCasesSearch_Year_Journal_PageNo(int.Parse(txtCitationHyperlinkingYear.Text.Trim()), int.Parse(CitationHyperlinkingJournal.SelectedValue), txtCitationHyperlinkingPageNo.Text.Trim());
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    gvCitationHyperlinkingSearch.DataSource = dt;
                    gvCitationHyperlinkingSearch.DataBind();
                }
            }
        }
        protected void gvCitationHyperlinkingSearch_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                GridViewRow row = gvCitationHyperlinkingSearch.Rows[e.NewEditIndex];
                HiddenField hd = default(HiddenField);
                Label lblCitation = default(Label);
                Label lblJournalName = default(Label);

                if ((row != null))
                {
                    hd = (HiddenField)row.FindControl("hdID");
                    lblCitation = (Label)row.FindControl("lblCitation");
                    lblJournalName = (Label)row.FindControl("lblJournalName");

                    int ID = Convert.ToInt32(hd.Value);


                    DataTable dt = (DataTable)ViewState["CitationHyperlinking"];
                    dt.Rows.Add(lblCitation.Text.Trim(), hd.Value.ToString(), lblJournalName.Text.ToString());
                    ViewState["CitationHyperlinking"] = dt;
                    this.BindGridHyperlinking();

                }
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("CasesMigrationList.aspx", "gv_RowEditing", ex.Message);
            }
        }
        protected void BindGridHyperlinking()
        {
            gvCitationLinking.DataSource =RemoveDuplicateRows((DataTable)ViewState["CitationHyperlinking"],"Citation");
            gvCitationLinking.DataBind();
        }
        #endregion

        #region Pracice Area
        void GetActivePracticeCategories()
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
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("ManagePracticeSubCategories.aspx", "GetActivePracticeCategories", e.Message);
            }
        }
        void GetActivePracticeSubCategories(int PACatID)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objPA.GetActivePracticeAreaSubCategoriesByCategory(PACatID);

                ddlPracticeAreaSubCat.DataValueField = "ID";
                ddlPracticeAreaSubCat.DataTextField = "PracticeAreaSubCatName";
                ddlPracticeAreaSubCat.DataSource = dt;
                ddlPracticeAreaSubCat.DataBind();
                ddlPracticeAreaSubCat.Items.Insert(0, new ListItem("Select", "0"));

            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("ManagePracticeSubCategories.aspx", "GetActivePracticeCategories", e.Message);
            }
        }
        protected void btnAddPracticeArea_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = (DataTable)ViewState["PALst"];
                dt.Rows.Add(ddlPracticeAreaSubCat.SelectedValue, ddlPracticeAreaCat.SelectedItem.Text,ddlPracticeAreaSubCat.SelectedItem.Text);
                ViewState["PALst"] = dt;
                this.BindPraciceAreaGrid();
                
            }
            catch { }
        }
        protected void BindPraciceAreaGrid()
        {
            gvPracticeAreaLst.DataSource = RemoveDuplicateRows((DataTable)ViewState["PALst"], "ID");
            gvPracticeAreaLst.DataBind();
        }
        protected void ddlPracticeAreaCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetActivePracticeSubCategories(int.Parse(ddlPracticeAreaCat.SelectedValue));
            }
            catch { }
        }

        void GetGridItemsPracticeArea(int CaseID)
        {
            try
            {
                HiddenField hdid = default(HiddenField);
                TextBox txtLinkedCaseID = default(TextBox);
                Label lblJournal = default(Label);

                foreach (GridViewRow row in gvPracticeAreaLst.Rows)
                {
                    if ((row != null))
                    {
                        string str = "";
                        hdid = (HiddenField)row.FindControl("hdID");
                        

                        if (!string.IsNullOrEmpty(txtCitation.Text.Trim()))
                        {
                            int chk = objCases.TaggeCaseinPracticeAreaTemp(int.Parse(hdid.Value.ToString()), CaseID, int.Parse(Session["UserID"].ToString()),ref str);
                        }



                    }
                }

            }
            catch (Exception ex)
            {
                //    string script = "alert('" + ex.Message() + "')";
                //    ScriptManager.RegisterStartupScript(this, typeof(Page), "RedirectTo", script, true);
            }
        }
        #endregion


    }
}