using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace EastlawUI_v2.adminpanel
{
    public partial class StatutesCategories : System.Web.UI.Page
    {
        EastLawBL.Statutes objStat = new EastLawBL.Statutes();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserID"] == null)
                {
                    Response.Redirect("default.aspx");
                }
                if (!ValidateUserGroup.ValidateGroup(int.Parse(Session["UserTypeID"].ToString()), ValidateUserGroup.getPageName(Request.Url.AbsolutePath)))
                    Response.Redirect("NotAuthorize.aspx");
                GetStatuesCat(0);
            }
        }
        public void GetStatuesCat(int ID)
        {
            try
            {

                DataTable dt = objStat.GetStatutesCategories(ID);
                if (dt.Rows.Count > 0)
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
                    grdContact.DataSource = dt;
                    grdContact.DataBind();
                }
                else
                {
                    dt.Columns.Add("strActive");
                    for (int a = 0; a < dt.Rows.Count; a++)
                    {
                        if (dt.Rows[a]["Active"].ToString() == "1")
                            dt.Rows[a]["strActive"] = "Yes";
                        else
                            dt.Rows[a]["strActive"] = "No";
                    }

                    dt.Rows.Add(dt.NewRow());
                    grdContact.DataSource = dt;
                    grdContact.DataBind();

                    int TotalColumns = grdContact.Rows[0].Cells.Count;
                    grdContact.Rows[0].Cells.Clear();
                    grdContact.Rows[0].Cells.Add(new TableCell());
                    grdContact.Rows[0].Cells[0].ColumnSpan = TotalColumns;
                    grdContact.Rows[0].Cells[0].Text = "No Record Found";
                }
            }
            catch { }

        }
        protected void grdContact_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //ContactTypeTableAdapter contactType = new ContactTypeTableAdapter();
            //DataTable contactTypes = contactType.GetData();
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    Label lblType = (Label)e.Row.FindControl("lblType");
            //    if (lblType != null)
            //    {
            //        int typeId = Convert.ToInt32(lblType.Text);
            //        lblType.Text = (string)contactType.GetTypeById(typeId);
            //    }
            //    DropDownList cmbType = (DropDownList)e.Row.FindControl("cmbType");
            //    if (cmbType != null)
            //    {
            //        cmbType.DataSource = contactTypes;
            //        cmbType.DataTextField = "TypeName";
            //        cmbType.DataValueField = "Id";
            //        cmbType.DataBind();
            //        cmbType.SelectedValue = grdContact.DataKeys[e.Row.RowIndex].Values[1].ToString();
            //    }
            //}
            //if (e.Row.RowType == DataControlRowType.Footer)
            //{
            //    DropDownList cmbNewType = (DropDownList)e.Row.FindControl("cmbNewType");
            //    cmbNewType.DataSource = contactTypes;
            //    cmbNewType.DataBind();
            //}
        }
        protected void grdContact_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdContact.EditIndex = -1;
            GetStatuesCat(0);
        }
        protected void grdContact_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            // ContactTableAdapter contact = new ContactTableAdapter();
            // bool flag = false;

            TextBox txtEdit = (TextBox)grdContact.Rows[e.RowIndex].FindControl("txtEditCat");
            CheckBox chkActive = (CheckBox)grdContact.Rows[e.RowIndex].FindControl("chkActive");
            HiddenField hdID = (HiddenField)grdContact.Rows[e.RowIndex].FindControl("hdID");

            //objkey.ID = int.Parse(hdID.Value.ToString());
            objStat.CatName = txtEdit.Text.Trim();
            if (chkActive.Checked == true)
                objStat.Active = 1;
            else
                objStat.Active = 0;
            objStat.ModifiedBy = int.Parse(Session["UserID"].ToString());
            int chk = objStat.EditStatutesCat(int.Parse(hdID.Value.ToString()));

            grdContact.EditIndex = -1;
            GetStatuesCat(0);

        }
        protected void grdContact_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //ContactTableAdapter contact = new ContactTableAdapter();
            //int id = Convert.ToInt32(grdContact.DataKeys[e.RowIndex].Values[0].ToString());
            //int chk = obj
            //contact.Delete(id);
            //FillGrid();


            try
            {
                GridViewRow row = grdContact.Rows[e.RowIndex];


                if ((row != null))
                {
                    //hd = (HiddenField)row.FindControl("hdID");
                    //int ID = Convert.ToInt32(hd.Value);
                    int ID = Convert.ToInt32(grdContact.DataKeys[e.RowIndex].Values[0].ToString());
                    int chk = objStat.DeleteStatutesCat(ID, int.Parse(Session["UserID"].ToString()));
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
                GetStatuesCat(0);

            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("StatutesCategories.aspx", "gv_RowDeleting", ex.Message);
            }
        }
        protected void grdContact_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //ContactTableAdapter contact = new ContactTableAdapter();
            //bool flag = false;
            if (e.CommandName.Equals("Insert"))
            {
                TextBox txtNew = (TextBox)grdContact.FooterRow.FindControl("txtNewCat");
                CheckBox chkNewActive = (CheckBox)grdContact.FooterRow.FindControl("chkNewActive");

                objStat.CatName = txtNew.Text.Trim();
                if (chkNewActive.Checked == true)
                    objStat.Active = 1;
                else
                    objStat.Active = 0;
                objStat.CreatedBy = int.Parse(Session["UserID"].ToString());
                int chk = objStat.InsertStatutesCat();
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

                GetStatuesCat(0);
            }
        }
        protected void grdContact_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdContact.EditIndex = e.NewEditIndex;
            GetStatuesCat(0);
        }

    }
}