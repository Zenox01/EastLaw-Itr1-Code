using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace EastlawUI_v2.adminpanel
{
    public partial class ManageGeneralAreas : System.Web.UI.Page
    {
        EastLawBL.GeneralArea objGA = new EastLawBL.GeneralArea();
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
                GetGeneralAreas(0);
            }
        }
        void GetGeneralAreas(int ID)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objGA.GetGeneralAreas(ID);
                if (ID == 0)
                {
                    dt.Columns.Add("strActive");
                    dt.Columns.Add("DocLinks");
                    for (int a = 0; a < dt.Rows.Count; a++)
                    {
                        if (dt.Rows[a]["Active"].ToString() == "1")
                            dt.Rows[a]["strActive"] = "Yes";
                        else
                            dt.Rows[a]["strActive"] = "No";

                        string[] Documents = dt.Rows[a]["Documents"].ToString().Split(',');
                        string DocWithLinks="";
                        foreach (string docName in Documents)
                        {
                            DocWithLinks = DocWithLinks + "<br> <a href='../store/generalareadocs/" + docName + "'>" + docName + "</a>"; 
                            //if (dsCheckUser.Tables["Login"].Rows[0]["DistributorID"].ToString() == mUser_val)
                            //{
                            //    user_Distri = true;
                            //}
                            //else
                            //{
                            //    user_Distri = false;
                            //}
                        }
                        dt.Rows[a]["DocLinks"] = DocWithLinks;
                    }
                   
                    dt.AcceptChanges();
                    gv.DataSource = dt;
                    gv.DataBind();
                }

            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("ManageCompanies.aspx", "GetCompanies", e.Message);
            }
        }

        protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gv.PageIndex = e.NewPageIndex;
                GetGeneralAreas(0);
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("ManageGeneralAreas.aspx", "gv_PageIndexChanging", ex.Message);
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

                    int chk = objGA.DeleteGeneralArea(ID, int.Parse(Session["UserID"].ToString()));
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
                GetGeneralAreas(0);

            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("ManageGeneralAreas.aspx", "gv_RowDeleting", ex.Message);
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

                    Response.Redirect("AddGeneralAreas.aspx?param=" + EncryptDecryptHelper.Encrypt(ID.ToString()));



                }
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("ManageGeneralAreas.aspx", "gv_RowEditing", ex.Message);
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
                ExceptionHandling.SendErrorReport("ManageGeneralAreas.aspx", "gv_RowDataBound", ex.Message);
            }
        }
    }
}