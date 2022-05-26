using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace EastlawUI_v2.adminpanel
{
    public partial class CitationDetailedReview : System.Web.UI.Page
    {
        EastLawBL.Cases objc = new EastLawBL.Cases();
        EastLawBL.Statutes objs = new EastLawBL.Statutes();
        EastLawBL.Journals objJo = new EastLawBL.Journals();
        EastLawBL.Judges objJudge = new EastLawBL.Judges();
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
                   
                    GetCourtMaster();
                    if (Request.QueryString["param"] != null)
                    {
                        GetStatutesLessInfo(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())));
                        GetDData(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())));
                        GetDataSOA(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())));
                        GetAlternateCitations(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())));
                        GetNewJudgesbyCaseID(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())));
                    }
                }



            }
            catch { }
        }
        void GetJournals()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objJo.GetActiveJournals();
                ddlJournalSearch.DataValueField = "ID";
                ddlJournalSearch.DataTextField = "JournalName";
                ddlJournalSearch.DataSource = dt;
                ddlJournalSearch.DataBind();
                ddlJournalSearch.Items.Insert(0, new ListItem("Select", "0"));

            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("ReviewCasesMigration.aspx", "GetAdvocates", e.Message);
            }
        }
        void GetStatutesLessInfo(int CaseID)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objs.GetStatutesLessInfo();
                //ddlStatutes.DataValueField = "ID";
                //ddlStatutes.DataTextField = "Title";
                //ddlStatutes.DataSource = dt;
                //ddlStatutes.DataBind();
                //ddlStatutes.Text = "Search Statutes";

                ddlStat.DataValueField = "ID";
                ddlStat.DataTextField = "Title";
                ddlStat.DataSource = dt;
                ddlStat.DataBind();

                DataTable dtStatutes = new DataTable();
                dtStatutes = objc.GetLinkedStatutesBackend(CaseID);

                ddlSOAStat.DataValueField = "StatuteID";
                ddlSOAStat.DataTextField = "Title";
                ddlSOAStat.DataSource = dtStatutes;
                ddlSOAStat.DataBind();
                ddlSOAStat.Items.Insert(0, new ListItem("Select", "0"));



                dt = objs.GetActiveStatutesLessInfo_ForSOATagging();
                
                //ddlSOAStat.DataValueField = "ID";
                //ddlSOAStat.DataTextField = "Title";
                //ddlSOAStat.DataSource = dt;
                //ddlSOAStat.DataBind();
                //ddlSOAStat.Items.Insert(0, new ListItem("Select", "0"));



                ddlStatutesNewSOAIndex.DataValueField = "ID";
                ddlStatutesNewSOAIndex.DataTextField = "Title";
                ddlStatutesNewSOAIndex.DataSource = dt;
                ddlStatutesNewSOAIndex.DataBind();
                ddlStatutesNewSOAIndex.Items.Insert(0, new ListItem("Select", "0"));


            }
            catch { }
        }
        void GetDataSOA(int CaseID)
        {
            try
            {
                DataTable dtSOA = new DataTable();
                dtSOA = objs.GetStatutesSOATaggingbyCaseID(CaseID);

                gvTaggedSOA.DataSource = dtSOA;
                gvTaggedSOA.DataBind();
            }
            catch { }
        }
        void GetAlternateCitations(int CaseID)
        {
            try
            {

                DataTable dtAltenateCitation = new DataTable();
                dtAltenateCitation = objc.GetAlternateCitationByCaseID_ForBackend(CaseID);

                gvAlternateCitations.DataSource = dtAltenateCitation;
                gvAlternateCitations.DataBind();
            }
            catch { }
        }
        void GetNewJudgesbyCaseID(int CaseID)
        {
            try
            {

                DataTable dt = new DataTable();
                dt = objc.GetListofJudgesByCaseNew(CaseID);

                gvNewJudgesList.DataSource = dt;
                gvNewJudgesList.DataBind();
            }
            catch { }
        }
        void GetDData(int CaseID)
        {
            try
            {
                // SOA







                DataSet ds = new DataSet();
                ds = objc.GetAllTaggingByCase(CaseID);


                gvTaggedStatutes.DataSource = ds.Tables["TaggedStatutes"];
                gvTaggedStatutes.DataBind();


                //gvSAR.DataSource = ds.Tables["Sections_Rule_Articles"];
                //gvSAR.DataBind();

                gvInsideCitation.DataSource = ds.Tables["InsideCitations"];
                gvInsideCitation.DataBind();

                ds.Tables["CaseJudges"].Columns.Add("Authortxt");
                for (int a = 0; a < ds.Tables["CaseJudges"].Rows.Count; a++)
                {
                    if (ds.Tables["CaseJudges"].Rows[a]["Author"].ToString() == "1")
                        ds.Tables["CaseJudges"].Rows[a]["Authortxt"] = "true";
                    else
                        ds.Tables["CaseJudges"].Rows[a]["Authortxt"] = "false";
                }
                ds.AcceptChanges();

                gvJudges.DataSource = ds.Tables["CaseJudges"];
                gvJudges.DataBind();

               

                DataTable dt = new DataTable();
                dt = objc.GetCases(CaseID);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        lblCitation.Text = dt.Rows[0]["Citation"].ToString();
                        lblJournalYear.Text = dt.Rows[0]["JournalName"].ToString() + " - " + dt.Rows[0]["Year"].ToString();
                        lblJDate.Text = dt.Rows[0]["JDate"].ToString();
                        lblPublish.Text = dt.Rows[0]["Publish"].ToString();
                        if (dt.Rows[0]["Publish"].ToString() == "1")
                            chkPublish.Checked = true;
                        else
                            chkPublish.Checked = false;

                        if (dt.Rows[0]["ReadyForPrint_Export"].ToString() == "1")
                            chkPrintReady.Checked = true;
                        else
                            chkPrintReady.Checked = false;

                        //divHeadnotes.InnerHtml=dt.Rows[0]["HeadNotes"].ToString();
                        //divJudgment.InnerHtml = dt.Rows[0]["Judgment"].ToString();
                        //divSummary.InnerHtml = dt.Rows[0]["CaseSummary"].ToString();
                        editortxtSummary.Content = dt.Rows[0]["CaseSummary"].ToString();

                        hplLnkEditCitation.NavigateUrl = "AddCaseUpdateHistory.aspx?param=" + Request.QueryString["param"].ToString();

                        hypLnkViewCaseDetails.NavigateUrl = "ViewCaseDetails.aspx?param=" + Request.QueryString["param"].ToString();
                    }
                }

            }
            catch { }
        }
        void GetCourtMaster()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objc.GetActiveCourtMasters();

                ddlCourtMasterJudge.DataSource = dt;
                ddlCourtMasterJudge.DataTextField = "CourtName";
                ddlCourtMasterJudge.DataValueField = "ID";
                ddlCourtMasterJudge.DataBind();

                ddlCourtMasterJudge.Items.Insert(0, new ListItem("Select", "0"));
            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("ManageJudges.aspx", "GetJudges", e.Message);
            }
        }
        void GetJudgesByCourtMaster(int CourtMasterID)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objJudge.GetJudgeNewByCourtMaster(CourtMasterID);

                ddlCourtJudgeNew.DataSource = dt;
                ddlCourtJudgeNew.DataTextField = "JudgeName";
                ddlCourtJudgeNew.DataValueField = "ID";
                ddlCourtJudgeNew.DataBind();
                ddlCourtJudgeNew.Items.Insert(0, new ListItem("Select", "0"));
               
            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("ManageJudges.aspx", "GetJudges", e.Message);
            }
        }
        protected void btnEditCitation_Click(object sender, EventArgs e)
        {
            try
            {
                string url = "AddCaseUpdateHistory.aspx?param=" + Request.QueryString["param"].ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( '" + url + "','_blank','height=600px,width=900px,scrollbars=1');", true);
            }
            catch { }
        }

        protected void btnFinalReview_Click(object sender, EventArgs e)
        {
            try
            {

                //UpdateStatuteTaggedAttribute
                int publish = 0;
                int printready = 0;
                if (chkPublish.Checked == true)
                    publish = 1;
                if (chkPrintReady.Checked == true)
                    printready = 1;

                int chk = objc.MarkAsFinalreview(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())), 1, publish, printready);

                if (chk > 0)
                {
                    GetGridItemsStatutesTaggedAttributes(0);
                    GetGridItemsSAR();
                    GetGridItemsInsideCitation();
                    GetGridItemsJudges();
                    divSuccess.Style["Display"] = "";
                    divError.Style["Display"] = "none";
                    updPnl.Update();
                }
                else
                {
                    divSuccess.Style["Display"] = "none";
                    divError.Style["Display"] = "";
                }
            }
            catch { }
        }
        void GetGridItemsStatutesTaggedAttributes(int Type)
        {
            try
            {

                //GridViewRow row = default(GridViewRow);
                HiddenField hdID = default(HiddenField);
                DropDownList ddlType = default(DropDownList);
                TextBox txtVal = default(TextBox);


                foreach (GridViewRow row in gvTaggedStatutes.Rows)
                {
                    if ((row != null))
                    {


                        hdID = (HiddenField)row.FindControl("hdID");
                        ddlType = (DropDownList)row.FindControl("ddlType");
                        txtVal = (TextBox)row.FindControl("txtAttriVal");
                        string AtrriubuteValues = "";
                        if (ddlType.SelectedValue != "0")
                        {
                            string[] attrival = txtVal.Text.Split(',');
                            for (int a = 0; a < attrival.Length; a++)
                            {
                                AtrriubuteValues = AtrriubuteValues + ddlType.SelectedValue + " " + attrival[a].ToString() + ",";
                            }
                            if (!string.IsNullOrEmpty(AtrriubuteValues))
                            {
                                AtrriubuteValues = AtrriubuteValues.Remove(AtrriubuteValues.Length - 1, 1);
                                int chk = objc.UpdateStatuteTaggedAttribute(int.Parse(hdID.Value.ToString()), AtrriubuteValues, int.Parse(Session["UserID"].ToString()));
                            }
                        }
                        else
                        {
                            AtrriubuteValues = txtVal.Text.Trim();
                            if (!string.IsNullOrEmpty(AtrriubuteValues))
                            {
                                int chk = objc.UpdateStatuteTaggedAttribute(int.Parse(hdID.Value.ToString()), AtrriubuteValues, int.Parse(Session["UserID"].ToString()));
                            }
                        }

                       
                       


                    }
                    
                }
                if (Type == 1)
                {
                    divStatueSuccess.Style["Display"] = "";
                    divStatutesFailed.Style["Display"] = "none";
                }
            }
            catch (Exception ex)
            {
                //    string script = "alert('" + ex.Message() + "')";
                //    ScriptManager.RegisterStartupScript(this, typeof(Page), "RedirectTo", script, true);
            }
        }
        void GetGridItemsSAR()
        {
            try
            {

                //GridViewRow row = default(GridViewRow);
                HiddenField hdID = default(HiddenField);
                DropDownList ddlType = default(DropDownList);
                TextBox txtLinkText = default(TextBox);


                foreach (GridViewRow row in gvSAR.Rows)
                {
                    if ((row != null))
                    {


                        hdID = (HiddenField)row.FindControl("hdID");
                        txtLinkText = (TextBox)row.FindControl("txtLinkText");
                        int chk = objc.UpdateInsideSection_Rule_Article(int.Parse(hdID.Value.ToString()), txtLinkText.Text.Trim(), int.Parse(Session["UserID"].ToString()));
                    }
                }
            }
            catch (Exception ex)
            {
                //    string script = "alert('" + ex.Message() + "')";
                //    ScriptManager.RegisterStartupScript(this, typeof(Page), "RedirectTo", script, true);
            }
        }
        void GetGridItemsInsideCitation()
        {
            try
            {

                //GridViewRow row = default(GridViewRow);
                HiddenField hdID = default(HiddenField);
                DropDownList ddlType = default(DropDownList);
                TextBox txtCitation = default(TextBox);


                foreach (GridViewRow row in gvInsideCitation.Rows)
                {
                    if ((row != null))
                    {


                        hdID = (HiddenField)row.FindControl("hdID");
                        txtCitation = (TextBox)row.FindControl("txtCitation");
                        int chk = objc.UpdateCasesInsideCitations(int.Parse(hdID.Value.ToString()), txtCitation.Text.Trim(), int.Parse(Session["UserID"].ToString()));
                    }
                }
            }
            catch (Exception ex)
            {
                //    string script = "alert('" + ex.Message() + "')";
                //    ScriptManager.RegisterStartupScript(this, typeof(Page), "RedirectTo", script, true);
            }
        }
        void GetGridItemsJudges()
        {
            try
            {

                //GridViewRow row = default(GridViewRow);
                HiddenField hdID = default(HiddenField);
                DropDownList ddlType = default(DropDownList);
                TextBox txtJudgeName = default(TextBox);
                CheckBox chkAuthor = default(CheckBox);


                foreach (GridViewRow row in gvJudges.Rows)
                {
                    if ((row != null))
                    {


                        hdID = (HiddenField)row.FindControl("hdID");
                        txtJudgeName = (TextBox)row.FindControl("txtJudgeName");
                        chkAuthor = (CheckBox)row.FindControl("chkAuthor");
                        int chk = 0;
                        if (chkAuthor.Checked == true)
                            chk = objc.UpdateJudgesListBycase(int.Parse(hdID.Value.ToString()), txtJudgeName.Text.Trim(), int.Parse(Session["UserID"].ToString()), 1);
                        else
                            chk = objc.UpdateJudgesListBycase(int.Parse(hdID.Value.ToString()), txtJudgeName.Text.Trim(), int.Parse(Session["UserID"].ToString()), 0);
                    }
                }
            }
            catch (Exception ex)
            {
                //    string script = "alert('" + ex.Message() + "')";
                //    ScriptManager.RegisterStartupScript(this, typeof(Page), "RedirectTo", script, true);
            }
        }

        protected void btnAddStatutes_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtStatTitle.Text))
                {
                    //int statID = AddStatutes(txtStatTitle.Text);
                    //if (statID > 0)
                    //{
                    //    int a = objs.InsertStatutesLinking("Case", statID, int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())), int.Parse(Session["UserID"].ToString()));
                    //}
                }
                else
                {
                    int a = objs.InsertStatutesLinking("Case", int.Parse(ddlStat.SelectedValue), int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())), int.Parse(Session["UserID"].ToString()));
                }
                //int a = objs.InsertStatutesLinking("Case", int.Parse(ddlStatutes.SelectedValue), int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())), int.Parse(Session["UserID"].ToString()));
                txtStatTitle.Text = "";
                GetDData(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())));
                GetStatutesLessInfo(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())));
            }
            catch { }

        }
        int AddStatutes(string Title)
        {
            try
            {
                EastLawBL.Statutes objstat = new EastLawBL.Statutes();
                objstat.CatName = "N/A";
                objstat.Title =CommonClass.MakeFirstCap(Title.Trim());
                objstat.CreatedBy = 999999;
                objstat.Active = 0;
                objstat.WorkflowID = 1;
                objstat.GroupID = 3;
                objstat.SubGroupID = 8;
                objstat.Primary_Secondary = "PRIMARY";
                objstat.StatutesContentType = "IndexContent";

                int chkinst = objstat.InsertStatutesUtility();
                return chkinst;
            }
            catch {
                return 0;
            }
        }


        protected void btnAddSAR_Click(object sender, EventArgs e)
        {
            try
            {
                int a =objc.AddCasesInsideSection_Rule_Article(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())), txtSAR.Text.Trim(), ddlSAR.SelectedValue, "Strong", "");
                GetDData(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())));
            }
            catch { }
        }

        protected void btnAddCitation_Click(object sender, EventArgs e)
        {
            try
            {

                string cri = "Where Citation is not null";

                if (!string.IsNullOrEmpty(txtCitationYear.Text.Trim()))
                    cri = cri + " AND Year='" + txtCitationYear.Text.Trim() + "'";

                if (ddlJournalSearch.SelectedValue != "0")
                    cri = cri + " AND JournalID='" + ddlJournalSearch.SelectedValue + "'";



                if (!string.IsNullOrEmpty(txtCitationNumber.Text.Trim()))
                    cri = cri + " AND  (PageNo='" + txtCitationNumber.Text.Trim() + "')";
                // cri = cri + " AND  CONTAINS (A.Citation, '" + txtCitationNumber.Text.Trim() + "' )";

                DataTable dt = new DataTable();
                dt = objc.GetCasesSearch(cri, 0, 30);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {

                        int a = objc.AddCasesInsideCitations(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())), dt.Rows[0]["Citation"].ToString() , "Strong", "", "", int.Parse(dt.Rows[0]["ID"].ToString()));

                        GetDData(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())));

                    }
                }

            }
            catch { }

            
        }

        protected void btnSaveEdit_Click(object sender, EventArgs e)
        {
            try
            {
                string a = editortxtSummary.Content;

                //int chk = objc.EditCaseSummary(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())), editortxtSummary.Content);
                //if (chk > 0)
                //{
                   
                //    divSuccessSummary.Style["Display"] = "";
                //    divErrorSummary.Style["Display"] = "none";
                //}
                //else
                //{
                //    divSuccessSummary.Style["Display"] = "none";
                //    divErrorSummary.Style["Display"] = "";
                //}
            }
            catch { }
            
        }

        protected void btnSaveEdit0_Click(object sender, EventArgs e)
        {
            try
            {
              //  string a = editortxtSummary.Content;

                int chk = objc.EditCaseSummary(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())), editortxtSummary.Content);
                if (chk > 0)
                {

                    divSuccessSummary.Style["Display"] = "";
                    divErrorSummary.Style["Display"] = "none";
                    updPnlSummary.Update();
                }
                else
                {
                    divSuccessSummary.Style["Display"] = "none";
                    divErrorSummary.Style["Display"] = "";
                    updPnlSummary.Update();
                }
            }
            catch { }
        }

        protected void gvTaggedStatutes_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                try
                {
                    EastLawBL.Statutes objstat = new EastLawBL.Statutes();
                    GridViewRow row = gvTaggedStatutes.Rows[e.RowIndex];
                    HiddenField hd = default(HiddenField);

                    if ((row != null))
                    {
                        hd = (HiddenField)row.FindControl("hdID");
                        int ID = Convert.ToInt32(hd.Value);

                        int chk = objstat.DeleteStatutesLinking(ID, int.Parse(Session["UserID"].ToString()));
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
                    gvTaggedStatutes.EditIndex = -1;
                    GetDData(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())));

                }
                catch (Exception ex)
                {
                    
                }
            }
            catch { }
        }

        protected void gvSAR_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                try
                {
                    GridViewRow row = gvSAR.Rows[e.RowIndex];
                    HiddenField hd = default(HiddenField);

                    if ((row != null))
                    {
                        hd = (HiddenField)row.FindControl("hdID");
                        int ID = Convert.ToInt32(hd.Value);

                        int chk = objc.DeleteCasesInsideSection_Rule_Article(ID, int.Parse(Session["UserID"].ToString()));
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
                    gvSAR.EditIndex = -1;
                    GetDData(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())));

                }
                catch (Exception ex)
                {

                }
            }
            catch { }
        }

        protected void gvInsideCitation_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            
                try
                {
                    GridViewRow row = gvInsideCitation.Rows[e.RowIndex];
                    HiddenField hd = default(HiddenField);

                    if ((row != null))
                    {
                        hd = (HiddenField)row.FindControl("hdID");
                        int ID = Convert.ToInt32(hd.Value);

                        int chk = objc.DeleteCasesInsideCitations(ID, int.Parse(Session["UserID"].ToString()));
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
                    gvInsideCitation.EditIndex = -1;
                    GetDData(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())));

                }
                catch (Exception ex)
                {

                }
            
        }

        protected void gvJudges_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                GridViewRow row = gvJudges.Rows[e.RowIndex];
                HiddenField hd = default(HiddenField);

                if ((row != null))
                {
                    hd = (HiddenField)row.FindControl("hdID");
                    int ID = Convert.ToInt32(hd.Value);

                    int chk = objc.DeleteJudgesListBycase(ID, int.Parse(Session["UserID"].ToString()));
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
                gvJudges.EditIndex = -1;
                GetDData(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())));

            }
            catch (Exception ex)
            {

            }
        }
        protected void gvNewJudgesList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                GridViewRow row = gvNewJudgesList.Rows[e.RowIndex];
                HiddenField hd = default(HiddenField);
                EastLawBL.Judges objjudges = new EastLawBL.Judges();

                if ((row != null))
                {
                    hd = (HiddenField)row.FindControl("hdID");
                    int ID = Convert.ToInt32(hd.Value);

                    int chk = objjudges.RemoveTaggeCaseinJudgesNewByID(ID, int.Parse(Session["UserID"].ToString()));
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
                gvNewJudgesList.EditIndex = -1;
                GetNewJudgesbyCaseID(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())));

            }
            catch (Exception ex)
            {

            }
        }
        protected void gvAlternateCitations_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                string msg = "";
                GridViewRow row = gvAlternateCitations.Rows[e.RowIndex];
                HiddenField hd = default(HiddenField);

                if ((row != null))
                {
                    hd = (HiddenField)row.FindControl("hdID");
                    int ID = Convert.ToInt32(hd.Value);

                    int chk = objc.DeleteCaseAlternate(ID, int.Parse(Session["UserID"].ToString()),ref msg);
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
                gvAlternateCitations.EditIndex = -1;
                GetAlternateCitations(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())));

            }
            catch (Exception ex)
            {

            }
        }

        protected void btnUpdateStatues_Click(object sender, EventArgs e)
        {
            try
            {
                GetGridItemsStatutesTaggedAttributes(1);
            }
            catch { }
        }

        protected void gvInsideCitation_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                TextBox txtCitation = default(TextBox);
                HiddenField hdLinkedCaseID = default(HiddenField);
               
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    txtCitation = (TextBox)e.Row.Controls[0].FindControl("txtCitation");
                    hdLinkedCaseID = (HiddenField)e.Row.Controls[0].FindControl("hdLinkedCaseID");
                    if (!string.IsNullOrEmpty(hdLinkedCaseID.Value.ToString()))
                    {
                        if (hdLinkedCaseID.Value.ToString() == "-1" || hdLinkedCaseID.Value.ToString() == "-2" )
                        {
                            txtCitation.BackColor = System.Drawing.Color.Wheat;
                        }
                        else if (hdLinkedCaseID.Value.ToString() == "0")
                        {
                            txtCitation.BackColor = System.Drawing.Color.Pink;
                        }
                    }
                    else
                    {
                        txtCitation.BackColor = System.Drawing.Color.Pink;
                    }
                  
                }
            }
            catch { }
        }

        private void GetAllIndexByIndex(int StatuteID)
        {
            try
            {
                DataTable dt = objs.GetStatutesSOAIndex(0, StatuteID);

                ddlSOA.DataValueField = "ID";
                ddlSOA.DataTextField = "ElementData";
                ddlSOA.DataSource = dt;
                ddlSOA.DataBind();

                ddlSOA.Items.Insert(0, new ListItem("Select", "0"));


            }
            catch (Exception Ex)
            {

            }
        }
        private void GetAllIndexByIndexForNew(int StatuteID)
        {
            try
            {
                DataTable dt = objs.GetStatutesSOAIndex(0, StatuteID);

                ddlStatutesNewSOAIndexParent.DataValueField = "ID";
                ddlStatutesNewSOAIndexParent.DataTextField = "ElementData";
                ddlStatutesNewSOAIndexParent.DataSource = dt;
                ddlStatutesNewSOAIndexParent.DataBind();

                ddlStatutesNewSOAIndexParent.Items.Insert(0, new ListItem("Select", "0"));


            }
            catch (Exception Ex)
            {

            }
        }
        protected void gvTaggedSOA_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                try
                {
                    EastLawBL.Statutes objstat = new EastLawBL.Statutes();
                    GridViewRow row = gvTaggedSOA.Rows[e.RowIndex];
                    HiddenField hd = default(HiddenField);

                    if ((row != null))
                    {
                        hd = (HiddenField)row.FindControl("hdID");
                        int ID = Convert.ToInt32(hd.Value);

                        int chk = objstat.DeleteStatutesSOATagging(ID, int.Parse(Session["UserID"].ToString()));
                        if (chk > 0)
                        {
                            divSuccessSOA.Style["Display"] = "";
                            divErrorSOA.Style["Display"] = "none";

                        }
                        else
                        {
                            divSuccessSOA.Style["Display"] = "none";
                            divErrorSOA.Style["Display"] = "";
                        }
                    }
                    gvTaggedSOA.EditIndex = -1;
                    GetDataSOA(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())));

                }
                catch (Exception ex)
                {

                }
            }
            catch { }
        }
        protected void ddlSOAStat_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetAllIndexByIndex(int.Parse(ddlSOAStat.SelectedValue));
            }
            catch { }
        }
        protected void btnAddSOA_Click(object sender, EventArgs e)
        {
            try
            {
                string str = "";
                int chk = objs.InsertStatutesSOATagging(int.Parse(ddlSOA.SelectedValue), "Case", int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())), 1, int.Parse(Session["UserID"].ToString()), ref str);
                if (chk > 0)
                {
                    divSuccessSOA.Style["Display"] = "";
                    divErrorSOA.Style["Display"] = "none";

                }
                else
                {
                    divSuccessSOA.Style["Display"] = "none";
                    divErrorSOA.Style["Display"] = "";
                }

              
                GetDataSOA(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())));
            }
            catch { }

        }
        protected void btnCitationHyperlinkingSearch_Click(object sender, EventArgs e)
        {

            //string cri = "Where Citation is not null";

            //if (!string.IsNullOrEmpty(txtCitationYear.Text.Trim()))
            //    cri = cri + " AND Year='" + txtCitationYear.Text.Trim() + "'";

            //if (ddlJournalSearch.SelectedValue != "0")
            //    cri = cri + " AND JournalID='" + ddlJournalSearch.SelectedValue + "'";



            //if (!string.IsNullOrEmpty(txtCitationNumber.Text.Trim()))
            //    cri = cri + " AND  (PageNo='" + txtCitationNumber.Text.Trim() + "')";
            //// cri = cri + " AND  CONTAINS (A.Citation, '" + txtCitationNumber.Text.Trim() + "' )";

            //DataTable dt = new DataTable();
            //dt = objc.GetCasesSearch(cri, 0, 30);
            //if (dt != null)
            //{
            //    if (dt.Rows.Count > 0)
            //    {

            //        int a = objc.AddCasesInsideCitations(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())), dt.Rows[0]["Citation"].ToString(), "Strong", "", "", int.Parse(dt.Rows[0]["ID"].ToString()));

            //        GetDData(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())));

            //    }

                DataTable dt = new DataTable();
            dt = objc.GetCasesSearch_Year_Journal_PageNo(int.Parse(txtCitationYear.Text.Trim()), int.Parse(ddlJournalSearch.SelectedValue), txtCitationNumber.Text.Trim());
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

                    int a = objc.AddCasesInsideCitations(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())), lblCitation.Text.Trim(), "Strong", "Judgment", "", int.Parse(hd.Value.ToString()));

                     GetDData(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())));

                    //DataTable dt = (DataTable)ViewState["CitationHyperlinking"];
                    //dt.Rows.Add(lblCitation.Text.Trim(), hd.Value.ToString(), lblJournalName.Text.ToString());
                    //ViewState["CitationHyperlinking"] = dt;
                    //this.BindGridHyperlinking();

                }
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("CasesMigrationList.aspx", "gv_RowEditing", ex.Message);
            }
        }
        protected void ddlCourtMasterJudge_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetJudgesByCourtMaster(int.Parse(ddlCourtMasterJudge.SelectedValue));
            }
            catch { }
        }
        protected void btnAddNewJudge_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                string msg = "";
                int chk = objJudge.TaggeCaseinJudgesNew(int.Parse(ddlCourtJudgeNew.SelectedValue), int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())),
                    int.Parse(Session["UserID"].ToString()),0,ref msg);
                if (chk > 0)
                {
                    GetJudgesByCourtMaster(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())));
                }
                updPnlJudgesNew.Update();
            }
            catch (Exception ex)
            {

            }
        }

        protected void ddlStatutesNewSOAIndexType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if(ddlStatutesNewSOAIndexType.SelectedValue== "Parent")
                {
                    divddlStatutesNewSOAIndexParent.Style["Display"] = "none";
                    rfvddlStatutesNewSOAIndexParent.Enabled = false;
                }
               else if (ddlStatutesNewSOAIndexType.SelectedValue == "Child")
                {
                    GetAllIndexByIndexForNew(int.Parse(ddlStatutesNewSOAIndex.SelectedValue));

                    divddlStatutesNewSOAIndexParent.Style["Display"] = "";
                    rfvddlStatutesNewSOAIndexParent.Enabled = true;
                }
            }
            catch { }
        }
        protected void btnStatutesNewSOAIndexSection_Click(object sender, EventArgs e)
        {
            try
            {
                string strMsg = "";
                int chk = 0;
                if (ddlStatutesNewSOAIndexType.SelectedValue == "Parent")
                {
                    chk = objs.InsertStatutesSOA(int.Parse(ddlStatutesNewSOAIndex.SelectedValue),0,
                    txtStatutesNewSOAIndexSection.Text.Trim(), 0, 1, int.Parse(Session["UserID"].ToString()), ref strMsg);
                }
                else
                {
                    chk = objs.InsertStatutesSOA(int.Parse(ddlStatutesNewSOAIndex.SelectedValue), int.Parse(ddlStatutesNewSOAIndexParent.SelectedValue),
                   txtStatutesNewSOAIndexSection.Text.Trim(), 0, 1, int.Parse(Session["UserID"].ToString()), ref strMsg);
                }
                string str = "";
                
                if (chk > 0)
                {
                    if (ddlSOAStat.SelectedIndex != 0)
                        GetAllIndexByIndexForNew(int.Parse(ddlSOAStat.SelectedValue));

                    GetAllIndexByIndexForNew(int.Parse(ddlStatutesNewSOAIndex.SelectedValue));
                    divSuccessSOA.Style["Display"] = "";
                    divErrorSOA.Style["Display"] = "none";

                    ddlStatutesNewSOAIndex.SelectedIndex = 0;
                    ddlStatutesNewSOAIndexType.SelectedIndex = 0;
                    divddlStatutesNewSOAIndexParent.Style["Display"] = "none";
                    rfvddlStatutesNewSOAIndexParent.Enabled = false;
                    txtStatutesNewSOAIndexSection.Text = "";

                }
                else
                {
                    divSuccessSOA.Style["Display"] = "none";
                    divErrorSOA.Style["Display"] = "";
                }


                GetDataSOA(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())));
            }
            catch { }

        }
    }
}