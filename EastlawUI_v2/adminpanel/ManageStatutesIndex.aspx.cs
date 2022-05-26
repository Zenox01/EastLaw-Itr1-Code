using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace EastlawUI_v2.adminpanel
{
    public partial class ManageStatutesIndex : System.Web.UI.Page
    {
        EastLawBL.Statutes objstat = new EastLawBL.Statutes();
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
                GetStatutesIndex(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["dis"].ToString())),0);
            }
        }
        void GetStatutesIndexSectionGroup(int ID)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objstat.GetStatutesIndexSectionGroupByStatutesID(ID);
                ddlSection.DataTextField = "Section";
                ddlSection.DataValueField = "Section";
                ddlSection.DataSource = dt;
                ddlSection.DataBind();

                ddlSection.Items.Insert(0, new ListItem("Select", "0"));
            }
            catch { }
        }
        void GetStatutesIndexChapter_Schdule(int StatutesID,string Chap_Sch)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objstat.GetStatutesIndexDetailedByChapter_Schdule(StatutesID, Chap_Sch);
                ddlChapSchd.DataTextField = "Chapter_SchduleTitle";
                ddlChapSchd.DataValueField = "Chapter_SchduleTitle";
                ddlChapSchd.DataSource = dt;
                ddlChapSchd.DataBind();

                ddlChapSchd.Items.Insert(0, new ListItem("New", "0"));
            }
            catch { }
        }
        void GetStatutesIndex(int ID,int edit)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objstat.GetStatutesIndexByStatutesID(ID);
                if (edit == 0)
                {
                    //dt.Columns.Add("strActive");
                    //for (int a = 0; a < dt.Rows.Count; a++)
                    //{
                    //    if (dt.Rows[a]["Active"].ToString() == "1")
                    //        dt.Rows[a]["strActive"] = "Yes";
                    //    else
                    //        dt.Rows[a]["strActive"] = "No";
                    //}
                    // dt.AcceptChanges();
                    gv.DataSource = dt;
                    gv.DataBind();
                }
                else
                {
                    lblID.Text = dt.Rows[0]["ID"].ToString();
                    txtIndexTitle.Text = dt.Rows[0]["IndexTitle"].ToString();
                    editorContent.Content= dt.Rows[0]["IndexContent"].ToString();
                    //if (dt.Rows[0]["Active"].ToString() == "1")
                    //    chkActive.Checked = true;
                    //else
                    //    chkActive.Checked = false;
                    if (!string.IsNullOrEmpty(dt.Rows[0]["Section"].ToString()))
                    {
                        divExistingSec.Style["display"] = "none";
                        divNewSec.Style["Display"] = "none";

                        rfvExistingSec.Enabled = false;

                        divNewSec.Style["Display"] = "";
                        rfvType.Enabled = false;
                        //rfvNewNo.Enabled = false;

                        ddlSelectSec.SelectedIndex = 0;
                    }
                    else
                    {
                        divExistingSec.Style["display"] = "";
                        rfvExistingSec.Enabled = true;
                        ddlSelectSec.SelectedValue = "Existing";
                        ddlSection.SelectedValue = dt.Rows[0]["Section"].ToString();
                    }

                    if (!string.IsNullOrEmpty(dt.Rows[0]["Chapter_Schedule"].ToString()))
                    {
                        divPart_Schedule.Style["Display"] = "";
                    }
                    else
                    {
                        divPart_Schedule.Style["Display"] = "none";
                    }
                    txtPart_Schedule.Text = dt.Rows[0]["Part_Schedule"].ToString();
                    txtFootnote.Text = dt.Rows[0]["Footnote"].ToString();
                    GetStatutesIndexSectionGroup(ID);
                    divSuccess.Style["Display"] = "none";
                    divError.Style["Display"] = "none";
                }
            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("ManageStatutesIndex.aspx", "GetStatutesIndex", e.Message);
            }
        }
        void GetStatutesIndexDetails(int ID)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objstat.GetStatutesIndex(ID);
                if(dt.Rows.Count > 0)
                {
                    //lblID.Text = dt.Rows[0]["ID"].ToString();
                    //txtIndexTitle.Text = dt.Rows[0]["IndexTitle"].ToString();
                    //editorContent.Content = dt.Rows[0]["IndexContent"].ToString();
                    ////if (dt.Rows[0]["Active"].ToString() == "1")
                    ////    chkActive.Checked = true;
                    ////else
                    ////    chkActive.Checked = false;
                    //divSuccess.Style["Display"] = "none";
                    //divError.Style["Display"] = "none";

                    lblID.Text = dt.Rows[0]["ID"].ToString();
                    txtIndexTitle.Text = dt.Rows[0]["IndexTitle"].ToString();
                    editorContent.Content = dt.Rows[0]["IndexContent"].ToString();
                    //if (dt.Rows[0]["Active"].ToString() == "1")
                    //    chkActive.Checked = true;
                    //else
                    //    chkActive.Checked = false;
                    if (!string.IsNullOrEmpty(dt.Rows[0]["Section"].ToString()))
                    {
                        divExistingSec.Style["display"] = "none";
                        divNewSec.Style["Display"] = "none";

                        rfvExistingSec.Enabled = false;

                        divNewSec.Style["Display"] = "";
                        rfvType.Enabled = false;
                        //rfvNewNo.Enabled = false;

                        ddlSelectSec.SelectedIndex = 0;
                    }
                    else
                    {
                        divExistingSec.Style["display"] = "";
                        rfvExistingSec.Enabled = true;
                        ddlSelectSec.SelectedValue = "Existing";
                        ddlSection.SelectedValue = dt.Rows[0]["Section"].ToString();
                    }

                    if (!string.IsNullOrEmpty(dt.Rows[0]["Chapter_Schedule"].ToString()))
                    {
                        divPart_Schedule.Style["Display"] = "";
                    }
                    else
                    {
                        divPart_Schedule.Style["Display"] = "none";
                    }
                    txtPart_Schedule.Text = dt.Rows[0]["Part_Schedule"].ToString();
                    txtFootnote.Text = dt.Rows[0]["Footnote"].ToString();
                    GetStatutesIndexSectionGroup(ID);
                    divSuccess.Style["Display"] = "none";
                    divError.Style["Display"] = "none";
                }
            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("ManageStatutesIndex.aspx", "GetStatutesIndex", e.Message);
            }
        }
        void SaveRecord()
        {
            try
            {
                if (ddlSelectSec.SelectedValue == "New")
                {
                    objstat.Section = txtTypeNew.Text;
                }
                else if (ddlSelectSec.SelectedValue == "Existing")
                {
                    objstat.Section = ddlSection.SelectedValue;
                }
                objstat.StatutesID = int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["dis"].ToString()));
                objstat.IndexTitle = txtIndexTitle.Text.Trim();
                objstat.IndexContent = editorContent.Content;
                if (chkActive.Checked == true)
                    objstat.Active = 1;
                else
                    objstat.Active = 0;
                
                objstat.CreatedBy = int.Parse(Session["UserID"].ToString());
                objstat.Chapter_Schedule = ddlChapter_Schdules.SelectedValue;
               
                objstat.Footnote = txtFootnote.Text.Trim();
                if (ddlChapSchd.SelectedValue == "0")
                {
                    objstat.Chapter_Schedule_Title = txtPart_Schedule.Text;
                    objstat.Part_Schedule = "";
                }
                else
                {
                    objstat.Chapter_Schedule_Title = ddlChapSchd.SelectedValue;
                    objstat.Part_Schedule = txtPart_Schedule.Text;
                }

                int chk = objstat.InsertStatutesIndexDetailsUtility();
                if (chk > 0)
                {
                    divSuccess.Style["Display"] = "";
                    divError.Style["Display"] = "none";
                    txtIndexTitle.Text = "";
                    editorContent.Content = "";
                    lblID.Text = "0";
                    chkActive.Checked = false;
                    GetStatutesIndex(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["dis"].ToString())), 0);
                }
                else
                {
                    divSuccess.Style["Display"] = "none";
                    divError.Style["Display"] = "";
                }
            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("ManageStatutesIndex.aspx", "SaveRecord", e.Message);
            }
        }
        void EditRecord(int ID)
        {
            try
            {
                if (ddlSelectSec.SelectedValue == "New")
                {
                    objstat.Section = txtTypeNew.Text;
                }
                else if (ddlSelectSec.SelectedValue == "Existing")
                {
                    objstat.Section = ddlSection.SelectedValue;
                }
                objstat.IndexTitle = txtIndexTitle.Text.Trim();
                objstat.IndexContent = editorContent.Content;
                if (chkActive.Checked == true)
                    objstat.Active = 1;
                else
                    objstat.Active = 0;
                objstat.ModifiedBy = int.Parse(Session["UserID"].ToString());
                objstat.Chapter_Schedule = ddlChapter_Schdules.SelectedValue;
                //objstat.Part_Schedule = txtPart_Schedule.Text.Trim();
                objstat.Footnote = txtFootnote.Text.Trim();
                if (ddlChapSchd.SelectedValue == "0")
                {
                    objstat.Chapter_Schedule_Title = txtPart_Schedule.Text;
                    objstat.Part_Schedule = "";
                }
                else
                {
                    objstat.Chapter_Schedule_Title = ddlChapSchd.SelectedValue;
                    objstat.Part_Schedule = txtPart_Schedule.Text;
                }
                int chk = objstat.EditStatutesIndexDetails(ID);
                if (chk > 0)
                {
                    divSuccess.Style["Display"] = "";
                    divError.Style["Display"] = "none";
                    txtIndexTitle.Text = "";
                    editorContent.Content = "";
                    lblID.Text = "0";
                    chkActive.Checked = false;
                    GetStatutesIndex(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["dis"].ToString())), 0);
                }
                else
                {
                    divSuccess.Style["Display"] = "none";
                    divError.Style["Display"] = "";
                }
            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("ManageJudges.aspx", "EditRecord", e.Message);
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtIndexTitle.Text = "";
            editorContent.Content = "";
            lblID.Text = "0";
            chkActive.Checked = false;
            GetStatutesIndex(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["dis"].ToString())), 0);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblID.Text == "0")
                    SaveRecord();
                else
                    EditRecord(int.Parse(lblID.Text));
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("ManageStatutesIndex.aspx", "btnSave_Click", ex.Message);
            }
        }
        protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gv.PageIndex = e.NewPageIndex;
                GetStatutesIndex(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["dis"].ToString())),0);
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("ManageStatutesIndex.aspx", "gv_PageIndexChanging", ex.Message);
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

                    int chk = objstat.DeleteStatutesIndexDetails(ID, int.Parse(Session["UserID"].ToString()));
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
                GetStatutesIndex(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["dis"].ToString())),0);

            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("ManageStatutesIndex.aspx", "gv_RowDeleting", ex.Message);
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

                    //GetStatutesIndex(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["dis"].ToString())),1);
                   // GetStatutesIndex(ID, 1);
                    GetStatutesIndexDetails(ID);


                }
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("ManageStatutesIndex.aspx", "gv_RowEditing", ex.Message);
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
                ExceptionHandling.SendErrorReport("ManageStatutesIndex.aspx", "gv_RowDataBound", ex.Message);
            }
        }

        protected void ddlSelectSec_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlSelectSec.SelectedValue == "Existing")
                {
                    divExistingSec.Style["Display"] = "";
                    rfvExistingSec.Enabled = true;

                    divNewSec.Style["Display"] = "none";
                    rfvType.Enabled = false;
                   // rfvNewNo.Enabled = false;
                    GetStatutesIndexSectionGroup(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["dis"].ToString())));
                }
                else if (ddlSelectSec.SelectedValue == "New")
                {
                    divExistingSec.Style["Display"] = "none";
                    rfvExistingSec.Enabled = false;

                    divNewSec.Style["Display"] = "";
                    rfvType.Enabled = true;
                   // rfvNewNo.Enabled = true;
                }
                else if (ddlSelectSec.SelectedValue == "None")
                {
                    divExistingSec.Style["Display"] = "none";
                    rfvExistingSec.Enabled = false;

                    divNewSec.Style["Display"] = "none";
                    rfvType.Enabled = false;
                    // rfvNewNo.Enabled = true;
                }
            }
            catch { }
        }

        protected void ddlChapter_Schdules_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlChapter_Schdules.SelectedValue == "N/A")
                {
                    divChapter_Scdhules.Style["Display"] = "none";
                    divPart_Schedule.Style["Display"] = "none";
                }
                else
                {
                    divChapter_Scdhules.Style["Display"] = "";
                    divPart_Schedule.Style["Display"] = "";
                    lblChapSch.Text = ddlChapter_Schdules.SelectedItem.Text;
                    GetStatutesIndexChapter_Schdule(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["dis"].ToString())), ddlChapter_Schdules.SelectedValue);
                }
            }
            catch { }

        }

        protected void btnUpdateSortOrder_Click(object sender, EventArgs e)
        {
            try
            {
                //GridViewRow row = default(GridViewRow);
                HiddenField hdID = default(HiddenField);
                TextBox txtSortOrder = default(TextBox);


                foreach (GridViewRow row in gv.Rows)
                {
                    if ((row != null))
                    {


                        hdID = (HiddenField)row.FindControl("hdID");
                        txtSortOrder = (TextBox)row.FindControl("txtSortOrder");

                        if (!string.IsNullOrEmpty(txtSortOrder.Text))
                        {
                            int chk = objstat.UpdateStatutesIndexSortOrder(int.Parse(hdID.Value.ToString()),int.Parse(txtSortOrder.Text.Trim()),int.Parse(Session["UserID"].ToString()));
                        }
                    }
                }
                divSucuss2.Style["Display"] = "";
                divFailed2.Style["Display"] = "none";
            }
            catch { }
        }

        protected void btnSave0_Click(object sender, EventArgs e)
        {
            //string s = txtEditor.Text;
            //string a = editor1.InnerHtml;
            //string c = editor1.InnerText;
        }
    }
}