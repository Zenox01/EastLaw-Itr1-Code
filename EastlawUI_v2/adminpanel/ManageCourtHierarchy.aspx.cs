using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace EastlawUI_v2.adminpanel
{
    public partial class ManageCourtHierarchy : System.Web.UI.Page
    {
        EastLawBL.Cases objcase = new EastLawBL.Cases();
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
                GetCourtHeirchyMasters(0);
            }
        }
        public void GetCourtHeirchyMasters(int ID)
        {
            try
            {

                DataTable dt = objcase.GetCourtHeirchyMasters(ID);
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
                    gv.DataSource = dt;
                    gv.DataBind();
                }
                else
                {
                    dt.Columns.Add("strActive");
                    dt.Rows.Add(dt.NewRow());
                    gv.DataSource = dt;
                    gv.DataBind();

                    int TotalColumns = gv.Rows[0].Cells.Count;
                    gv.Rows[0].Cells.Clear();
                    gv.Rows[0].Cells.Add(new TableCell());
                    gv.Rows[0].Cells[0].ColumnSpan = TotalColumns;
                    gv.Rows[0].Cells[0].Text = "No Record Found";
                }
            }
            catch { }

        }
        protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
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
        protected void gv_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gv.EditIndex = -1;
            GetCourtHeirchyMasters(0);
        }
        protected void gv_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            // ContactTableAdapter contact = new ContactTableAdapter();
            // bool flag = false;


            TextBox txtEditCourtName = (TextBox)gv.Rows[e.RowIndex].FindControl("txtEditCourtCrtName");
            TextBox txtEditCourtCrtSort = (TextBox)gv.Rows[e.RowIndex].FindControl("txtEditCourtCrtSort");
            CheckBox chkActive = (CheckBox)gv.Rows[e.RowIndex].FindControl("chkActive");
            HiddenField hdID = (HiddenField)gv.Rows[e.RowIndex].FindControl("hdID");

            int chk = objcase.EditCourtHeirchyMaster(int.Parse(hdID.Value.ToString()), txtEditCourtName.Text.Trim(),int.Parse(txtEditCourtCrtSort.Text), chkActive.Checked == true ? 1 : 0);

            gv.EditIndex = -1;
            GetCourtHeirchyMasters(0);

        }
        protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //ContactTableAdapter contact = new ContactTableAdapter();
            //int id = Convert.ToInt32(grdContact.DataKeys[e.RowIndex].Values[0].ToString());
            //int chk = obj
            //contact.Delete(id);
            //FillGrid();


            try
            {
                GridViewRow row = gv.Rows[e.RowIndex];


                if ((row != null))
                {
                    //hd = (HiddenField)row.FindControl("hdID");
                    //int ID = Convert.ToInt32(hd.Value);
                    int ID = Convert.ToInt32(gv.DataKeys[e.RowIndex].Values[0].ToString());
                    int chk = objcase.DeleteCourtHeirchyMaster(ID);
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
                GetCourtHeirchyMasters(0);

            }
            catch (Exception ex)
            {

            }
        }

        protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //ContactTableAdapter contact = new ContactTableAdapter();
            //bool flag = false;
            if (e.CommandName.Equals("Insert"))
            {


                TextBox txtNewCourtCrtName = (TextBox)gv.FooterRow.FindControl("txtNewCourtCrtName");
                TextBox txtNewCourtCrtSort = (TextBox)gv.FooterRow.FindControl("txtNewCourtCrtSort");
                CheckBox chkNewActive = (CheckBox)gv.FooterRow.FindControl("chkNewActive");

                int chk = objcase.AddCourtHeirchyMaster(txtNewCourtCrtName.Text.Trim(),int.Parse(txtNewCourtCrtSort.Text), chkNewActive.Checked == true ? 1 : 0);
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

                GetCourtHeirchyMasters(0);
            }
        }
        protected void gv_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gv.EditIndex = e.NewEditIndex;
            GetCourtHeirchyMasters(0);
        }
    }
}