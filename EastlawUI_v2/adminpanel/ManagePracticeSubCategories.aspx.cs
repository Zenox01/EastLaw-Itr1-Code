using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace EastlawUI_v2.adminpanel
{
    public partial class ManagePracticeSubCategories : System.Web.UI.Page
    {
        EastLawBL.PracticeAreas objPA = new EastLawBL.PracticeAreas();
        EastLawBL.Statutes objs = new EastLawBL.Statutes();
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
                GetActivePracticeCategories();
                GetPracticeSubCategories(0);
                GetStatutesLessInfo();
            }
        }
        void GetActivePracticeCategories()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objPA.GetActivePracticeAreaCategories();

                ddlPracticeCat.DataValueField = "ID";
                ddlPracticeCat.DataTextField = "PracticeAreaCatName";
                ddlPracticeCat.DataSource = dt;
                ddlPracticeCat.DataBind();
                ddlPracticeCat.Items.Insert(0, new ListItem("Select", "0"));

            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("ManagePracticeSubCategories.aspx", "GetActivePracticeCategories", e.Message);
            }
        }
        void GetPracticeSubCategories(int ID)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objPA.GetPracticeAreaSubCategories(ID);
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
                    gv.DataSource = dt;
                    gv.DataBind();
                }
                else
                {
                    lblID.Text = dt.Rows[0]["ID"].ToString();
                    ddlPracticeCat.SelectedValue = dt.Rows[0]["PracticeAreaCatID"].ToString();
                    txtCatName.Text = dt.Rows[0]["PracticeAreaSubCatName"].ToString();
                    txtDes.Text = dt.Rows[0]["Des"].ToString();
                    if (dt.Rows[0]["Active"].ToString() == "1")
                        chkActive.Checked = true;
                    else
                        chkActive.Checked = false;

                    GetStatuteByPracticeArea(ID);
                    divSuccess.Style["Display"] = "none";
                    divError.Style["Display"] = "none";
                }
            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("ManagePracticeSubCategories.aspx", "GetPracticeSubCategories", e.Message);
            }
        }
        void GetStatuteByPracticeArea(int PracticeAreaID)
        {
            try
            {

                DataTable dt = new DataTable();
                dt = objPA.GetTaggesStatuesWithPracticeArea_List(PracticeAreaID);

                gvStatuesList.DataSource = dt;
                gvStatuesList.DataBind();
            }
            catch { }
        }
        void GetStatutesLessInfo()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objs.GetStatutesLessInfo();
              
                ddlStatutes.DataValueField = "ID";
                ddlStatutes.DataTextField = "Title";
                ddlStatutes.DataSource = dt;
                ddlStatutes.DataBind();
                ddlStatutes.Items.Insert(0, new ListItem("Select", "0"));


                


            }
            catch { }
        }
        void SaveRecord()
        {
            try
            {
                objPA.PracticeAreaCatID = int.Parse(ddlPracticeCat.SelectedValue);
                objPA.PracticeAreaSubCatName = txtCatName.Text.Trim();
                objPA.Des = txtDes.Text.Trim();
                if (chkActive.Checked == true)
                    objPA.Active = 1;
                else
                    objPA.Active = 0;
                objPA.CreatedBy = int.Parse(Session["UserID"].ToString());
                int chk = objPA.InsertPracticeAreaSubCat();
                if (chk > 0)
                {
                    divSuccess.Style["Display"] = "";
                    divError.Style["Display"] = "none";
                    ddlPracticeCat.SelectedIndex = 0;
                    txtCatName.Text = "";
                    txtDes.Text = "";
                    lblID.Text = "0";
                    chkActive.Checked = false;
                    GetPracticeSubCategories(0);
                }
                else
                {
                    divSuccess.Style["Display"] = "none";
                    divError.Style["Display"] = "";
                }
            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("ManagePracticeSubCategories.aspx", "SaveRecord", e.Message);
            }
        }
        void EditRecord(int ID)
        {
            try
            {
                objPA.PracticeAreaCatID = int.Parse(ddlPracticeCat.SelectedValue);
                objPA.PracticeAreaSubCatName = txtCatName.Text.Trim();
                objPA.Des = txtDes.Text.Trim();
                if (chkActive.Checked == true)
                    objPA.Active = 1;
                else
                    objPA.Active = 0;
                objPA.ModifiedBy = int.Parse(Session["UserID"].ToString());
                int chk = objPA.EditPracticeAreaSubCat(ID);
                if (chk > 0)
                {
                    divSuccess.Style["Display"] = "";
                    divError.Style["Display"] = "none";
                    ddlPracticeCat.SelectedIndex = 0;
                    txtCatName.Text = "";
                    txtDes.Text = "";
                    lblID.Text = "0";
                    chkActive.Checked = false;
                    GetPracticeSubCategories(0);
                }
                else
                {
                    divSuccess.Style["Display"] = "none";
                    divError.Style["Display"] = "";
                }
            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("ManagePracticeSubCategories.aspx", "EditRecord", e.Message);
            }
        }
        protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gv.PageIndex = e.NewPageIndex;
                GetPracticeSubCategories(0);
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("ManagePracticeSubCategories.aspx", "gv_PageIndexChanging", ex.Message);
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

                    int chk = objPA.DeletePracticeAreaSubCat(ID, int.Parse(Session["UserID"].ToString()));
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
                GetPracticeSubCategories(0);

            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("ManagePracticeSubCategories.aspx", "gv_RowDeleting", ex.Message);
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

                    GetPracticeSubCategories(ID);


                }
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("ManagePracticeSubCategories.aspx", "gv_RowEditing", ex.Message);
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
                ExceptionHandling.SendErrorReport("ManagePracticeSubCategories.aspx", "gv_RowDataBound", ex.Message);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ddlPracticeCat.SelectedIndex = 0;
            txtCatName.Text = "";
            txtDes.Text = "";
            chkActive.Checked = false;
            lblID.Text = "0";
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
                ExceptionHandling.SendErrorReport("ManagePracticeSubCategories.aspx", "btnSave_Click", ex.Message);
            }
        }
        protected void gvStatuesList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                GridViewRow row = gvStatuesList.Rows[e.RowIndex];
                HiddenField hd = default(HiddenField);
                

                if ((row != null))
                {
                    hd = (HiddenField)row.FindControl("hdID");
                    int ID = Convert.ToInt32(hd.Value);

                    int chk = objPA.DeleteTaggedPracticeAreaWithStatues(ID, int.Parse(Session["UserID"].ToString()));
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
                gvStatuesList.EditIndex = -1;
                

            }
            catch (Exception ex)
            {

            }
        }
        protected void btnAddStatutes_Click(object sender, EventArgs e)
        {
            try
            {
                if(lblID.Text != "0")
                {
                    int chk = objPA.TagPracticeAreaWithStatues(int.Parse(lblID.Text), int.Parse(ddlStatutes.SelectedValue), int.Parse(Session["UserID"].ToString()),0);
                    if (chk > 0)
                    {
                        divSuccess.Style["Display"] = "";
                        divError.Style["Display"] = "none";
                        GetStatuteByPracticeArea(int.Parse(lblID.Text));
                    }
                    else
                    {
                        divSuccess.Style["Display"] = "none";
                        divError.Style["Display"] = "";
                    }

                }
               
            }
            catch { }

        }
    }
}