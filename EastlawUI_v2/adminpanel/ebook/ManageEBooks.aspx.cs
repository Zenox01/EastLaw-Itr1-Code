using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace EastlawUI_v2.adminpanel.ebook
{
    public partial class ManageEBooks : System.Web.UI.Page
    {
        EastLawBL.EBook objeb = new EastLawBL.EBook();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!ValidateUserGroup.ValidateGroup(int.Parse(Session["UserTypeID"].ToString()), ValidateUserGroup.getPageName(Request.Url.AbsolutePath)))
                    Response.Redirect("NotAuthorize.aspx");
                
                    GetEBooks(0);
            }
        }
        void GetEBooks(int ID)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objeb.GetEBook(ID);
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
               
            }
            catch (Exception e)
            {

            }
        }
     
        protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gv.PageIndex = e.NewPageIndex;
                GetEBooks(0);
            }
            catch (Exception ex)
            {

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

                    int chk = objeb.DeleteEBook(ID, int.Parse(Session["UserID"].ToString()));
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
                GetEBooks(0);

            }
            catch (Exception ex)
            {

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
                    GetEBooks(0);
                    Response.Redirect("/adminpanel/ebook/AddBook.aspx?param=" + EncryptDecryptHelper.Encrypt(ID.ToString()));



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
                if (e.CommandName == "ManageIndex")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow row = gv.Rows[index];
                    //HiddenField hdnField = (HiddenField)row.FindControl("hdID");
                    string val = (string)this.gv.DataKeys[index]["Id"].ToString();
                    Response.Redirect("/adminpanel/ebook/ManageEBookIndexes.aspx?param=" + EncryptDecryptHelper.Encrypt(val.ToString()));
                }
               
            }
            catch (Exception ex)
            {

            }
        }
        protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //try
            //{
            //    ImageButton imgBtn = default(ImageButton);
            //    string script = null;
            //    script = "";

            //    if (e.Row.RowType == DataControlRowType.DataRow)
            //    {
            //        imgBtn = (ImageButton)e.Row.Controls[0].FindControl("ibtnDelete");
            //        script = "javascript:return(confirm_delete())";
            //        imgBtn.Attributes.Add("onclick", script);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ExceptionHandling.SendErrorReport("ManagePlans.aspx", "gv_RowDataBound", ex.Message);
            //}
        }

    }
}