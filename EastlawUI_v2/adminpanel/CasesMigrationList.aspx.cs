using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


namespace EastlawUI_v2.adminpanel
{
    public partial class CasesMigrationList : System.Web.UI.Page
    {
        EastLawBL.Cases objCases = new EastLawBL.Cases();
        EastLawBL.Journals objJo = new EastLawBL.Journals();
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
                GetJournals();
                GetCourts();
                LoadYears();
                GetCases(0);
                Session.Remove("CitationSearch");
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

                //ddlCiJournal.DataValueField = "ID";
                //ddlCiJournal.DataTextField = "JournalName";
                //ddlCiJournal.DataSource = dt;
                //ddlCiJournal.DataBind();
                //ddlCiJournal.Items.Insert(0, new ListItem("Select", "0"));

            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("UtilityCases.aspx", "GetJournals", e.Message);
            }
        }
        void GetCourts()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objCases.GetCasesCourtsGroup();

                ddlRadCourts.DataValueField = "court";
                ddlRadCourts.DataTextField = "court";
                ddlRadCourts.DataSource = dt;
                ddlRadCourts.DataBind();

                


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
               // ddlCYear.Items.Add(DateTime.Now.AddYears(-i).Year.ToString());
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
                    dt.AcceptChanges();
                    gv.DataSource = dt;
                    gv.DataBind();
                }

            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("CasesMigrationList.aspx", "GetCases", e.Message);
            }
        }

        protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                if (Session["CitationSearch"] != null)
                {
                    if(Session["CitationSearch"].ToString()=="Yes")
                    {
                        gv.PageIndex = e.NewPageIndex;
                        CitationsSearch();
                    }
                }
                else
                {
                    gv.PageIndex = e.NewPageIndex;
                    GetCases(0);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("CasesMigrationList.aspx", "gv_PageIndexChanging", ex.Message);
            }
        }

        protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //try
            //{
            //    GridViewRow row = gv.Rows[e.RowIndex];
            //    HiddenField hd = default(HiddenField);

            //    if ((row != null))
            //    {
            //        hd = (HiddenField)row.FindControl("hdID");
            //        int ID = Convert.ToInt32(hd.Value);

            //        int chk = objUsr.DeleteCompany(ID, int.Parse(Session["UserID"].ToString()));
            //        if (chk > 0)
            //        {
            //            divSuccess.Style["Display"] = "";
            //            divError.Style["Display"] = "none";

            //        }
            //        else
            //        {
            //            divSuccess.Style["Display"] = "none";
            //            divError.Style["Display"] = "";
            //        }
            //    }
            //    gv.EditIndex = -1;
            //    GetCompanies(0);

            //}
            //catch (Exception ex)
            //{
            //    ExceptionHandling.SendErrorReport("CasesMigrationList.aspx", "gv_RowDeleting", ex.Message);
            //}
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

                    //Response.Redirect("ReviewCasesMigration.aspx?param=" + EncryptDecryptHelper.Encrypt(ID.ToString()));
                    Response.Redirect("AddCaseUpdateHistory.aspx?param=" + EncryptDecryptHelper.Encrypt(ID.ToString()));


                }
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("CasesMigrationList.aspx", "gv_RowEditing", ex.Message);
            }
        }

        protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                ImageButton imgBtn = default(ImageButton);
                Button btnMakePublicLink = default(Button);
                Button btnDisablePublicLink = default(Button);
                HiddenField hdPublicDisplay = default(HiddenField);
                    
                string script = null;
                script = "";

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    imgBtn = (ImageButton)e.Row.Controls[0].FindControl("ibtnDelete");
                    hdPublicDisplay = (HiddenField)e.Row.Controls[0].FindControl("hdPublicDisplay");

                    btnMakePublicLink = (Button)e.Row.Controls[0].FindControl("btnMakePublicLink");
                    btnDisablePublicLink = (Button)e.Row.Controls[0].FindControl("btnDisablePublicLink");

                    script = "javascript:return(confirm_delete())";
                    imgBtn.Attributes.Add("onclick", script);

                    if (hdPublicDisplay.Value.ToString() == "1")
                    {
                        btnMakePublicLink.Visible = false;
                        btnDisablePublicLink.Visible = true;
                    }
                    else
                    {
                        btnMakePublicLink.Visible = true;
                        btnDisablePublicLink.Visible = false;
 
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("CasesMigrationList.aspx", "gv_RowDataBound", ex.Message);
            }
        }

        void CitationsSearch()
        {
            try
            {
                string cri = "Where A.Citation is not null";

                //if (ddlCiJournal.SelectedValue != "0" && ddlCYear.SelectedValue != "0" && txtCNumber.Text != "")
                //    cri = cri + " AND E.JournalName='" + ddlCiJournal.SelectedValue + "' AND A.Year=" + ddlCYear.SelectedValue + " AND A.Citation like '%" + txtCNumber.Text.Trim() + "%'";

                if (!string.IsNullOrEmpty(txtID.Text.Trim()))
                    cri = cri + " AND A.ID='" + txtID.Text.Trim() + "'";

                if (ddlJournal.SelectedValue != "0")
                    cri = cri + " AND A.JournalID='" + ddlJournal.SelectedValue + "'";

                if (ddlYear.SelectedValue !="0")
                    cri = cri + " AND A.Year='" + ddlYear.SelectedValue + "'";

                //if (!string.IsNullOrEmpty(txtCNumber.Text.Trim()))
                //    cri = cri + " AND A.Citation like '%" + txtCNumber.Text.Trim() + "%'";

                if (!string.IsNullOrEmpty(txtCNumber.Text.Trim()))
                    cri = cri + " AND  CONTAINS (A.Citation, '" + txtCNumber.Text.Trim() + "' )";

                if (!string.IsNullOrEmpty(txtCourtName.Text.Trim()))
                    cri = cri + " AND  A.Court='"+ txtCourtName.Text +"' ";

                if (ddlRadCourts.SelectedIndex !=0)
                    cri = cri + " AND  A.Court='" + ddlRadCourts.SelectedValue + "' ";

                if (!string.IsNullOrEmpty(txtJDate.Text.Trim()))
                    cri = cri + " AND  A.JDate='" + txtJDate.Text + "' ";

                if (!string.IsNullOrEmpty(txtAppeallant.Text.Trim()))
                    cri = cri + " AND A.Appeallant like '%" + txtAppeallant.Text.Trim() + "%'";


                if (!string.IsNullOrEmpty(txtRespondent.Text.Trim()))
                    cri = cri + " AND A.Respondent like '%" + txtRespondent.Text.Trim() + "%'";



                DataTable dt = new DataTable();
                dt = objCases.GetCasesSearchBackend(cri);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        Session["CitationSearch"] = "Yes";
                        gv.DataSource = dt;
                        gv.DataBind();
                    }
                }

            }
            catch (Exception ex)
            {

            }
        }
        protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "View")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow row = gv.Rows[index];
                    //HiddenField hdnField = (HiddenField)row.FindControl("hdID");
                    string val = (string)this.gv.DataKeys[index]["Id"].ToString();
                    //string url = "StatutesIndex.aspx?dis=" + hdnField.Value;
                    string url = "ViewCaseDetails.aspx?dis=" + val.ToString();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( '" + url + "','_blank','height=600px,width=700px,scrollbars=1');", true);
                }
                if (e.CommandName == "FinalReview")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow row = gv.Rows[index];
                    //HiddenField hdnField = (HiddenField)row.FindControl("hdID");
                    string val = (string)this.gv.DataKeys[index]["Id"].ToString();
                    Response.Redirect("CitationDetailedReview.aspx?param=" + EncryptDecryptHelper.Encrypt(val.ToString()));
                }
                if (e.CommandName == "MakePublic")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow row = gv.Rows[index];
                    string val = (string)this.gv.DataKeys[index]["Id"].ToString();
                    objCases.UpdateCasePublicEnable_Disable(int.Parse(val), 1, int.Parse(Session["UserID"].ToString()));
                    DataTable dt = new DataTable();
                    dt = objCases.GetCases(int.Parse(val));
                    lblLnk.Text = "https://eastlaw.pk/public-cases/" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dt.Rows[0]["Appeallant"].ToString(), 3).ToString())).Replace(" ", "-").ToLower() + "VS" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dt.Rows[0]["Respondent"].ToString(), 3).ToString())).Replace(" ", "-").ToLower() + "." + EncryptDecryptHelper.Encrypt(dt.Rows[0]["ID"].ToString());
                    lblLnk.Visible = true;

                    
                    
                }
                if (e.CommandName == "DisableMakePublic")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow row = gv.Rows[index];
                    string val = (string)this.gv.DataKeys[index]["Id"].ToString();
                    objCases.UpdateCasePublicEnable_Disable(int.Parse(val), 0, int.Parse(Session["UserID"].ToString()));
                    lblLnk.Text = "Case Disabled from Public View";
                    lblLnk.Visible = true;

                }
                
            }
            catch (Exception ex)
            {

            }
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {

            CitationsSearch();
        }

        protected void btnAll_Click(object sender, EventArgs e)
        {
            GetCases(0);
        }

        protected void btnAll_Click1(object sender, EventArgs e)
        {
            GetCases(0);
        }
    }
}