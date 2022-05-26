using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
namespace EastlawUI_v2.adminpanel.newsletter
{
    public partial class AddNewsletter : System.Web.UI.Page
    {
        EastLawBL.Newsletter objn = new EastLawBL.Newsletter();
        EastLawBL.Cases objc = new EastLawBL.Cases();
        EastLawBL.Departments objd = new EastLawBL.Departments();
        EastLawBL.Statutes objs = new EastLawBL.Statutes();
        EastLawBL.Journals objJo = new EastLawBL.Journals();
        NewsletterHTMLGenerator objNG = new NewsletterHTMLGenerator();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    if (!ValidateUserGroup.ValidateGroup(int.Parse(Session["UserTypeID"].ToString()), ValidateUserGroup.getPageName(Request.Url.AbsolutePath)))
                        Response.Redirect("NotAuthorize.aspx");

                    //                    GetCasesLessInfo();
                    GetJournals();
                    GetDeptLessInfo();
                    GetStatutesLessInfo();
                    if (Request.QueryString["param"] != null)
                        GetNewsletter(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())));
                }
            }
            catch (Exception ex)
            {

            }
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
        void GetCasesLessInfo()
        {
            try
            {
                //DataTable dt = new DataTable();
                //dt = objc.GetActiveCasesLessInfo();
                //ddlCases.DataValueField = "ID";
                //ddlCases.DataTextField = "Citation";
                //ddlCases.DataSource = dt;
                //ddlCases.DataBind();
                //ddlCases.Items.Insert(0,new ListItem("Search citation","0"));
            }
            catch { }
        }
        void GetDeptLessInfo()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objd.GetActiveDepartmentsLessInfo();
                ddlDept.DataValueField = "ID";
                ddlDept.DataTextField = "Title";
                ddlDept.DataSource = dt;
                ddlDept.DataBind();
                //ddlDept.Text = "Search Department";
                ddlDept.Items.Insert(0, new ListItem("Search Department", "0"));
            }
            catch { }
        }
        void GetStatutesLessInfo()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objs.GetActiveStatutesLessInfo();
                ddlStatutes.DataValueField = "ID";
                ddlStatutes.DataTextField = "Title";
                ddlStatutes.DataSource = dt;
                ddlStatutes.DataBind();
                // ddlStatutes.Text = "Search Statutes";
                ddlStatutes.Items.Insert(0, new ListItem("Search Statutes", "0"));
            }
            catch { }
        }
        void GetNewsletter(int ID)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objn.GetNewsletter(ID);
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
                    ddlNewsletterType.SelectedValue = dt.Rows[0]["NewsletterType"].ToString();
                    txtNewsletterTitle.Text = dt.Rows[0]["NewsletterTitle"].ToString();
                    ddlTemplate.SelectedValue = dt.Rows[0]["TemplateName"].ToString();
                    if (!string.IsNullOrEmpty(dt.Rows[0]["NewsletterBanner"].ToString()))
                    {
                        lblCoverUpload.Text = dt.Rows[0]["NewsletterBanner"].ToString();
                        imgCover.ImageUrl = "/adminpanel/newsletter/banners/" + dt.Rows[0]["NewsletterBanner"].ToString();
                        rfvFupload.Enabled = false;

                    }
                    editorContent.Content = dt.Rows[0]["NewsletterContent"].ToString();

                    if (dt.Rows[0]["Active"].ToString() == "1")
                        chkActive.Checked = true;
                    else
                        chkActive.Checked = false;

                    divSuccess.Style["Display"] = "none";
                    divError.Style["Display"] = "none";

                    GetCasesByNewsletter(ID);
                }
            }
            catch (Exception e)
            {

            }
        }
        void SaveRecord()
        {
            try
            {
                string bannerimg = "";
                if (fupload.HasFile)
                {
                    string destDir = Server.MapPath("/adminpanel/newsletter/banners/");

                    string FileName = Path.GetFileName(fupload.PostedFile.FileName);
                    string destPath = Path.Combine(destDir, FileName);
                    fupload.SaveAs(destPath);

                    bannerimg = fupload.FileName;
                }
                objn.NewsletterType = ddlNewsletterType.SelectedValue;
                objn.TemplateName = ddlTemplate.SelectedValue;
                objn.NewsletterTitle = txtNewsletterTitle.Text.Trim();
                objn.NewsletterContent = editorContent.Content;
                objn.NewsletterFile = "";
                objn.NewsletterBanner = bannerimg;

                if (chkActive.Checked == true)
                    objn.Active = 1;
                else
                    objn.Active = 0;
                objn.CreatedBy = int.Parse(Session["UserID"].ToString());
                int chk = objn.InsertNewsletter();
                if (chk > 0)
                {
                    GetGridItemsCases(chk);
                    GetGridItemsStatutes(chk);
                    GetGridItemsDept(chk);
                    GetGridItemsNews(chk);
                    divSuccess.Style["Display"] = "";
                    divError.Style["Display"] = "none";
                    ClearFields();

                    var res = objNG.GenerateNewsletter(1, chk);
                    string filePath = Server.MapPath("/adminpanel/newsletter/htmlfiles/");
                    var pathcmb = filePath + "/" + chk.ToString() + ".html";
                    System.IO.File.WriteAllText(pathcmb, res);
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
        void ClearFields()
        {

            ddlNewsletterType.SelectedIndex = 0;
            ddlTemplate.SelectedIndex = 0;
            txtNewsletterTitle.Text = "";
            chkActive.Checked = false;


            Session.Remove("CaseID");
            Session.Remove("CaseTitle");
            Session.Remove("ShortText");
            Session.Remove("DeptID");
            Session.Remove("DeptTitle");
            Session.Remove("DeptShortText");
            Session.Remove("StatutesID");
            Session.Remove("StatutesTitle");
            Session.Remove("StatutesShortText");

            Session.Remove("NewsDes");
            Session.Remove("NewsLink");

        }
        void EditRecord(int ID)
        {
            try
            {
                string bannerimg = "";
                if (fupload.HasFile)
                {
                    string destDir = Server.MapPath("/adminpanel/newsletter/banners/");

                    string FileName = Path.GetFileName(fupload.PostedFile.FileName);
                    string destPath = Path.Combine(destDir, FileName);
                    fupload.SaveAs(destPath);

                    bannerimg = fupload.FileName;
                }
                else
                {
                    bannerimg = lblCoverUpload.Text;
                }
                objn.NewsletterType = ddlNewsletterType.SelectedValue;
                objn.TemplateName = ddlTemplate.SelectedValue;
                objn.NewsletterTitle = txtNewsletterTitle.Text.Trim();
                objn.NewsletterContent = editorContent.Content;
                objn.NewsletterFile = "";
                objn.NewsletterBanner = bannerimg;

                if (chkActive.Checked == true)
                    objn.Active = 1;
                else
                    objn.Active = 0;
                objn.ModifiedBy = int.Parse(Session["UserID"].ToString());
                int chk = objn.EditNewsletter(ID);
                if (chk > 0)
                {
                    divSuccess.Style["Display"] = "";
                    divError.Style["Display"] = "none";
                    ClearFields();

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
        void GetCasesByNewsletter(int NewsletterID)
        {
            try
            {
                DataTable dtItemCases = new DataTable();
                dtItemCases = objn.GetNewsletterItems(NewsletterID, "Case");
                if (dtItemCases != null && dtItemCases.Rows.Count > 0)
                {
                    dtItemCases.Columns.Add("CaseID");
                    dtItemCases.Columns.Add("CaseTitle");

                    for (int a = 0; a < dtItemCases.Rows.Count; a++)
                    {
                        dtItemCases.Rows[a]["CaseID"] = dtItemCases.Rows[a]["ItemID"].ToString();
                        dtItemCases.Rows[a]["CaseTitle"] = dtItemCases.Rows[a]["Title"].ToString();
                    }
                    dtItemCases.AcceptChanges();

                    gvCases.DataSource = dtItemCases;
                    gvCases.DataBind();
                }

            }
            catch { }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearFields();
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

        protected void ddlNewsletterType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlTemplate.SelectedValue == "General")
                {
                    divNewsletterContent.Style["Display"] = "";
                    divTemplateSelection.Style["Display"] = "none";
                    rfvddlTemplate.Enabled = false;
                }
                else if (ddlTemplate.SelectedValue == "Elements")
                {
                    divNewsletterContent.Style["Display"] = "none";
                    divTemplateSelection.Style["Display"] = "";
                    rfvddlTemplate.Enabled = true;
                }
            }
            catch { }
        }

        protected void btnAddStatutes_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {

                    Session["StatutesID"] += ddlStatutes.SelectedValue + "|";
                    Session["StatutesTitle"] += ddlStatutes.SelectedItem.Text + "|";
                    Session["StatutesShortText"] += ddlStatutes.SelectedItem.Text + "|";

                    CreateTableValuesForStatutes();
                    ddlStatutes.SelectedIndex = 0;
                }
                catch { }
            }
            catch { }
        }
        public void CreateTableValuesForStatutes()
        {
            try
            {

                string[] StatutesID = Session["StatutesID"].ToString().Split('|');
                string[] StatutesTitle = Session["StatutesTitle"].ToString().Split('|');
                string[] StatutesShortText = Session["StatutesShortText"].ToString().Split('|');

                int recordnum = StatutesID.Length;

                DataTable dt = new DataTable("tblStatutes");

                dt.Columns.Add("StatutesID");
                dt.Columns.Add("StatutesTitle");
                dt.Columns.Add("ShortText");

                for (int j = 0; j < recordnum - 1; j++)
                {
                    if (!string.IsNullOrEmpty(StatutesID[j].ToString()))
                    {
                        DataRow dr = dt.NewRow();

                        dr["StatutesID"] = StatutesID[j].ToString();
                        dr["StatutesTitle"] = StatutesTitle[j].ToString();
                        dr["ShortText"] = StatutesShortText[j].ToString();

                        dt.Rows.Add(dr);
                    }

                }
                ViewState["dtStatutes"] = dt;
                DataTable dt1 = (DataTable)ViewState["dtStatutes"];
                gvStatutes.DataSource = dt1;
                gvStatutes.DataBind();
            }
            catch (Exception ex)
            {

            }



        }

        protected void btnAddDept_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {

                    Session["DeptID"] += ddlDept.SelectedValue + "|";
                    Session["DeptTitle"] += ddlDept.SelectedItem.Text + "|";
                    Session["DeptShortText"] += ddlDept.SelectedItem.Text + "|";

                    CreateTableValuesForDept();
                    ddlDept.SelectedIndex = 0;
                }
                catch { }
            }
            catch { }
        }
        public void CreateTableValuesForNews()
        {
            try
            {

                string[] NewsDes = Session["NewsDes"].ToString().Split('|');
                string[] NewsLink = Session["NewsLink"].ToString().Split('|');


                int recordnum = NewsDes.Length;

                DataTable dt = new DataTable("tbl");

                dt.Columns.Add("ID");
                dt.Columns.Add("Title");
                dt.Columns.Add("ShortText");
                dt.Columns.Add("URL");

                for (int j = 0; j < recordnum - 1; j++)
                {
                    if (!string.IsNullOrEmpty(NewsDes[j].ToString()))
                    {
                        DataRow dr = dt.NewRow();

                        dr["ID"] = "0";
                        dr["Title"] = "0";
                        dr["ShortText"] = NewsDes[j].ToString();
                        dr["URL"] = NewsLink[j].ToString();


                        dt.Rows.Add(dr);
                    }

                }
                ViewState["dtNews"] = dt;
                DataTable dt1 = (DataTable)ViewState["dtNews"];
                gvNews.DataSource = dt1;
                gvNews.DataBind();
            }
            catch (Exception ex)
            {

            }



        }

        protected void btnAddNews_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {

                    
                    Session["NewsDes"] += txtNewsDes.Text.Trim() + "|";
                    Session["NewsLink"] += txtNewsLink.Text.Trim() + "|";

                    CreateTableValuesForNews();
                    
                    txtNewsDes.Text = "";
                    txtNewsLink.Text = "";
                }
                catch { }
            }
            catch { }
        }
        public void CreateTableValuesForDept()
        {
            try
            {

                string[] DeptID = Session["DeptID"].ToString().Split('|');
                string[] DeptTitle = Session["DeptTitle"].ToString().Split('|');
                string[] DeptShortText = Session["DeptShortText"].ToString().Split('|');

                int recordnum = DeptID.Length;

                DataTable dt = new DataTable("tblDept");

                dt.Columns.Add("DeptID");
                dt.Columns.Add("DeptTitle");
                dt.Columns.Add("ShortText");

                for (int j = 0; j < recordnum - 1; j++)
                {
                    if (!string.IsNullOrEmpty(DeptID[j].ToString()))
                    {
                        DataRow dr = dt.NewRow();

                        dr["DeptID"] = DeptID[j].ToString();
                        dr["DeptTitle"] = DeptTitle[j].ToString();
                        dr["ShortText"] = DeptShortText[j].ToString();


                        dt.Rows.Add(dr);
                    }

                }
                ViewState["dtDept"] = dt;
                DataTable dt1 = (DataTable)ViewState["dtDept"];
                gvDept.DataSource = dt1;
                gvDept.DataBind();
            }
            catch (Exception ex)
            {

            }



        }

        protected void btnAddCase_Click(object sender, EventArgs e)
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
                        if ((Request.QueryString["param"] != null))
                        {

                            objn.ItemType = "Case";
                            objn.ItemID = int.Parse(dt.Rows[0]["ID"].ToString());
                            objn.Active = 1;
                            objn.CreatedBy = int.Parse(Session["UserID"].ToString());
                            objn.ShortText = txtCaseShortDes.Text.Trim();

                            int chk = objn.InsertNewsletterItems(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())));

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




                            Session["CaseID"] += dt.Rows[0]["ID"].ToString() + "|";
                            Session["CaseTitle"] += dt.Rows[0]["Citation"].ToString() + " " + dt.Rows[0]["Appeallant"].ToString() + "|";
                            Session["ShortText"] += dt.Rows[0]["Citation"].ToString() + " " + dt.Rows[0]["Appeallant"].ToString() + " "+ txtCaseShortDes.Text.Trim()+ "|";

                            CreateTableValuesForCase();
                            //ddlCases.SelectedIndex = 0;
                        }
                        }
                    }
               



              
            }
            catch { }
        }
        public void CreateTableValuesForCase()
        {
            try
            {

                string[] CaseID = Session["CaseID"].ToString().Split('|');
                string[] CaseTitle = Session["CaseTitle"].ToString().Split('|');
                string[] ShortText = Session["ShortText"].ToString().Split('|');

                int recordnum = CaseID.Length;

                DataTable dt = new DataTable("tblCase");

                dt.Columns.Add("CaseID");
                dt.Columns.Add("CaseTitle");
                dt.Columns.Add("ShortText");

                for (int j = 0; j < recordnum - 1; j++)
                {
                    if (!string.IsNullOrEmpty(CaseID[j].ToString()))
                    {
                        DataRow dr = dt.NewRow();

                        dr["CaseID"] = CaseID[j].ToString();
                        dr["CaseTitle"] = CaseTitle[j].ToString();
                        dr["ShortText"] = ShortText[j].ToString();

                        dt.Rows.Add(dr);
                    }

                }
                ViewState["dtCase"] = dt;
                DataTable dt1 = (DataTable)ViewState["dtCase"];
                gvCases.DataSource = dt1;
                gvCases.DataBind();
            }
            catch (Exception ex)
            {

            }



        }
        void GetGridItemsCases(int NewsletterID)
        {
            try
            {

                //GridViewRow row = default(GridViewRow);
                HiddenField hdID = default(HiddenField);
                TextBox txtShortTitle = default(TextBox);

                foreach (GridViewRow row in gvCases.Rows)
                {
                    if ((row != null))
                    {


                        hdID = (HiddenField)row.FindControl("hdID");
                        txtShortTitle = (TextBox)row.FindControl("txtShortText");

                        objn.ItemType = "Case";
                        objn.ItemID = int.Parse(hdID.Value);
                        objn.Active = 1;
                        objn.CreatedBy = int.Parse(Session["UserID"].ToString());
                        objn.ShortText = txtShortTitle.Text.Trim();

                        int chk = objn.InsertNewsletterItems(NewsletterID);

                    }
                }
            }
            catch (Exception ex)
            {
                //    string script = "alert('" + ex.Message() + "')";
                //    ScriptManager.RegisterStartupScript(this, typeof(Page), "RedirectTo", script, true);
            }
        }
        void GetGridItemsStatutes(int NewsletterID)
        {
            try
            {

                //GridViewRow row = default(GridViewRow);
                HiddenField hdID = default(HiddenField);
                TextBox txtShortTitle = default(TextBox);

                foreach (GridViewRow row in gvStatutes.Rows)
                {
                    if ((row != null))
                    {


                        hdID = (HiddenField)row.FindControl("hdID");
                        txtShortTitle = (TextBox)row.FindControl("txtShortText");

                        objn.ItemType = "Statutes";
                        objn.ItemID = int.Parse(hdID.Value);
                        objn.Active = 1;
                        objn.CreatedBy = int.Parse(Session["UserID"].ToString());
                        objn.ShortText = txtShortTitle.Text.Trim();

                        int chk = objn.InsertNewsletterItems(NewsletterID);

                    }
                }
            }
            catch (Exception ex)
            {
                //    string script = "alert('" + ex.Message() + "')";
                //    ScriptManager.RegisterStartupScript(this, typeof(Page), "RedirectTo", script, true);
            }
        }
        void GetGridItemsDept(int NewsletterID)
        {
            try
            {

                //GridViewRow row = default(GridViewRow);
                HiddenField hdID = default(HiddenField);
                TextBox txtShortTitle = default(TextBox);

                foreach (GridViewRow row in gvDept.Rows)
                {
                    if ((row != null))
                    {


                        hdID = (HiddenField)row.FindControl("hdID");
                        txtShortTitle = (TextBox)row.FindControl("txtShortText");

                        objn.ItemType = "Dept";
                        objn.ItemID = int.Parse(hdID.Value);
                        objn.Active = 1;
                        objn.CreatedBy = int.Parse(Session["UserID"].ToString());
                        objn.ShortText = txtShortTitle.Text.Trim();

                        int chk = objn.InsertNewsletterItems(NewsletterID);

                    }
                }
            }
            catch (Exception ex)
            {
                //    string script = "alert('" + ex.Message() + "')";
                //    ScriptManager.RegisterStartupScript(this, typeof(Page), "RedirectTo", script, true);
            }
        }
        void GetGridItemsNews(int NewsletterID)
        {
            try
            {

                //GridViewRow row = default(GridViewRow);
                HiddenField hdID = default(HiddenField);
                TextBox txtShortTitle = default(TextBox);
                TextBox txtURL = default(TextBox);

                foreach (GridViewRow row in gvNews.Rows)
                {
                    if ((row != null))
                    {


                        hdID = (HiddenField)row.FindControl("hdID");
                        txtShortTitle = (TextBox)row.FindControl("txtShortText");
                        txtShortTitle = (TextBox)row.FindControl("txtShortText");
                        txtURL = (TextBox)row.FindControl("txtURL");

                        objn.ItemType = "News";
                        objn.ItemID = 0;
                        objn.Active = 1;
                        objn.CreatedBy = int.Parse(Session["UserID"].ToString());
                        objn.ShortText = txtShortTitle.Text.Trim();
                        objn.URL = txtURL.Text.Trim();

                        int chk = objn.InsertNewsletterItems(NewsletterID);

                    }
                }
            }
            catch (Exception ex)
            {
                //    string script = "alert('" + ex.Message() + "')";
                //    ScriptManager.RegisterStartupScript(this, typeof(Page), "RedirectTo", script, true);
            }
        }

        protected void gvCases_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                try
                {
                    EastLawBL.Statutes objstat = new EastLawBL.Statutes();
                    GridViewRow row = gvCases.Rows[e.RowIndex];
                    HiddenField hd = default(HiddenField);

                    if ((row != null))
                    {
                        hd = (HiddenField)row.FindControl("hdID");
                        int ID = Convert.ToInt32(hd.Value);

                        int chk = objn.DeleteNewsletterItems(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())),ID, int.Parse(Session["UserID"].ToString()));
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
                    gvCases.EditIndex = -1;
                    GetCasesByNewsletter(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())));

                }
                catch (Exception ex)
                {

                }
            }
            catch { }
        }
    }
}