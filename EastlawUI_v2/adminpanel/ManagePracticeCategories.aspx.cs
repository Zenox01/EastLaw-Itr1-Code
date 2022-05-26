using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;

namespace EastlawUI_v2.adminpanel
{
    public partial class ManagePracticeCategories : System.Web.UI.Page
    {
        EastLawBL.PracticeAreas objPA = new EastLawBL.PracticeAreas();
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
                GetPracticeCategories(0);
            }
        }
        void GetPracticeCategories(int ID)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objPA.GetPracticeAreaCategories(ID);
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
                    txtCatName.Text = dt.Rows[0]["PracticeAreaCatName"].ToString();
                    txtDes.Text = dt.Rows[0]["Des"].ToString();
                    if (dt.Rows[0]["Active"].ToString() == "1")
                        chkActive.Checked = true;
                    else
                        chkActive.Checked = false;
                    divSuccess.Style["Display"] = "none";
                    divError.Style["Display"] = "none";
                }
            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("ManagePracticeCategories.aspx", "GetPracticeCategories", e.Message);
            }
        }
        void SaveRecord()
        {
            try
            {
                objPA.PracticeAreaCatName = txtCatName.Text.Trim();
                objPA.Des = txtDes.Text.Trim();
                if (chkActive.Checked == true)
                    objPA.Active = 1;
                else
                    objPA.Active = 0;
                objPA.CreatedBy = int.Parse(Session["UserID"].ToString());
                int chk = objPA.InsertPracticeAreaCat();
                if (chk > 0)
                {
                    divSuccess.Style["Display"] = "";
                    divError.Style["Display"] = "none";
                    txtCatName.Text = "";
                    txtDes.Text = "";
                    lblID.Text = "0";
                    chkActive.Checked = false;
                    GetPracticeCategories(0);
                }
                else
                {
                    divSuccess.Style["Display"] = "none";
                    divError.Style["Display"] = "";
                }
            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("ManagePracticeCategories.aspx", "SaveRecord", e.Message);
            }
        }
        void EditRecord(int ID)
        {
            try
            {
                objPA.PracticeAreaCatName = txtCatName.Text.Trim();
                objPA.Des = txtDes.Text.Trim();
                if (chkActive.Checked == true)
                    objPA.Active = 1;
                else
                    objPA.Active = 0;
                objPA.ModifiedBy = int.Parse(Session["UserID"].ToString());
                int chk = objPA.EditPracticeAreaCat(ID);
                if (chk > 0)
                {
                    divSuccess.Style["Display"] = "";
                    divError.Style["Display"] = "none";
                    txtCatName.Text = "";
                    txtDes.Text = "";
                    lblID.Text = "0";
                    chkActive.Checked = false;
                    GetPracticeCategories(0);
                }
                else
                {
                    divSuccess.Style["Display"] = "none";
                    divError.Style["Display"] = "";
                }
            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("ManagePracticeCategories.aspx", "EditRecord", e.Message);
            }
        }
        protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gv.PageIndex = e.NewPageIndex;
                GetPracticeCategories(0);
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("ManagePracticeCategories.aspx", "gv_PageIndexChanging", ex.Message);
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

                    int chk = objPA.DeletePracticeAreaCat(ID, int.Parse(Session["UserID"].ToString()));
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
                GetPracticeCategories(0);

            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("ManagePracticeCategories.aspx", "gv_RowDeleting", ex.Message);
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

                    GetPracticeCategories(ID);


                }
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("ManagePracticeCategories.aspx", "gv_RowEditing", ex.Message);
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
                ExceptionHandling.SendErrorReport("ManagePracticeCategories.aspx", "gv_RowDataBound", ex.Message);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
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
                ExceptionHandling.SendErrorReport("ManagePracticeCategories.aspx", "btnSave_Click", ex.Message);
            }
        }

    }
}