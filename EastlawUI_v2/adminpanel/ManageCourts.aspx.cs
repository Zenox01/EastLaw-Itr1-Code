using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


namespace EastlawUI_v2.adminpanel
{
    public partial class ManageCourts : System.Web.UI.Page
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
                GetCourts(0);
            }
        }
        public void GetCourts(int ID)
        {
            try
            {

                DataTable dt = objcase.GetCasesCourtsGroup();
                if (dt.Rows.Count > 0)
                {
                    grdContact.DataSource = dt;
                    grdContact.DataBind();
                }
                else
                {
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
            try
            {
                 DataTable dtCourtMaster = objcase.GetActiveCourtMasters();
                 if (e.Row.RowType == DataControlRowType.DataRow)
                 {
                    
                     //DropDownList ddlEditCourtMaster = (DropDownList)e.Row.FindControl("ddlEditCourtMaster");
                     //if (ddlEditCourtMaster != null)

                     //    ddlEditCourtMaster.DataSource = dtCourtMaster;
                     //    ddlEditCourtMaster.DataTextField = "CourtName";
                     //    ddlEditCourtMaster.DataValueField = "ID";
                     //    ddlEditCourtMaster.DataBind();

                     //    ddlEditCourtMaster.Items.Insert(0, new ListItem("Select", "0"));

                     //DataRowView drCourtMaster = e.Row.DataItem as DataRowView;
                     //ddlEditCourtMaster.SelectedValue = drCourtMaster[3].ToString();

                   
                 }
                
              
        
            }
            catch { }
            
        }
        protected void grdContact_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdContact.EditIndex = -1;
            GetCourts(0);
        }
        protected void grdContact_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            // ContactTableAdapter contact = new ContactTableAdapter();
            // bool flag = false;

            TextBox txtvari = (TextBox)grdContact.Rows[e.RowIndex].FindControl("txtEdit");
            TextBox txtArea = (TextBox)grdContact.Rows[e.RowIndex].FindControl("txtEditArea");
            HiddenField hdID = (HiddenField)grdContact.Rows[e.RowIndex].FindControl("hdID");
            Label txtOldCourtName = (Label)grdContact.Rows[e.RowIndex].FindControl("lblOldName");
            string OldName = hdID.Value.ToString();
           // DropDownList ddlEditCourtMaster = (DropDownList)grdContact.Rows[e.RowIndex].FindControl("ddlEditCourtMaster");

            //if (OldName.Contains("\n"))
            //    OldName = OldName.Replace("\n", "");
            int chk = objcase.UpdateCourtNames_Area(OldName, txtvari.Text.Trim(), txtArea.Text.Trim(), int.Parse(Session["UserID"].ToString()));

            //objcase.CourtName = txtvari.Text.Trim();
            //objcase.CourtMasterID =int.Parse(ddlEditCourtMaster.SelectedValue);
            //int chkNew = objcase.AddCourtMasterMapping();

            grdContact.EditIndex = -1;
            GetCourts(0);

        }
        protected void grdContact_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //ContactTableAdapter contact = new ContactTableAdapter();
            //int id = Convert.ToInt32(grdContact.DataKeys[e.RowIndex].Values[0].ToString());
            //int chk = obj
            //contact.Delete(id);
            //FillGrid();


            //try
            //{
            //    GridViewRow row = grdContact.Rows[e.RowIndex];


            //    if ((row != null))
            //    {
            //        //hd = (HiddenField)row.FindControl("hdID");
            //        //int ID = Convert.ToInt32(hd.Value);
            //        int ID = Convert.ToInt32(grdContact.DataKeys[e.RowIndex].Values[0].ToString());
            //        int chk = objcase.DeleteCitationVariation(ID, int.Parse(Session["UserID"].ToString()));
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
            //    GetCitationVariations(0);

            //}
            //catch (Exception ex)
            //{
            //    ExceptionHandling.SendErrorReport("ManageJudges.aspx", "gv_RowDeleting", ex.Message);
            //}
        }
        protected void grdContact_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //ContactTableAdapter contact = new ContactTableAdapter();
            //bool flag = false;
            //if (e.CommandName.Equals("Insert"))
            //{
            //    TextBox txtNew = (TextBox)grdContact.FooterRow.FindControl("txtNew");

            //    objcase.Vari = txtNew.Text.Trim();
            //    objcase.CreatedBy = int.Parse(Session["UserID"].ToString());
            //    int chk = objcase.InsertCitationVariation();
            //    if (chk > 0)
            //    {
            //        divSuccess.Style["Display"] = "";
            //        divError.Style["Display"] = "none";

            //    }
            //    else
            //    {
            //        divSuccess.Style["Display"] = "none";
            //        divError.Style["Display"] = "";
            //    }

            //    GetCitationVariations(0);
            //}
        }
        protected void grdContact_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdContact.EditIndex = e.NewEditIndex;
            GetCourts(0);
        }
    }
}