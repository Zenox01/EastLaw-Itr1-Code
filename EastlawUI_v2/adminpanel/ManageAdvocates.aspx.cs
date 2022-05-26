using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace EastlawUI_v2.adminpanel
{
    public partial class ManageAdvocates : System.Web.UI.Page
    {
        EastLawBL.Advocates objA = new EastLawBL.Advocates();
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
                GetAdvocates(0,0,20);
            }
        }
        void GetAdvocates(int ID, int StartPage, int EndPage)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objA.GetAdvocates(ID, StartPage, EndPage);
                DataTable dtCount = objA.GetAdvocatesCount();

                if (dtCount != null)
                {
                    lblCount.Text = dtCount.Rows[0]["CountRecords"].ToString();
                    gv.VirtualItemCount = int.Parse(dtCount.Rows[0]["CountRecords"].ToString());
                }
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
                    // dt.AcceptChanges();
                    gv.DataSource = dt;
                    gv.DataBind();
                }
                else
                {
                    lblID.Text = dt.Rows[0]["ID"].ToString();
                    txtName.Text = dt.Rows[0]["AdvocateName"].ToString();
                    Editor1.Content = dt.Rows[0]["Details"].ToString();
                    //if (dt.Rows[0]["Active"].ToString() == "1")
                    //    chkActive.Checked = true;
                    //else
                    //    chkActive.Checked = false;
                    divSuccess.Style["Display"] = "none";
                    divError.Style["Display"] = "none";
                }
            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("ManageAdvocates.aspx", "GetAdvocates", e.Message);
            }
        }
        void SaveRecord()
        {
            try
            {
                objA.AdvocateName = txtName.Text.Trim();
                objA.Details = Editor1.Content;
                //if (chkActive.Checked == true)
                //    objPA.Active = 1;
                //else
                //    objPA.Active = 0;
                objA.CreatedBy = int.Parse(Session["UserID"].ToString());
                int chk = objA.InsertAdvocate();
                if (chk > 0)
                {
                    divSuccess.Style["Display"] = "";
                    divError.Style["Display"] = "none";
                    txtName.Text = "";
                    Editor1.Content = "";
                    lblID.Text = "0";
                    //chkActive.Checked = false;
                    GetAdvocates(0,0,20);
                }
                else
                {
                    divSuccess.Style["Display"] = "none";
                    divError.Style["Display"] = "";
                }
            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("ManageAdvocates.aspx", "SaveRecord", e.Message);
            }
        }
        void EditRecord(int ID)
        {
            try
            {
                objA.AdvocateName = txtName.Text.Trim();
                objA.Details = Editor1.Content;
                //if (chkActive.Checked == true)
                //    objPA.Active = 1;
                //else
                //    objPA.Active = 0;
                objA.ModifiedBy = int.Parse(Session["UserID"].ToString());
                int chk = objA.EditAdvocate(ID);
                if (chk > 0)
                {
                    divSuccess.Style["Display"] = "";
                    divError.Style["Display"] = "none";
                    txtName.Text = "";
                    Editor1.Content = "";
                    lblID.Text = "0";
                    //chkActive.Checked = false;
                    GetAdvocates(0,0,20);
                }
                else
                {
                    divSuccess.Style["Display"] = "none";
                    divError.Style["Display"] = "";
                }
            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("ManageAdvocates.aspx", "EditRecord", e.Message);
            }
        }
        protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gv.PageIndex = e.NewPageIndex;
                GetAdvocates(0,e.NewPageIndex,20);
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("ManageAdvocates.aspx", "gv_PageIndexChanging", ex.Message);
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

                    int chk = objA.DeleteAdvocate(ID, int.Parse(Session["UserID"].ToString()));
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
                GetAdvocates(0,0,20);

            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("ManageAdvocates.aspx", "gv_RowDeleting", ex.Message);
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

                    GetAdvocates(ID,0,20);


                }
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("ManageAdvocates.aspx", "gv_RowEditing", ex.Message);
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
                ExceptionHandling.SendErrorReport("ManageAdvocates.aspx", "gv_RowDataBound", ex.Message);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtName.Text = "";
            Editor1.Content = "";
            //chkActive.Checked = false;
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
                ExceptionHandling.SendErrorReport("ManageAdvocates.aspx", "btnSave_Click", ex.Message);
            }
        }
    }
}